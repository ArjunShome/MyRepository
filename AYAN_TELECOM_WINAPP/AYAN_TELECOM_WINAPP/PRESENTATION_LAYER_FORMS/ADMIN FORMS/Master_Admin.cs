using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AYAN_TELECOM_WINAPP.PRESENTATION_LAYER_FORMS.ADMIN_FORMS
{
    public partial class Master_Admin : Form
    {
        int click = 0; //Variable to Store Dropdown image counts
        string name = String.Empty;
        string email = String.Empty;
        byte[] uimg;
        AdminDropDown DropDown;

        public Master_Admin( string username,string id, byte[] img)
        {
            InitializeComponent();
            
            //POPULATE ADMIN NAME
            name = username;
            lblAdminName.Text = name;
            email = id;
            uimg = img;

            Image newimg;
            MemoryStream ms = new MemoryStream(uimg);
            ms.Seek(0, SeekOrigin.Begin);
            ms.Write(uimg, 0, uimg.Length);
            newimg = Image.FromStream(ms);

            pictureBox2.Image = newimg;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

            //CHANGE THE PICTUREBOX FOR ADMIN PICTURE SHAPE TO ELLIPSE
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox1.Width, pictureBox1.Height);
            Region rg = new Region(gp);
            pictureBox1.Region = rg;

            //CHANGE HE PICTUREBOX FOR ADMIN USER CONTRL DROPDOWN SHAPE TO ELLIPSE
            System.Drawing.Drawing2D.GraphicsPath gp1 = new System.Drawing.Drawing2D.GraphicsPath();
            gp1.AddEllipse(0, 0, pictureBox2.Width, pictureBox2.Height);
            Region rg1 = new Region(gp1);
            pictureBox2.Region = rg1;

            //CHANGE RECHARGE BUTTON BACKGROUND COLOR
            btn_VwRechargeReq.BackColor = Color.SkyBlue;
        }

        public void drpDwnClick()
        {
            //CREATE AN OBJECT OF THE DROPDOWN CLASS
            DropDown = new AdminDropDown(email);

            if (DropDown.Visible)
            {
                DropDown.Hide();
            }
            else if (DropDown.Visible==false && click>0)
            {
                DropDown.Size = new System.Drawing.Size(10, 100);
                DropDown.Location = new Point(106, 309);
                DropDown.Show();
            }
            else
            {
                DropDown.Size = new System.Drawing.Size(10, 100);
                DropDown.Location = new Point(106, 309);
               // DropDown.TopMost = true;
                DropDown.Show();
                click++;
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.SandyBrown;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            drpDwnClick();
        }

        private void btn_VwRechargeReq_Click(object sender, EventArgs e)
        {
            ViewRechargeRequests Requests = new ViewRechargeRequests();
            Requests.TopLevel = false;
            panel3.Controls.Add(Requests);
            Requests.Show();
        }

    }
}
