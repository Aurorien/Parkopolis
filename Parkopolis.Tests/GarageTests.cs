using Moq;
using Parkopolis.Interfaces;

namespace Parkopolis.Tests
{
    public class GarageTests
    {
        [Fact]
        public void IsRegNumExists_ChecksIfRegNumExistsToPreventRegNumDuplicates_HappyPath()
        {
            // Arrange
            var garage = new Garage<IVehicle>(3);
            var existingVehicle = new Mock<IVehicle>();
            existingVehicle.Setup(v => v.RegNum).Returns("ABC123");
            garage.Add(existingVehicle.Object);

            // Act
            bool isDuplicate = garage.IsRegNumExists("ABC123");

            // Assert
            Assert.True(isDuplicate);
        }
    }
}
