using CarRentalApp.Data.Entity;

namespace CarRentalApp.Data.Entities
{
    public class Feature : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CarFeature> CarFeatures { get; set; }
    }
}

