using WarehouseApp.Models;

namespace WarehouseApp.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    { }

    [Test]
    public void WhenCreatingBoxWithNegativeValues_ThenThrowsException()
    {
        var date1 = DateTime.Now;
        Assert.Throws<ArgumentException>(() => CreateBox(date1, -1));
    }
    [Test]
    public void WhenCreatingPalletWithNegativeValues_ThenThrowsException()
    {
        Assert.Throws<ArgumentException>(() => CreatePallet(1, -1));
    }
    [Test]
    public void WhenCreatingBoxLargerThanThePalletSize_ThenThrowsException()
    {
        var pallet = CreatePallet(1);
        var box = CreateBox(DateTime.Now, 5);
        Assert.Throws<ArgumentException>(() => pallet.AddBox(box));
    }
    [Test]
    public void WhenCreatingTwoPalletsWithSameExpirationDate_ThenGettingOnePalletsGroup()
    {
        var date = DateTime.Now;
        var box1 = CreateBox(date);
        var box2 = CreateBox(date);
        var pallet1 = CreatePallet(1);
        var pallet2 = CreatePallet(2);
        pallet1.AddBox(box1);
        pallet2.AddBox(box2);
        var warehouse = new Warehouse(1);
        warehouse.AddPallet(pallet1);
        warehouse.AddPallet(pallet2);

        var groupDto = warehouse.GroupPalletsByExpirationData();
        Assert.That(groupDto.OrderedPallets.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void WhenCreatingTwoPalletsWithDifferentExpirationDate_ThenGettingTwoPalletsGroup()
    {
        var date1 = DateTime.Now;
        var date2 = date1.AddMonths(1);
        var box1 = CreateBox(date1);
        var box2 = CreateBox(date2);
        var pallet1 = CreatePallet(1);
        var pallet2 = CreatePallet(2);
        pallet1.AddBox(box1);
        pallet2.AddBox(box2);
        var warehouse = new Warehouse(1);
        warehouse.AddPallet(pallet1);
        warehouse.AddPallet(pallet2);

        var groupDto = warehouse.GroupPalletsByExpirationData();
        Assert.That(groupDto.OrderedPallets.Count, Is.EqualTo(2));
    }
    private Box CreateBox(in DateTime dateOfManufacture, in int paramValue = 1) => 
        new Box(dateOfManufacture, paramValue, paramValue, paramValue, paramValue);
    private Pallet CreatePallet(in int id, in int paramValue = 1) => 
        new Pallet(id, paramValue, paramValue, paramValue);
}