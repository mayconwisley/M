using M.Application.File;
using M.Application.Folder;
using M.Application.Pdf;
using M.Application.Timeout;
using M.Infrastructure.File;
using M.Infrastructure.Folder;
using M.Infrastructure.Pdf;
using M.Presentation;
using M.Presentation.Cli;

namespace M;

internal static class Program
{
    static int Main(string[] args)
    {
        ConsoleUi.PrintHeader();

        if (args.Length < 1 || args[0] is "-h" or "--help" or "help")
        {
            ConsoleUi.PrintHelp();
            return 0;
        }

        var handlers = BuildHandlers();

        try
        {
            if (!handlers.TryGetValue(args[0], out var handler))
            {
                ConsoleUi.Error($"Comando desconhecido: '{args[0]}'");
                ConsoleUi.PrintHelp();
                return 2;
            }

            handler(args);
            ConsoleUi.Success("Processo concluído com sucesso.");
            return 0;
        }
        catch (ArgumentException ex)
        {
            ConsoleUi.Error(ex.Message);
            return 2;
        }
        catch (Exception ex)
        {
            ConsoleUi.Error(ex.Message);
            return 1;
        }
    }

    private static Dictionary<string, Action<string[]>> BuildHandlers()
    {
        var folderRepo = new FolderRepository();
        var fileRepo   = new FileRepository();
        var pdfMerger  = new PdfMerger();

        var folderHandler = new FolderCliHandler(
            new CreateFolderUseCase(folderRepo),
            new MoveFolderUseCase(folderRepo),
            new DeleteFolderUseCase(folderRepo),
            new DeleteSubFoldersUseCase(folderRepo),
            new DeleteFolderFilesUseCase(folderRepo),
            new CopyFolderFilesUseCase(folderRepo));

        var fileHandler   = new FileCliHandler(
            new CreateFileUseCase(fileRepo),
            new MoveFileUseCase(fileRepo),
            new DeleteFileUseCase(fileRepo),
            new CopyFileUseCase(fileRepo));

        var pdfHandler     = new PdfCliHandler(new MergePdfUseCase(pdfMerger));
        var timeoutHandler = new TimeoutCliHandler(new TimeoutUseCase());

        return new Dictionary<string, Action<string[]>>
        {
            ["-folder"]  = folderHandler.Handle,
            ["-file"]    = fileHandler.Handle,
            ["-merge"]   = pdfHandler.Handle,
            ["-timeout"] = timeoutHandler.Handle,
        };
    }
}
