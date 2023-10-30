
namespace NMDManagement
{
    partial class FrmNMDStatus
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
            this.dgvfirst = new System.Windows.Forms.DataGridView();
            this.hnr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.casseteId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.open = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IntStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvSecond = new System.Windows.Forms.DataGridView();
            this.Task = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statuss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvfirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecond)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvfirst
            // 
            this.dgvfirst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvfirst.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.hnr,
            this.casseteId,
            this.open,
            this.status,
            this.IntStatus});
            this.dgvfirst.Location = new System.Drawing.Point(0, 3);
            this.dgvfirst.Name = "dgvfirst";
            this.dgvfirst.Size = new System.Drawing.Size(670, 314);
            this.dgvfirst.TabIndex = 0;
            // 
            // hnr
            // 
            this.hnr.DataPropertyName = "hnr";
            this.hnr.HeaderText = "Hnr";
            this.hnr.Name = "hnr";
            this.hnr.ReadOnly = true;
            this.hnr.Width = 40;
            // 
            // casseteId
            // 
            this.casseteId.DataPropertyName = "cassetteID";
            this.casseteId.HeaderText = "Cass. ID";
            this.casseteId.Name = "casseteId";
            this.casseteId.ReadOnly = true;
            this.casseteId.Width = 80;
            // 
            // open
            // 
            this.open.DataPropertyName = "open";
            this.open.HeaderText = "Open/Close";
            this.open.Name = "open";
            this.open.ReadOnly = true;
            this.open.Width = 200;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 50;
            // 
            // IntStatus
            // 
            this.IntStatus.DataPropertyName = "IntStatusRV";
            this.IntStatus.HeaderText = "Internal Status";
            this.IntStatus.Name = "IntStatus";
            this.IntStatus.ReadOnly = true;
            this.IntStatus.Width = 200;
            // 
            // dgvSecond
            // 
            this.dgvSecond.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSecond.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Task,
            this.statuss});
            this.dgvSecond.Location = new System.Drawing.Point(0, 329);
            this.dgvSecond.Name = "dgvSecond";
            this.dgvSecond.Size = new System.Drawing.Size(670, 226);
            this.dgvSecond.TabIndex = 1;
            // 
            // Task
            // 
            this.Task.DataPropertyName = "task";
            this.Task.HeaderText = "Task";
            this.Task.Name = "Task";
            this.Task.ReadOnly = true;
            this.Task.Width = 150;
            // 
            // statuss
            // 
            this.statuss.DataPropertyName = "statuss";
            this.statuss.HeaderText = "Status";
            this.statuss.Name = "statuss";
            this.statuss.ReadOnly = true;
            this.statuss.Width = 400;
            // 
            // FrmNMDStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 566);
            this.Controls.Add(this.dgvSecond);
            this.Controls.Add(this.dgvfirst);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNMDStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmNMDStatus";
            ((System.ComponentModel.ISupportInitialize)(this.dgvfirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecond)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvfirst;
        private System.Windows.Forms.DataGridViewTextBoxColumn hnr;
        private System.Windows.Forms.DataGridViewTextBoxColumn casseteId;
        private System.Windows.Forms.DataGridViewTextBoxColumn open;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn IntStatus;
        private System.Windows.Forms.DataGridView dgvSecond;
        private System.Windows.Forms.DataGridViewTextBoxColumn Task;
        private System.Windows.Forms.DataGridViewTextBoxColumn statuss;
    }
}