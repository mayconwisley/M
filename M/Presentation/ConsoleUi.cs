namespace M.Presentation;

internal static class ConsoleUi
{
    private const string Version = "1.0.0";

    internal static void PrintHeader()
    {
        Colored(ConsoleColor.Cyan, () =>
        {
            Console.WriteLine();
            Console.WriteLine("  ╔══════════════════════════════╗");
            Console.WriteLine($"  ║  M — File Toolkit  v{Version}  ║");
            Console.WriteLine("  ╚══════════════════════════════╝");
        });
        Console.WriteLine();
    }

    internal static void PrintHelp()
    {
        Section("USO");
        Console.WriteLine("  M <comando> [opções]");
        Console.WriteLine();

        Section("COMANDOS");
        Row("-merge",   "<input_dir> <output_dir> [filename]", "Mescla PDFs e imagens em um único PDF");
        Row("-timeout", "<seconds>",                           "Aguarda N segundos (integração LSP)");
        Row("-folder",  "<cmd> <args...>",                     "Operações em pastas");
        Row("-file",    "<cmd> <args...>",                     "Operações em arquivos");
        Console.WriteLine();

        Section("-folder  SUBCOMANDOS");
        SubRow("c  <path> <name>",  "Criar subpasta");
        SubRow("m  <src>  <dst>",   "Mover pasta");
        SubRow("d  <path>",         "Deletar pasta (recursivo)");
        SubRow("ds <path>",         "Deletar subpastas");
        SubRow("da <path>",         "Deletar arquivos da pasta");
        SubRow("k  <src>  <dst>",   "Copiar arquivos da pasta");
        SubRow("k x <src> <dst>",   "Recortar arquivos da pasta");
        Console.WriteLine();

        Section("-file  SUBCOMANDOS");
        SubRow("c  <dir>  <name>",  "Criar arquivo");
        SubRow("m  <file> <dst>",   "Mover arquivo");
        SubRow("d  <file>",         "Deletar arquivo");
        SubRow("k  <file> <dst>",   "Copiar arquivo");
        SubRow("k x <file> <dst>",  "Recortar arquivo");
        Console.WriteLine();

        Colored(ConsoleColor.DarkGray, () => Console.WriteLine("  M --help  para exibir esta ajuda."));
        Console.WriteLine();
    }

    internal static void Success(string message)
    {
        Colored(ConsoleColor.Green, () => Console.WriteLine($"  ✓  {message}"));
        Console.WriteLine();
    }

    internal static void Error(string message)
    {
        Colored(ConsoleColor.Red, () => Console.Error.WriteLine($"  ✗  {message}"));
        Console.WriteLine();
    }

    // ── private ────────────────────────────────────────────────────────────

    private static void Section(string title) =>
        Colored(ConsoleColor.Yellow, () => Console.WriteLine($"  {title}"));

    private static void Row(string cmd, string args, string description)
    {
        Colored(ConsoleColor.White,    () => Console.Write($"    {cmd,-10}"));
        Colored(ConsoleColor.DarkCyan, () => Console.Write($"  {args,-38}"));
        Colored(ConsoleColor.Gray,     () => Console.WriteLine($"  {description}"));
    }

    private static void SubRow(string usage, string description)
    {
        Colored(ConsoleColor.DarkCyan, () => Console.Write($"      {usage,-26}"));
        Colored(ConsoleColor.Gray,     () => Console.WriteLine($"  {description}"));
    }

    private static void Colored(ConsoleColor color, Action write)
    {
        var previous = Console.ForegroundColor;
        Console.ForegroundColor = color;
        write();
        Console.ForegroundColor = previous;
    }
}
