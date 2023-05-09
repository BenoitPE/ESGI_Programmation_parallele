using ParallelProgramming;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        ImageResizer imageResizer = new ImageResizer();

        WriteInColor("---------------------------");
        Console.WriteLine("Matière: Programmation parallèle");
        Console.WriteLine("Créé par: Benoit PEGAZ");
        WriteInColor("---------------------------");

        int nbFiles = imageResizer.CountFilesInInputFolder();
        Console.WriteLine($"Nombres de fichiers présents dans le dossier 'Inputs': {nbFiles}");
        if (nbFiles <= 1)
        {
            WriteInColor("Quantité trop faible. Peuplement du dossier en cours...");
            imageResizer.InitFilesInInputFolder(50);
            WriteInColor($"Remplissage terminé");
        } else
        {
            WriteInColor("Nombre de fichiers suffisants. Poursuite du programme.");
        }

        Console.WriteLine("\nDébut de la compression des images en séquentiel (single thread)");
        Stopwatch swSingleThread = new Stopwatch();
        swSingleThread.Start();
        imageResizer.SingleThreadImageResizer();
        swSingleThread.Stop();
        WriteInColor($"--> Compression séquentielle terminée en {swSingleThread.ElapsedMilliseconds} ms");

        Console.WriteLine("\nDébut de la compression des images en parallèle (multi threads)");
        Stopwatch swMultiThread = new Stopwatch();
        swMultiThread.Start();
        imageResizer.MultiThreadImageResizer();
        swMultiThread.Stop();
        WriteInColor($"--> Compression parallèle terminée en {swMultiThread.ElapsedMilliseconds} ms");

        float percentage = swSingleThread.ElapsedMilliseconds - swMultiThread.ElapsedMilliseconds;

        Console.WriteLine("\nComparaison entre les deux implémentations:");
        if (percentage > 0)
        {
            percentage = percentage / swSingleThread.ElapsedMilliseconds;
            percentage = percentage * 100;
            WriteInColor($"--> La compression parallèle est plus rapide que la compression séquentielle de {percentage}%");
        }
        else
        {
            WriteInColor("--> La compression séquentielle a été plus rapide. Dans cet exemple, ce cas peut se produire si la quantité d'images à redimensionner est trop faible/ si le nombre de thread allouable est trop petit");
        }

        Console.WriteLine(@"
En fonction du cas d'utilisation, la programmation parallèle est plus ou moins rapide qu'une programmation séquentielle.
Si la charge de travail est trop légère, la programmation séquentielle sera plus rapide car la séparation en plusieurs threads nécessitera un temps d'initialisation qui ne pourra être amorti.
En revanche, si la charge de travail est volumineuse et décomposable en multi threads, alors la programmation parallèle sera plus efficiente.
            ");

        Console.ReadLine();
    }

    public static void WriteInColor(string content)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(content);
        Console.ForegroundColor = ConsoleColor.White;
    }
}
