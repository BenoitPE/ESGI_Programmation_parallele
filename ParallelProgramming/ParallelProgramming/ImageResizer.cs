namespace ParallelProgramming
{
    public class ImageResizer
    {
        private List<string> imageFiles = new List<string>();
        private string inputDirectory = string.Empty;
        private string outputDirectory = string.Empty;

        public ImageResizer()
        {
            inputDirectory = Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Inputs\"));
            outputDirectory = Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Outputs\"));

            // Charger les fichiers d'images .jpg présents dans le dossier Inputs/
            imageFiles = new List<string>(Directory.GetFiles(inputDirectory, "*.jpg", SearchOption.TopDirectoryOnly));
        }

        /// <summary>
        /// Compression d'image en séquentielle
        /// </summary>
        public void SingleThreadImageResizer()
        {
            foreach (var file in imageFiles)
            {
                string outputFilePath = outputDirectory + Path.GetFileNameWithoutExtension(file) + "_compressed.jpg";

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
                string outputFilePath = outputDirectory + Path.GetFileNameWithoutExtension(file) + "_compressed.jpg";

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
    }
}
