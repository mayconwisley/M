using M.Application.Timeout;

namespace M.Presentation.Cli;

public sealed class TimeoutCliHandler(TimeoutUseCase timeout)
{
    public void Handle(string[] args)
    {
        if (args.Length != 2 || !int.TryParse(args[1], out var seconds))
            throw new ArgumentException("Uso: M -timeout <seconds>");

        timeout.Execute(seconds);
    }
}
