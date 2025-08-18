namespace ApprovalTests.Publisher.Abstractions;

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.XPath;

using PublicApiGenerator;

using VerifyTests.DiffPlex;

using Xunit.OpenCategories;

[Category($"{nameof(Publisher)}.{nameof(Abstractions)}")]
public class PublisherAbstractionsApproval
{
    private const string ProjectFolder = "Publisher.Abstractions";
    private const string ProjectName = $"{PublisherAbstractionsApproval.ProjectFolder}.csproj";
    private const string SourceFolder = "../src/libraries";

    static PublisherAbstractionsApproval()
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
        var dllName = $"{GetProjectElement("/Project/PropertyGroup/AssemblyName")?.Value ?? PublisherAbstractionsApproval.ProjectFolder}.dll";

        string configuration = typeof(PublisherAbstractionsApproval).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>()!.Configuration;

        string assemblyFile = CombinedPaths(PublisherAbstractionsApproval.SourceFolder,
            PublisherAbstractionsApproval.ProjectFolder,
            "bin",
            configuration,
            framework,
            dllName);

        Assembly assembly = Assembly.LoadFile(assemblyFile);
        string publicApi = assembly.GeneratePublicApi();

        string outputDirectory = Path.Combine("ApprovedApi");

        if (!Path.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        return Verify(publicApi)
            .ScrubLinesContaining("FrameworkDisplayName")
            .UseDirectory(outputDirectory)
            .UseFileName(framework)
            .DisableDiff();
    }

    private static string CombinedPaths(params string[] paths)
    {
        return Path.GetFullPath(Path.Combine(paths.Prepend(GetSolutionDirectory()).ToArray()));
    }

    private static XElement? GetProjectElement(string expression)
    {
        string csproj = CombinedPaths(PublisherAbstractionsApproval.SourceFolder,
            PublisherAbstractionsApproval.ProjectFolder,
            PublisherAbstractionsApproval.ProjectName);

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