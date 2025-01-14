using System.Diagnostics;

namespace Problems.Easy;

/// <summary>
/// Given an array of integers nums and an integer target, return indices of the two numbers such that they add up to target.
/// You may assume that each input would have exactly one solution, and you may not use the same element twice.
/// You can return the answer in any order.
///
/// Example 1:
///
/// Input: nums = [2,7,11,15], target = 9
/// Output: [0,1]
/// Explanation: Because nums[0] + nums[1] == 9, we return [0, 1].
///
/// Example 2:
///
/// Input: nums = [3,2,4], target = 6
/// Output: [1,2]
///
/// Example 3:
///
/// Input: nums = [3,3], target = 6
/// Output: [0,1]
///
/// </summary>

internal record TwoSumParameters
{
    public required int[] Nums;
    public required int Target;
    public required int[] ExpectedOutput;
}

public class TwoSum : IProblemDefinition
{
    private readonly List<TwoSumParameters> _params = [
        new()
        {
            Nums = [2,7,11,15],
            Target = 9,
            ExpectedOutput = [0,1]
        },
        new()
        {
            Nums = [3,2,4],
            Target = 6,
            ExpectedOutput = [1,2]
        },
        new()
        {
            Nums = [3,3],
            Target = 6,
            ExpectedOutput = [0,1]
        },
        new()
        {
            Nums = [1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,0,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,0,1,1, 
                    1,1,1,1,1,1,1,-1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,-1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,-1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,-1,1,1,1,1, 
                    1,1,1,1,1,-1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,-1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,1,1, 
                    1,1,1,1,1,1,1,1,3,2],
            Target = 5,
            ExpectedOutput = [498,499]
        }
    ];

    public ProblemType ProblemType => ProblemType.Easy;

    public string Name => "Two Sum";

    public ProblemResult TestProblem()
    {
        List<SolutionResult> solutionResults = [];

        solutionResults.Add(RunSolution("Brute Force", BruteForce));
        solutionResults.Add(RunSolution("Better Brute Force", SlightlyBetterBruteForce));
        solutionResults.Add(RunSolution("Dictionary Lookup", DictionaryLookup));

        return new ProblemResult(this, solutionResults);
    }

    private SolutionResult RunSolution(string solutionName, Func<int[], int, int[]> func)
    {
        List<RunResult> runResults = [];
        var stopwatch = new Stopwatch();    

        // Call before measuring to ensure func is compiled
        func.Invoke(_params[0].Nums, _params[0].Target);    

        for(int i = 0; i < _params.Count; i++)
        {
            var error = "";
            
            try
            {
                stopwatch.Restart();
                var res = func.Invoke(_params[i].Nums, _params[i].Target);
                stopwatch.Stop();

                Array.Sort(res);
                if (!res.SequenceEqual(_params[i].ExpectedOutput))
                {
                    error = $"Incorrect output. Got {string.Join(", ", res)}, expected {string.Join(", ", _params[i].ExpectedOutput)}";
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

    private static int[] DictionaryLookup(int[] nums, int target)
    {
        Dictionary<int, int> map = [];

        // Iterate nums and use map to find if a match exists
        for (int i = 0; i < nums.Length; i++)
        {
            int toFind = target - nums[i];

            if (map.TryGetValue(toFind, out int index))
            {
                return [i, index];
            }

            map[nums[i]] = i;
        }

        // default case
        return [-1, -1];
    }

    private static int[] SlightlyBetterBruteForce(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] + nums[j] == target)
                {
                    return [i, j];
                }
            }
        }

        // default case
        return [-1, -1];
    }

    private static int[] BruteForce(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = 0; j < nums.Length; j++)
            {
                if (nums[i] + nums[j] == target)
                {
                    if (i != j)
                    {
                        return [i, j];
                    }
                }
            }
        }

        // default case
        return [-1, -1];
    }
}