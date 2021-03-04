using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data.Common;
namespace ZarplataLab
{


    public partial class Form1 : Form
    {
        //**************************************************************
        struct str
        {
            public string id;
            public string name;
            public string surname;
            public string otch;
            public string status;
            public string money;
        }

        MySqlConnection connections;


        List<str> zarplata(int id)
        {
            MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
            stringBuilder.Server = "localhost";
            stringBuilder.UserID = "root";
            stringBuilder.Database = "zarplatalab";
            stringBuilder.SslMode = MySqlSslMode.None;
            connections = new MySqlConnection(stringBuilder.ConnectionString);

            MySqlCommand command = connections.CreateCommand();
            command.CommandText = "SELECT zarplata.id, `surname`, `name`, `otch`, `status` ,`money` FROM zarplatalab.zarplata JOIN  zarplatalab.info ON zarplata.fio_id = info.id WHERE fio_id = @id";
            command.Parameters.AddWithValue("@id", id);
            List<str> bd = new List<str>();
            try
            {
                connections.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        str databd = new str
                        {
                            id = reader.GetString(0),
                            name = reader.GetString(1),
                            surname = reader.GetString(2),
                            otch = reader.GetString(3),
                            status = reader.GetString(4),
                            money = reader.GetString(5),
                        };
                        bd.Add(databd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bd;
        }
        //**************************************************************
        List<str> information()
        {
            MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
            stringBuilder.Server = "localhost";
            stringBuilder.UserID = "root";
            stringBuilder.Database = "zarplatalab";
            stringBuilder.SslMode = MySqlSslMode.None;
            connections = new MySqlConnection(stringBuilder.ConnectionString);

            MySqlCommand command = connections.CreateCommand();
            command.CommandText = "SELECT id, name, surname, otch FROM zarplatalab.info";

            List<str> bd = new List<str>();

            try
            {
                connections.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        str databd = new str
                        {
                            id = reader.GetString(0),
                            name = reader.GetString(1),
                            surname = reader.GetString(2),
                            otch = reader.GetString(3),
                        };
                        bd.Add(databd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return bd;

        }





        public DataTable getTableInfo(string query)
        {
            MySqlCommand queryExecute = new MySqlCommand(query, connections);
            DataTable ass = new DataTable();
            ass.Load(queryExecute.ExecuteReader());
            zagruzka();
            return ass;
        }
        //**************************************************************
        database db = new database("127.0.0.1", "root", "", "zarplatalab");





        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = db.getTableInfo("SELECT  zarplata.id,  `name` FROM `zarplata` join `info` on `fio_id` = info.id");
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";

            comboBox2.DataSource = db.getTableInfo("SELECT id,  surname FROM info");
            comboBox2.DisplayMember = "surname";
            comboBox2.ValueMember = "id";
        }

        public void zagruzka()
        {

            database db = new database("127.0.0.1", "root", "", "zarplatalab");
            //comboBox1.;
            //comboBox1.Dispose();
            comboBox1.DataSource = null;
            //comboBox1.Items.Clear();
            comboBox1.DataSource = db.getTableInfo("SELECT  zarplata.id,  `name` FROM `zarplata` join `info` on `fio_id` = info.id");
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";

            comboBox2.DataSource = db.getTableInfo("SELECT id,  surname FROM info");
            comboBox2.DisplayMember = "surname";
            comboBox2.ValueMember = "id";
        }
        private void Label1_Click(object sender, EventArgs e)
        {

        }



        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            string name = textBox1.Text;
            string surname = textBox2.Text;
            string otch = textBox3.Text;
            var end = db.download(name, surname, otch);
            if (end != -1)
            {
                label1.Text = String.Format("Успешно. Добавленный id: {0}", end);
                zagruzka();
            }
            else
            {
                label1.Text = ("Ошибка добавления.");
            }
            zagruzka();
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            Form1 pp = new Form1();
            var ll = pp.information();
            textBox6.Text = "";
            foreach (var data in ll)
            {           
                textBox6.Text += (data.id + " | " + data.name.PadRight(15) + " | " + data.surname.PadRight(10) + " | " + data.otch.PadRight(10) + "\r\n");
            }
            pp.zagruzka();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

           
            Form1 form = new Form1();
            string status = textBox4.Text;
            string money = textBox5.Text;
            int fio_id = Convert.ToInt32(comboBox2.SelectedValue);
            // int fio_id = (int)numericUpDown1.Value;
            var end = db.download2(status, money, fio_id);
            if (end != -1)
            {
                textBox6.Text = String.Format("Успешно", end);
                zagruzka();
            }
            else
            {
                textBox6.Text = ("Ошибка добавления.");
            }
            zagruzka();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {          
            Form1 form = new Form1();
            int id = Convert.ToInt32(comboBox2.SelectedValue);
            var ll = form.zarplata(id);
            textBox6.Text = "";
            foreach (var data in ll)
            {
                textBox6.Text += (data.id + " | " + data.name.PadRight(15) + " | " + data.surname.PadRight(10) + " | " + data.otch.PadRight(10) + " | " + data.status.PadRight(10) + " | " + data.money.PadRight(10) + "\r\n");
            }
            zagruzka();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void button5_Click(object sender, EventArgs e)
        {
            database db = new database("127.0.0.1", "root", "", "zarplatalab");
            int id = Convert.ToInt32(comboBox1.SelectedValue);
            if (db.delete(id) == -1)
            {
                MessageBox.Show("error");
                zagruzka();
            }
            else
            {
                MessageBox.Show("ok");
                zagruzka();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }
    }


}