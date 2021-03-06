﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GoalAPI.Models;
using GoalAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace GoalAPI.Controllers
{
    [ApiController]
    [Produces("application/json", "application/xml")]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? 
                throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<BookDto>> GetBooks([FromQuery] string author, string searchQuery)
        {
            var booksFromRepo = _bookRepository.GetBooks(author, searchQuery);
            var books = _mapper.Map<IEnumerable<BookDto>>(booksFromRepo);

            return Ok(books);
        }

        /// <summary>
        /// Gets a book by Id
        /// </summary>
        /// <param name="bookId">The id of the book you want to get</param>
        /// <returns>IActionResult</returns>
        [HttpGet("{bookId:guid}", Name = "GetBook")]
        public IActionResult GetBook(Guid bookId)
        {
            var bookFromRepo = _bookRepository.GetBook(bookId);

            if (bookFromRepo == null)
            {
                return NotFound();
            }

            var book = _mapper.Map<BookDto>(bookFromRepo);

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<BookDto> CreateBook(BookForCreationDto book)
        {
            var bookEntity = _mapper.Map<Entities.Book>(book);
            _bookRepository.AddBook(bookEntity);
            _bookRepository.Save();

            var bookToReturn = _mapper.Map<BookDto>(bookEntity);

            return CreatedAtRoute("GetBook", new { bookId = bookToReturn.Id }, bookToReturn);
        }


        /// <summary>
        /// Partially update a book
        /// </summary>
        /// <param name="bookId">The id of the book you want update</param>
        /// <param name="patchDocument">The set of operations to apply to the book</param>
        /// <returns>An ActionResult</returns>
        /// <remarks>
        /// Sample request (this request updates the book **title**)    
        /// ```
        /// PATCH /books/id   
        /// [ 
        ///     { 
        ///         "op": "replace", 
        ///         "path": "/title", 
        ///         "value": "new title" 
        ///     } 
        /// ]
        /// ```
        /// </remarks>
        [HttpPatch("{bookId}")]
        public ActionResult PartiallyUpdateBook(Guid bookId, JsonPatchDocument<BookForUpdateDto> patchDocument)
        {
            if (!_bookRepository.BookExists(bookId))
            {
                return NotFound();
            }

            var bookFromRepo = _bookRepository.GetBook(bookId);

            var bookToPatch = _mapper.Map<BookForUpdateDto>(bookFromRepo);

            patchDocument.ApplyTo(bookToPatch, ModelState);

            if (!TryValidateModel(bookToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(bookToPatch, bookFromRepo);
            _bookRepository.UpdateBook(bookFromRepo);
            _bookRepository.Save();

            return NoContent();
        }

        [HttpDelete("{bookId}")]
        public ActionResult DeleteBook(Guid bookId)
        {
            var bookFromRepo = _bookRepository.GetBook(bookId);

            if (bookFromRepo == null)
            {
                return NotFound();
            }

            _bookRepository.DeleteBook(bookFromRepo);
            _bookRepository.Save();

            return NoContent();

        }
    }
}
