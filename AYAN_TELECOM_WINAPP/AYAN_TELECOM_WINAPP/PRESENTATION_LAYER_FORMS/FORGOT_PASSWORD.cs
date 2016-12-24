using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AYAN_TELECOM_WINAPP.PRESENTATION_LAYER_FORMS
{
    public partial class FORGOT_PASSWORD : Form
    {
        public FORGOT_PASSWORD()
        {
            InitializeComponent();
        }

        //Count to Check for the number of times next is pressed.
        int count = 0;
        string userid = String.Empty;
        Label lbl_id = new Label();
        Label lbl_passwd = new Label();
        TextBox txt_passwd = new TextBox();
        Label lbl_confirmpasswd = new Label();
        TextBox txt_passwdconfirm = new TextBox();

        Forgot_Password_verify_ID ID = new Forgot_Password_verify_ID();
        private void FORGOT_PASSWORD_Load(object sender, EventArgs e)
        {
            //Display ID Verification form in the panel by default 
            ID.TopLevel = false;
            panel_forgotpassword.Controls.Add(ID);
            ID.Show();

            //Make the Finish button Non-Visible
            btn_finish.Visible = false;
            btn_back.Visible = false;
        }

        private void btn_next_Click(object sender, EventArgs e)
        {

            //Clicked first time
            if (count < 1)
            {
                //Forgot_Password_verify_ID idverify = new Forgot_Password_verify_ID();
                System.Windows.Forms.Form form = System.Windows.Forms.Application.OpenForms["Forgot_Password_verify_ID"];
                string id = String.Empty;
                id = ((Forgot_Password_verify_ID)form).TextBx1.Text;
                userid = id;

                //Display back button
                btn_back.Visible = true;

                //Call function from business layer to verify the id and return message
                BUSINESS_Layer business = new BUSINESS_Layer();
                string returnval = String.Empty;
                returnval = business.verifyID(id);
                if (String.Compare(returnval, "1") == 0)
                {
                    //if 1 then continue to the next page
                    count++; //Increment count 
                    Forgot_Password_verify_Mobile mob = new Forgot_Password_verify_Mobile();
                    mob.TopLevel = false;
                    panel_forgotpassword.Controls.Remove(ID);
                    panel_forgotpassword.Controls.Add(mob);
                    mob.Show();
                }
                else
                {
                    MessageBox.Show(returnval);
                }
            }
            
            //Clicked Second Time
            else if (count == 1)
            {
                System.Windows.Forms.Form form = System.Windows.Forms.Application.OpenForms["Forgot_Password_verify_Mobile"];
                string mobilenum = String.Empty;
                mobilenum = ((Forgot_Password_verify_Mobile)form).TextBox.Text;
                BUSINESS_Layer business = new BUSINESS_Layer();
                count++;

                string returnval = String.Empty;
                returnval = business.verifymob(userid, mobilenum);
                if (String.Compare(returnval, "1") == 0)
                {
                    //Set new Location for exixting textbox
                    ((Forgot_Password_verify_Mobile)form).TextBox.Location = new Point(480, 60);
                    ((Forgot_Password_verify_Mobile)form).Label.Location = new Point(159, 63);

                    //Create a new Label to Print Enter Password
                    lbl_id.Name = "lbl_id";
                    lbl_id.Location = new Point(370, 20);
                    lbl_id.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_id.Size = new System.Drawing.Size(360, 28);
                    lbl_id.Enabled = true;
                    lbl_id.Visible = true;
                    lbl_id.Text = userid;
                    form.Controls.Add(lbl_id);

                    //Create a new Label to Print Enter Password
                    lbl_passwd.Name = "lbl_passwd";
                    lbl_passwd.Location = new Point(240, 110);
                    lbl_passwd.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_passwd.Size = new System.Drawing.Size(230, 28);
                    lbl_passwd.Enabled = true;
                    lbl_passwd.Visible = true;
                    lbl_passwd.Text = "Please Enter The New Password:- ";
                    form.Controls.Add(lbl_passwd);

                    //Create a new Textbox to get new password from user.
                    txt_passwd.Name = "txt_passwd";
                    txt_passwd.Location = new Point(480, 110);
                    txt_passwd.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    txt_passwd.Size = new System.Drawing.Size(227, 28);
                    txt_passwd.PasswordChar = '*';
                    txt_passwd.Enabled = true;
                    txt_passwd.Visible = true;
                    form.Controls.Add(txt_passwd);

                    //Create a new Label to Print Confirm Password
                    lbl_confirmpasswd.Name = "lbl_passwd";
                    lbl_confirmpasswd.Location = new Point(323, 160);
                    lbl_confirmpasswd.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lbl_confirmpasswd.Size = new System.Drawing.Size(150, 28);
                    lbl_confirmpasswd.Enabled = true;
                    lbl_confirmpasswd.Visible = true;
                    lbl_confirmpasswd.Text = "Confirm Password :- ";
                    form.Controls.Add(lbl_confirmpasswd);

                    //Create new TextBox to confirm the user password 
                    txt_passwdconfirm.Name = "txt_passwdconfirm";
                    txt_passwdconfirm.Location = new Point(480, 160);
                    txt_passwdconfirm.Font = new System.Drawing.Font("Palatino Linotype", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    txt_passwdconfirm.Size = new System.Drawing.Size(227, 28);
                    txt_passwdconfirm.PasswordChar = '*';
                    txt_passwdconfirm.Enabled = true;
                    txt_passwdconfirm.Visible = true;
                    form.Controls.Add(txt_passwdconfirm);

                }
                else
                {
                    MessageBox.Show(returnval);
                }
            }

            else
            {
                string passwd = String.Empty;
                string confirmpasswd = String.Empty;

                passwd = txt_passwd.Text;
                confirmpasswd = txt_passwdconfirm.Text;

                if (String.Compare(passwd, confirmpasswd) == 0)
                {
                    //MessageBox.Show("Update IN DB");
                    BUSINESS_Layer business = new BUSINESS_Layer();
                    string message = business.ResetPasswd(passwd, userid);
                    MessageBox.Show(message);
                }
                else
                {
                    MessageBox.Show("Password Did Not Match");
                }

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();

            LOGIN login = new LOGIN();
            login.Show();
        }
    }
}
