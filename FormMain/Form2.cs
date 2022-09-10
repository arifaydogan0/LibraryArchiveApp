using FormMain.Context;
using FormMain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormMain
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (LibDbContext db = new())
            {
                string title = txtTitle.Text;
                List<string> kinds = txtKind.Text.Split(',').ToList();
                List<string> authors = txtAuthor.Text.Split(',').ToList();
                int publisherId = db.Publishers.Where(x => x.PublisherName == txtPublisher.Text).FirstOrDefault()?.PublisherId ?? 0;
                string language = txtLanguage.Text;
                int piece = int.Parse(txtPiece.Text);
                int pageCount = int.Parse(txtPageCount.Text);
                DateTime publishDate = Convert.ToDateTime(mskPublishDate.Text);

                List<Kind> kinds2 = new List<Kind>();
                foreach (var kind in kinds)
                    kinds2.Add(new Kind() { KindName = kind });

                List<Author> authors2 = new List<Author>();
                foreach (var author in authors)
                    authors2.Add(new Author() { Name = author, Surname = author });

                Book book = new Book()
                {
                    Title = title,
                    Piece = piece,
                    PageCount = pageCount,
                    PublishDate = publishDate,
                    Kinds = kinds2,
                    Authors = authors2,
                };

                if (db.Books.Where(x => x == book).ToList().Count() != 0)
                {
                    MessageBox.Show("Kayıt Zaten Var");
                    return;
                }

                db.Books.Add(book);
                db.SaveChanges();
                MessageBox.Show("Yeni Kayıt Eklendi.");
            }
        }
    }
}
