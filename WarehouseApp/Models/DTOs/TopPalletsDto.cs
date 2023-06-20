using System.Text;

namespace WarehouseApp.Models.DTOs
{
    public class TopPalletsDto
    {
        private readonly int _warehouseId;
        private readonly List<Pallet> _sortedPallets;

        public TopPalletsDto(int warehouseId, List<Pallet> sortedPallets)
        {
            _warehouseId = warehouseId;
            _sortedPallets = sortedPallets;
        }
        
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Top expiration date pallets\n");
            stringBuilder.Append($"Warehouse {_warehouseId}\n");
            foreach (var pallet in _sortedPallets)
            {
                stringBuilder.Append($"Pallet Id = {pallet.Id} --- {pallet.MaxExpirationDate.ToShortDateString()} --- Volume = {pallet.Volume}\n");
            }

            return stringBuilder.ToString();
        }
    }
}