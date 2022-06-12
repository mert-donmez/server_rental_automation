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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=server_kiralama;Integrated Security=True");
        int has_reference = 0;
        int has_products = 0;
        string check_product;
        public static int captcha=0;

        public static string user_name;
        public static string password;
        private void Form1_Load(object sender, EventArgs e)
        {
            Random random = new Random();
            int number1, number2;
            number1 = random.Next(0, 99);
            number2 = random.Next(0, 10);
            label9.Text = number1.ToString();
            label11.Text = number2.ToString();
            captcha = Convert.ToInt32(label9.Text)+Convert.ToInt32(label11.Text);

            



        }


        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            con.Open();
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox6.Text != string.Empty)
            {
               
                string user_input_captcha = (textBox6.Text).ToString();


                string sql = "Select *From login_table where login_username = @username AND login_password = @password";
                SqlParameter prm1 = new SqlParameter("username", textBox1.Text);
                SqlParameter prm2 = new SqlParameter("password", textBox2.Text);
                SqlCommand komut = new SqlCommand(sql, con);
                komut.Parameters.Add(prm1);
                komut.Parameters.Add(prm2);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);

                if (dt.Rows.Count > 0 && Convert.ToInt32(user_input_captcha) == Convert.ToInt32(captcha)) 
                {
                    SqlCommand cmd = new SqlCommand("select *from login_Table where login_username='" + textBox1.Text+ "'", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        check_product = dr["has_products"].ToString();
                        

                    }
                    
                    if (Convert.ToInt32(check_product) == 0 && textBox1.Text != "admin")
                    {
                        dr.Close();
                        user_name = textBox1.Text;
                        password = textBox2.Text;
                        Form2 fr = new Form2();
                        fr.Show();
                        this.Hide();
                        con.Close();
                    }

                    else if (Convert.ToInt32(check_product) == 1 && textBox1.Text != "admin")
                    {
                        dr.Close();
                        user_name = textBox1.Text;
                        password = textBox2.Text;
                        Form4 fr4 = new Form4();
                        fr4.Show();
                        this.Hide();
                        con.Close();
                    }
                    else if (textBox1.Text == "admin")
                    {
                        dr.Close();
                        user_name = textBox1.Text;
                        password = textBox2.Text;
                        Form5 fr5 = new Form5();
                        fr5.Show();
                        this.Hide();
                        con.Close();

                    }
                }
                else if (dt.Rows.Count > 0 && Convert.ToInt32(user_input_captcha) != captcha)
                {
                    MessageBox.Show("Captcha doğrulanamadı");
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre yanlış.");
                    con.Close();
                }

            }
            else
            {
                MessageBox.Show("Gerekli boşlukları doldurun.");
                con.Close();
            }
            
        }
       

        private int Generate_ref_code()
        {
            
            Random random = new Random();
            int reference_code = random.Next(1000, 10000);
            SqlCommand cmd_check_ref = new SqlCommand("select * from login_table where ref_code='"+ reference_code +"'",con);
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

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            if (textBox3.Text != string.Empty && textBox7.Text != string.Empty && textBox4.Text != string.Empty)
            {
                if (textBox3.Text == textBox7.Text)
                {
                    SqlCommand cmd = new SqlCommand("select * from login_Table where login_username='" + textBox4.Text + "'", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        MessageBox.Show("Kullanıcı adı daha önce alınmış. Farklı bir kullanıcı adı deneyin.");
                        con.Close();
                    }
                    else
                    {
                        
                        if (textBox5.Text == string.Empty)
                        {
                            dr.Close();
                            int new_ref_code = Generate_ref_code();
                            
                            if (new_ref_code == 0)
                            {
                                new_ref_code = Generate_ref_code();
                            }

                            
                            cmd = new SqlCommand("insert into login_table(login_username,login_password,has_reference,has_products,ref_code)values(@login_username,@login_password,@has_reference,@has_products,@ref_code)", con);
                            cmd.Parameters.AddWithValue("login_username", textBox4.Text);
                            cmd.Parameters.AddWithValue("login_password", textBox3.Text);
                            cmd.Parameters.AddWithValue("has_reference", has_reference);
                            cmd.Parameters.AddWithValue("has_products", has_products);
                            cmd.Parameters.AddWithValue("ref_code", new_ref_code);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Kayıt oldunuz giriş yapabilirsiniz.");
                            con.Close();

                        }
                        else if (textBox5.Text != string.Empty)
                        {
                            dr.Close();
                            SqlCommand cmd_ref = new SqlCommand("select * from login_table where ref_code='" + textBox5.Text + "'", con);
                            SqlDataReader dr2 = cmd_ref.ExecuteReader();
                            if (dr2.Read())
                            {
                                dr2.Close();
                                int new_ref_code = Generate_ref_code();

                                if (new_ref_code == 0)
                                {
                                    new_ref_code = Generate_ref_code();
                                }
                                cmd = new SqlCommand("insert into login_table(login_username,login_password,has_reference,has_products,ref_code)values(@login_username,@login_password,@has_reference,@has_products,@ref_code)", con);
                                cmd.Parameters.AddWithValue("login_username", textBox4.Text);
                                cmd.Parameters.AddWithValue("login_password", textBox3.Text);
                                cmd.Parameters.AddWithValue("has_reference", 1);
                                cmd.Parameters.AddWithValue("has_products", has_products);
                                cmd.Parameters.AddWithValue("ref_code", new_ref_code);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Kayıt oldunuz giriş yapabilirsiniz. Referans kodunuz olduğu için siz ve referansınız %10 indirim kazandınız!");
                                int ref_code = Convert.ToInt32(textBox5.Text);
                                SqlCommand cmd3 = new SqlCommand("update login_table set has_reference=@has_reference where ref_code='" + ref_code + "'", con);
                                cmd3.Parameters.AddWithValue("@has_reference", 1);
   
                                cmd3.ExecuteNonQuery();
                                con.Close();
                            }
                            else
                            {
                                MessageBox.Show("Referans kodunuz geçersiz.");
                                con.Close();
                            }

                            

                        }
                       
                    }

                }
                else
                {
                    MessageBox.Show("Girdiginiz şifreler aynı değil");
                    con.Close();
                }
                
                


         
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ve şifre giriniz.");
                con.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
