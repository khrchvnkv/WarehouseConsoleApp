namespace WarehouseApp.Models
{
    public class Pallet
    {
        private const float DefaultPalletWeight = 30;

        public List<Box> Boxes { get; set; } = new();

        public int Id { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Depth { get; set; }

        public float Weight
        {
            get
            {
                var weight = DefaultPalletWeight;
                foreach (var box in Boxes) weight += box.Weight;

                return weight;
            }
        }

        public float Volume
        {
            get
            {
                var volume = Width * Height * Depth;
                foreach (var box in Boxes) volume += box.Volume;

                return volume;
            }
        }

        public DateTime ExpirationDate
        {
            get
            {
                var result = DateTime.MaxValue;
                foreach (var box in Boxes)
                    if (box.ExpirationDate < result)
                        result = box.ExpirationDate;

                return result;
            }
        }
        
        public DateTime MaxExpirationDate
        {
            get
            {
                var result = DateTime.MinValue;
                foreach (var box in Boxes)
                    if (box.ExpirationDate > result)
                        result = box.ExpirationDate;

                return result;
            }
        }

        public Pallet(int id, float width, float height, float depth)
        {
            if (width < 0.0f || height < 0.0f || depth < 0.0f)
                throw new ArgumentException("values cannot be negative");

            Id = id;
            Width = width;
            Height = height;
            Depth = depth;
        }

        public void AddBox(Box newBox)
        {
            if (newBox.Width > Width || newBox.Height > Height)
                throw new ArgumentException("the size of the box cannot be larger than the size of the pallet");

            Boxes.Add(newBox);
        }
    }
}