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
                var books = db.Books.Where(x => x.Title == textBox1.Text).ToList();
                dataGridView1.DataSource = books;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }
    }
}