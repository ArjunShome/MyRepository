using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AYAN_TELECOM_WINAPP.PRESENTATION_LAYER_FORMS.ADMIN_FORMS
{
    public partial class ViewRechargeRequests : Form
    {
        BUSINESS_Layer business = new BUSINESS_Layer();
        DataTable dt = new DataTable();
        int count = 1;
        public ViewRechargeRequests()
        {
            InitializeComponent();
            PopulateGridView();
        }

        private void grdvwViewRechargeRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var sendergrid = (DataGridView)sender;
            if (sendergrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex > -1 )
            {
                int index = sendergrid.CurrentCell.RowIndex;

                //GET THE ID OF THE RECHARGE RECORD
                int id = (int)sendergrid.Rows[index].Cells[3].Value;

                string MSG = business.Recharge(id);
                MessageBox.Show(MSG,"RECHARGE STATUS",MessageBoxButtons.OK,MessageBoxIcon.Information);

                //REFRESH THE GRIDVIEW TO PRINT THE UPDATED DETAILS
                PopulateGridView();
            }
        }

        public void PopulateGridView()
        {
            DataTable dtfilter = new DataTable();
            dt = business.GetRechargeRequests();

            //Shw Recharge details on gridview
            grdvwViewRechargeRequests.DataSource = dt;
            grdvwViewRechargeRequests.AllowUserToAddRows = false;

            //Add only name, rechargemobie, amount and button for only pending recharge.
            if (count < 2)
            {
                //CREATE A button to ADD to the gridview on each column
                DataGridViewButtonColumn button = new DataGridViewButtonColumn();
                button.Text = "RECHARGE";
                button.HeaderText = "RECHARGE";
                button.UseColumnTextForButtonValue = true;

                //Add Button to each column at the end
                grdvwViewRechargeRequests.Columns.Add(button);
            }

            //ORDERING AS PER COLUMN NO 3
            grdvwViewRechargeRequests.Sort(grdvwViewRechargeRequests.Columns[2], ListSortDirection.Ascending);
            count++;
        }
        
    }
}
