using AutoMapper;
using AutoMapper.Execution;
using BookStore.Core.Dtos;
using BookStore.Core.Entities.Books;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Helpers
{
    public class BookPuctureUrlResolver : IValueResolver<Book, BookDto, string>
    {
        private IConfiguration _configuration;
        public BookPuctureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Book source, BookDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["AppUrl"]}/{source.PictureUrl}";

            return string.Empty;

        }
    }
}
