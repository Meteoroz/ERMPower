using System;
using System.Globalization;
using System.IO;
using System.Linq;
using ERM_Models;
using FileUtilities;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UnitTests
{
    public class FileUtilityTests
    {
        private string location = String.Empty;
        private string filter = String.Empty;

        private IFileEnumerator _fileEnumerator = new FileEnumerator();

        public FileUtilityTests()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-AU");
            var config = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json")
                 .Build();

            location = config["path"];
            filter = config["filter"];
        }

        [Fact]
        public void GetFileNames_ShouldFindCorrectNumberOfFiles()
        {
            // Arrange
            var expected = 2;

            // Act
            var fileNames = _fileEnumerator.GetFileNames(location, filter);
            var actual = fileNames.Count();

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void GetFileNames_ShouldFindCorrectNamedFiles()
        {
            // Arrange
            var expected = false;

            // Act
            var fileNames = _fileEnumerator.GetFileNames(location, filter);
            var actual = false;
            foreach (var fileName in fileNames)
            {
                var shortFilename = Path.GetFileNameWithoutExtension(fileName);
                if (!shortFilename.StartsWith("tou_", StringComparison.CurrentCultureIgnoreCase) &&
                !shortFilename.StartsWith("lp_", StringComparison.CurrentCultureIgnoreCase))
                {
                    actual = true;
                    break;
                }
            }

            // Assert
            Assert.Equal(expected, actual);

        }
    }
}