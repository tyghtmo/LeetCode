namespace Problems;

public enum ProblemType
{
    Easy,
    Medium,
    Hard
}

public interface IProblemDefinition
{
    string Name { get; }
    ProblemType ProblemType { get;}
    ProblemResult TestProblem();
}
