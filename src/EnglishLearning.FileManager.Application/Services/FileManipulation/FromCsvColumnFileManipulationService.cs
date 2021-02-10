using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Configuration;
using EnglishLearning.FileManager.Application.Constants;
using EnglishLearning.FileManager.Application.Models;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;

namespace EnglishLearning.FileManager.Application.Services.FileManipulation
{
    internal class FromCsvColumnFileManipulationService : BaseFileManipulationService
    {
        private readonly TextFileManipulationService _textFileManipulationService;
        
        public FromCsvColumnFileManipulationService(
            IOptions<FileShareConfiguration> fileShareConfiguration,
            TextFileManipulationService textFileManipulationService)
            : base(fileShareConfiguration)
        {
            _textFileManipulationService = textFileManipulationService;
        }
        
        public override async Task CreateFile(Stream fileStream, FileCreateModel fileCreateModel)
        {
            if (fileStream.CanSeek)
            {
                fileStream.Seek(0, SeekOrigin.Begin);    
            }
            
            using var parser = new TextFieldParser(fileStream);
            await using var outputStream = new MemoryStream();
            await using var streamWriter = new StreamWriter(outputStream);
            
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            parser.HasFieldsEnclosedInQuotes = true;
            parser.TrimWhiteSpace = true;

            var headerFields = parser.ReadFields();
            var sourceColumnIndex = GetIndexOfSourceColumn(headerFields, fileCreateModel.CsvColumnToRead);
            while (parser.PeekChars(1) != null)
            {
                var rowCells = parser.ReadFields();
                if (rowCells == null)
                {
                    continue;
                }
                
                var sourceColumn = rowCells[sourceColumnIndex];
                await streamWriter.WriteLineAsync(sourceColumn);
            }

            await streamWriter.FlushAsync();
            outputStream.Seek(0, SeekOrigin.Begin);

            fileCreateModel.Extension = FileConstants.Txt;
            await _textFileManipulationService.CreateFile(outputStream, fileCreateModel);
        }

        private int GetIndexOfSourceColumn(string[]? fields, string columnName)
        {
            if (fields == null)
            {
                throw new Exception("File isn't correct");
            }

            var columnIndex = Array.IndexOf(fields, columnName);
            if (columnIndex == -1)
            {
                throw new Exception($"File don't have column {columnName}");
            }

            return columnIndex;
        }
    }
}
