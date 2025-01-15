using Problems;

namespace LeetCode;

class Program
{
    static void Main(string[] args)
    {
        ProblemRepo problems = new();
        List<ProblemResult> problemResults = [];

        // Test all problems and display results as they execute.
        foreach (var problem in problems)
        {
            var res = problem.TestProblem();
            problemResults.Add(res);

            Console.WriteLine(res.ToString());
            Console.WriteLine("");
        }

        // Calculate and display overall result
        var successCount = problemResults.Sum(p => p.SuccessCount);
        var totalCount = problemResults.Sum(p => p.TotalCount);

        Console.WriteLine("-------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Overview");
        Console.WriteLine($"{successCount} / {totalCount} runs succeeded");
        Console.WriteLine("");
        Console.WriteLine("-------------------------------------");
    }
}
