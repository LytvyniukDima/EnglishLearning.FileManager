namespace EnglishLearning.FileManager.Web.Constants
{
    public static class ContentTypes
    {
        public const string Zip = "application/zip";

        public const string Txt = "text/plain";
        
        public static readonly string[] ArchiveContentTypes = new[]
        {
            ContentTypes.Zip,
        };

        public static readonly string[] TextFileContentTypes = new[]
        {
            ContentTypes.Txt,
        };
    }
}
