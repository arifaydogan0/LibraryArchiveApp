using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public partial class Language
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }


        public virtual ICollection<Author> Authors { get; set; }  //one to many
        public virtual ICollection<Book> Books { get; set; }  //one to many
        public virtual ICollection<Publisher> Publisher { get; set; }  //one to many
    }
}
