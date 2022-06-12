using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace server_kiralama
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=server_kiralama;Integrated Security=True");
        public static string user_name_admin;
        public static string password_admin;
        public static int is_admin = 0;
        private void Form5_Load(object sender, EventArgs e)
        {
            is_admin = 0;
            
            // TODO: This line of code loads data into the 'server_kiralamaDataSet2.login_table' table. You can move, or remove it, as needed.
            this.login_tableTableAdapter.Fill(this.server_kiralamaDataSet2.login_table);
            // TODO: This line of code loads data into the 'server_kiralamaDataSet2.choosen_products_table' table. You can move, or remove it, as needed.
            this.choosen_products_tableTableAdapter.Fill(this.server_kiralamaDataSet2.choosen_products_table);
            // TODO: This line of code loads data into the 'server_kiralamaDataSet2.choosen_products_table' table. You can move, or remove it, as needed.
            this.choosen_products_tableTableAdapter.Fill(this.server_kiralamaDataSet2.choosen_products_table);
            // TODO: This line of code loads data into the 'server_kiralamaDataSet1.product_detail_table' table. You can move, or remove it, as needed.
           

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private int Generate_ref_code()
        {

            Random random = new Random();
            int reference_code = random.Next(1000, 10000);
            SqlCommand cmd_check_ref = new SqlCommand("select * from login_table where ref_code='" + reference_code + "'", con);
            SqlDataReader dr = cmd_check_ref.ExecuteReader();

            if (dr.Read())
            {
                dr.Close();
                con.Close();
                return 0;
            }
            else
            {
                dr.Close();
                return reference_code;

            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from login_Table where login_username='" + textBox1.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
                MessageBox.Show("Kullanıcı adı daha önce alınmış. Farklı bir kullanıcı adı dene.", "HATA");
                con.Close();
            }
            else
            {

                dr.Close();
                int new_ref_code = Generate_ref_code();

                if (new_ref_code == 0)
                {
                    new_ref_code = Generate_ref_code();
                }


                cmd = new SqlCommand("insert into login_table(login_username,login_password,has_reference,has_products,ref_code)values(@login_username,@login_password,@has_reference,@has_products,@ref_code)", con);
                cmd.Parameters.AddWithValue("login_username", textBox1.Text);
                cmd.Parameters.AddWithValue("login_password", textBox2.Text);
                cmd.Parameters.AddWithValue("has_reference", 0);
                cmd.Parameters.AddWithValue("has_products", 0);
                cmd.Parameters.AddWithValue("ref_code", new_ref_code);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Kullanıcı eklendi. Ürün eklemeye yönlendiriliyor..", "Kullanıcı ekleme başarılı");
                user_name_admin = textBox1.Text;
                password_admin = textBox2.Text;
                is_admin = 1;

                Form2 form2 = new Form2();
                form2.Show();
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 fr5 = new Form5();
            this.Hide();
            fr5.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd2 = new SqlCommand("delete from choosen_products_table where user_id='"+textBox3.Text+"'", con);
            cmd2.ExecuteNonQuery();
            SqlCommand cmd3 = new SqlCommand("delete from login_table where user_id='" + textBox3.Text + "'", con);
            cmd3.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kullanıcı verileri silindi", "Başarılı !");


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 fr_1 = new Form1();
            this.Hide();
            fr_1.Show();
        }
    }
}
