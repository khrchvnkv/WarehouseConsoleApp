namespace WarehouseApp.Models
{
    public class Box
    {
        private const int DefaultBestBeforeDate = 100;

        public DateTime ExpirationDate { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Depth { get; set; }
        public float Weight { get; set; }
        public float Volume => Width * Height * Depth;

        public Box() { }
        
        public Box(DateTime dateOfManufacture, float width, float height, float depth, float weight)
        {
            if (width < 0.0f || height < 0.0f || depth < 0.0f || weight < 0.0f)
                throw new ArgumentException("values cannot be negative");

            ExpirationDate = dateOfManufacture.AddDays(DefaultBestBeforeDate);
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;
        }
        
        public Box(TimeSpan bestBeforeDate, float width, float height, float depth, float weight)
        {
            if (width < 0.0f || height < 0.0f || depth < 0.0f || weight < 0.0f)
                throw new ArgumentException("values cannot be negative");

            ExpirationDate = DateTime.Today.Add(bestBeforeDate);
            Width = width;
            Height = height;
            Depth = depth;
            Weight = weight;
        }
    }
}