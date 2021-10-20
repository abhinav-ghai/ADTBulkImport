using System;

namespace ADTBulkImportConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: ADTBulkImportConverter <InputFolderPath> <OutputFileName>");
                return;
            }

            var generator = new BulkImportInputGenerator();
            var inputPath = args[0];
            try
            {
                generator.GenerateBulkModelImportInputFile(inputPath, args[1]);
                Console.WriteLine("Done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
