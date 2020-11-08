using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalAPI.Models
{
    /// <summary>
    /// A book for update with Title, Subtitle and Author fields
    /// </summary>
    public class BookForUpdateDto
    {
        /// <summary>
        /// The title of the book
        /// </summary>
        [Required]
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
