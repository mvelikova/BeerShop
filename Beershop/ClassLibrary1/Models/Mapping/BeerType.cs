namespace BeerShop.Data.Models.Mapping
{
  public  class BeerType
    {
        public int BeerId { get; set; }
        public Beer Beer { get; set; }

        public int TypeId { get; set; }
        public Type Type { get; set; }
    }
}
