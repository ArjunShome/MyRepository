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
    public partial class User_Recharge_Request : Form
    {
        string mailID = String.Empty;
        BUSINESS_Layer business = new BUSINESS_Layer();

        public User_Recharge_Request()
        {
            InitializeComponent();
        }

        public User_Recharge_Request(String str)
        {
            InitializeComponent();
            mailID = str;
        }

        private void label9_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void User_Recharge_Request_Load(object sender, EventArgs e)
        {
            //POPULATE RECHARGE TYPES
            List<string> RechargeTypes = new List<string>();
            RechargeTypes = business.DispForRecharge();
            ComboRechargeType.Items.AddRange(RechargeTypes.ToArray());
            //ComboRechargeType.Text = "              SELECT  ";
            ComboRechargeType.SelectedItem = "              SELECT  ";

            //POPULATE RECHARGE VENDORS
            //List<string> RechargeTypes = new List<string>();
            RechargeTypes = business.DispForRecharge_Vendors();
            ComboVendor.Items.AddRange(RechargeTypes.ToArray());
            //ComboVendor.Text = "              SELECT  ";
            ComboVendor.SelectedItem = "              SELECT  ";

            //POPULATE USER DETAILS
            string Name = String.Empty;
            string PhoneNumber = String.Empty;
            string Email = String.Empty;

            List<object> str = new List<object>();
            str = business.DispUserDetails(mailID);

            Name = str[0].ToString();
            PhoneNumber = str[1].ToString();
            Email = str[2].ToString();

            TxtName.Text = Name;
            TxtName.TextAlign = HorizontalAlignment.Center;
            TxtName.ReadOnly = true;

            TxtRegPhoneNumber.Text = PhoneNumber;
            TxtRegPhoneNumber.TextAlign = HorizontalAlignment.Center;
            TxtRegPhoneNumber.ReadOnly = true;

            Txt_EmailID.Text = Email;
            Txt_EmailID.ReadOnly = true;
        }

        private void BtnSubmitReguest_Click(object sender, EventArgs e)
        {
            string CustPhoneNumber = String.Empty;
            string RechPhoneNumber = String.Empty;
            int Amount = 0;
            string Desc = String.Empty;
            string RechVendor = String.Empty;
            string RechType = String.Empty;
            string InsertMSG = String.Empty;

            CustPhoneNumber = TxtRegPhoneNumber.Text;
            RechPhoneNumber = TxtRechargeNumber.Text;
            Amount = Convert.ToInt32(Txt_RechargeAmount.Text);
            Desc = TxtDescription.Text;
            RechVendor = ComboVendor.SelectedItem.ToString();
            RechType = ComboRechargeType.SelectedItem.ToString();

            InsertMSG = business.InsertRechargeDetails(CustPhoneNumber, RechPhoneNumber, Amount, Desc, RechVendor, RechType);

            MessageBox.Show(InsertMSG, "RECHARGE REQUEST SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
    }
}
