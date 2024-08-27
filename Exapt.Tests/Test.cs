using System.Text.Json;
using System.Text.Json.Nodes;

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

        JsonObject expectedResults = JsonSerializer.Deserialize<JsonObject>(
            File.ReadAllText("resources/expected_results.json")
        )!;
        string[] solutionFiles = Directory.GetFiles("resources/solutions", "*", SearchOption.AllDirectories);
        foreach (string solutionFile in solutionFiles)
        {
            string expectedResultKey = Path.GetRelativePath("resources/solutions", solutionFile)
                .Replace(".solution", "", StringComparison.Ordinal)
                .Replace("\\", "/", StringComparison.Ordinal);
            Assert.IsTrue(expectedResults.ContainsKey(expectedResultKey));
            SolutionData expectedResult = expectedResults[expectedResultKey].Deserialize<SolutionData>()!;

            SolutionData result = Program.Simulate(solutionFile);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
