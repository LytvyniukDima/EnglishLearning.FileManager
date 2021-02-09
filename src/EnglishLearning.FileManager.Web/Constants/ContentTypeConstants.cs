using System.Collections.Generic;
using EnglishLearning.FileManager.Application.Constants;

namespace EnglishLearning.FileManager.Web.Constants
{
    public static class ContentTypeConstants
    {
        public const string Zip = "application/zip";

        public const string ZipCompressed = "application/x-zip-compressed";
        
        public const string Txt = "text/plain";

        public const string Csv = "text/csv";
        
        public static readonly IReadOnlyDictionary<string, string> FileExtensionContentTypeMap = new Dictionary<string, string>()
        {
            { FileConstants.Txt, Txt },
            { FileConstants.Zip, Zip },
            { FileConstants.Csv, Csv },
        };
    }
}
