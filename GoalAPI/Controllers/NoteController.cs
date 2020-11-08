using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GoalAPI.Models;
using GoalAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoalAPI.Controllers
{
    [ApiController]
    [Produces("application/json", "application/xml")]
    [Route("api/books/{bookId}/notes")]
    public class NoteController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public NoteController(IBookRepository bookRepository, INoteRepository noteRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ??
                 throw new ArgumentNullException(nameof(bookRepository));
            _noteRepository = noteRepository ??
                throw new ArgumentNullException(nameof(noteRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<NoteDto>> GetBookNotes(Guid bookId)
        {
            if (!_bookRepository.BookExists(bookId))
            {
                return NotFound();
            }

            var bookNotesFromRepo = _noteRepository.GetNotes(bookId);
            var bookNotes = _mapper.Map<IEnumerable<NoteDto>>(bookNotesFromRepo);

            return Ok(bookNotes);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{noteId}", Name = "GetBookNote")]
        public ActionResult<NoteDto> GetBookNote(Guid bookId, Guid noteId)
        {
            if (!_bookRepository.BookExists(bookId))
            {
                return NotFound();
            }

            var bookNoteFromRepo = _noteRepository.GetNote(bookId, noteId);

            if (bookNoteFromRepo == null)
            {
                return NotFound();
            }

            var bookNote = _mapper.Map<NoteDto>(bookNoteFromRepo);

            return Ok(bookNote);
        }

        [HttpPost]
        [Consumes("application/json")]
        public ActionResult<NoteDto> CreateBookNote(Guid bookId, NoteForCreationDto note)
        {
            if (!_bookRepository.BookExists(bookId))
            {
                return NotFound();
            }

            var noteEntity = _mapper.Map<Entities.Note>(note);
            _noteRepository.AddNote(bookId, noteEntity);
            _noteRepository.Save();

            var noteToReturn = _mapper.Map<NoteDto>(noteEntity);

            return CreatedAtRoute("GetBookNote", new { bookId = bookId, noteId = noteToReturn.Id }, noteToReturn);
        }

        [HttpPut("{noteId}")]
        public ActionResult UpdateBookNote(Guid bookId, Guid noteId, NoteForUpdateDto note)
        {
            if (!_bookRepository.BookExists(bookId))
            {
                return NotFound();
            }

            var bookNoteFromRepo = _noteRepository.GetNote(bookId, noteId);

            if (bookNoteFromRepo == null)
            {
                return NotFound();
            }

            // map the entity to a NoteForUpdateDto
            // apply the updated field values to that dto
            // map the NoteForUpdateDto back to an entity
            _mapper.Map(note, bookNoteFromRepo);

            _noteRepository.UpdateNote(bookNoteFromRepo);

            _noteRepository.Save();
            return NoContent();
        }

        [HttpDelete("{noteId}")]
        public ActionResult DeleteBookNote(Guid bookId, Guid noteId)
        {
            if (!_bookRepository.BookExists(bookId))
            {
                return NotFound();
            }

            var bookNoteFromRepo = _noteRepository.GetNote(bookId, noteId);

            if (bookNoteFromRepo == null)
            {
                return NotFound();
            }

            _noteRepository.DeleteNote(bookNoteFromRepo);
            _noteRepository.Save();

            return NoContent();
        }
    }
}
