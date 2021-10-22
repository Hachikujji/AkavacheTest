using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkavacheTest
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset DateTimeOffset { get; set; }

    }
}
