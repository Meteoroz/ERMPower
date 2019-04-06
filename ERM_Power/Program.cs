using System;
using System.IO;
using System.Linq;
using CSVUtilities;
using ERM_Models;
using FileUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ERM_Power
{
    class Program
    {
        private static IFileEnumerator _fileEnumerator;
        private static ICsvProcessor _csvProcessor;

        static void Main(string[] args)
        {
            RegisterServices();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var filesPath = config["path"];
            var filter = config["filter"];

            try
            {
                var fileNames = _fileEnumerator.GetFileNames(filesPath, filter);
                var lines = _csvProcessor.ProcessFiles(fileNames);

                lines.ToList().ForEach(Console.WriteLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IFileEnumerator, FileEnumerator>();
            collection.AddScoped<ICsvProcessor, CSVProcessor>();

            var serviceProvider = collection.BuildServiceProvider();
            _fileEnumerator = serviceProvider.GetService<IFileEnumerator>();
            _csvProcessor = serviceProvider.GetService<ICsvProcessor>();
        }
    }
}
