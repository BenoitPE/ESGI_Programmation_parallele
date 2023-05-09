namespace ParallelProgramming
{
    public class SemaphoreExample
    {
        // 1 seul thread peut écrire dans un fichier
        private static SemaphoreSlim fileSemaphore = new SemaphoreSlim(1);

        public static void Run()
        {
            // On crée plusieurs threads. Ceux-ci ont pour travail d'écrire dans un fichier
            for (int i = 1; i <= 5; i++)
            {
                Thread t = new Thread(WriteToFile);
                t.Start(i);
            }
        }

        private static void WriteToFile(object id)
        {
            Program.WriteInColor($"Thread {id}: Démarre.", ConsoleColor.Blue);

            try
            {
                // Attend que le sémaphore soit disponible
                fileSemaphore.Wait();
                Console.WriteLine($"Sémaphore disponible:\t Le thread {id} commence son processus");

                DoWork(id);
            }
            finally
            {
                fileSemaphore.Release();
                Console.WriteLine($"Sémaphore libéré:\t Le thread {id} a terminé son processus");
            }
        }

        /// <summary>
        /// Méthode qui réalise l'action (ici écrire dans un fichier)
        /// </summary>
        /// <param name="id">L'id du thread</param>
        private static void DoWork(object id)
        {
            var path = Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Outputs\file.txt"));
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine($"Thread {id}: Ecrit dans le fichier à {DateTime.Now}");
                Thread.Sleep(1000); // Simule un traitement d'une secondes
            }
        }
    }
}
