using System.Collections.Generic;
using Extensions;
using Xunit;

namespace UnitTests
{
    public class MedianTests
    {
        [Fact]
        public void GetMedian_ShouldCalculateForOdd()
        {
            // Arrange
            var values = new List<decimal> { 1, 4, 3, 2, 5 };
            var expected = 3m;

            // Act
            var actual = values.GetMedian();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetMedian_ShouldCalculateForEven()
        {
            // Arrange
            var values = new List<decimal> { 5, 4, 6, 2, 1, 3 };
            var expected = 3.5m;

            // Act
            var actual = values.GetMedian();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsOutOfRangeOfMedian_ShouldBeOutOfRange()
        {
            // Arrange
            var expected = true;

            // Act
            var source = 100.5m;
            var actual = source.IsOutOfRangeOfMedian(20, 0.2m);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsOutOfRangeOfMedian_ShouldNotBeOutOfRange()
        {
            // Arrange
            var expected = false;

            // Act
            var source = 100.5m;
            var actual = source.IsOutOfRangeOfMedian(90, 0.2m);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
