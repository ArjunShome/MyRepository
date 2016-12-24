using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AYAN_TELECOM_WINAPP
{
    class BUSINESS_Layer
    {
        Data_Layer data = new Data_Layer();


        /*
         * VALIDATE LOGIN CREDENTIALS
         */

        public List<object> ValidateLogin(string id,string passwd,int admin)
        {
            List<object> objects = new List<object>();
            DataSet ds;
            ds = data.CheckLogin(id, passwd, admin);

            //Classify dataset values and insert into list
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    objects.Add(ds.Tables[0].Rows[0][i]);
                }
            return objects;
        }

        /*
         * INSERT NEW USER INTO DATABASE
         */
        public string InsertNewReg(string name, string phone, string id, string passwd, string address, string address2, string pin, string city, string state, byte[] img)
        {
            string str = String.Empty;
            str = data.InsertNewUserCredentials(name, phone, id, passwd, address, address2, pin, city, state, img);
            return str;
        }

        public string verifyID(string id)
        {
            string str = String.Empty;
            str = data.idverify(id);
            return str;
        }

        public string verifymob(string userid,string mob)
        {
            string str = String.Empty;
            str = data.mobverify(userid,mob);
            return str;
        }

        public string ResetPasswd(string passwd, string id)
        {
            string str = String.Empty;
            str = data.Reset_Passwd(passwd,id);
            return str;
        }

        public List<object> DispUserDetails(string id)
        {
            DataTable DT = new DataTable();
            DT = data.DispUserDetails(id);

            List<object> str = new List<object>();

            int countcolumns = DT.Columns.Count;

            for (int i = 0; i < countcolumns; i++)
            {
                str.Add(DT.Rows[0][i]);
            }
            
            return str;
        }

        public string EditUser(string name, string phonenumber, string email, string passwd, string address1, string address2, string pin, string city, string state, byte[] img)
        {
            
           data.EditUser(name,phonenumber,email,passwd,address1,address2,pin,city,state,img);
           string message = "USER EDITED SUCCESSFULLY";
           return message;
        }

        public List<string> DispForRecharge()
        {
            List<string> str = new List<string>();
            DataTable dt = new DataTable();
               dt = data.RechargeType();
               int count = dt.Rows.Count;
               for (int i = 0; i < count; i++)
               {
                   str.Add(dt.Rows[i][0].ToString());
               }
                   return str;
        }

        public List<string> DispForRecharge_Vendors()
        {
            List<string> str = new List<string>();
            DataTable dt = new DataTable();
            dt = data.VendorType();
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                str.Add(dt.Rows[i][0].ToString());
            }
            return str;
        }

        public string InsertRechargeDetails(string CustPhoneNumber, string RechPhoneNumber, int Amount, string Desc, string RechVendor, string RechType)
        {
            string message = string.Empty;
            message = data.InsertRechargeDetails(CustPhoneNumber, RechPhoneNumber, Amount, Desc, RechVendor, RechType);
            return message;
        }

        public DataTable GetRechargeRequests()
        {
            DataTable dt = new DataTable();
            dt = data.GetRechargeRequests();
            return dt;
        }

        public string Recharge(int id)
        {
            string msg = data.Recharge(id);
            return msg;
        }
    }
}
