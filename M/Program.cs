using M.Frank;
using M.Pastaruga;
using M.TimeoutLsp;

namespace M;

internal class Program
{
    private static void PrintUsage()
    {
        Console.WriteLine("Uso:");
        Console.WriteLine("M -merge <input_directory> <output_directory> [output_filename]");
        Console.WriteLine("M -timeout <time>");
        Console.WriteLine("M -folder c|m|d|k [x] <path> [pathDestination]");
        Console.WriteLine("M -file c|m|d|k [x] <path> [pathDestination]");
    }

    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            PrintUsage();
            return;
        }

        string command = args[0];

        switch (command)
        {
            case "-timeout":
                if (args.Length != 2 || !int.TryParse(args[1], out _))
                {
                    PrintUsage();
                    Console.WriteLine("M -timeout <time>\n");
                    throw new ArgumentException("Para o timeout precisa existir todos parametros e time precisa ser um numero");
                }

                TimeOutLsp.Execute(args);
                break;
            case "-merge":
                if (args.Length < 3 || args.Length > 4)
                {
                    PrintUsage();
                    Console.WriteLine("M -merge <input_directory> <output_directory> [output_filename]\n");
                    throw new ArgumentException("Para o merge é necessário informar os diretórios de entrada e saída");
                }

                Pdf.Execute(args);
                break;
            case "-folder":
                Ninja.Execute(args);
                break;
            case "-file":
                ArquivoNinja.Execute(args);
                break;
            default:
                PrintUsage();
                break;
        }

        Console.WriteLine("Processo concluído com sucesso.");
    }
}
