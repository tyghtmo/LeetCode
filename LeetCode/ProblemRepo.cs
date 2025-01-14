using System.Collections;
using Problems;
using Problems.Easy;
using Problems.Hard;
using Problems.Medium;

namespace LeetCode;

internal class ProblemRepo : IEnumerable<IProblemDefinition>
{
    private readonly List<IProblemDefinition> _problems;

    public ProblemRepo()
    {
        // TODO use reflection to grab all IProblemDefinitions from Problems assembly.
        // TODO allow filtering by ProblemType

        _problems = [
            // new TwoSum(),
            // new PascalsTriangle(),
            // new TrappingRainWater(),
            new LongestConsecutiveSequence()
        ];
    }

    public IEnumerator<IProblemDefinition> GetEnumerator()
    {
        return _problems.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _problems.GetEnumerator();
    }
}