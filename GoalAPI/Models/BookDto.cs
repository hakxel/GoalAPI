using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAPI.Models
{
    /// <summary>
    /// A book with Id, Title, Subtitle, and Author fields
    /// </summary>
    public class BookDto
    {
        /// <summary>
        /// The id of the book
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The title of the book
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The subtitle of the book
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// The author of the book
        /// </summary>
        public string Author { get; set; }
    }
}
