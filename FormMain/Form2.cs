using FormMain.Context;
using FormMain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
            /* Kayıt ekle. */
            AddBook();
        }

        async void AddBook()
        {
            using (LibDbContext db = new())
            {
                Book book = new();

                book.Title = txtTitle.Text.ToUpper();

                book.Piece = int.Parse(txtPiece.Text);

                book.PageCount = int.Parse(txtPageCount.Text);

                book.PublishDate = Convert.ToDateTime(mskPublishDate.Text);

                List<Kind> kinds = new List<Kind>();
                foreach (var item in txtKind.Text.ToUpper().Split(',').ToList())
                    kinds.Add(new Kind() { KindName = item.ToUpper() });
                book.Kinds = kinds;

                List<Author> authors = new List<Author>();
                foreach (var item in txtAuthor.Text.ToUpper().Split(',').ToList())
                    authors.Add(new Author() { Name = item.ToUpper() });
                book.Authors = authors;

                Publisher p = db.Publishers.Where(x => x.PublisherName == txtPublisher.Text.ToUpper()).FirstOrDefault();
                if (p == null)
                {
                    p = new Publisher() { PublisherName = txtPublisher.Text.ToUpper() };
                    db.Publishers.Add(p);
                    await db.SaveChangesAsync();
                }
                book.PublisherId = p.PublisherId;

                Language l = db.Languages.Where(x => x.LanguageName == txtLanguage.Text.ToUpper()).FirstOrDefault();
                if (l == null)
                {
                    l = new Language() { LanguageName = txtLanguage.Text.ToUpper() };
                    db.Languages.Add(l);
                    await db.SaveChangesAsync();
                }
                book.LanguageId = l.LanguageId;

                if (db.Books.ToList().Where(x => x.Title == book.Title && x.PublishDate == book.PublishDate && x.LanguageId == book.LanguageId && x.PublisherId == book.PublisherId && x.PageCount == book.PageCount).FirstOrDefault() != null)
                {
                    MessageBox.Show("Kayıt Zaten Var");
                    return;
                }

                db.Books.Add(book);
                await db.SaveChangesAsync();
                MessageBox.Show("Yeni Kayıt Eklendi.");

            }
        }
    }
}
