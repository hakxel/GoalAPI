using System;
using System.Collections.Generic;
using GoalAPI.Entities;

namespace GoalAPI.Services
{
    public interface IBookRepository
    {
        bool BookExists(Guid bookId);
        Book GetBook(Guid bookId);
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> GetBooks(IEnumerable<Guid> bookIds);
        IEnumerable<Book> GetBooks(string author, string searchQuery);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        bool Save();
    }
}