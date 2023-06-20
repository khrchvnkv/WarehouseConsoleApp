using WarehouseApp.Models.DTOs;

namespace WarehouseApp.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public List<Pallet> Pallets { get; set; } = new();

        public Warehouse(int id)
        {
            Id = id;
        }
        
        public void AddPallet(Pallet newPallet)
        {
            if (GetPalletById(newPallet.Id) is not null)
                throw new ArgumentException("pallet with this id already exists");

            Pallets.Add(newPallet);
        }

        public GroupingPalletsDto GroupPalletsByExpirationData()
        {
            var expiredDateOrderedPalletGroups = Pallets.GroupBy(p => p.ExpirationDate);
            var orderedExpiredDateOrderedPalletGroups =
                expiredDateOrderedPalletGroups.OrderBy(p => p.Key);

            var groupDto = new GroupingPalletsDto(Id);
            foreach (var group in orderedExpiredDateOrderedPalletGroups)
            {
                var pallets = group.ToList();
                var orderedPallets = pallets.OrderBy(p => p.Weight);
                groupDto.OrderedPallets.Add(group.Key, orderedPallets.ToList());
            }

            return groupDto;
        }

        public TopPalletsDto GetTopExpirationPalletsData()
        {
            var sortedByMaxExpirationDatePallets = Pallets.OrderByDescending(p => p.MaxExpirationDate);
            var limitList = sortedByMaxExpirationDatePallets.ToList().GetRange(0, 3);
            var sortedByVolumeRangedList = limitList.OrderBy(p => p.Volume).ToList();
            var topPallets = new TopPalletsDto(Id, sortedByVolumeRangedList);
            return topPallets;
        }

        private Pallet? GetPalletById(int id)
        {
            return Pallets.SingleOrDefault(p => p.Id == id);
        }
    }
}