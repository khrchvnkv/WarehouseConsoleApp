using System.Text.Json;
using WarehouseApp.Models;

namespace WarehouseApp.Data
{
    public class WarehouseDb
    {
        private const string Path = "data.json";
        
        public async Task<List<Warehouse>?> ReadDataAsync()
        {
            await TryWriteRandomTestDataIfFileEmpty();
            using var reader = new StreamReader(Path);
            var data = await reader.ReadToEndAsync();
            return JsonSerializer.Deserialize<List<Warehouse>>(data);
        }
        private async Task TryWriteRandomTestDataIfFileEmpty()
        {
            if (!File.Exists(Path))
            {
                var file = File.Create(Path);
                file.Close();
            }

            var data = await File.ReadAllTextAsync(Path);
            if (!string.IsNullOrWhiteSpace(data)) return;

            var random = new Random();
            await using var writer = new StreamWriter(Path);
            var warehouse1 = CreateWarehouseWithRandomPallets(1, 10);

            var warehousesList = new List<Warehouse> { warehouse1 };
            await writer.WriteAsync(JsonSerializer.Serialize(warehousesList));

            Warehouse CreateWarehouseWithRandomPallets(in int warehouseId, in int palletsCount)
            {
                var warehouse = new Warehouse(warehouseId);
                for (int i = 0; i < palletsCount; i++)
                {
                    var pallet = GetRandomPallet(i + 1);
                    warehouse.AddPallet(pallet);
                }

                return warehouse;
            }
            
            Pallet GetRandomPallet(int id)
            {
                var pallet = new Pallet(id, GetRandomFloat(), GetRandomFloat(), GetRandomFloat());
                var randomCount = random.Next(1, 10);
                for (var i = 0; i < randomCount; i++)
                    pallet.AddBox(i % 2 == 0
                        ? CreateRandomBox1(pallet.Width, pallet.Height)
                        : CreateRandomBox2(pallet.Width, pallet.Height));

                return pallet;
            }

            Box CreateRandomBox1(float widthLimit, float heightLimit)
            {
                return new(DateTime.Today.AddDays(random.Next(1, 5)), GetRandomFloatWithLimit(widthLimit),
                    GetRandomFloatWithLimit(heightLimit), GetRandomFloat(), GetRandomFloat());
            }

            Box CreateRandomBox2(float widthLimit, float heightLimit)
            {
                return new(TimeSpan.FromDays(random.Next(1, 5)), GetRandomFloatWithLimit(widthLimit),
                    GetRandomFloatWithLimit(heightLimit), GetRandomFloat(), GetRandomFloat());
            }

            float GetRandomFloat()
            {
                return random.NextSingle() * 20;
            }

            float GetRandomFloatWithLimit(float maximum)
            {
                return random.NextSingle() * maximum;
            }
        }
    }
}