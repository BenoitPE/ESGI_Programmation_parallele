using ParallelProgramming;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        ImageResizer imageResizer = new ImageResizer();

        Console.WriteLine("\nDébut de la compression des images en séquentiel (single thread)");
        Stopwatch swSingleThread = new Stopwatch();
        swSingleThread.Start();
        imageResizer.SingleThreadImageResizer();
        swSingleThread.Stop();
        Console.WriteLine($"Compression séquentielle terminée en {swSingleThread.ElapsedMilliseconds} ms");

        Console.WriteLine("\nDébut de la compression des images en parallèle (multi thread)");
        Stopwatch swMultiThread = new Stopwatch();
        swMultiThread.Start();
        imageResizer.MultiThreadImageResizer();
        swMultiThread.Stop();
        Console.WriteLine($"Compression parallèle terminée en {swMultiThread.ElapsedMilliseconds} ms");

        float percentage = swSingleThread.ElapsedMilliseconds - swMultiThread.ElapsedMilliseconds;
        percentage = percentage / swSingleThread.ElapsedMilliseconds;
        percentage = percentage * 100;
        Console.WriteLine($"\n--> La compression parallèle est plus rapide que la compression séquentielle de {percentage}%");

        Console.ReadLine();
    }

}
