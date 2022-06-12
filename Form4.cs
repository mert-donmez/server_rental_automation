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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=server_kiralama;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            
            Form2 Form2 = new Form2();
            this.Hide();
            Form2.Show();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            con.Open();
            if (Form5.is_admin == 0)
            {
                SqlCommand cmd = new SqlCommand("select *from choosen_products_table where user_name='" + Form1.user_name + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    label9.Text = dr["server_host_name"].ToString();
                    label8.Text = dr["server_ip_address"].ToString();
                    label6.Text = dr["server_password"].ToString();
                    cpu_label.Text = dr["CPU"].ToString();
                    ram_label.Text = dr["RAM"].ToString();
                    storage_label.Text = dr["storage"].ToString();
                    os_label.Text = dr["os_type"].ToString();
                    server_type_label.Text = dr["server_type"].ToString();
                    server_charge_label.Text = ("'" + dr["server_charge"].ToString() + "' TL");

                }
                dr.Close();

                SqlCommand cmd2 = new SqlCommand("select *from login_table where login_username='" + Form1.user_name + "'", con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    username_label.Text = dr2["login_username"].ToString();
                    user_id_label.Text = dr2["user_id"].ToString();
                    ref_code_label.Text = dr2["ref_code"].ToString();
                }
                dr.Close();
                con.Close();

            }
            else if (Form5.is_admin == 1)
            {
                SqlCommand cmd = new SqlCommand("select *from choosen_products_table where user_name='" + Form5.user_name_admin + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    label9.Text = dr["server_host_name"].ToString();
                    label8.Text = dr["server_ip_address"].ToString();
                    label6.Text = dr["server_password"].ToString();
                    cpu_label.Text = dr["CPU"].ToString();
                    ram_label.Text = dr["RAM"].ToString();
                    storage_label.Text = dr["storage"].ToString();
                    os_label.Text = dr["os_type"].ToString();
                    server_type_label.Text = dr["server_type"].ToString();
                    server_charge_label.Text = ("'"+dr["server_charge"].ToString()+"' TL");

                }
                dr.Close();

                SqlCommand cmd2 = new SqlCommand("select *from login_table where login_username='" + Form5.user_name_admin + "'", con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    username_label.Text = dr2["login_username"].ToString();
                    user_id_label.Text = dr2["user_id"].ToString();
                    ref_code_label.Text = dr2["ref_code"].ToString();
                }
                dr.Close();
                con.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
