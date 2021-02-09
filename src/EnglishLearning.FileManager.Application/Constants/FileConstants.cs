namespace EnglishLearning.FileManager.Application.Constants
{
    public static class FileConstants
    {
        public static readonly string Txt = "txt";

        public static readonly string Zip = "zip";

        public static readonly string Csv = "csv";
        
        public static readonly string[] ArchiveFileExtensions = new[]
        {
            Zip,
        };

        public static readonly string[] TextFileExtensions = new[]
        {
            Txt,
            Csv,
        };
    }
}
