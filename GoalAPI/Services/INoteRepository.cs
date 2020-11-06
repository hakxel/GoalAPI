using System;
using System.Collections.Generic;
using GoalAPI.Entities;

namespace GoalAPI.Services
{
    public interface INoteRepository
    {
        Note GetNote(Guid bookId, Guid noteId);
        IEnumerable<Note> GetNotes(Guid bookId);
        void AddNote(Guid bookId, Note note);
        void UpdateNote(Note note);
        void DeleteNote(Note note);
        bool Save();
    }
}