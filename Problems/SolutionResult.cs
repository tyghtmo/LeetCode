using System.Text;

namespace Problems;

public class SolutionResult(string Name, List<RunResult> runResults)
{
    public string Name { get; init; } = Name;
    public List<RunResult> RunResults { get; init; } = runResults;

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendLine($"{Name}");

        foreach (var runResult in RunResults)
        {
            sb.AppendLine(runResult.ToString());
        }

        return sb.ToString();
    }
}