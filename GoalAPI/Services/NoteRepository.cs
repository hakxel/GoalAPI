using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoalAPI.DBContexts;
using GoalAPI.Entities;

namespace GoalAPI.Services
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _context;

        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public Note GetNote(Guid bookId, Guid noteId)
        {
            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            if (noteId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(noteId));
            }

            return _context.Notes.Where(n => n.BookId == bookId && n.Id == noteId).FirstOrDefault();
        }

        public IEnumerable<Note> GetNotes(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            return _context.Notes.Where(n => n.BookId == bookId).ToList();
        }

        public void AddNote(Guid bookId, Note note)
        {
            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }
            // always set the AuthorId to the passed-in authorId
            note.BookId = bookId;
            _context.Notes.Add(note);
        }

        public void UpdateNote(Note note)
        {
            // no code in this implementation
            // changes in context added by mapper
        }

        public void DeleteNote(Note note)
        {
            _context.Notes.Remove(note);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
