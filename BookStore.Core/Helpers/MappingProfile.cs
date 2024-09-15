using AutoMapper;
using BookStore.Core.Dtos;
using BookStore.Core.Entities.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
