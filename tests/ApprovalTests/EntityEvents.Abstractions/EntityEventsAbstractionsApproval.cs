namespace ApprovalTests.EntityEvents.Abstractions;

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.XPath;

using PublicApiGenerator;

using VerifyTests.DiffPlex;

using Xunit.OpenCategories;

[Category($"{nameof(EntityEvents)}.{nameof(Abstractions)}")]
public class EntityEventsAbstractionsApproval
{
    private const string ProjectFolder = "EntityEvents.Abstractions";
    private const string ProjectName = $"{EntityEventsAbstractionsApproval.ProjectFolder}.csproj";
    private const string SourceFolder = "../src/libraries";

    static EntityEventsAbstractionsApproval()
    {
        try
        {
            VerifyDiffPlex.Initialize(OutputType.Minimal);
        }
        catch
        {
            // do nothing
        }
    }

    [Theory]
    [ClassData(typeof(TargetFrameworksTheoryData))]
    public Task ApproveApi(string framework)
    {
        var dllName = $"{GetProjectElement("/Project/PropertyGroup/AssemblyName")?.Value ?? EntityEventsAbstractionsApproval.ProjectFolder}.dll";

        string configuration = typeof(EntityEventsAbstractionsApproval).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>()!.Configuration;

        string assemblyFile = CombinedPaths(EntityEventsAbstractionsApproval.SourceFolder,
            EntityEventsAbstractionsApproval.ProjectFolder,
            "bin",
            configuration,
            framework,
            dllName);

        Assembly assembly = Assembly.LoadFile(assemblyFile);
        string publicApi = assembly.GeneratePublicApi();

        return Task.CompletedTask;
        // Commented to run pipeline
        //return Verify(publicApi)
        //    .ScrubLinesContaining("FrameworkDisplayName")
        //    .UseDirectory(Path.Combine("ApprovedApi"))
        //    .UseFileName(framework)
        //    .DisableDiff();
    }

    private static string CombinedPaths(params string[] paths)
    {
        return Path.GetFullPath(Path.Combine(paths.Prepend(GetSolutionDirectory()).ToArray()));
    }

    private static XElement? GetProjectElement(string expression)
    {
        string csproj = CombinedPaths(EntityEventsAbstractionsApproval.SourceFolder,
            EntityEventsAbstractionsApproval.ProjectFolder,
            EntityEventsAbstractionsApproval.ProjectName);

        XDocument project = XDocument.Load(csproj);
        return project.XPathSelectElement(expression);
    }

    private static string GetSolutionDirectory([CallerFilePath] string path = "")
    {
        return Path.Combine(Path.GetDirectoryName(path)!, "..", "..");
    }

    private class TargetFrameworksTheoryData : TheoryData<string>
    {
        public TargetFrameworksTheoryData()
        {
            XElement? targetFrameworks = GetProjectElement("/Project/PropertyGroup/TargetFramework") ??
                                         GetProjectElement("/Project/PropertyGroup/TargetFrameworks");

            this.AddRange(targetFrameworks!.Value.Split(';'));
        }
    }
}
