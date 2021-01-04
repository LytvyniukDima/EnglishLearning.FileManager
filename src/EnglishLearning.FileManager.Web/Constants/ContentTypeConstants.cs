using System.Collections.Generic;
using EnglishLearning.FileManager.Application.Constants;

namespace EnglishLearning.FileManager.Web.Constants
{
    public static class ContentTypeConstants
    {
        public const string Zip = "application/zip";

        public const string Txt = "text/plain";
        
        public static readonly string[] ArchiveContentTypes = new[]
        {
            Zip,
        };

        public static readonly string[] TextFileContentTypes = new[]
        {
            Txt,
        };
        
        public static readonly IReadOnlyDictionary<string, string> ContentTypeFileExtensionMap = new Dictionary<string, string>()
        {
            { Txt, FileConstants.Txt },
            { Zip, FileConstants.Zip },
        };
        
        public static readonly IReadOnlyDictionary<string, string> FileExtensionContentTypeMap = new Dictionary<string, string>()
        {
            { FileConstants.Txt, Txt },
            { FileConstants.Zip, Zip },
        };
    }
}
