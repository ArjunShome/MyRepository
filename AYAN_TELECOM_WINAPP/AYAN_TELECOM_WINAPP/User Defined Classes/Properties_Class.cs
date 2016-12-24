using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYAN_TELECOM_WINAPP.User_Defined_Classes
{
    class Properties_Class
    {
        string msg ;
        public bool Is_ExceptionIND { get; set; }
        public string ExceptionMsg 
        {
            get
            {
                return msg;
            }
            set
            {
                this.msg = value;
            }
        }
    }
}
