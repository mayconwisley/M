using M.Frank;
using M.Pastaruga;
using M.TimeoutLsp;

namespace M;

internal class Program
{
    static void Main(string[] args)
    {
        //if (args.Length < 1 || args.Length > 2)
        //{
        //    Console.WriteLine("Uso:");
        //    Console.WriteLine("M -merge <input_directory> [output_directory]");
        //    Console.WriteLine("M -timeout <time>");
        //    Console.WriteLine("M -folder c|m|k [x] <path> [pathDestination]");
        //    return;
        //}

        string command = args[0];

        switch (command)
        {
            case "-timeout":
                var isTimeout = int.TryParse(args[1], out _);

                if (args.Length != 2 || !isTimeout)
                {
                    Console.WriteLine("Uso:");
                    Console.WriteLine("M -timeout <time>\n");
                    throw new ArgumentException("Para o timeout precisa existir todos parametros e time precisa ser um numero");
                }

                TimeOutLsp.Execute(args);
                break;
            case "-merge":
                if (args.Length != 3)
                {
                    Console.WriteLine("Uso:");
                    Console.WriteLine("M -merge <input_directory> [output_directory]\n");
                    throw new ArgumentException("Para o merge precisa existir todos parametros");
                }
                Pdf.Execute(args);
                break;
            case "-folder":
                Ninja.Execute(args);
                break;
            default:
                break;
        }
        Console.WriteLine("Processo concluído com sucesso.");
    }
}
