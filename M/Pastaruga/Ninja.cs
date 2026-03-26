namespace M.Pastaruga;

public sealed class Ninja
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
                    {
                        Environment.Exit(2);
                    }
                    Pasta.Criar(args[2], args[3]);
                    break;

                case "m":
                    if (args.Length < 4)
                    {
                        Environment.Exit(2);
                    }
                    Pasta.Mover(args[2], args[3]);
                    break;

                case "d":
                    if (args.Length < 3)
                    {
                        Environment.Exit(2);
                    }
                    Pasta.Deletar(args[2]);
                    break;
                case "ds":
                    if (args.Length < 3)
                    {
                        Environment.Exit(2);
                    }
                    Pasta.DeletarSubPasta(args[2]);
                    break;

                case "k":
                    if (args.Length < 4)
                    {
                        Environment.Exit(2);
                    }

                    if (args[2] == "x")
                    {
                        if (args.Length < 5)
                        {
                            Environment.Exit(2);
                        }

                        Arquivo.Copiar(args[3], args[4], true);
                        break;
                    }

                    Arquivo.Copiar(args[2], args[3]);
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
