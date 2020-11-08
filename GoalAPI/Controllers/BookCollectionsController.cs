using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GoalAPI.Helpers;
using GoalAPI.Models;
using GoalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace GoalAPI.Controllers
{
    [ApiController]
    [Produces("application/json", "application/xml")]
    [Route("api/bookcollections")]
    public class BookCollectionsController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookCollectionsController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("({ids})", Name = "GetBookCollection")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetBookCollection([FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var bookEntities = _bookRepository.GetBooks(ids);

            if (ids.Count() != bookEntities.Count())
            {
                return NotFound();
            }

            var booksToReturn = _mapper.Map<IEnumerable<BookDto>>(bookEntities);

            return Ok(booksToReturn);
        }

        [HttpPost]
        public ActionResult<IEnumerable<BookDto>> CreateBookCollection(IEnumerable<BookForCreationDto> bookCollection)
        {
            var bookEntities = _mapper.Map<IEnumerable<Entities.Book>>(bookCollection);
            foreach (var book in bookEntities)
            {
                _bookRepository.AddBook(book);
            }

            _bookRepository.Save();

            var bookCollectionToReturn = _mapper.Map<IEnumerable<BookDto>>(bookEntities);
            var idsString = string.Join(",", bookCollectionToReturn.Select(b => b.Id));

            return CreatedAtAction("GetBookCollection", new { ids = idsString }, bookCollectionToReturn);
        }
    }
}
