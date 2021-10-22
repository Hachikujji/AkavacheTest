using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkavacheTest.Models
{
    public class Note
    {
        /// <summary>
        /// Used as key
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Users text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Date of caching
        /// </summary>
        public DateTimeOffset DateTimeOffset { get; set; }

    }
}
