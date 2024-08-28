using Newtonsoft.Json.Linq;

namespace Exapt.Tests;

[TestClass]
public class Test
{
    [TestMethod]
    public void RunSolutions()
    {
        Program.Initialize(
            Environment.GetEnvironmentVariable("EXAPUNKS_DIR")
                ?? throw new MissingEnvironmentVariableException("""Environment variable "EXAPUNKS_DIR" not set""")
        );

        JObject expectedResults = JObject.Parse(File.ReadAllText("resources/expected_results.json"))!;
        string[] solutionFiles = Directory.GetFiles("resources/solutions", "*", SearchOption.AllDirectories);
        foreach (string solutionFile in solutionFiles)
        {
            string expectedResultKey = Path.GetRelativePath("resources/solutions", solutionFile)
                .Replace(".solution", "", StringComparison.Ordinal)
                .Replace("\\", "/", StringComparison.Ordinal);
            SolutionData? expectedResult = expectedResults[expectedResultKey]?.ToObject<SolutionData>();
            Assert.IsNotNull(expectedResult);

            SolutionData result = Program.Simulate(solutionFile);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
