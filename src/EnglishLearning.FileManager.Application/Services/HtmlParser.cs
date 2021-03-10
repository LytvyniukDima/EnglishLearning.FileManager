using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EnglishLearning.FileManager.Application.Abstract;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace EnglishLearning.FileManager.Application.Services
{
    internal class HtmlParser : IHtmlParser
    {
        private readonly ILogger<HtmlParser> _logger;

        public HtmlParser(ILogger<HtmlParser> logger)
        {
            _logger = logger;
        }
        
        public async Task<Stream> ParseHtmlTableToCsv(Stream inputStream)
        {
            inputStream.Seek(0, SeekOrigin.Begin);
            
            var doc = new HtmlDocument();
            var memStream = new MemoryStream();
            var streamWriter = new StreamWriter(memStream);
            
            doc.Load(inputStream);
            var tableNode = doc.DocumentNode.SelectSingleNode("//tbody");
            foreach (HtmlNode row in tableNode.SelectNodes("tr"))
            {
                var values = new List<string>();
                foreach (HtmlNode cell in row.SelectNodes("td")) 
                {
                    values.Add(cell.InnerText);
                }

                var line = string.Join(",", values);
                await streamWriter.WriteLineAsync(line);
            }

            await streamWriter.FlushAsync();
            memStream.Seek(0, SeekOrigin.Begin);
            
            return memStream;
        }
    }
}