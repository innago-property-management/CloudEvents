namespace ApprovalTests.Publisher.Amqp;

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.XPath;

using PublicApiGenerator;

using VerifyTests.DiffPlex;

using Xunit.OpenCategories;

[Category($"{nameof(Publisher)}.{nameof(Amqp)}")]
public class PublisherAmqpApproval
{
    private const string ProjectFolder = "Publisher.Amqp";
    private const string ProjectName = $"{PublisherAmqpApproval.ProjectFolder}.csproj";
    private const string SourceFolder = "../src/libraries";

    static PublisherAmqpApproval()
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
        var dllName = $"{GetProjectElement("/Project/PropertyGroup/AssemblyName")?.Value ?? PublisherAmqpApproval.ProjectFolder}.dll";

        string configuration = typeof(PublisherAmqpApproval).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>()!.Configuration;

        string assemblyFile = CombinedPaths(PublisherAmqpApproval.SourceFolder,
            PublisherAmqpApproval.ProjectFolder,
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
        string csproj = CombinedPaths(PublisherAmqpApproval.SourceFolder, PublisherAmqpApproval.ProjectFolder, PublisherAmqpApproval.ProjectName);
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