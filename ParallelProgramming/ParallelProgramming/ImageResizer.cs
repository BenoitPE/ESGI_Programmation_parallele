namespace ParallelProgramming
{
    public class ImageResizer
    {
        private readonly List<string> imageFiles = new List<string>();
        private string inputDirectory = string.Empty;
        private readonly string outputDirectory = string.Empty;
        private const string ExtensionType = "jpg";

        public ImageResizer()
        {
            inputDirectory = Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Inputs\"));
            outputDirectory = Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Outputs\"));

            // Charger les fichiers d'images .jpg présents dans le dossier Inputs/
            imageFiles = new List<string>(Directory.GetFiles(inputDirectory, $"*.{ExtensionType}", SearchOption.TopDirectoryOnly));
        }

        /// <summary>
        /// Compression d'image en séquentielle
        /// </summary>
        public void SingleThreadImageResizer()
        {
            foreach (var file in imageFiles)
            {
                string outputFilePath = outputDirectory + Path.GetFileNameWithoutExtension(file) + $"_compressed.{ExtensionType}";

                Resize(file, outputFilePath);
            }
        }

        /// <summary>
        /// Compression d'image en parallèle
        /// </summary>
        public void MultiThreadImageResizer()
        {
            Parallel.ForEach(imageFiles, new ParallelOptions { MaxDegreeOfParallelism = 30 }, (file) =>
            {
                string outputFilePath = outputDirectory + Path.GetFileNameWithoutExtension(file) + $"_compressed.{ExtensionType}";

                Resize(file, outputFilePath);
            });
        }

        /// <summary>
        /// Méthode pour compresser une image en 100x100
        /// </summary>
        private static void Resize(string file, string outputFilePath)
        {
            using (var image = Image.Load(file))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(100, 100),
                    Mode = ResizeMode.Max
                }));
                image.Save(outputFilePath);
            }
        }

        public int CountFilesInInputFolder()
        {
            return Directory.GetFiles(inputDirectory, "*", SearchOption.AllDirectories).Length;
        }

        public void InitFilesInInputFolder(int nbCopy)
        {
            var file = imageFiles.First();
            string filePath = inputDirectory + Path.GetFileNameWithoutExtension(file) + $".{ExtensionType}";
            for (int i = 0; i < nbCopy; i++)
            {
                string fileCopiedPath = inputDirectory + Path.GetFileNameWithoutExtension(file) + $"_{i}.{ExtensionType}";
                File.Copy(filePath, fileCopiedPath, true);
            }
        }
    }
}
