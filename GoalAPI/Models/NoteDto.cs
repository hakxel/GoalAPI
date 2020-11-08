using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAPI.Models
{
    /// <summary>
    /// A note with Id, Content, and BookId fields
    /// </summary>
    public class NoteDto
    {
        /// <summary>
        /// The id of the book note
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The content of the book note
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// The book id for the note
        /// </summary>
        public Guid BookId { get; set; }
    }
}
