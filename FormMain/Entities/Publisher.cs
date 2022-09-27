using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public partial class Publisher
    {
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string? ContactNo { get; set; }
        public string? Adress { get; set; }
        public int? LanguageId { get; set; }


        public virtual Language Language { get; set; }  //many to one
        public virtual ICollection<Book> Books { get; set; }  //many to many
    }
}
