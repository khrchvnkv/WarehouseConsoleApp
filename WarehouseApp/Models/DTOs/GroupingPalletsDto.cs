using System.Text;

namespace WarehouseApp.Models.DTOs
{
    public class GroupingPalletsDto
    {
        private readonly int _warehouseId;

        public Dictionary<DateTime, List<Pallet>> OrderedPallets { get; } = new();

        public GroupingPalletsDto(int warehouseId)
        {
            _warehouseId = warehouseId;
        }
        
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"Grouping Pallets By Expiration Date\n");
            stringBuilder.Append($"Warehouse {_warehouseId}\n");
            foreach (var palletsPair in OrderedPallets)
            {
                var date = palletsPair.Key;
                stringBuilder.Append($"{date.ToShortDateString()} --- ");
                foreach (var pallet in palletsPair.Value)
                {
                    stringBuilder.Append($"Pallet Id = {pallet.Id}; ");
                }

                stringBuilder.Append('\n');
            }

            return stringBuilder.ToString();
        }
    }
}