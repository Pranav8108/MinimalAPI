namespace MagicVilla_CouponAPI.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pecentage { get; set; }

        public bool IsActive { get; set; }
        public DateTime? Created { get; set; } //even though this accepts nullable we should diable in edit project file bcoz endpoint might break
        public DateTime? LastUpdated { get; set;
        }


    }
}
