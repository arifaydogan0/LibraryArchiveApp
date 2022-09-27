using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public partial class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public int? LanguageId { get; set; }

        public virtual Language Language { get; set; }   //many to one
        public virtual ICollection<Book> Books { get; set; }  //many to many
    }
}
