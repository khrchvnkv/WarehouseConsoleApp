using WarehouseApp.Models;
using WarehouseApp.Models.DTOs;

namespace WarehouseApp.Services
{
    public class WarehouseSortingService
    {
        public List<GroupingPalletsDto> GetGroupedPalletsDtos(in List<Warehouse>? warehouses)
        {
            var result = new List<GroupingPalletsDto>();
            if (warehouses is null) return result;
            
            foreach (var warehouse in warehouses)
            {
                var groupingDto = warehouse.GroupPalletsByExpirationData();
                result.Add(groupingDto);
            }

            return result;
        }

        public List<TopPalletsDto> GetTopPalletsDtos(in List<Warehouse>? warehouses)
        {
            var result = new List<TopPalletsDto>();
            if (warehouses is null) return result;
            
            foreach (var warehouse in warehouses)
            {
                var topPalletsDto = warehouse.GetTopExpirationPalletsData();
                result.Add(topPalletsDto);
            }

            return result;
        }
    }
}