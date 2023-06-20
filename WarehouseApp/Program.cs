using WarehouseApp.Data;
using WarehouseApp.Services;

var warehouseDb = new WarehouseDb();
var data = await warehouseDb.ReadDataAsync();
var warehouseSortingService = new WarehouseSortingService();

var groupPalletsByExpirationData = 
    warehouseSortingService.GetGroupingPalletsDtos(data);
foreach (var groupData in groupPalletsByExpirationData)
{
    Console.WriteLine(groupData.ToString());
}

var topPalletsGroup = warehouseSortingService.GetTopPalletsDtos(data);
foreach (var groupData in topPalletsGroup)
{
    Console.WriteLine(groupData.ToString());
}