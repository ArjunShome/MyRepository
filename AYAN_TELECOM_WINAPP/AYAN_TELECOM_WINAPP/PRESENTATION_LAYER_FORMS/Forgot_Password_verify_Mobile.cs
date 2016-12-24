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
    public partial class Forgot_Password_verify_Mobile : Form
    {
        public Forgot_Password_verify_Mobile()
        {
            InitializeComponent();
        }

        public TextBox TextBox
        {
            get
            {
               return textBox1;
            }
            set
            {
                textBox1 = value;
            }
        }
        public Label Label
        {
            get
            {
                return label1;
            }
            set 
            {
                label1 = value;
            }
        }

        internal TextBox FindControl(string p)
        {
            throw new NotImplementedException();
        }
    }
}
