namespace WPFApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public double StorageValue { get; set; }
        public string Name { get; set; }
        public int UnitId { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
