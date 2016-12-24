using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AYAN_TELECOM_WINAPP.User_Defined_Classes;
using System.IO;

namespace AYAN_TELECOM_WINAPP.PRESENTATION_LAYER_FORMS.CUSTOMER_FORMS
{
    public partial class Edit_User : Form
    {
        string id = String.Empty;
        Image img;
        byte[] ImageByteArry;
        public Edit_User()
        {
            InitializeComponent();
        }

        public Edit_User(string str)
        {
            InitializeComponent();
            id = str;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void Edit_User_Load(object sender, EventArgs e)
        {
            BUSINESS_Layer business = new BUSINESS_Layer();
            Properties_Class propind = new Properties_Class();


            List<object> str = new List<object>();
            str=business.DispUserDetails(id);

            if (str.Count < 1)
            {
                MessageBox.Show(propind.ExceptionMsg+propind.Is_ExceptionIND, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //Variable Declarations for displaying in respective textboxes/Comboboxes.
                string Name = String.Empty;
                string PhoneNumber = String.Empty;
                string Email = String.Empty;
                string Passwd = String.Empty;
                string Address1 = String.Empty;
                string Address2 = String.Empty;
                string Pin = String.Empty;
                string City = String.Empty;
                string State = String.Empty;
                //Variable Declaration End

                //for (int i = 0; i < str.Count; i++)
                //{
                    Name = str[0].ToString();
                    PhoneNumber = str[1].ToString();
                    Email = str[2].ToString();
                    Passwd = str[3].ToString();
                    Address1 = str[4].ToString();
                    Address2 = str[5].ToString();
                    Pin = str[6].ToString();
                    City = str[7].ToString();
                    State = str[8].ToString();
                    ImageByteArry = (byte[])str[9];
                //}

                // Populate Data into UI
                Txt_Name.Text = Name;
                TXT_Phone.Text = PhoneNumber;
                TXT_EmailID.Text = Email;
                TXT_Passwd.Text = Passwd;
                TXT_Address1.Text = Address1;
                TXT_Address2.Text = Address2;
                TXT_Pin.Text = Pin;
                TXT_City.Text = City;
                TXT_State.SelectedItem = State;

                MemoryStream ms = new MemoryStream(ImageByteArry);
                ms.Seek(0, SeekOrigin.Begin);
                ms.Write(ImageByteArry, 0, ImageByteArry.Length);
                img = Image.FromStream(ms);

                pictureBox1.Image = img;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message;
            
            BUSINESS_Layer business = new BUSINESS_Layer();

            message = business.EditUser(Txt_Name.Text, TXT_Phone.Text, TXT_EmailID.Text, TXT_Passwd.Text, TXT_Address1.Text, TXT_Address2.Text, TXT_Pin.Text, TXT_City.Text, TXT_State.Text, ImageByteArry);
            MessageBox.Show(message);

            //Master_User master = (Master_User)this.Owner;
            //master.uimg = img;
        }

        private void btn_changeImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = @"C:\";
            file.RestoreDirectory = true;
            file.Title = "CHANGE USER PICTURE";
            file.DefaultExt = ".png";
            file.Filter = "JPG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg|PNG (*.png)|*.png|GIF (*.gif)|*.gif";

            if (file.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(file.FileName.ToString());
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                img = pictureBox1.Image;
                ImageConverter converter = new ImageConverter();
                ImageByteArry = (byte[])converter.ConvertTo(img, typeof(byte[]));
            }
        }

    }
}
