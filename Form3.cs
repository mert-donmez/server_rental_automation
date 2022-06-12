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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=server_kiralama;Integrated Security=True");

        public static int new_price = Form2.price;
        public static int has_ref = 0;
        public static int server_charge = 0;
        public static string server_host_name;
        public static string server_ip_address;
        public static int server_password = 0;
        public static string payment_type;
        public static int user_id = 0;
        public static int has_products = 0;
        public static int admin_check;

        private void Form3_Load(object sender, EventArgs e)
        {
            
            con.Open();
            if (Form5.is_admin == 0)
            {
                SqlCommand cmd = new SqlCommand("select *from login_Table where login_username='" + Form1.user_name + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    has_products = Convert.ToInt32(dr["has_products"]);
                    user_id = Convert.ToInt32(dr["user_id"]);

                }
                dr.Close();
                con.Close();

            }
            else if (Form5.is_admin == 1)
            {
                SqlCommand cmd = new SqlCommand("select *from login_Table where login_username='" + Form5.user_name_admin + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    has_products = Convert.ToInt32(dr["has_products"]);
                    user_id = Convert.ToInt32(dr["user_id"]);

                }
                dr.Close();
                con.Close();
            }
            

            

            label11.Text = Form2.cpu;
            label10.Text = Form2.ram;
            label9.Text = Form2.storage;
            label8.Text = Form2.traffic;
            label14.Text = Form2.os_type;
            label13.Text = Form2.server_type;
            label12.Text = Form2.backup;
            label7.Text = Form2.user_panel;
            

            if (has_products == 1)
            {
                button1.Enabled = false;
                button1.Visible = false;
                button2.Enabled = true;
                button2.Visible = true;
            }
            else
            {
                button1.Enabled = true;
                button1.Visible = true;
                button2.Enabled = false;
                button2.Visible = false;
            }

            new_price = Form2.price;
            
            


        }

        private int Server_charge_calculate(int charge)
        {
            con.Open();
            if (Form5.is_admin == 0)
            {
                SqlCommand cmd = new SqlCommand("select *from login_Table where login_username='" + Form1.user_name + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    has_ref = Convert.ToInt32(dr["has_reference"]);
                    user_id = Convert.ToInt32(dr["user_id"]);
                    has_products = Convert.ToInt32(dr["has_products"]);

                }
                dr.Close();
                con.Close();

            }
            if (Form5.is_admin == 1)
            {
                SqlCommand cmd = new SqlCommand("select *from login_Table where login_username='" + Form5.user_name_admin + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    has_ref = Convert.ToInt32(dr["has_reference"]);
                    user_id = Convert.ToInt32(dr["user_id"]);
                    has_products = Convert.ToInt32(dr["has_products"]);

                }
                dr.Close();
                con.Close();
            }
            
            if (has_ref == 1)
            {
                if (radioButton1.Checked == true)
                {
                    int server_monthly_charge_10 = (charge * 90 / 100);
                    return server_monthly_charge_10;
                }
                else if (radioButton2.Checked == true)
                {
                    int server_yearly_charge = ((charge)*90/100)*12;
                    int server_yearly_charge_10 = server_yearly_charge * 90 / 100;
                    return server_yearly_charge_10;
                }

            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    int server_monthly_charge = charge;
                    return server_monthly_charge;
                }
                else if (radioButton2.Checked == true)
                {
                    int server_yearly_charge = (((charge) * 12) * 90 / 100);
                    return server_yearly_charge;
                }
            }
            return 0;

            
        }
        private int password_generator()
        {
            Random random = new Random();
            int new_server_password = random.Next(10000, 99999);
            return new_server_password;
        }
        private string ip_address_generator()
        {
            Random random = new Random();
            int ip_1 = random.Next(10, 99);
            int ip_2 = random.Next(100, 255);
            int ip_3 = random.Next(10, 99);
            int ip_4 = random.Next(100, 255);
            string new_ip_address = ("'"+ip_1+"."+ip_2+"."+ip_3+"."+ip_4+"'");
            return new_ip_address;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            if (Form5.is_admin == 0)
            {
                server_password = password_generator();
                server_host_name = ("'server." + Form1.user_name + ".com'");
                con.Close();
                server_ip_address = ip_address_generator();
                server_charge = Server_charge_calculate(new_price);
                con.Open();




                SqlCommand cmd = new SqlCommand("insert into choosen_products_table(user_id,user_name,CPU,RAM,storage,traffic,os_type,server_type,backup_type,user_panel,server_charge,server_password,server_host_name,server_ip_address,payment_type)values(@user_id,@user_name,@CPU,@RAM,@storage,@traffic,@os_type,@server_type,@backup_type,@user_panel,@server_charge,@server_password,@server_host_name,@server_ip_address,@payment_type)", con);
                cmd.Parameters.AddWithValue("user_name", Form1.user_name);
                cmd.Parameters.AddWithValue("user_id", user_id);
                cmd.Parameters.AddWithValue("CPU", Form2.cpu);
                cmd.Parameters.AddWithValue("RAM", Form2.ram);
                cmd.Parameters.AddWithValue("storage", Form2.storage);
                cmd.Parameters.AddWithValue("traffic", Form2.traffic);
                cmd.Parameters.AddWithValue("os_type", Form2.os_type);
                cmd.Parameters.AddWithValue("server_type", Form2.server_type);
                cmd.Parameters.AddWithValue("backup_type", Form2.backup);
                cmd.Parameters.AddWithValue("user_panel", Form2.backup);


                cmd.Parameters.AddWithValue("server_charge", server_charge);
                cmd.Parameters.AddWithValue("payment_type", payment_type);
                cmd.Parameters.AddWithValue("server_password", server_password);
                cmd.Parameters.AddWithValue("server_host_name", server_host_name);
                cmd.Parameters.AddWithValue("server_ip_address", server_ip_address);

                SqlCommand cmd3 = new SqlCommand("update login_table set has_products=@has_products where login_username='" + Form1.user_name + "'", con);
                cmd3.Parameters.AddWithValue("@has_products", 1);



                cmd.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();

                con.Close();
                Form4 Form4 = new Form4();
                this.Hide();
                Form4.Show();
            }
            else if (Form5.is_admin == 1)
            {
                server_password = password_generator();
                server_host_name = ("'server." + Form5.user_name_admin + ".com'");
                con.Close();
                server_ip_address = ip_address_generator();
                server_charge = Server_charge_calculate(new_price);
                con.Open();




                SqlCommand cmd = new SqlCommand("insert into choosen_products_table(user_id,user_name,CPU,RAM,storage,traffic,os_type,server_type,backup_type,user_panel,server_charge,server_password,server_host_name,server_ip_address,payment_type)values(@user_id,@user_name,@CPU,@RAM,@storage,@traffic,@os_type,@server_type,@backup_type,@user_panel,@server_charge,@server_password,@server_host_name,@server_ip_address,@payment_type)", con);
                cmd.Parameters.AddWithValue("user_name", Form5.user_name_admin);
                cmd.Parameters.AddWithValue("user_id", user_id);
                cmd.Parameters.AddWithValue("CPU", Form2.cpu);
                cmd.Parameters.AddWithValue("RAM", Form2.ram);
                cmd.Parameters.AddWithValue("storage", Form2.storage);
                cmd.Parameters.AddWithValue("traffic", Form2.traffic);
                cmd.Parameters.AddWithValue("os_type", Form2.os_type);
                cmd.Parameters.AddWithValue("server_type", Form2.server_type);
                cmd.Parameters.AddWithValue("backup_type", Form2.backup);
                cmd.Parameters.AddWithValue("user_panel", Form2.backup);


                cmd.Parameters.AddWithValue("server_charge", server_charge);
                cmd.Parameters.AddWithValue("payment_type", payment_type);
                cmd.Parameters.AddWithValue("server_password", server_password);
                cmd.Parameters.AddWithValue("server_host_name", server_host_name);
                cmd.Parameters.AddWithValue("server_ip_address", server_ip_address);

                SqlCommand cmd3 = new SqlCommand("update login_table set has_products=@has_products where login_username='" + Form5.user_name_admin + "'", con);
                cmd3.Parameters.AddWithValue("@has_products", 1);



                cmd.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();

                con.Close();
                Form4 Form4 = new Form4();
                this.Hide();
                Form4.Show();

            }
            
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            server_charge = Server_charge_calculate(new_price);
            label21.Visible = false;
            label22.Visible = false;
            label20.Text = ("'"+new_price.ToString())+"' TL";
            if (has_ref == 1)
            {
                label23.Visible = true;
                label24.Visible = true;
                label24.Text = ("'" + server_charge.ToString() + "TL");
            }
            payment_type = radioButton1.Text;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            server_charge = Server_charge_calculate(new_price);
            label20.Text = ("'"+(new_price * 12).ToString()+"' TL");
            label22.Text = ("'"+ (new_price * 12) * 90 / 100).ToString() +"' TL";
            label21.Visible = true;
            label22.Visible = true;
            if (has_ref == 1)
                {
                    label23.Visible = true;
                    label24.Visible = true;
                    
                    label24.Text = ("'"+ server_charge.ToString() + "' TL" );
                }
            payment_type = radioButton2.Text;

        }

        private void label21_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Form5.is_admin == 0)
            {
                server_password = password_generator();
                server_host_name = ("'server." + Form1.user_name + ".com'");
                server_ip_address = ip_address_generator();
                server_charge = Server_charge_calculate(new_price);
                con.Open();

                SqlCommand cmd3 = new SqlCommand("update choosen_products_table set CPU=@CPU,RAM=@RAM,storage=@storage,traffic=@traffic,os_type=@os_type,server_type=@server_type,backup_type=@backup_type,user_panel=@user_panel,server_charge=@server_charge,payment_type=@payment_type,server_password=@server_password,server_host_name=@server_host_name,server_ip_address=@server_ip_address where user_name='" + Form1.user_name + "'", con);
                cmd3.Parameters.AddWithValue("CPU", Form2.cpu);
                cmd3.Parameters.AddWithValue("RAM", Form2.ram);
                cmd3.Parameters.AddWithValue("storage", Form2.storage);
                cmd3.Parameters.AddWithValue("traffic", Form2.traffic);
                cmd3.Parameters.AddWithValue("os_type", Form2.os_type);
                cmd3.Parameters.AddWithValue("server_type", Form2.server_type);
                cmd3.Parameters.AddWithValue("backup_type", Form2.backup);
                cmd3.Parameters.AddWithValue("user_panel", Form2.backup);


                cmd3.Parameters.AddWithValue("server_charge", server_charge);
                cmd3.Parameters.AddWithValue("payment_type", payment_type);
                cmd3.Parameters.AddWithValue("server_password", server_password);
                cmd3.Parameters.AddWithValue("server_host_name", server_host_name);
                cmd3.Parameters.AddWithValue("server_ip_address", server_ip_address);
                cmd3.ExecuteNonQuery();

                con.Close();
                Form4 Form4 = new Form4();
                this.Hide();
                Form4.Show();


            }
            else if (Form5.is_admin == 1)
            {
                server_password = password_generator();
                server_host_name = ("'server." + Form5.user_name_admin + ".com'");
                server_ip_address = ip_address_generator();
                server_charge = Server_charge_calculate(new_price);
                con.Open();

                SqlCommand cmd3 = new SqlCommand("update choosen_products_table set CPU=@CPU,RAM=@RAM,storage=@storage,traffic=@traffic,os_type=@os_type,server_type=@server_type,backup_type=@backup_type,user_panel=@user_panel,server_charge=@server_charge,payment_type=@payment_type,server_password=@server_password,server_host_name=@server_host_name,server_ip_address=@server_ip_address where user_name='" + Form5.user_name_admin + "'", con);
                cmd3.Parameters.AddWithValue("CPU", Form2.cpu);
                cmd3.Parameters.AddWithValue("RAM", Form2.ram);
                cmd3.Parameters.AddWithValue("storage", Form2.storage);
                cmd3.Parameters.AddWithValue("traffic", Form2.traffic);
                cmd3.Parameters.AddWithValue("os_type", Form2.os_type);
                cmd3.Parameters.AddWithValue("server_type", Form2.server_type);
                cmd3.Parameters.AddWithValue("backup_type", Form2.backup);
                cmd3.Parameters.AddWithValue("user_panel", Form2.backup);


                cmd3.Parameters.AddWithValue("server_charge", server_charge);
                cmd3.Parameters.AddWithValue("payment_type", payment_type);
                cmd3.Parameters.AddWithValue("server_password", server_password);
                cmd3.Parameters.AddWithValue("server_host_name", server_host_name);
                cmd3.Parameters.AddWithValue("server_ip_address", server_ip_address);
                cmd3.ExecuteNonQuery();

                con.Close();
                Form4 Form4 = new Form4();
                this.Hide();
                Form4.Show();

            }




        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2();
            this.Hide();
            Form2.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}
