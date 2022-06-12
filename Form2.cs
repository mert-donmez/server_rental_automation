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
using System.Collections;

namespace server_kiralama
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=server_kiralama;Integrated Security=True");
        
        public static string os_type;
        public static int os_check = 0;
        public static string server_type;
        public static int server_check = 0;
        public static string backup;
        public static int backup_check = 0;
        public static string user_panel;
        public static int user_check = 0;
        public static string cpu;
        public static string ram;
        public static string storage;
        public static string traffic;
        public static int price;
        public static int get_price;
        public static int price_getting;

        

        ArrayList products_choosen = new ArrayList();
        ArrayList choosen = new ArrayList();

        


        




        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            user_panel = plesk_radio.Text;
            user_check = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cpu = cpu_list.SelectedItems[0].Text;
            ram = ram_list.SelectedItems[0].Text;
            storage = storage_list.SelectedItems[0].Text;
            traffic = traffic_list.SelectedItems[0].Text;

            products_choosen.Add(cpu);
            products_choosen.Add(ram);
            products_choosen.Add(storage);
            products_choosen.Add(traffic);
            products_choosen.Add(os_type);
            products_choosen.Add(server_type);
            products_choosen.Add(backup);
            products_choosen.Add(user_panel);

            for(int i = 0; i < 8; i++)
            {
                
                string value = products_choosen[i].ToString();
                price_getting += calculate_price(value);
                
            }
            price = price_getting;
            price_getting = 0;
            

            
            
            

            Form3 Form3 = new Form3();
            this.Hide();
            Form3.Show();
        }

        private int calculate_price(string value) //for döngüsü kullanılarak seçilen ürünlerin fiyatları veri tabanından çekiliyor
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from price_table where product_detail_name='" + value + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                get_price = Convert.ToInt32(dr["product_detail_price"]);
                
                


            }
            con.Close();
            return get_price;
            
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linux_radio_CheckedChanged(object sender, EventArgs e)
        {
            os_type = linux_radio.Text;
            os_check = 1;

        }

        private void windows_radio_CheckedChanged(object sender, EventArgs e)
        {
            os_type = windows_radio.Text;
            os_check = 1;
        }

        private void vps_radio_CheckedChanged(object sender, EventArgs e)
        {
            server_type = vps_radio.Text;
            server_check = 1;
        }

        private void vds_radio_CheckedChanged(object sender, EventArgs e)
        {
            server_type = vds_radio.Text;
            server_check = 1;
        }

        private void otomatik_radio_CheckedChanged(object sender, EventArgs e)
        {
            backup = otomatik_radio.Text;
            backup_check = 1;
        }

        private void manuel_radio_CheckedChanged(object sender, EventArgs e)
        {
            backup = manuel_radio.Text;
            backup_check = 1;
        }

        private void istemiyorum_radio_CheckedChanged(object sender, EventArgs e)
        {
            user_panel = istemiyorum_radio.Text;
            user_check = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            cpu = cpu_list.SelectedItems[0].Text;
            ram = ram_list.SelectedItems[0].Text;
            storage = storage_list.SelectedItems[0].Text;
            traffic = traffic_list.SelectedItems[0].Text;

            products_choosen.Add(cpu);
            products_choosen.Add(ram);
            products_choosen.Add(storage);
            products_choosen.Add(traffic);
            products_choosen.Add(os_type);
            products_choosen.Add(server_type);
            products_choosen.Add(backup);
            products_choosen.Add(user_panel);

            for (int i = 0; i < 8; i++)
            {

                string value = products_choosen[i].ToString();
                price += calculate_price(value);
                MessageBox.Show(price.ToString());

            }



            Form3 Form3 = new Form3();
            this.Hide();
            Form3.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void ram_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void storage_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void traffic_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
