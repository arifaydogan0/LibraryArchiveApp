using FormMain.Context;
using FormMain.Entities;
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
            using (LibDbContext db = new())
            {
                try
                {
                    string title = txtTitle.Text.ToUpper();
                    int piece = int.Parse(txtPiece.Text);
                    int pageCount = int.Parse(txtPageCount.Text);
                    DateTime publishDate = Convert.ToDateTime(mskPublishDate.Text);

                    List<Kind> kinds = new List<Kind>();
                    foreach (var item in txtKind.Text.ToUpper().Split(',').ToList())
                        kinds.Add(new Kind() { KindName = item.ToUpper() });

                    List<Author> authors = new List<Author>();
                    foreach (var item in txtAuthor.Text.ToUpper().Split(',').ToList())
                        authors.Add(new Author() { Name = item.ToUpper() });

                    Publisher p = db.Publishers.Where(x => x.PublisherName == txtPublisher.Text.ToUpper()).FirstOrDefault();
                    if (p == null)
                        p = new Publisher() { PublisherName = txtPublisher.Text.ToUpper() };
                    int publisherId = 0;

                    Language l = db.Languages.Where(x => x.LanguageName == txtLanguage.Text.ToUpper()).FirstOrDefault();
                    if (l == null)
                        l = new Language() { LanguageName = txtLanguage.Text.ToUpper() };
                    int languageId = 0;

                    db.Publishers.Add(p);
                    db.Languages.Add(l);
                    db.SaveChanges();

                    publisherId = p.PublisherId;
                    languageId = l.LanguageId;

                    Book book = new Book()
                    {
                        Title = title,
                        Piece = piece,
                        PageCount = pageCount,
                        PublishDate = publishDate,
                        PublisherId = publisherId,
                        LanguageId = languageId,
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
                catch (Exception ex)
                {
                    MessageBox.Show("Kayıt Yapılamadı! Eksik veya Hatalı Bilgi Var Lütfen Kontrol Ediniz.\n\n" + ex.Message);
                }
            }

        }
    }
}
