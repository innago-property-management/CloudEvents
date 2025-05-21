# syntax=docker/dockerfile:1
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG TARGETARCH
WORKDIR /work

ARG NUGET_SOURCE_NAME
ARG NUGET_SOURCE_URL
ARG NUGET_USERNAME

RUN --mount=type=secret,id=nuget_password,target=/run/secrets/nuget_password \
    if [ -n "$NUGET_SOURCE_URL" ] && [ -n "$NUGET_USERNAME" ]; then \
        NUGET_PASSWORD_FROM_SECRET=$(cat /run/secrets/nuget_password); \
        dotnet nuget add source \
            --name "$NUGET_SOURCE_NAME" \
            --username "$NUGET_USERNAME" \
            --password "$NUGET_PASSWORD_FROM_SECRET" \
            --store-password-in-clear-text \
            "$NUGET_SOURCE_URL"; \
    fi
    
#RUN apt-get update && apt install gcc --yes

COPY . .

ARG TARGETPLATFORM
ARG BUILDPLATFORM
#RUN echo "I am running on $BUILDPLATFORM, building for $TARGETPLATFORM and $TARGETARCH" > /log

RUN dotnet restore examples/RabbitPoc/RabbitPoc.csproj  --arch $TARGETARCH

RUN dotnet publish examples/RabbitPoc/RabbitPoc.csproj  \
    --no-restore \
    --configuration Release \
    --output /app \
    --self-contained false \
    /p:NoWarn=RS0041 \
    --arch $TARGETARCH \
    -p:SKIP_OPENAPI_GENERATION=true
    

FROM --platform=$TARGETPLATFORM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS app

LABEL vendor="Innago"
LABEL com.innago.image="Innago.CloudEventsExample"

RUN apk update --no-cache && apk upgrade --no-cache && rm -rf /var/cache/apk/*

RUN addgroup --gid 10001 notroot \
    && adduser --uid 10001 --ingroup notroot notroot --disabled-password --no-create-home

WORKDIR /app
COPY --from=build /app .

USER notroot:notroot
ENV DOTNET_EnableDiagnostics=1
ENV DOTNET_EnableDiagnostics_IPC=0
ENV DOTNET_EnableDiagnostics_Debugger=0
ENV DOTNET_EnableDiagnostics_Profiler=1
ENV ASPNETCORE_HOSTBUILDER_RELOADCONFIGONCHANGE="false"

ENTRYPOINT ["dotnet","Innago.Platform.Messaging.CloudEvents.ConsoleExample.dll"]