namespace M.TimeoutLsp;

public sealed class TimeOutLsp
{
    public static void Execute(string[] args)
    {
        int timeout = int.Parse(args[1]);
        Thread.Sleep(timeout * 1000);
    }
}
