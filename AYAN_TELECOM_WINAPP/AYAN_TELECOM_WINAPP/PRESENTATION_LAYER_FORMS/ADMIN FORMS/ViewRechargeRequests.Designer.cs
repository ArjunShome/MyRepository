namespace AYAN_TELECOM_WINAPP.PRESENTATION_LAYER_FORMS.ADMIN_FORMS
{
    partial class ViewRechargeRequests
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdvwViewRechargeRequests = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdvwViewRechargeRequests)).BeginInit();
            this.SuspendLayout();
            // 
            // grdvwViewRechargeRequests
            // 
            this.grdvwViewRechargeRequests.AllowUserToOrderColumns = true;
            this.grdvwViewRechargeRequests.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grdvwViewRechargeRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Brown;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.SeaShell;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdvwViewRechargeRequests.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdvwViewRechargeRequests.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.grdvwViewRechargeRequests.Location = new System.Drawing.Point(13, 13);
            this.grdvwViewRechargeRequests.Name = "grdvwViewRechargeRequests";
            this.grdvwViewRechargeRequests.Size = new System.Drawing.Size(994, 393);
            this.grdvwViewRechargeRequests.TabIndex = 0;
            this.grdvwViewRechargeRequests.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdvwViewRechargeRequests_CellContentClick);
            // 
            // ViewRechargeRequests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 418);
            this.Controls.Add(this.grdvwViewRechargeRequests);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewRechargeRequests";
            this.Text = "ViewRechargeRequests";
            ((System.ComponentModel.ISupportInitialize)(this.grdvwViewRechargeRequests)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdvwViewRechargeRequests;
    }
}