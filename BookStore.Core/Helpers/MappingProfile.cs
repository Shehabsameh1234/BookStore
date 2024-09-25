using AutoMapper;
using BookStore.Core.Dtos;
using BookStore.Core.Entities.Basket;
using BookStore.Core.Entities.Books;
using BookStore.Core.Entities.Orders;



namespace BookStore.Core.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Book,BookDto>()
                .ForMember(b=>b.Category,o=>o.MapFrom(s=>s.Category.Name))
                .ForMember(b=>b.PictureUrl,o=>o.MapFrom<BookPuctureUrlResolver>());
            CreateMap<AddressDto, OrderAddress>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(o => o.DeliveryMethod, o => o.MapFrom(d => d.DeliveryMethod.ShortName));


        }



    }
}
