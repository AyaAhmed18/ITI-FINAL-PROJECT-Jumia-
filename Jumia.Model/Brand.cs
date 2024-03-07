namespace Jumia.Model
{
    public class Brand
    {
        public int BrandID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public byte[]? LogoURL { get; set; }
        public string? Website { get; set; }

        // Navigation property to represent the one-to-many relationship with products
        public ICollection<Product> Products { get; set; }
    }
}