using System.Diagnostics;
namespace Problems.Hard;

//Given n non-negative integers representing an elevation map where the width of each bar is 1, 
//compute how much water it can trap after raining.
//
// Example 1:
//
// Input: height = [0,1,0,2,1,0,1,3,2,1,2,1]
// Output: 6
// Explanation: The above elevation map (black section) is represented by array [0,1,0,2,1,0,1,3,2,1,2,1]. In this case, 6 units of rain water (blue section) are being trapped.
//
// Example 2:
//
// Input: height = [4,2,0,3,2,5]
// Output: 9


internal record TrappingRainWaterParams
{
    public required int[] Heights;
    public required int ExpectedOutput;
}

public class TrappingRainWater : IProblemDefinition
{
    private readonly List<TrappingRainWaterParams> _params =
    [
        new()
        {
            Heights = [0,1,0,2,1,0,1,3,2,1,2,1],
            ExpectedOutput = 6
        },
        new()
        {
            Heights = [4,2,0,3,2,5],
            ExpectedOutput = 9
        },
        new()
        {
            Heights = [2,0,2],
            ExpectedOutput = 2
        },
        new()
        {
            Heights = [0,1,2,0,3,0,1,2,0,0,4,2,1,2,5,0,1,2,0,2],
            ExpectedOutput = 26
        }
    ];

    public string Name => "Trapping Rain Water";

    public ProblemType ProblemType => ProblemType.Hard;

    public ProblemResult TestProblem()
    {
        List<SolutionResult> solutionResults = [];

        solutionResults.Add(RunSolution("RowByRow", RowByRow));
        solutionResults.Add(RunSolution("2 Pass", PassTwice));

        return new ProblemResult(this, solutionResults);
    }

    private SolutionResult RunSolution(string solutionName, Func<int[], int> func)
    {
        List<RunResult> runResults = [];
        var stopwatch = new Stopwatch();

        // Call before measuring to ensure func is compiled
        func.Invoke(_params[0].Heights);

        for (int i = 0; i < _params.Count; i++)
        {
            var error = "";

            try
            {
                stopwatch.Restart();
                var res = func.Invoke(_params[i].Heights);
                stopwatch.Stop();

                if (res != _params[i].ExpectedOutput)
                {
                    error = $"Incorrect output. Got {res}, expected {_params[i].ExpectedOutput}";
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

    private static int RowByRow(int[] height)
    {
        int top = height.Max();
        int result = 0;

        // Go row by row and find enclosed count
        for (int row = 0; row <= top; row++)
        {
            int left = -1;

            for (int i = left + 1; i < height.Length; i++)
            {
                if (height[i] > row)
                {
                    int right = i;

                    if (left != right && left > -1 && right > -1)
                    {
                        result += right - left - 1;
                    }

                    left = right;
                }
            }
        }

        return result;
    }

    private static int PassTwice(int[] height)
    {
        int maxHeight = 0;
        int result = 0;

        for (int i = 0; i < height.Length; i++)
        {
            maxHeight = Math.Max(maxHeight, height[i]);
            result += maxHeight - height[i];
        }

        int rightMax = 0;
        for (int i = height.Length - 1; i >= 0; i--)
        {
            if(height[i] == maxHeight)
            {
                break;
            }

            rightMax = Math.Max(rightMax, height[i]);
            result -= maxHeight - rightMax;
        }

        return result;
    }
}