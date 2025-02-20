using AutoMapper;
using AquaFeedShop.core.Models;
using AquaFeedShop.shared.Models.Request;
//using AquaFeedShop.shared.Models.Request;

namespace AquaFeedShop.shared.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CartModel, Cart>().ReverseMap();

        }
    }
}
