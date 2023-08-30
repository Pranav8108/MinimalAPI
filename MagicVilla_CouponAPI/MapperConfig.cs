using AutoMapper;
using MagicVilla_CouponAPI.Models;
using MagicVilla_CouponAPI.Models.dto;

namespace MagicVilla_CouponAPI
{
    public class MapperConfig : Profile //from nuget mapper
    {
        public MapperConfig()
        {
            CreateMap<Coupon , CouposDto>().ReverseMap();
            CreateMap<Coupon,CCoupondto>().ReverseMap();
                
        }
    }
}
