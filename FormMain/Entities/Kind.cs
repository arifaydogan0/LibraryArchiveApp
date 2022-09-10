using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormMain.Entities
{
    public partial class Kind
    {
        public int KindId { get; set; }
        public string KindName { get; set; }

        public virtual ICollection<Book> Books { get; set; }  //many to many
    }
}
