using AutoMapper;
using BookStore.Core.Dtos;
using BookStore.Core.Entities.Basket;
using BookStore.Core.Entities.Books;



namespace BookStore.Core.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Book,BookDto>()
                .ForMember(b=>b.Category,o=>o.MapFrom(s=>s.Category.Name))
                .ForMember(b=>b.PictureUrl,o=>o.MapFrom<BookPuctureUrlResolver>());
          
        }



    }
}
