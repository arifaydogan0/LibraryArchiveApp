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
            try
            {
                using (LibDbContext db = new())
                {
                    string title = txtTitle.Text;
                    int piece = int.Parse(txtPiece.Text);
                    int pageCount = int.Parse(txtPageCount.Text);
                    DateTime publishDate = Convert.ToDateTime(mskPublishDate.Text);
                    int publisherId = 0;

                    List<Kind> kinds = new List<Kind>();
                    foreach (var item in txtKind.Text.Split(',').ToList())
                        kinds.Add(new Kind() { KindName = item });

                    List<Author> authors = new List<Author>();
                    foreach (var item in txtAuthor.Text.Split(',').ToList())
                        authors.Add(new Author() { Name = item });

                    Publisher p = db.Publishers.Where(x => x.PublisherName == txtPublisher.Text).FirstOrDefault();
                    if (p == null)
                        p = new Publisher() { PublisherName = txtPublisher.Text };
                    publisherId = p.PublisherId;


                    Book book = new Book()
                    {
                        Title = title,
                        Piece = piece,
                        PageCount = pageCount,
                        PublishDate = publishDate,
                        PublisherId = publisherId,
                        Kinds = kinds,
                        Authors = authors,
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
            catch
            {
                MessageBox.Show("Kayıt Yapılamadı! Eksik veya Hatalı Bilgi Var Lütfen Kontrol Ediniz.");
            }
        }
    }
}
