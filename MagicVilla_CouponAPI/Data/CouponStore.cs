using MagicVilla_CouponAPI.Models;

namespace MagicVilla_CouponAPI.Data
{
    public static class CouponStore
    {
        public static  List<Coupon> couponList = new List<Coupon>
        {
            new Coupon{Id = 1,Name = "10FF",Pecentage = 10 , IsActive = true},
             new Coupon{Id = 2,Name = "11FF",Pecentage = 20 , IsActive = false},
        };
    }
}
