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
using System.Windows.Shapes;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Vtoraya
{
    /// <summary>
    /// Interaction logic for Red.xaml
    /// </summary>
    public partial class Red : Window
    {
        public int num;
        public string im;
        public Red(string id)
        {
            InitializeComponent();
            num = Int32.Parse(id);
            LoadOut();

        }
        public void LoadOut()
        {
            string connect = "Server = vc-stud-mssql1,1433; Initial Catalog = user89_db; User id = user89_db; Password = user89; MultipleActiveResultSets = True; App = EntityFrameword; Connection Timeout = 2; ";
            string command = "Select ID, Title, Cost, Description, MainImagePath, IsActive, ManufacturerID From Product where ID ='" + num + "'";
            SqlConnection myConnection = new SqlConnection(@connect);
            SqlCommand Mycommand = new SqlCommand(command, myConnection);
            myConnection.Open();
            SqlDataReader rd = Mycommand.ExecuteReader();

            while (rd.Read())
            {
                ID_textbox.Text = rd[0].ToString();
                Name_textbox.Text = rd.GetString(1);
                cost_textbox.Text = rd.GetString(2);
                Des_TextBox.Text = rd.GetString(3);
                if (rd.GetString(5) == "1")
                {
                    nalich_textbox.Text = "есть в наличии";
                }
                else
                {
                    nalich_textbox.Text = "нет в наличии";
                }
                Manuf_Textbox.Text = rd.GetString(6);
                string img1 = rd[4].ToString();
                string img2 = img1.Trim();
                string pho = "F:\\Подготовка к ДЕМО-экзамену\\Sessia_2\\Vtoraya\\Vtoraya\\" + img2;
                Uri imgUri = new Uri(pho);
                ImageSource i = new BitmapImage(imgUri);
                Image_photo.Source = i;
            }
            myConnection.Close();

        }
    }
}
