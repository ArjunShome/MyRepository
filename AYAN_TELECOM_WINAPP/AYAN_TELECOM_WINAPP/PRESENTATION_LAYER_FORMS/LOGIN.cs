using AYAN_TELECOM_WINAPP.PRESENTATION_LAYER_FORMS.ADMIN_FORMS;
using AYAN_TELECOM_WINAPP.PRESENTATION_LAYER_FORMS.CUSTOMER_FORMS;
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
using AYAN_TELECOM_WINAPP.PRESENTATION_LAYER_FORMS;

namespace AYAN_TELECOM_WINAPP
{
    public partial class LOGIN : Form
    {
        int i = 1; //Variable to check count of login attempts
        public LOGIN()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //START VALIDATION START
            if (Txt_EmailID.Text != "admin" && !System.Text.RegularExpressions.Regex.IsMatch(Txt_EmailID.Text, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$"))
            {
                MessageBox.Show("PLEASE ENTER A VALID EMAIL ID, (eg.'id' followed by @'domainname'.type");
            }
            
            else if (Txt_EmailID.Text == "")
            {
                MessageBox.Show("PLEASE ENTER THE USER EMAIL");
            }

            else if (TXT_Password.Text == "")
            {
                MessageBox.Show("PLEASE ENTER THE PASSWORD");
            }
            //VALIDATION END

            else
            {
                string name = String.Empty;
                string identifier = String.Empty;
                byte[] img;
                
                i++;
                BUSINESS_Layer business = new BUSINESS_Layer();
                Properties_Class propind = new Properties_Class();

                //GETTING FRONT END VALUES
                List<object> message = new List<object>();
                string id = Txt_EmailID.Text;
                string passwd = TXT_Password.Text;

                if (i > 1)
                {
                    label_forgotpasswd.Enabled = true;
                    label_forgotpasswd.Visible = true;
                }

                //Setting admin as 0
                int admin = 0;
                if (chkAdmin.Checked)
                {
                    admin = 1;  
                }

                message = business.ValidateLogin(id, passwd, admin);

                if (propind.Is_ExceptionIND == true)
                {
                    MessageBox.Show(message[0].ToString(), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (message.Count < 2)
                {
                    string errMsg = String.Empty;
                    errMsg = message[0].ToString();
                    MessageBox.Show(errMsg);
                }

                else
                {
                    identifier = message[0].ToString();
                    name = message[1].ToString();
                    img = (byte[])message[2];

                    //OPEN USER OR ADMIN MASTER FORMS.
                    if (String.Compare(identifier, "0") == 0)
                    {
                        //Open User Master Page
                        Master_User usermaster = new Master_User(name, id, img);
                        usermaster.Show();
                        this.Hide();
                    }
                    else
                    {
                        //Open Admin Master Page
                        Master_Admin adminmaster = new Master_Admin(name, id, img);
                        //Master_Admin adminmaster = new Master_Admin();
                        adminmaster.Show();
                        this.Hide();
                    }
                }
            }
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(50, Color.Bisque);

            Txt_EmailID.TextAlign = HorizontalAlignment.Center;
            TXT_Password.TextAlign = HorizontalAlignment.Center;
            Txt_EmailID.Text = "EMAIL ID";
            TXT_Password.Text = "PASSWORD";

            label_forgotpasswd.Enabled = false;
            label_forgotpasswd.Visible = false;
        }

        //OPEN SIGNUP REGISTRATION FORM FOR NEW REGISTRY
        private void label2_Click(object sender, EventArgs e)
        {
            SIGNUP_USER_FORM signupform = new SIGNUP_USER_FORM();
            signupform.Show();
            this.Hide();
        }


        //EXIT APPLICATION WHENCLICKED ON CROSS (LABEL)
        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label_forgotpasswd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FORGOT_PASSWORD forgot = new FORGOT_PASSWORD();
            forgot.Show();
            this.Hide();
        }

        private void Txt_EmailID_Click(object sender, EventArgs e)
        {
            Txt_EmailID.Text = "";
            if (TXT_Password.Text == "")
            {
                TXT_Password.Text = "PASSWORD";
            }
        }

        private void TXT_Password_Click(object sender, EventArgs e)
        {
            TXT_Password.Text = "";
            if (Txt_EmailID.Text == "")
            {
                Txt_EmailID.Text = "EMAIL ID";
            }
        }
    }
}
