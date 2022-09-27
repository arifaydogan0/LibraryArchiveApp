using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public partial class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int Piece { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
        public int? LanguageId { get; set; } 
        public int? PublisherId { get; set; }

        public virtual Language Language { get; set; }    //many to one
        public virtual Publisher Publisher { get; set; }  //many to one
        public virtual ICollection<Kind> Kinds { get; set; }     //many to many
        public virtual ICollection<Author> Authors { get; set; } //many to many
    }
}
