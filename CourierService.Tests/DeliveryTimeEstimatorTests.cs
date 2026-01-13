using CourierService.Application.Interfaces;
using CourierService.Domain;
using CourierService.Infrastructure.Services;
using FluentAssertions;
using Moq;

namespace CourierService.Tests
{
    public class DeliveryTimeEstimatorTests
    {
        [Fact]
        public void Estimate_ShouldAssignDeliveryTime_ToAllPackages()
        {
            // Arrange
            var packages = new List<Package>
            {
                new("PKG1",50,30, "OFR001"),
                new("PKG2",75,125, "OFR002")

            };

            var shipmentSelectorMock = new Mock<IShipmentPlanner>();

            shipmentSelectorMock
                .Setup(s => s.PlanShipment(It.IsAny<IList<Package>>(), 200))
                .Returns(packages);

            var estimator = new DeliveryTimeEstimator(shipmentSelectorMock.Object);

            // Act
            estimator.Estimate(
                packages,
                vehicleCount: 1,
                speed: 70,
                maxWeight: 200);

            // Assert
            packages.All(p => p.DeliveryTime > 0).Should().BeTrue();
        }

        [Fact]
        public void Estimate_ShouldUseShipmentSelector()
        {
            var packages = new List<Package>
            {
                new("PKG1",10,20, "OFR001")
            };

            var selectorMock = new Mock<IShipmentPlanner>();
            selectorMock
                .Setup(s => s.PlanShipment(It.IsAny<IList<Package>>(), 100))
                .Returns(packages);

            var estimator = new DeliveryTimeEstimator(selectorMock.Object);

            estimator.Estimate(packages, 1, 50, 100);

            selectorMock.Verify(
                s => s.PlanShipment(It.IsAny<IList<Package>>(), 100),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Estimate_ShouldCalculateCorrectDeliveryTime()
        {
            var packages = new List<Package>
            {
                new("PKG1",20,140, "OFR001")
            };

            var selectorMock = new Mock<IShipmentPlanner>();
            selectorMock
                .Setup(s => s.PlanShipment(It.IsAny<IList<Package>>(), 200))
                .Returns(packages);

            var estimator = new DeliveryTimeEstimator(selectorMock.Object);

            estimator.Estimate(packages,
                vehicleCount: 1,
                speed: 70,
                maxWeight: 200);

            packages.First().DeliveryTime.Should().Be(2); // 140 / 70
        }
    }
}
