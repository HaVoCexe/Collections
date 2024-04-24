namespace HankCollections.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CollectionId { get; set; }
        public int ImageId { get; set; }
        public double Price { get; set; }
        public Status Status { get; set; }
        public Satisfaction Satisfaction { get; set; }
        public string Comment { get; set; }
        public bool IsSold { get; set; }
        public int? TempId { get; set; }
    }

    public enum Status
    {
        New,
        Used,
        ForSale,
        Sold,
        WantToBuy,
        Undefined
    }
    public enum Satisfaction
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Undefined
    }
}
