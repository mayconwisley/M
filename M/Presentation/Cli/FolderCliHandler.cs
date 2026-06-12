using M.Application.Folder;

namespace M.Presentation.Cli;

public sealed class FolderCliHandler(
    CreateFolderUseCase      create,
    MoveFolderUseCase        move,
    DeleteFolderUseCase      delete,
    DeleteSubFoldersUseCase  deleteSubFolders,
    DeleteFolderFilesUseCase deleteFolderFiles,
    CopyFolderFilesUseCase   copyFiles)
{
    public void Handle(string[] args)
    {
        if (args.Length < 2)
            throw new ArgumentException("Subcomando obrigatório para -folder.");

        var cmd = args[1].Trim().ToLowerInvariant();

        switch (cmd)
        {
            case "c":
                RequireArgs(args, 4, "M -folder c <path> <name>");
                create.Execute(args[2], args[3]);
                break;

            case "m":
                RequireArgs(args, 4, "M -folder m <src> <dst>");
                move.Execute(args[2], args[3]);
                break;

            case "d":
                RequireArgs(args, 3, "M -folder d <path>");
                delete.Execute(args[2]);
                break;

            case "ds":
                RequireArgs(args, 3, "M -folder ds <path>");
                deleteSubFolders.Execute(args[2]);
                break;

            case "da":
                RequireArgs(args, 3, "M -folder da <path>");
                deleteFolderFiles.Execute(args[2]);
                break;

            case "k":
                RequireArgs(args, 4, "M -folder k [x] <src> <dst>");
                if (args[2] == "x")
                {
                    RequireArgs(args, 5, "M -folder k x <src> <dst>");
                    copyFiles.Execute(args[3], args[4], cut: true);
                }
                else
                {
                    copyFiles.Execute(args[2], args[3]);
                }
                break;

            default:
                throw new ArgumentException($"Subcomando desconhecido para -folder: '{cmd}'");
        }
    }

    private static void RequireArgs(string[] args, int count, string usage)
    {
        if (args.Length < count)
            throw new ArgumentException($"Argumentos insuficientes. Uso: {usage}");
    }
}
