using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoalAPI.DBContexts;
using GoalAPI.Entities;
using GoalAPI.Models;

namespace GoalAPI.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool BookExists(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId)); 
            }

            return _context.Books.Any(b => b.Id == bookId);
        }

        public Book GetBook(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            return _context.Books.FirstOrDefault(b => b.Id == bookId);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.OrderBy(b => b.Title).ToList<Book>();
        }

        public IEnumerable<Book> GetBooks(IEnumerable<Guid> bookIds)
        {
            if (bookIds == null)
            {
                throw new ArgumentNullException(nameof(bookIds));
            }

            return _context.Books.Where(b => bookIds.Contains(b.Id)).OrderBy(b => b.Title).ToList();
        }

        public IEnumerable<Book> GetBooks(string author, string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(author) && string.IsNullOrWhiteSpace(searchQuery))
            {
                return GetBooks();
            }

            var collection = _context.Books as IQueryable<Book>;

            if (!string.IsNullOrWhiteSpace(author))
            {
                author = author.Trim().ToLower();
                collection = collection.Where(b => b.Author.ToLower() == author);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim().ToLower();
                collection = collection.Where(b => b.Title.Contains(searchQuery)
                    || b.Subtitle.Contains(searchQuery)
                    || b.Author.Contains(searchQuery)).OrderBy(b => b.Title);
            }

            return collection.ToList();
        }

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            book.Id = Guid.NewGuid();

            foreach (var note in book.Notes)
            {
                note.Id = Guid.NewGuid();
                note.BookId = book.Id;
            }

            _context.Books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            // no implementation here
        }

        public void DeleteBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _context.Books.Remove(book);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
