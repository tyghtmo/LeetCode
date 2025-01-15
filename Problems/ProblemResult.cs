using System.Text;

namespace Problems;

public class ProblemResult(IProblemDefinition definition, IEnumerable<SolutionResult> solutionResults)
{
    public string Name { get; init; } = definition.Name;
    public IEnumerable<SolutionResult> Results { get; init; } = solutionResults;    

    public int SuccessCount => Results.Sum(r => r.SuccessCount);
    public int TotalCount => Results.Sum(r => r.TotalCount);

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendLine("-------------------------------------");
        sb.AppendLine($"{Name} results.");
        sb.AppendLine("");

        foreach (var result in Results)
        {
            sb.AppendLine(result.ToString());
        }

        sb.AppendLine("-------------------------------------");
        return sb.ToString();
    }
}