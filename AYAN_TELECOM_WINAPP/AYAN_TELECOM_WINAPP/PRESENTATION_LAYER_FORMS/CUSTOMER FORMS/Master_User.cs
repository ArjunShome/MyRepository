using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AYAN_TELECOM_WINAPP.PRESENTATION_LAYER_FORMS.CUSTOMER_FORMS;
using AYAN_TELECOM_WINAPP.PRESENTATION_LAYER_FORMS;
using System.IO;

namespace AYAN_TELECOM_WINAPP
{
    public partial class Master_User : Form
    {
        string User = String.Empty;
        string mailid = String.Empty;
        byte[] userimg;
        public Image uimg { get {return pictureBox1.Image;} set {pictureBox1.Image=value;} }
        //object userimg;

        //Variable to Switch Forms
        int on = 0;
        public Master_User()
        {
            InitializeComponent();
        }

        public Master_User(string username,string id, byte[] img)
        {
            InitializeComponent();
            User = username;
            mailid = id;
            userimg = img;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (on > 0)
            {
                panel3.Controls.Clear(); //Clear all forms in Panel to display the new form

                User_Recharge_Request request = new User_Recharge_Request(mailid);
                request.TopLevel = false;
                panel3.Controls.Add(request);
                request.Show();
                on++;
            }
            else 
            {
                User_Recharge_Request request = new User_Recharge_Request(mailid);
                request.TopLevel = false;
                panel3.Controls.Add(request);
                request.Show();
                on++;
            }
        }

        private void Master_User_Load(object sender, EventArgs e)
        {
            label_name.Text = "Welcome " + User;

            MemoryStream ms = new MemoryStream(userimg);
            ms.Seek(0, SeekOrigin.Begin);
            ms.Write(userimg, 0, userimg.Length);
            uimg = Image.FromStream(ms);

            pictureBox1.Image = uimg;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void lbl_signout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LOGIN login = new LOGIN();
            login.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (on > 0)
            {
                panel3.Controls.Clear();
                Edit_User usr = new Edit_User(mailid);
                usr.TopLevel = false;
                panel3.Controls.Add(usr);
                usr.Show();
                on++;
            }
            else
            {
                Edit_User usr = new Edit_User(mailid);
                usr.TopLevel = false;
                panel3.Controls.Add(usr);
                usr.Show();
                on++;
            }
        }

                private void panel3_Paint(object sender, PaintEventArgs e)
                {
                    
                }   
    }
}
