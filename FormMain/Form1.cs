using FormMain.Context;
using FormMain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace FormMain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            BackgroundImage = Image.FromFile(Application.StartupPath + @"\libraryPicture.JPG");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
                label2.Visible = false;
            else
                label2.Visible = true;
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            GetBooks();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }


        async void GetBooks()
        {
            using (LibDbContext db = new())
            {
                List<Book> books = new List<Book>();
                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    books = await db.Books.Include(x => x.Authors)
                    .Include(x => x.Kinds)
                    .Include(x => x.Language)
                    .Include(x => x.Publisher)
                    .ToListAsync();
                }
                else
                {
                    books = await db.Books.Include(x => x.Authors)
                    .Include(x => x.Kinds)
                    .Include(x => x.Language)
                    .Include(x => x.Publisher)
                    .Where(x => (x.Title == textBox1.Text) || (x.Publisher.PublisherName == textBox1.Text) || x.Kinds.Any(a => a.KindName == textBox1.Text)).ToListAsync();
                }

                int syc = 0;
                foreach (var book in books)
                {
                    syc = dataGridView1.Rows.Add();
                    dataGridView1.Rows[syc].Cells[0].Value = book.Title;
                    dataGridView1.Rows[syc].Cells[1].Value = book.Authors.FirstOrDefault()?.Name ?? " ";
                    dataGridView1.Rows[syc].Cells[2].Value = book.Kinds.FirstOrDefault()?.KindName ?? " ";
                    dataGridView1.Rows[syc].Cells[3].Value = book.Language?.LanguageName ?? " ";
                    dataGridView1.Rows[syc].Cells[4].Value = book.PageCount;
                    dataGridView1.Rows[syc].Cells[5].Value = book.Piece;
                    dataGridView1.Rows[syc].Cells[6].Value = book.Publisher?.PublisherName ?? " ";
                    dataGridView1.Rows[syc].Cells[7].Value = book.PublishDate.Year;
                    syc++;
                }
            }
        }
    }
}