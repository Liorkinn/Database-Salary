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

        List<str> zarplata()
        {
            MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
            stringBuilder.Server = "localhost";
            stringBuilder.UserID = "root";
            stringBuilder.Database = "zarplatalab";
            stringBuilder.SslMode = MySqlSslMode.None;
            connections = new MySqlConnection(stringBuilder.ConnectionString);

            MySqlCommand command = connections.CreateCommand();
            command.CommandText = "SELECT zarplata.id,`name`, `surname`, `otch`,`status`,`money` FROM zarplatalab.zarplata JOIN  zarplatalab.info ON zarplata.fio_id = info.id";

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
            command.CommandText = "SELECT `id`,`name`, `surname`, `otch` FROM zarplatalab.info";

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
        //**************************************************************
        public Form1()
            {
            InitializeComponent();
            }
        database db = new database("127.0.0.1", "root", "", "zarplatalab");
        private void Label1_Click(object sender, EventArgs e)
        {

        }

    
       
        private void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string surname = textBox2.Text;
            string otch = textBox3.Text;
            var end = db.download(name, surname, otch);
            if (end != -1)
            {
                label1.Text = String.Format("Успешно. Добавленный id: {0}", end);
            }
            else
            {
                label1.Text = ("Ошибка добавления.");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form1 pp = new Form1();
            var ll = pp.information();
            label6.Text = "";
            foreach (var data in ll)
            {
                label6.Text += (data.id + " | " + data.name.PadRight(15) + " | " + data.surname.PadRight(10) + " | " + data.otch.PadRight(10) + "\n");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string status = textBox4.Text;
            string money = textBox5.Text;
            int fio_id = (int)numericUpDown1.Value;
            var end = db.download2(status, money, fio_id);
            if (end != -1)
            {
                label6.Text = String.Format("Успешно", end);
            }
            else
            {
                label6.Text = ("Ошибка добавления.");
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            var ll = form.zarplata();
            label6.Text = "";
            foreach (var data in ll)
            {
                label6.Text += (data.id + " | " + data.name.PadRight(15) + " | " + data.surname.PadRight(10) + " | " + data.otch.PadRight(10) + " | " + data.status.PadRight(10) + " | " + data.money.PadRight(10) + "\n");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

       

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int id = (int)numericUpDown2.Value;
            var del = db.delete(id);
            if (del != -1)
            {
                label6.Text = String.Format("Успешно. Запись удалена");
            }
            else
            {
                label6.Text = ("Ошибка удаления.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }


}