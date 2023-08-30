namespace MagicVilla_CouponAPI.Models.dto
{
    public class CCoupondto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pecentage { get; set; }

        public bool IsActive { get; set; }
        public DateTime? Created { get; set; }
    }
}
