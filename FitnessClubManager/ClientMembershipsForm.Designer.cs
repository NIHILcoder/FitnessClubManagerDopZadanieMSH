namespace FitnessClubManager
{
    partial class ClientMembershipsForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblClientName = new System.Windows.Forms.Label();
            this.dataGridMemberships = new System.Windows.Forms.DataGridView();
            this.btnAddMembership = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMemberships)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(168, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Абонементы клиента:";
            // 
            // lblClientName
            // 
            this.lblClientName.AutoSize = true;
            this.lblClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblClientName.Location = new System.Drawing.Point(176, 9);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Size = new System.Drawing.Size(74, 16);
            this.lblClientName.TabIndex = 1;
            this.lblClientName.Text = "[Фамилия]";
            // 
            // dataGridMemberships
            // 
            this.dataGridMemberships.AllowUserToAddRows = false;
            this.dataGridMemberships.AllowUserToDeleteRows = false;
            this.dataGridMemberships.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridMemberships.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridMemberships.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridMemberships.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridMemberships.Location = new System.Drawing.Point(12, 39);
            this.dataGridMemberships.MultiSelect = false;
            this.dataGridMemberships.Name = "dataGridMemberships";
            this.dataGridMemberships.ReadOnly = true;
            this.dataGridMemberships.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridMemberships.Size = new System.Drawing.Size(760, 370);
            this.dataGridMemberships.TabIndex = 2;
            // 
            // btnAddMembership
            // 
            this.btnAddMembership.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddMembership.Location = new System.Drawing.Point(12, 424);
            this.btnAddMembership.Name = "btnAddMembership";
            this.btnAddMembership.Size = new System.Drawing.Size(158, 35);
            this.btnAddMembership.TabIndex = 3;
            this.btnAddMembership.Text = "Добавить абонемент";
            this.btnAddMembership.UseVisualStyleBackColor = true;
            this.btnAddMembership.Click += new System.EventHandler(this.btnAddMembership_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(672, 424);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ClientMembershipsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(784, 471);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddMembership);
            this.Controls.Add(this.dataGridMemberships);
            this.Controls.Add(this.lblClientName);
            this.Controls.Add(this.lblTitle);
            this.MinimumSize = new System.Drawing.Size(800, 510);
            this.Name = "ClientMembershipsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Абонементы клиента";
            this.Load += new System.EventHandler(this.ClientMembershipsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridMemberships)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblClientName;
        private System.Windows.Forms.DataGridView dataGridMemberships;
        private System.Windows.Forms.Button btnAddMembership;
        private System.Windows.Forms.Button btnClose;
    }
}