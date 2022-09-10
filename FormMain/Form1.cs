using FormMain.Context;
using FormMain.Entities;

namespace FormMain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

            using (LibDbContext db = new())
            {
                var books = db.Books.ToList();

                var syc = 0;
                foreach (var book in books)
                {
                    syc = dataGridView1.Rows.Add();
                    dataGridView1.Rows[syc].Cells[0].Value = book.Title;
                    dataGridView1.Rows[syc].Cells[1].Value = book.Authors;
                    dataGridView1.Rows[syc].Cells[2].Value = book.Language?.LanguageName ?? " ";
                    dataGridView1.Rows[syc].Cells[3].Value = book.PageCount;
                    dataGridView1.Rows[syc].Cells[4].Value = book.Piece;
                    dataGridView1.Rows[syc].Cells[5].Value = book.Publisher;
                    dataGridView1.Rows[syc].Cells[6].Value = book.PublishDate.Year;
                    syc++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }
    }
}