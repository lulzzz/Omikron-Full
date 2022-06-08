using System.Collections.Generic;
using FluentAssertions;
using Omikron.Sync;
using Xunit;

namespace Omikron.SyncService.UnitTest
{
    public class SyncIntervalTests
    {
        [Fact]
        public void SyncInterval_Parse_Should_Construct_Object_With_Ascending_Order()
        {
            // Arrange
            var values = new List<string> { "13:15:00", "13:10:00", "13:14:00", "12:15:00" };

            // Act
            var syncInterval = SyncInterval.Parse(values: values.ToArray());


            // Assert
            syncInterval.Should().BeInAscendingOrder();
            syncInterval.Should().HaveCount(expected: values.Count);
        }

        [Fact]
        public void SyncInterval_Parse_Should_Construct_Object_And_Accept_Unique_Values()
        {
            // Arrange
            var values = new List<string> { "13:15:00", "13:10:00", "13:10:00", "12:15:00" };

            // Act
            var syncInterval = SyncInterval.Parse(values: values.ToArray());


            // Assert
            syncInterval.Should().BeInAscendingOrder();
            syncInterval.Should().HaveCount(expected: 3);
        }

        [Fact]
        public void SyncInterval_Parse_Should_Construct_Object_And_Accept_Valid_Values()
        {
            // Arrange
            var values = new List<string> { "13:15:00", "1rr10:00", "13:10:00", "12:15:00" };

            // Act
            var syncInterval = SyncInterval.Parse(values: values.ToArray());


            // Assert
            syncInterval.Should().BeInAscendingOrder();
            syncInterval.Should().HaveCount(expected: 3);
        }
    }
}