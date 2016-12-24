namespace AYAN_TELECOM_WINAPP.PRESENTATION_LAYER_FORMS.ADMIN_FORMS
{
    partial class AdminDropDown
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminDropDown));
            this.lbl_Edit = new System.Windows.Forms.Label();
            this.lbl_SignOut = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_Edit
            // 
            resources.ApplyResources(this.lbl_Edit, "lbl_Edit");
            this.lbl_Edit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_Edit.Name = "lbl_Edit";
            this.lbl_Edit.Click += new System.EventHandler(this.lbl_Edit_Click);
            this.lbl_Edit.MouseEnter += new System.EventHandler(this.lbl_Edit_MouseEnter);
            // 
            // lbl_SignOut
            // 
            resources.ApplyResources(this.lbl_SignOut, "lbl_SignOut");
            this.lbl_SignOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_SignOut.Name = "lbl_SignOut";
            this.lbl_SignOut.Click += new System.EventHandler(this.lbl_SignOut_Click);
            this.lbl_SignOut.MouseEnter += new System.EventHandler(this.lbl_SignOut_MouseEnter);
            // 
            // AdminDropDown
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.lbl_SignOut);
            this.Controls.Add(this.lbl_Edit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminDropDown";
            this.Opacity = 0.85D;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.MouseHover += new System.EventHandler(this.AdminDropDown_MouseHover);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Edit;
        private System.Windows.Forms.Label lbl_SignOut;
    }
}