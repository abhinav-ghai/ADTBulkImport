using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ADTBulkImportConverter
{
    public class BulkImportInputGenerator
    {
        public void GenerateBulkModelImportInputFile(string inputPath, string outputFileName)
        {
            if (string.IsNullOrWhiteSpace(inputPath))
            {
                throw new ArgumentException("Input path is not valid");
            }

            if (string.IsNullOrWhiteSpace(outputFileName))
            {
                outputFileName = "output.ndjson";
            }

            if (!Directory.Exists(inputPath))
            {
                throw new ArgumentException("Input path must be a valid directory");
            }

            using (var inputStream = new MemoryStream())
            {
                using (var inputStreamWriter = new StreamWriter(inputStream))
                {
                    ProcessDirectory(inputPath, inputStreamWriter);
                    inputStream.Position = 0;

                    using (var outputStreamWriter = new StreamWriter(outputFileName))
                    {
                        outputStreamWriter.WriteLine(@"{""Section"": ""Header""}");
                        outputStreamWriter.WriteLine(@"{""fileVersion"": ""1.0.0"", ""author"": ""foobar"", ""organization"": ""contoso""}");
                        outputStreamWriter.WriteLine(@"{""Section"": ""Models""}");
                        outputStreamWriter.Flush();
                        inputStream.CopyTo(outputStreamWriter.BaseStream);
                    }
                }
            }
        }

        private void ProcessDirectory(string inputPath, StreamWriter streamWriter)
        {
            var files = Directory.GetFiles(inputPath);
            foreach (var file in files)
            {
                var fileContent = GetValidJsonFileContent(file);
                if (fileContent.Length > 0)
                {
                    streamWriter.WriteLine(fileContent);
                    streamWriter.Flush();
                }
            }

            var subDirectories = Directory.GetDirectories(inputPath);
            foreach (var subDirectory in subDirectories)
            {
                ProcessDirectory(subDirectory, streamWriter);
            }
        }

        private string GetValidJsonFileContent(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            try
            {
                var json = JToken.Parse(jsonString);
                return json.ToString(Formatting.None);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid JSON at path: " + filePath);
            }

            return string.Empty;
        }
    }
}
