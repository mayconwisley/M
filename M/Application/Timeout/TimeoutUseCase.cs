namespace M.Application.Timeout;

public sealed class TimeoutUseCase
{
    public void Execute(int seconds) => Thread.Sleep(seconds * 1000);
}
