namespace Problems;

public class RunResult(int index, TimeSpan elapsedTime, string error)
{
    private int _index = index;
    public TimeSpan ElapsedTime { get; init; } = elapsedTime;
    public bool IsSuccess { get; init; } = string.IsNullOrEmpty(error);
    public string Error { get; init; } = error;

    public override string ToString()
    {
        if (IsSuccess)
        {
            return $"Run {_index + 1} Elapsed Time: {ElapsedTime.TotalMilliseconds} ms.";
        }

        return $"Run {_index + 1} Error: {Error}";
    }
}