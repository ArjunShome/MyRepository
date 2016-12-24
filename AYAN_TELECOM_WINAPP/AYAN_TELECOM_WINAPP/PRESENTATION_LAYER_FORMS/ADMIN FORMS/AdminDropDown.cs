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

namespace AYAN_TELECOM_WINAPP.PRESENTATION_LAYER_FORMS.ADMIN_FORMS
{
    public partial class AdminDropDown : Form
    {
        string email = String.Empty;
        public AdminDropDown(string id)
        {
            InitializeComponent();
            email = id;
        }


        private void lbl_Edit_MouseEnter(object sender, EventArgs e)
        {
            this.Show();
        }


        private void lbl_SignOut_MouseEnter(object sender, EventArgs e)
        {
            this.Show();
        }

        private void AdminDropDown_MouseHover(object sender, EventArgs e)
        {
            this.Show();
        }

        private void lbl_Edit_Click(object sender, EventArgs e)
        {
            //CALL THE SAME CUSTOMER EDIT FORM
            Edit_User edit = new Edit_User(email);
            this.Hide();
            edit.Show();
        }

        private void lbl_SignOut_Click(object sender, EventArgs e)
        {
            //CLOSING THE CURRENT FORM
            this.Close();

            //GET ALL OPEN FORM IN A FORM COLLECTION
            List<Form> Forms =new List<Form>();
            foreach(Form f in Application.OpenForms)    
                Forms.Add(f);

            //CLOSE THE ADMIN FORM AND REOPEN THE LOGIN FORM
            foreach (Form Frm in Forms)
            {   
                //CLOSE MASTER ADMIN
                if (Frm.Name == "Master_Admin")
                    Frm.Close();

                //REOPEN LOGIN FORM
                if (Frm.Name == "LOGIN")
                    Frm.Show();
            }

        }
    }
}
