using ParallelProgramming;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Program.WriteInColor("---------------------------", ConsoleColor.Green);
            Console.WriteLine("Matière: Programmation parallèle");
            Console.WriteLine("Créé par: Benoit PEGAZ");
            Program.WriteInColor("---------------------------\n", ConsoleColor.Green);


            Console.WriteLine("Veuillez sélectionner un programme:");
            Console.WriteLine("\t- 1: Séquentiel vs Parallèle");
            Console.WriteLine("\t- 2: Sémaphore");

            if (!int.TryParse(Console.ReadLine(), out int value))
                Environment.Exit(0);

            Console.Clear();
            Console.WriteLine("<Appuyez sur une ENTRER pour revenir au menu>\n");

            switch (value)
            {
                case 0:
                default:
                    Environment.Exit(0);
                    break;
                case 1:
                    SequentialVersusMultiThread.Run();
                    break;
                case 2:
                    SemaphoreExample.Run();
                    break;
            }

            Console.ReadLine();
            Console.Clear();
        }
    }

    public static void WriteInColor(string content, ConsoleColor consoleColor)
    {
        Console.ForegroundColor = consoleColor;
        Console.WriteLine(content);
        Console.ForegroundColor = ConsoleColor.White;
    }
}
