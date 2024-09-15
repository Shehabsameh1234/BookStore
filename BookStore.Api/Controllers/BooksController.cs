using AutoMapper;
using BookStore.Api.Errors;
using BookStore.Core.Dtos;
using BookStore.Core.Entities.Books;
using BookStore.Core.Helpers;
using BookStore.Core.Service.Contract;
using Microsoft.AspNetCore.Mvc;


namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ApiBaseController
    {
        private readonly IBooksService _booksService;
        private readonly IMapper _mapper;

        public BooksController(IBooksService booksService,IMapper mapper)
        {
            _booksService = booksService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(BookDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]

        [HttpGet]
        public async Task<ActionResult<Pagination<BookDto>>> GetBooks([FromQuery] QuerySpecParameters querySpec)
        {
            var books = await _booksService.GetAllBooksAsync(querySpec);
            if (books == null || books.Count==0) return NotFound(new ApisResponse(404,"a7a"));
            var mappedBook = _mapper.Map<IReadOnlyList<Book>,IReadOnlyList<BookDto>>(books);
            var count = await _booksService.GetCountAsync(querySpec);
            return Ok(new Pagination<BookDto>(querySpec.PageIndex,querySpec.PageSize,count, mappedBook));
        }
        [ProducesResponseType(typeof(BookDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book =await _booksService.GetBookAsync(id);
            if(book == null) return NotFound(new ApisResponse(404));
            var mappedBook = _mapper.Map<Book, BookDto>(book);
            return Ok(mappedBook);
        }
        [ProducesResponseType(typeof(Category),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status404NotFound)]

        [HttpGet("categories")]
        public async Task<ActionResult<Category>> GetCategories()
        {
            var categories = await _booksService.GetAllCategoriesAsync();
            if (categories == null) return NotFound(new ApisResponse(404));
            return Ok(categories);
        }
    }
}
