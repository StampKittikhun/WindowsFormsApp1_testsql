namespace WindowsFormsApp1_testsql
{
    partial class MainMenuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenuForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.MenuPanal = new System.Windows.Forms.Panel();
            this.btnSetLate = new System.Windows.Forms.Button();
            this.btnComplete = new System.Windows.Forms.Button();
            this.btnBathe = new System.Windows.Forms.Button();
            this.btnBury = new System.Windows.Forms.Button();
            this.btnPolishAndBathe = new System.Windows.Forms.Button();
            this.btnPolish = new System.Windows.Forms.Button();
            this.btnDress = new System.Windows.Forms.Button();
            this.btnFoundry = new System.Windows.Forms.Button();
            this.childPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.LeftPanel.SuspendLayout();
            this.MenuPanal.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(19, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(174, 181);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // LeftPanel
            // 
            this.LeftPanel.BackColor = System.Drawing.Color.Salmon;
            this.LeftPanel.Controls.Add(this.MenuPanal);
            this.LeftPanel.Controls.Add(this.pictureBox1);
            this.LeftPanel.Location = new System.Drawing.Point(-2, -2);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(227, 612);
            this.LeftPanel.TabIndex = 1;
            // 
            // MenuPanal
            // 
            this.MenuPanal.Controls.Add(this.btnSetLate);
            this.MenuPanal.Controls.Add(this.btnComplete);
            this.MenuPanal.Controls.Add(this.btnBathe);
            this.MenuPanal.Controls.Add(this.btnBury);
            this.MenuPanal.Controls.Add(this.btnPolishAndBathe);
            this.MenuPanal.Controls.Add(this.btnPolish);
            this.MenuPanal.Controls.Add(this.btnDress);
            this.MenuPanal.Controls.Add(this.btnFoundry);
            this.MenuPanal.Location = new System.Drawing.Point(11, 206);
            this.MenuPanal.Name = "MenuPanal";
            this.MenuPanal.Size = new System.Drawing.Size(195, 363);
            this.MenuPanal.TabIndex = 8;
            // 
            // btnSetLate
            // 
            this.btnSetLate.BackColor = System.Drawing.Color.MistyRose;
            this.btnSetLate.Font = new System.Drawing.Font("TH SarabunPSK", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnSetLate.Location = new System.Drawing.Point(8, 312);
            this.btnSetLate.Name = "btnSetLate";
            this.btnSetLate.Size = new System.Drawing.Size(177, 34);
            this.btnSetLate.TabIndex = 7;
            this.btnSetLate.Text = "ตั้งค่าวันและเนื้อเงิน";
            this.btnSetLate.UseVisualStyleBackColor = false;
            this.btnSetLate.Click += new System.EventHandler(this.btnSetLate_Click);
            // 
            // btnComplete
            // 
            this.btnComplete.BackColor = System.Drawing.Color.MistyRose;
            this.btnComplete.Font = new System.Drawing.Font("TH SarabunPSK", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnComplete.Location = new System.Drawing.Point(8, 222);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(177, 34);
            this.btnComplete.TabIndex = 6;
            this.btnComplete.Text = "ตรวจเรือนตัวงานสำเร็จ";
            this.btnComplete.UseVisualStyleBackColor = false;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // btnBathe
            // 
            this.btnBathe.BackColor = System.Drawing.Color.MistyRose;
            this.btnBathe.Font = new System.Drawing.Font("TH SarabunPSK", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnBathe.Location = new System.Drawing.Point(8, 186);
            this.btnBathe.Name = "btnBathe";
            this.btnBathe.Size = new System.Drawing.Size(177, 34);
            this.btnBathe.TabIndex = 5;
            this.btnBathe.Text = "ตรวจงานชุบ";
            this.btnBathe.UseVisualStyleBackColor = false;
            this.btnBathe.Click += new System.EventHandler(this.btnBathe_Click);
            // 
            // btnBury
            // 
            this.btnBury.BackColor = System.Drawing.Color.MistyRose;
            this.btnBury.Font = new System.Drawing.Font("TH SarabunPSK", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnBury.Location = new System.Drawing.Point(8, 149);
            this.btnBury.Name = "btnBury";
            this.btnBury.Size = new System.Drawing.Size(177, 34);
            this.btnBury.TabIndex = 4;
            this.btnBury.Text = "ตรวจงานฝัง";
            this.btnBury.UseVisualStyleBackColor = false;
            this.btnBury.Click += new System.EventHandler(this.btnBury_Click);
            // 
            // btnPolishAndBathe
            // 
            this.btnPolishAndBathe.BackColor = System.Drawing.Color.MistyRose;
            this.btnPolishAndBathe.Font = new System.Drawing.Font("TH SarabunPSK", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPolishAndBathe.Location = new System.Drawing.Point(8, 113);
            this.btnPolishAndBathe.Name = "btnPolishAndBathe";
            this.btnPolishAndBathe.Size = new System.Drawing.Size(177, 34);
            this.btnPolishAndBathe.TabIndex = 3;
            this.btnPolishAndBathe.Text = "ตรวจงานขัด+ชุบ";
            this.btnPolishAndBathe.UseVisualStyleBackColor = false;
            this.btnPolishAndBathe.Click += new System.EventHandler(this.btnPolishAndBathe_Click);
            // 
            // btnPolish
            // 
            this.btnPolish.BackColor = System.Drawing.Color.MistyRose;
            this.btnPolish.Font = new System.Drawing.Font("TH SarabunPSK", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnPolish.Location = new System.Drawing.Point(8, 76);
            this.btnPolish.Name = "btnPolish";
            this.btnPolish.Size = new System.Drawing.Size(177, 34);
            this.btnPolish.TabIndex = 2;
            this.btnPolish.Text = "ตรวจงานขัด";
            this.btnPolish.UseVisualStyleBackColor = false;
            this.btnPolish.Click += new System.EventHandler(this.btnPolish_Click);
            // 
            // btnDress
            // 
            this.btnDress.BackColor = System.Drawing.Color.MistyRose;
            this.btnDress.Font = new System.Drawing.Font("TH SarabunPSK", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnDress.Location = new System.Drawing.Point(8, 39);
            this.btnDress.Name = "btnDress";
            this.btnDress.Size = new System.Drawing.Size(177, 34);
            this.btnDress.TabIndex = 1;
            this.btnDress.Text = "ตรวจงานแต่ง";
            this.btnDress.UseVisualStyleBackColor = false;
            this.btnDress.Click += new System.EventHandler(this.btnDress_Click);
            // 
            // btnFoundry
            // 
            this.btnFoundry.BackColor = System.Drawing.Color.MistyRose;
            this.btnFoundry.Font = new System.Drawing.Font("TH SarabunPSK", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.btnFoundry.Location = new System.Drawing.Point(8, 3);
            this.btnFoundry.Name = "btnFoundry";
            this.btnFoundry.Size = new System.Drawing.Size(177, 34);
            this.btnFoundry.TabIndex = 0;
            this.btnFoundry.Text = "ตรวจงานหล่อ";
            this.btnFoundry.UseVisualStyleBackColor = false;
            this.btnFoundry.Click += new System.EventHandler(this.btnFoundry_Click);
            // 
            // childPanel
            // 
            this.childPanel.Location = new System.Drawing.Point(220, -2);
            this.childPanel.Name = "childPanel";
            this.childPanel.Size = new System.Drawing.Size(688, 591);
            this.childPanel.TabIndex = 2;
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 588);
            this.Controls.Add(this.childPanel);
            this.Controls.Add(this.LeftPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(922, 627);
            this.Name = "MainMenuForm";
            this.Text = "MainMenuForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.LeftPanel.ResumeLayout(false);
            this.MenuPanal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Panel childPanel;
        private System.Windows.Forms.Button btnSetLate;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnBathe;
        private System.Windows.Forms.Button btnBury;
        private System.Windows.Forms.Button btnPolishAndBathe;
        private System.Windows.Forms.Button btnPolish;
        private System.Windows.Forms.Button btnDress;
        private System.Windows.Forms.Button btnFoundry;
        private System.Windows.Forms.Panel MenuPanal;
        private System.Windows.Forms.Button SetDateSliver;
    }
}