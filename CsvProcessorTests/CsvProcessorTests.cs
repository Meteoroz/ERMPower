using System;
using System.Globalization;
using System.Linq;
using CSVUtilities;
using ERM_Models;
using FileUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace UnitTests
{
    public class CsvProcessorTests
    {
        private string location = String.Empty;
        private string filter = String.Empty;

        private ICsvProcessor _csvProcessor;
        private IFileEnumerator _fileEnumerator;

        public CsvProcessorTests()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-AU");
            RegisterServices();
            var config = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();

            location = config["path"];
            filter = config["filter"];
        }

        private void RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<IFileEnumerator, FileEnumerator>();
            collection.AddScoped<ICsvProcessor, CSVProcessor>();

            var serviceProvider = collection.BuildServiceProvider();
            _fileEnumerator = serviceProvider.GetService<IFileEnumerator>();
            _csvProcessor = serviceProvider.GetService<ICsvProcessor>();
        }

        [Fact]
        public void ProcessFiles_ShouldFindOneLine()
        {
            // Arrange
            var expected = 1;

            // Act
            var fileNames = _fileEnumerator.GetFileNames(location, filter);
            var lines = _csvProcessor.ProcessFiles(fileNames);
            var actual = lines.Count();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
