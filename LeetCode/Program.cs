namespace LeetCode;

class Program
{
    static void Main(string[] args)
    {
        ProblemRepo problems = new();

        foreach (var problem in problems)
        {
            var res = problem.TestProblem();

            Console.WriteLine(res.ToString());
            Console.WriteLine("");
        }
    }
}
