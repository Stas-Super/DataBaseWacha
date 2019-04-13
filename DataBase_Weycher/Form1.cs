using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase_Weycher
{
	public partial class Form1 : Form
	{
		SqlConnection connection;
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string connectionString = $"Initial Catalog={textBox1.Text};Integrated Security=True";
			connection = new SqlConnection(connectionString);
			connection.Open();
			comboBox1.Items.Clear();

			string query = "select * from sys.Tables;";
			SqlCommand cmd = new SqlCommand(query, connection);
			SqlDataReader dr = cmd.ExecuteReader();
			while (dr.Read())
				comboBox1.Items.Add(dr[0]);
			dr.Close();
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			string table = comboBox1.SelectedItem.ToString();
			string query = $"select * from {table};";
			SqlCommand command = new SqlCommand(query, connection);

			DataTable dataTable = new DataTable();
			SqlDataReader reader = command.ExecuteReader();

			dataTable.Load(reader);

			dataGridView1.DataSource = dataTable.DefaultView;

			reader.Close();
		}
	}
}
