namespace WindowsFormsApp1_testsql
{
    partial class SearchEmpForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSearchEmp = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(64, 137);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(672, 180);
            this.dataGridView1.TabIndex = 0;
            // 
            // btnSearchEmp
            // 
            this.btnSearchEmp.BackColor = System.Drawing.Color.Lime;
            this.btnSearchEmp.Font = new System.Drawing.Font("TH SarabunPSK", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSearchEmp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSearchEmp.Location = new System.Drawing.Point(199, 329);
            this.btnSearchEmp.Name = "btnSearchEmp";
            this.btnSearchEmp.Size = new System.Drawing.Size(184, 66);
            this.btnSearchEmp.TabIndex = 1;
            this.btnSearchEmp.Text = "ค้นหาข้อมูลพนักงาน";
            this.btnSearchEmp.UseVisualStyleBackColor = false;
            this.btnSearchEmp.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Red;
            this.btnClear.Font = new System.Drawing.Font("TH SarabunPSK", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnClear.Location = new System.Drawing.Point(432, 329);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(188, 66);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "เคลียร์ข้อมูลทั้งหมด";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("TH SarabunPSK", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(161, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(495, 71);
            this.label1.TabIndex = 3;
            this.label1.Text = "ตารางแสดงข้อมูลพนักงาน";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSearchEmp);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "โปรแกรมแสดงข้อมูลพนักงงาน";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSearchEmp;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label1;
    }
}

