using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AYAN_TELECOM_WINAPP.User_Defined_Classes;

namespace AYAN_TELECOM_WINAPP
{
    public partial class SIGNUP_USER_FORM : Form
    {
        Image NewImage;
        byte[] ImageByteArry;
        public SIGNUP_USER_FORM()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;

            //START:- VALIDATION OF ITEMS(UI)
            if (Txt_Name.Text == "")
            {
                i = 1;
                MessageBox.Show("PLEASE ENTER YOUR NAME");
            }

            else if (TXT_Phone.Text == "")
            {
                i = 1;
                MessageBox.Show("PLEASE ENTER YOUR PHONE NUMBER");
            }

            else if (!System.Text.RegularExpressions.Regex.IsMatch(TXT_Phone.Text,"^[0-9]*$"))
            {
                i = 1;
                MessageBox.Show("PLEASE ENTER ONLY DIGITS FOR PHONE NUMBER, ALPHABETS ARE NOT ACCEPTED");
            }

            else if (TXT_EmailID.Text == "")
            {
                i=1;
                MessageBox.Show("PLEASE ENTER YOUR EMAIL ID, FROM WHICH YOU WILL NEED TO LOGIN");
            }

            else if (!System.Text.RegularExpressions.Regex.IsMatch(TXT_EmailID.Text, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$"))
            {
                i = 1;
                MessageBox.Show("PLEASE ENTER A VALID EMAIL ID, (eg.'id' followed by @'domainname'.type");
            }

            else if (TXT_Passwd.Text == "")
            {
                i = 1;
                MessageBox.Show("PLEASE ENTER A PASSWORD");
            }

            else if (TXT_Address1.Text == "")
            {
                i = 1;
                MessageBox.Show("PLEASE ENTER YOUR PERMANENT ADDRESS");
            }

            else if (TXT_City.Text == "")
            {
                i = 1;
                MessageBox.Show("PLEASE ENTER THE CITY NAME");
            }

            else if (TXT_Pin.Text == "")
            {
                i = 1;
                MessageBox.Show("PLEASE ENTER THE PIN");
            }

            else if (TXT_State.Text == "")
            {
                MessageBox.Show("PLEASE ENTER YOUR STATE");
            }
            //END:- VALIDATION OF ITEMS(UI)

            if (i == 0)
            {
                //INSERT INTO DB NEW REGISTRATION
                BUSINESS_Layer business = new BUSINESS_Layer();
                Properties_Class propind = new Properties_Class();

                //DECLARE VARIABLES FOR STORING VALUES
                string name = String.Empty;
                string phonenumber = String.Empty;
                string EmailId = String.Empty;
                string passwd = String.Empty;
                string address = String.Empty;
                string address2 = String.Empty;
                string pin = String.Empty;
                string city = String.Empty;
                string state = String.Empty;
                string message = String.Empty;
                ;

                name = Txt_Name.Text;
                phonenumber = TXT_Phone.Text;
                EmailId = TXT_EmailID.Text;
                passwd = TXT_Passwd.Text;
                address = TXT_Address1.Text;
                address2 = TXT_Address2.Text;
                pin = TXT_Pin.Text;
                city = TXT_City.Text;
                state = TXT_State.Text;

                NewImage = pictureBox1.Image;
                ImageConverter converter = new ImageConverter();
                ImageByteArry = (byte[])converter.ConvertTo(NewImage, typeof(byte[]));

                message = business.InsertNewReg(name, phonenumber, EmailId, passwd, address, address2, pin, city, state, ImageByteArry);
                if (propind.Is_ExceptionIND == true)
                {
                    MessageBox.Show(message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string[] indicator = message.Split(',');

                    MessageBox.Show(indicator[0].ToString());

                    if (String.Compare(indicator[1], "1") == 0)
                    {
                        LOGIN login = new LOGIN();
                        login.Show();
                        this.Hide();
                    }
                }
            }

        }

        private void label10_Click(object sender, EventArgs e)
        {
            LOGIN LG = new LOGIN();
            LG.Show();
            this.Hide();

        }

        private void btn_UploadPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = @"C:\";
            file.RestoreDirectory = true;
            file.Title = "BROWSE USER PICTURE";
            file.DefaultExt = ".png";
            file.Filter = "JPG (*.jpg)|*.jpg|JPEG (*.jpeg)|*.jpeg|PNG (*.png)|*.png|GIF (*.gif)|*.gif";

            if (file.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(file.FileName.ToString());
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                NewImage = pictureBox1.Image;
                ImageConverter converter = new ImageConverter();
                ImageByteArry = (byte[])converter.ConvertTo(NewImage, typeof(byte[]));
            }
            
            btn_UploadPic.Text = "Change Image";
        }
    }
}
