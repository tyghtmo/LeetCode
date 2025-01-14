using System.Diagnostics;
using System.Text;

namespace Problems.Easy;

//Given an integer numRows, return the first numRows of Pascal's triangle.
//
// In Pascal's triangle, each number is the sum of the two numbers directly above it as shown:
//
// Example 1:
//
// Input: numRows = 5
// Output: [[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1]]
//
// Example 2:
//
// Input: numRows = 1
// Output: [[1]]
//
// Constraints:
//
// 1 <= numRows <= 30

internal record PascalsTriangleParams
{
    public required int NumRows { get; set; }
    public required IList<IList<int>> ExpectedOutput { get; set; }
}

public class PascalsTriangle : IProblemDefinition
{
    private readonly List<PascalsTriangleParams> _params = [
        new()
        {
            NumRows = 5,
            ExpectedOutput = [[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1]]
        },
        new()
        {
            NumRows = 1,
            ExpectedOutput = [[1]]
        },
        new()
        {
            NumRows = 3,
            ExpectedOutput = [[1],[1,1],[1,2,1]]
        },
        new()
        {
            NumRows = 10,
            ExpectedOutput = [[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1],[1,5,10,10,5,1],[1,6,15,20,15,6,1],[1,7,21,35,35,21,7,1],[1,8,28,56,70,56,28,8,1],[1,9,36,84,126,126,84,36,9,1]]
        },
        new()
        {
            NumRows = 22,
            ExpectedOutput = [[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1],[1,5,10,10,5,1],[1,6,15,20,15,6,1],[1,7,21,35,35,21,7,1],[1,8,28,56,70,56,28,8,1],[1,9,36,84,126,126,84,36,9,1],[1,10,45,120,210,252,210,120,45,10,1],[1,11,55,165,330,462,462,330,165,55,11,1],[1,12,66,220,495,792,924,792,495,220,66,12,1],[1,13,78,286,715,1287,1716,1716,1287,715,286,78,13,1],[1,14,91,364,1001,2002,3003,3432,3003,2002,1001,364,91,14,1],[1,15,105,455,1365,3003,5005,6435,6435,5005,3003,1365,455,105,15,1],[1,16,120,560,1820,4368,8008,11440,12870,11440,8008,4368,1820,560,120,16,1],[1,17,136,680,2380,6188,12376,19448,24310,24310,19448,12376,6188,2380,680,136,17,1],[1,18,153,816,3060,8568,18564,31824,43758,48620,43758,31824,18564,8568,3060,816,153,18,1],[1,19,171,969,3876,11628,27132,50388,75582,92378,92378,75582,50388,27132,11628,3876,969,171,19,1],[1,20,190,1140,4845,15504,38760,77520,125970,167960,184756,167960,125970,77520,38760,15504,4845,1140,190,20,1],[1,21,210,1330,5985,20349,54264,116280,203490,293930,352716,352716,293930,203490,116280,54264,20349,5985,1330,210,21,1]]
        },
        new()
        {
            NumRows = 27,
            ExpectedOutput = [[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1],[1,5,10,10,5,1],[1,6,15,20,15,6,1],[1,7,21,35,35,21,7,1],[1,8,28,56,70,56,28,8,1],[1,9,36,84,126,126,84,36,9,1],[1,10,45,120,210,252,210,120,45,10,1],[1,11,55,165,330,462,462,330,165,55,11,1],[1,12,66,220,495,792,924,792,495,220,66,12,1],[1,13,78,286,715,1287,1716,1716,1287,715,286,78,13,1],[1,14,91,364,1001,2002,3003,3432,3003,2002,1001,364,91,14,1],[1,15,105,455,1365,3003,5005,6435,6435,5005,3003,1365,455,105,15,1],[1,16,120,560,1820,4368,8008,11440,12870,11440,8008,4368,1820,560,120,16,1],[1,17,136,680,2380,6188,12376,19448,24310,24310,19448,12376,6188,2380,680,136,17,1],[1,18,153,816,3060,8568,18564,31824,43758,48620,43758,31824,18564,8568,3060,816,153,18,1],[1,19,171,969,3876,11628,27132,50388,75582,92378,92378,75582,50388,27132,11628,3876,969,171,19,1],[1,20,190,1140,4845,15504,38760,77520,125970,167960,184756,167960,125970,77520,38760,15504,4845,1140,190,20,1],[1,21,210,1330,5985,20349,54264,116280,203490,293930,352716,352716,293930,203490,116280,54264,20349,5985,1330,210,21,1],[1,22,231,1540,7315,26334,74613,170544,319770,497420,646646,705432,646646,497420,319770,170544,74613,26334,7315,1540,231,22,1],[1,23,253,1771,8855,33649,100947,245157,490314,817190,1144066,1352078,1352078,1144066,817190,490314,245157,100947,33649,8855,1771,253,23,1],[1,24,276,2024,10626,42504,134596,346104,735471,1307504,1961256,2496144,2704156,2496144,1961256,1307504,735471,346104,134596,42504,10626,2024,276,24,1],[1,25,300,2300,12650,53130,177100,480700,1081575,2042975,3268760,4457400,5200300,5200300,4457400,3268760,2042975,1081575,480700,177100,53130,12650,2300,300,25,1],[1,26,325,2600,14950,65780,230230,657800,1562275,3124550,5311735,7726160,9657700,10400600,9657700,7726160,5311735,3124550,1562275,657800,230230,65780,14950,2600,325,26,1]]
        }
    ];

    public string Name => "Pascal's Traingle";

    public ProblemType ProblemType => ProblemType.Easy;

    public ProblemResult TestProblem()
    {
        List<SolutionResult> solutionResults = [];

        solutionResults.Add(RunSolution("Solution 1", Generate));
        solutionResults.Add(RunSolution("Recursive Combinatorics", RecursiveCombinatorics));

        return new ProblemResult(this, solutionResults);
    }

    private SolutionResult RunSolution(string solutionName, Func<int, IList<IList<int>>> func)
    {
        List<RunResult> runResults = [];
        var stopwatch = new Stopwatch();

        // Call before measuring to ensure func is compiled
        func.Invoke(_params[0].NumRows);

        for (int i = 0; i < _params.Count; i++)
        {
            var error = "";

            try
            {
                stopwatch.Restart();
                var res = func.Invoke(_params[i].NumRows);
                stopwatch.Stop();

                for (int j = 0; j < res.Count; j++)
                {
                    for (int k = 0; k < res[j].Count; k++)
                    {
                        if (res[j][k] != _params[i].ExpectedOutput[j][k])
                        {
                            error = $"Incorrect output. Got {ListOfListToString(res)}, expected {ListOfListToString(_params[i].ExpectedOutput)}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error = $"Exception: {ex.Message}";
            }

            runResults.Add(new RunResult(i, stopwatch.Elapsed, error));
        }

        return new SolutionResult(solutionName, runResults);
    }

    private static string ListOfListToString(IList<IList<int>> list)
    {
        StringBuilder sb = new();
        sb.AppendLine("");

        foreach (IList<int> listItem in list)
        {
            sb.Append('[');
            sb.Append(string.Join(", ", listItem));
            sb.AppendLine("]");
        }

        return sb.ToString();
    }

    private static IList<IList<int>> Generate(int numRows)
    {
        IList<IList<int>> triangle = [];

        for (int i = 0; i < numRows; i++)
        {
            List<int> row = [];

            for (int j = 0; j <= i; j++)
            {
                if (j == 0 || j == i)
                {
                    row.Add(1);
                }
                else
                {
                    row.Add(triangle[i - 1][j - 1] + triangle[i - 1][j]);
                }
            }

            triangle.Add(row);
        }

        return triangle;
    }

    // https://stackoverflow.com/questions/15580291/how-to-efficiently-calculate-a-row-in-pascals-triangle 
    private static IList<IList<int>> RecursiveCombinatorics(int numRows)
    {
        IList<IList<int>> triangle = [];

        for (int i = 0; i < numRows; i++)
        {
            List<int> row = [1];

            for (int j = 0; j < i; j++)
            {
                row.Add(row[j] * (i - j) / (j + 1));
            }

            triangle.Add(row);
        }

        return triangle;
    }
}