using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Vtoraya
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadOut();
        }
        private void LoadOut() {
            string connect = "Server = vc-stud-mssql1,1433; Initial Catalog = user89_db; User id = user89_db; Password = user89; MultipleActiveResultSets = True; App = EntityFrameword; Connection Timeout = 2; ";
            SqlConnection myConnection = new SqlConnection(@connect);
            myConnection.Open();
            string a = "SELECT ID, Name FROM Manufacturer";
            string b = "Select ID, Title, Cost, Description, MainImagePath IsActive, ManufacturerID From Product";
            SqlCommand Mycommand = new SqlCommand(b, myConnection);
            Mycommand.ExecuteNonQuery();
            SqlDataAdapter dapter = new SqlDataAdapter(Mycommand);
            DataTable Table = new DataTable("Books");
            dapter.Fill(Table);
            DaGr_users_viev.ItemsSource = Table.DefaultView;
            SqlCommand cmd = new SqlCommand(a, myConnection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt1 = new DataTable();
            dataAdapter.Fill(dt1);
            ComboBox1.ItemsSource = dt1.DefaultView;
            ComboBox1.DisplayMemberPath = "Name";
            cmd.Dispose();
            myConnection.Close();

        }
                private void Evolve1(string a)
        {
            string connect = "Server = vc-stud-mssql1,1433; Initial Catalog = user89_db; User id = user89_db; Password = user89; MultipleActiveResultSets = True; App = EntityFrameword; Connection Timeout = 2; ";
            SqlConnection myConnection = new SqlConnection(@connect);
            SqlCommand Mycommand = new SqlCommand(a, myConnection);
            myConnection.Open();
            Mycommand.ExecuteNonQuery();
            SqlDataAdapter dapter = new SqlDataAdapter(Mycommand);
            DataTable Table = new DataTable("Books");
            dapter.Fill(Table);
            DaGr_users_viev.ItemsSource = Table.DefaultView;
            myConnection.Close();
        }


        private void ComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            string res = ComboBox1.Text;
            string command = "Select ID, Title, Cost, Description, MainImagePath IsActive, ManufacturerID From Product where (ManufacturerID like '%" + res + "%')";
            Evolve1(command);
        }

        private void TB_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string ser = TB_Search.Text;
            string res = ComboBox1.Text;
            if (ComboBox1.Text == "") {
                res = " ";
            }
            else {
                res = "(ManufacturerID like '%" + res + "%') and";
            }
            string command = "Select ID, Title, Cost, Description, MainImagePath IsActive, ManufacturerID From Product where "+res+" ((Title like '%"+ser+ "%') or (Description like '%" + ser + "%'))";
            Evolve1(command);
        }

        private void newItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DaGr_users_viev_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView rowView = DaGr_users_viev.SelectedValue as DataRowView;
            string id = rowView[0].ToString();
            Red red = new Red(id);
            red.Show();
        }
    }
}
