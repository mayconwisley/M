namespace M.Pastaruga;

public sealed class ArquivoNinja
{
    public static void Execute(string[] args)
    {
        try
        {
            var cmd = args[1].Trim().ToLowerInvariant();

            if (cmd == "help" || cmd == "-h" || cmd == "--help")
            {
                return;
            }

            switch (cmd)
            {
                case "c":
                    if (args.Length < 4)
                        Environment.Exit(2);

                    Arquivo.Criar(args[2], args[3]);
                    break;

                case "m":
                    if (args.Length < 4)
                        Environment.Exit(2);

                    Arquivo.Mover(args[2], args[3]);
                    break;

                case "d":
                    if (args.Length < 3)
                        Environment.Exit(2);

                    Arquivo.Deletar(args[2]);
                    break;

                case "k":
                    if (args.Length < 4)
                        Environment.Exit(2);

                    if (args[2] == "x")
                    {
                        if (args.Length < 5)
                            Environment.Exit(2);

                        Arquivo.CopiarArquivo(args[3], args[4], true);
                        break;
                    }

                    Arquivo.CopiarArquivo(args[2], args[3]);
                    break;

                default:
                    Environment.Exit(2);
                    break;
            }
        }
        catch (Exception ex)
        {
            Environment.Exit(1);
        }
    }
}
