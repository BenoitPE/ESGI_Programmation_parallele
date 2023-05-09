using System.Diagnostics;

namespace ParallelProgramming
{
    public class SequentialVersusMultiThread
    {
        public static void Run()
        {
            ImageResizer imageResizer = new ImageResizer();

            int nbFiles = imageResizer.CountFilesInInputFolder();
            Console.WriteLine($"Nombres de fichiers présents dans le dossier 'Inputs': {nbFiles}");
            if (nbFiles <= 1)
            {
                Program.WriteInColor("Quantité trop faible. Peuplement du dossier en cours...", ConsoleColor.Green);
                imageResizer.InitFilesInInputFolder(100);
                Program.WriteInColor($"Remplissage terminé", ConsoleColor.Green);
            }
            else
            {
                Program.WriteInColor("Nombre de fichiers suffisants. Poursuite du programme.", ConsoleColor.Green);
            }

            Console.WriteLine("\nDébut de la compression des images en séquentiel (single thread)");
            Stopwatch swSingleThread = new Stopwatch();
            swSingleThread.Start();
            imageResizer.SingleThreadImageResizer();
            swSingleThread.Stop();
            Program.WriteInColor($"--> Compression séquentielle terminée en {swSingleThread.ElapsedMilliseconds} ms", ConsoleColor.Green);

            Console.WriteLine("\nDébut de la compression des images en parallèle (multi threads)");
            Stopwatch swMultiThread = new Stopwatch();
            swMultiThread.Start();
            imageResizer.MultiThreadImageResizer();
            swMultiThread.Stop();
            Program.WriteInColor($"--> Compression parallèle terminée en {swMultiThread.ElapsedMilliseconds} ms", ConsoleColor.Green);

            float percentage = swSingleThread.ElapsedMilliseconds - swMultiThread.ElapsedMilliseconds;

            Console.WriteLine("\nComparaison entre les deux implémentations:");
            if (percentage > 0)
            {
                percentage /= swSingleThread.ElapsedMilliseconds;
                percentage *= 100;
                Program.WriteInColor($"--> La compression parallèle est plus rapide que la compression séquentielle de {percentage}%", ConsoleColor.Green);
            }
            else
            {
                Program.WriteInColor("--> La compression séquentielle a été plus rapide. Dans cet exemple, ce cas peut se produire si la quantité d'images à redimensionner est trop faible/ si le nombre de thread allouable est trop petit", ConsoleColor.Green);
            }

            Console.WriteLine(@"
En fonction du cas d'utilisation, la programmation parallèle est plus ou moins rapide qu'une programmation séquentielle.
Si la charge de travail est trop légère, la programmation séquentielle sera plus rapide car la séparation en plusieurs threads nécessitera un temps d'initialisation qui ne pourra être amorti.
En revanche, si la charge de travail est volumineuse et décomposable en multi threads, alors la programmation parallèle sera plus efficiente.
            ");
        }
    }
}
