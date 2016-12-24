using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using AYAN_TELECOM_WINAPP.User_Defined_Classes;

namespace AYAN_TELECOM_WINAPP
{
    class Data_Layer
    {
        //SQL DATA OBJECT REFERRENCES
        SqlConnection CON;
        SqlCommand COM;
        Properties_Class propind = new Properties_Class();
        
        /*
         * FETCH LOGIN CREDENTIALS FROM DATABASE:
         */
        public DataSet CheckLogin(string id,string passwd,int isadmin)
        {
                DataSet ds = new DataSet();
                SqlDataAdapter da;

                CON = new SqlConnection(ConfigurationManager.ConnectionStrings["AYAN_TELECOM"].ConnectionString);
                COM = new SqlCommand("USP_LoginChcek", CON);
                COM.CommandType = CommandType.StoredProcedure;

                SqlParameter emailid = COM.Parameters.Add("@UserID", SqlDbType.NVarChar, 100);
                SqlParameter password = COM.Parameters.Add("@Passwd", SqlDbType.NVarChar, 50);
                SqlParameter admin = COM.Parameters.Add("@IsAdmin", SqlDbType.Int);

                emailid.Direction = ParameterDirection.Input;
                password.Direction = ParameterDirection.Input;
                admin.Direction = ParameterDirection.Input;

                emailid.Value = id;
                password.Value = passwd;
                admin.Value = isadmin;

                da = new SqlDataAdapter(COM);
                da.Fill(ds);

                return ds;
            
        }


        /*
         * INSERT NEW USER DETAILS INTO DATABASE 
         */
        public string InsertNewUserCredentials(string name, string phone, string id, string passwd, string address, string address2, string pin, string city, string state, byte[] img)
        {
            string msg = String.Empty;
            try
            {
                CON = new SqlConnection(ConfigurationManager.ConnectionStrings["AYAN_TELECOM"].ConnectionString);
                COM = new SqlCommand("USP_InsertRegDetailsInDb", CON);
                COM.CommandType = CommandType.StoredProcedure;
                CON.Open();

                SqlParameter nme = COM.Parameters.Add("@name", SqlDbType.NVarChar, 50);
                SqlParameter phonenum = COM.Parameters.Add("@Phonenumber", SqlDbType.NVarChar, 50);
                SqlParameter emailid = COM.Parameters.Add("@emailid", SqlDbType.NVarChar, 50);
                SqlParameter password = COM.Parameters.Add("@password", SqlDbType.NVarChar, 50);
                SqlParameter addr = COM.Parameters.Add("@address1", SqlDbType.NVarChar, 500);
                SqlParameter addr2 = COM.Parameters.Add("@address2", SqlDbType.NVarChar, 500);
                SqlParameter pn = COM.Parameters.Add("@pin", SqlDbType.NVarChar, 10);
                SqlParameter ct = COM.Parameters.Add("@city", SqlDbType.NVarChar, 50);
                SqlParameter st = COM.Parameters.Add("@state", SqlDbType.NVarChar, 20);
                SqlParameter pic = COM.Parameters.Add("@img", SqlDbType.VarBinary,img.Length);
                SqlParameter output = COM.Parameters.Add("@OUTPUT", SqlDbType.NVarChar, 100);
                SqlParameter output1 = COM.Parameters.Add("@OUTPUT1", SqlDbType.Int);

                nme.Direction = ParameterDirection.Input;
                phonenum.Direction = ParameterDirection.Input;
                emailid.Direction = ParameterDirection.Input;
                password.Direction = ParameterDirection.Input;
                addr.Direction = ParameterDirection.Input;
                addr2.Direction = ParameterDirection.Input;
                pn.Direction = ParameterDirection.Input;
                ct.Direction = ParameterDirection.Input;
                st.Direction = ParameterDirection.Input;
                pic.Direction = ParameterDirection.Input;
                output.Direction = ParameterDirection.Output;
                output1.Direction = ParameterDirection.Output;

                nme.Value = name;
                phonenum.Value = phone;
                emailid.Value = id;
                password.Value = passwd;
                addr.Value = address;
                addr2.Value = address2;
                pn.Value = pin;
                ct.Value = city;
                st.Value = state;
                pic.Value = img;

                COM.ExecuteNonQuery();

                msg = output.Value.ToString() + "," + output1.Value.ToString();
                propind.Is_ExceptionIND = false;
                return msg;
               
            }
            catch(SqlException e) 
            {
                propind.Is_ExceptionIND = true;
                return e.Message;
            }
            finally
            {
                CON.Close();
            }
        }

        /*
         * VERIFY USER-ID FROM DATABASE
         */
        public string idverify( string id)
        {
            try
            {
                CON = new SqlConnection(ConfigurationManager.ConnectionStrings["AYAN_TELECOM"].ConnectionString);
                COM = new SqlCommand("USP_Check_UserID", CON);
                COM.CommandType = CommandType.StoredProcedure;
                CON.Open();

                SqlParameter userid = COM.Parameters.Add("@ID", SqlDbType.NVarChar, 500);
                SqlParameter output = COM.Parameters.Add("@Output", SqlDbType.NVarChar, 100);

                userid.Direction = ParameterDirection.Input;
                output.Direction = ParameterDirection.Output;

                userid.Value = id;

                COM.ExecuteNonQuery();

                string str = String.Empty;
                str = output.Value.ToString();
                return str;
            }
            catch (SqlException e)
            {
                return e.Message;
            }
            finally 
            {
                CON.Close();
            }
        }


        /*
         * VERIFFY USER MOBILE FROM DATABASE
         */
        public string mobverify(string uid,string mob)
        {
            try
            {
                CON = new SqlConnection(ConfigurationManager.ConnectionStrings["AYAN_TELECOM"].ConnectionString);
                COM = new SqlCommand("USP_Check_UserMobile", CON);
                COM.CommandType = CommandType.StoredProcedure;
                CON.Open();

                SqlParameter userid = COM.Parameters.Add("@userid", SqlDbType.NVarChar, 500);
                SqlParameter mobile = COM.Parameters.Add("@mobilenum", SqlDbType.NVarChar, 11);
                SqlParameter output = COM.Parameters.Add("@Output", SqlDbType.NVarChar, 100);

                userid.Direction = ParameterDirection.Input;
                mobile.Direction = ParameterDirection.Input;
                output.Direction = ParameterDirection.Output;

                userid.Value = uid;
                mobile.Value = mob;

                COM.ExecuteNonQuery();

                string str = String.Empty;
                str = output.Value.ToString();
                propind.Is_ExceptionIND = false;
                return str;
            }
            catch (SqlException e)
            {
                propind.Is_ExceptionIND = true;
                return e.Message;
            }
            finally 
            {
                CON.Close();
            }
        }

        /*
         * RESET PASSWORD IF USER HAS FORGOTTEN PASSWORD
         */
        public string Reset_Passwd(string passwd, string id)
        {
            try
            {
                CON = new SqlConnection(ConfigurationManager.ConnectionStrings["AYAN_TELECOM"].ConnectionString);
                COM = new SqlCommand("USP_Update_User_Passwd", CON);
                COM.CommandType = CommandType.StoredProcedure;
                CON.Open();

                SqlParameter userid = COM.Parameters.Add("@id", SqlDbType.NVarChar, 500);
                SqlParameter passwrd = COM.Parameters.Add("@passwd", SqlDbType.NVarChar, 11);
                SqlParameter output = COM.Parameters.Add("@Output", SqlDbType.NVarChar, 100);

                userid.Direction = ParameterDirection.Input;
                passwrd.Direction = ParameterDirection.Input;
                output.Direction = ParameterDirection.Output;

                userid.Value = id;
                passwrd.Value = passwd;

                COM.ExecuteNonQuery();

                string str = String.Empty;
                str = output.Value.ToString();
                propind.Is_ExceptionIND = false;
                return str;
            }
            catch (SqlException e)
            {
                propind.Is_ExceptionIND = true;
                return e.Message;
            }
            finally
            {
                CON.Close();
            }
        }

        public DataTable DispUserDetails(string id)
        {
            DataTable DT = new DataTable();
            try
            {
                CON = new SqlConnection(ConfigurationManager.ConnectionStrings["AYAN_TELECOM"].ConnectionString);
                COM = new SqlCommand("USP_Fetch_User_Details", CON);
                COM.CommandType = CommandType.StoredProcedure;
                CON.Open();

                SqlParameter userid = COM.Parameters.Add("@UserId", SqlDbType.NVarChar, 100);

                userid.Direction = ParameterDirection.Input;

                userid.Value = id;

                SqlDataReader reader = COM.ExecuteReader();

                DT.Load(reader);

                CON.Close();

                propind.Is_ExceptionIND = false;

                return DT;
            }
            catch (SqlException e)
            {
                propind.Is_ExceptionIND = true;
                propind.ExceptionMsg = e.Message;
                return DT;
            }
        }

        public void EditUser(string name, string phonenumber, string email, string passwd, string address1, string address2, string pin, string city, string state, byte[] img)
        {
            CON = new SqlConnection(ConfigurationManager.ConnectionStrings["AYAN_TELECOM"].ConnectionString);
            COM = new SqlCommand("USP_EDIT_USER_DETAILS", CON);
            COM.CommandType = CommandType.StoredProcedure;
            CON.Open();
                SqlParameter uname = COM.Parameters.Add("@name", SqlDbType.NVarChar, 100);
                SqlParameter uphonenumber = COM.Parameters.Add("@phonenumber", SqlDbType.NVarChar, 100);
                SqlParameter uemail = COM.Parameters.Add("@email", SqlDbType.NVarChar, 100);
                SqlParameter upasswd = COM.Parameters.Add("@passwd", SqlDbType.NVarChar, 100);
                SqlParameter uadr1 = COM.Parameters.Add("@address1", SqlDbType.NVarChar, 100);
                SqlParameter uadr2 = COM.Parameters.Add("@address2", SqlDbType.NVarChar, 100);
                SqlParameter upin = COM.Parameters.Add("@pin", SqlDbType.NVarChar, 100);
                SqlParameter ucty = COM.Parameters.Add("@city", SqlDbType.NVarChar, 100);
                SqlParameter ustate = COM.Parameters.Add("@state", SqlDbType.NVarChar, 100);
                SqlParameter image = COM.Parameters.Add("@image", SqlDbType.VarBinary,img.Length);

                uname.Direction = ParameterDirection.Input;
                uphonenumber.Direction = ParameterDirection.Input;
                uemail.Direction = ParameterDirection.Input;
                upasswd.Direction = ParameterDirection.Input;
                uadr1.Direction = ParameterDirection.Input;
                uadr2.Direction = ParameterDirection.Input;
                upin.Direction = ParameterDirection.Input;
                ucty.Direction = ParameterDirection.Input;
                ustate.Direction = ParameterDirection.Input;
                image.Direction = ParameterDirection.Input;

                uname.Value = name;
                uphonenumber.Value = phonenumber;
                uemail.Value = email;
                upasswd.Value = passwd;
                uadr1.Value = address1;
                uadr2.Value = address2;
                upin.Value = pin;
                ucty.Value = city;
                ustate.Value = state;
                image.Value = img;

                COM.ExecuteNonQuery();

            CON.Close();
        }

        public DataTable RechargeType()
        {
            CON = new SqlConnection(ConfigurationManager.ConnectionStrings["AYAN_TELECOM"].ConnectionString);
            COM = new SqlCommand("SELECT RechargeTypeLongName FROM DBO.RECHARGE_TYPE", CON);
            COM.CommandType = CommandType.Text;
            DataTable DT = new DataTable();
            CON.Open();
            
            SqlDataReader Reader = COM.ExecuteReader();

            DT.Load(Reader);

            CON.Close();

            return DT;
        }

        public DataTable VendorType()
        {
            CON = new SqlConnection(ConfigurationManager.ConnectionStrings["AYAN_TELECOM"].ConnectionString);
            COM = new SqlCommand("SELECT Vendor_Name FROM DBO.RECHARGE_VENDOR", CON);
            COM.CommandType = CommandType.Text;
            DataTable DT = new DataTable();
            CON.Open();

            SqlDataReader Reader = COM.ExecuteReader();

            DT.Load(Reader);

            CON.Close();

            return DT;
        }

        public string InsertRechargeDetails(string CustPhoneNumber, string RechPhoneNumber, int Amount, string Desc, string RechVendor, string RechType)
        {
            string outputMSG = String.Empty;
            CON = new SqlConnection(ConfigurationManager.ConnectionStrings["AYAN_TELECOM"].ConnectionString);
            COM = new SqlCommand("USP_InsertRechargeDetailsIntoDB", CON);
            COM.CommandType = CommandType.StoredProcedure;

            SqlParameter output = COM.Parameters.Add("@SuccessMessage",SqlDbType.NVarChar,500);
            SqlParameter custPhone = COM.Parameters.Add("@RegCustPhoneNumber",SqlDbType.NVarChar,10);
            SqlParameter rechPhone = COM.Parameters.Add("@PhoneNumberToRecharge", SqlDbType.NVarChar, 10);
            SqlParameter amt = COM.Parameters.Add("@Amount", SqlDbType.Int);
            SqlParameter desc = COM.Parameters.Add("@Desc", SqlDbType.NVarChar, 500);
            SqlParameter rechVendor = COM.Parameters.Add("@RechargeVendor",SqlDbType.NVarChar,100);
            SqlParameter rechType = COM.Parameters.Add("@RechargeType", SqlDbType.NVarChar, 100);

            output.Direction = ParameterDirection.Output;
            custPhone.Value = CustPhoneNumber;
            rechPhone.Value = RechPhoneNumber;
            amt.Value = Amount;
            desc.Value = Desc;
            rechVendor.Value = RechVendor;
            rechType.Value = RechType;

            CON.Open();

            COM.ExecuteNonQuery();
            
            outputMSG = output.Value.ToString();

            CON.Close();

            return outputMSG;
        }


        public DataTable GetRechargeRequests()
        {
            CON = new SqlConnection(ConfigurationManager.ConnectionStrings["AYAN_TELECOM"].ConnectionString);
            COM = new SqlCommand("SELECT * FROM DBO.UVW_GetRechargeRequests", CON);
            COM.CommandType = CommandType.Text;
            DataTable DT = new DataTable();
            CON.Open();

            SqlDataReader Reader = COM.ExecuteReader();

            DT.Load(Reader);

            CON.Close();

            return DT;
        }

        public string Recharge(int id)
        {
            string msg;
            CON = new SqlConnection(ConfigurationManager.ConnectionStrings["AYAN_TELECOM"].ConnectionString);
            COM = new SqlCommand("USP_Recharge_By_Admin", CON);
            COM.CommandType = CommandType.StoredProcedure;

            SqlParameter output = COM.Parameters.Add("@SuccessMsg", SqlDbType.NVarChar, 1000);
            SqlParameter rech_id = COM.Parameters.Add("@ID",SqlDbType.Int);

            output.Direction = ParameterDirection.Output;
            rech_id.Value = id;
            CON.Open();

            COM.ExecuteNonQuery();

            msg = output.Value.ToString();
            CON.Close();
            return msg;
        }

        }
}
