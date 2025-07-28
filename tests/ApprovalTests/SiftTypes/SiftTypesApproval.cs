namespace ApprovalTests.SiftTypes;

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Xml.XPath;

using EntityEvents.Abstractions;

using PublicApiGenerator;

using VerifyTests.DiffPlex;

using Xunit.OpenCategories;

[Category(nameof(SiftTypes))]
public class SiftTypesApproval
{
    private const string SourceFolder = "../src/libraries";
    private const string ProjectFolder = "SiftTypes";
    private const string ProjectName = $"{ProjectFolder}.csproj";

    static SiftTypesApproval()
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
        var dllName = $"{GetProjectElement("/Project/PropertyGroup/AssemblyName")?.Value ?? ProjectFolder}.dll";
        
        string configuration = typeof(EntityEventsAbstractionsApproval).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>()!.Configuration;

        string assemblyFile = CombinedPaths(SourceFolder,
            ProjectFolder,
            "bin",
            configuration,
            framework,
            dllName);

        Assembly assembly = Assembly.LoadFile(assemblyFile);
        string publicApi = assembly.GeneratePublicApi(options: null);

        return Verifier
            .Verify(publicApi)
            .ScrubLinesContaining("FrameworkDisplayName")
            .UseDirectory(Path.Combine("ApprovedApi"))
            .UseFileName(framework)
            .DisableDiff();
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

    private static XElement? GetProjectElement(string expression)
    {
        string csproj = CombinedPaths(SourceFolder, ProjectFolder, ProjectName);
        XDocument project = XDocument.Load(csproj);
        return project.XPathSelectElement(expression);
    }

    private static string GetSolutionDirectory([CallerFilePath] string path = "") =>
        Path.Combine(Path.GetDirectoryName(path)!, "..", "..");

    private static string CombinedPaths(params string[] paths) =>
        Path.GetFullPath(Path.Combine(paths.Prepend(GetSolutionDirectory()).ToArray()));
}
