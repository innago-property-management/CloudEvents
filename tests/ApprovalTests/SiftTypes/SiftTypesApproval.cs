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
    private const string ProjectFolder = "SiftTypes";
    private const string ProjectName = $"{SiftTypesApproval.ProjectFolder}.csproj";
    private const string SourceFolder = "../src/libraries";

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
        var dllName = $"{GetProjectElement("/Project/PropertyGroup/AssemblyName")?.Value ?? SiftTypesApproval.ProjectFolder}.dll";

        string configuration = typeof(EntityEventsAbstractionsApproval).Assembly.GetCustomAttribute<AssemblyConfigurationAttribute>()!.Configuration;

        string assemblyFile = CombinedPaths(SiftTypesApproval.SourceFolder,
            SiftTypesApproval.ProjectFolder,
            "bin",
            configuration,
            framework,
            dllName);

        Assembly assembly = Assembly.LoadFile(assemblyFile);
        string publicApi = assembly.GeneratePublicApi();

        return Verify(publicApi)
            .ScrubLinesContaining("FrameworkDisplayName")
            .UseDirectory(Path.Combine("ApprovedApi"))
            .UseFileName(framework)
            .DisableDiff();
    }

    private static string CombinedPaths(params string[] paths)
    {
        return Path.GetFullPath(Path.Combine(paths.Prepend(GetSolutionDirectory()).ToArray()));
    }

    private static XElement? GetProjectElement(string expression)
    {
        string csproj = CombinedPaths(SiftTypesApproval.SourceFolder, SiftTypesApproval.ProjectFolder, SiftTypesApproval.ProjectName);
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