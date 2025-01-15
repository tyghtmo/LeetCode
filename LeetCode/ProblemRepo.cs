using System.Collections;
using System.Reflection;
using Problems;

namespace LeetCode;

internal class ProblemRepo : IEnumerable<IProblemDefinition>
{
    private readonly List<IProblemDefinition> _problems;

    public ProblemRepo()
    {
        _problems = GetProblemsFromAssembly([ProblemType.Easy, ProblemType.Medium, ProblemType.Hard]);
    }

    public ProblemRepo(List<ProblemType> TypeFilter)
    {

        _problems = GetProblemsFromAssembly(TypeFilter);
    }

    public IEnumerator<IProblemDefinition> GetEnumerator()
    {
        return _problems.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _problems.GetEnumerator();
    }

    private static List<IProblemDefinition> GetProblemsFromAssembly(List<ProblemType> ProblemTypes)
    {
        var commonInterface = typeof(IProblemDefinition);

        var problems = Assembly.GetAssembly(commonInterface)?
            .GetTypes()
            .Where(p => commonInterface.IsAssignableFrom(p) && p.IsClass);

        List<IProblemDefinition> result = [];

        if (problems is null || !problems.Any())
        {
            return result;
        }

        foreach (var problemClass in problems)
        {
            IProblemDefinition instance = (IProblemDefinition?)Activator.CreateInstance(problemClass) ?? throw new Exception($"Error creating instance of {problemClass.Name}");
            
            if (ProblemTypes.Contains(instance.ProblemType))
            {
                result.Add(instance);
            }
        }

        return result;
    }
}