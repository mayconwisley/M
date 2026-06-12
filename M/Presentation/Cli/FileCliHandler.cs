using M.Application.File;

namespace M.Presentation.Cli;

public sealed class FileCliHandler(
    CreateFileUseCase create,
    MoveFileUseCase   move,
    DeleteFileUseCase delete,
    CopyFileUseCase   copy)
{
    public void Handle(string[] args)
    {
        if (args.Length < 2)
            throw new ArgumentException("Subcomando obrigatório para -file.");

        var cmd = args[1].Trim().ToLowerInvariant();

        switch (cmd)
        {
            case "c":
                RequireArgs(args, 4, "M -file c <dir> <name>");
                create.Execute(args[2], args[3]);
                break;

            case "m":
                RequireArgs(args, 4, "M -file m <file> <dst>");
                move.Execute(args[2], args[3]);
                break;

            case "d":
                RequireArgs(args, 3, "M -file d <file>");
                delete.Execute(args[2]);
                break;

            case "k":
                RequireArgs(args, 4, "M -file k [x] <file> <dst>");
                if (args[2] == "x")
                {
                    RequireArgs(args, 5, "M -file k x <file> <dst>");
                    copy.Execute(args[3], args[4], cut: true);
                }
                else
                {
                    copy.Execute(args[2], args[3]);
                }
                break;

            default:
                throw new ArgumentException($"Subcomando desconhecido para -file: '{cmd}'");
        }
    }

    private static void RequireArgs(string[] args, int count, string usage)
    {
        if (args.Length < count)
            throw new ArgumentException($"Argumentos insuficientes. Uso: {usage}");
    }
}
