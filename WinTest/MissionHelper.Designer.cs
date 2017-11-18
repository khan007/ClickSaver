namespace WinTest
{
    partial class frmMissionHelper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtBoxInfo = new System.Windows.Forms.TextBox();
            this.picBox1 = new System.Windows.Forms.PictureBox();
            this.btnFindWindows = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(341, 372);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(341, 60);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtBoxInfo
            // 
            this.txtBoxInfo.Location = new System.Drawing.Point(13, 37);
            this.txtBoxInfo.Multiline = true;
            this.txtBoxInfo.Name = "txtBoxInfo";
            this.txtBoxInfo.Size = new System.Drawing.Size(292, 155);
            this.txtBoxInfo.TabIndex = 3;
            // 
            // picBox1
            // 
            this.picBox1.Location = new System.Drawing.Point(311, 144);
            this.picBox1.Name = "picBox1";
            this.picBox1.Size = new System.Drawing.Size(48, 48);
            this.picBox1.TabIndex = 4;
            this.picBox1.TabStop = false;
            // 
            // btnFindWindows
            // 
            this.btnFindWindows.Location = new System.Drawing.Point(341, 307);
            this.btnFindWindows.Name = "btnFindWindows";
            this.btnFindWindows.Size = new System.Drawing.Size(75, 23);
            this.btnFindWindows.TabIndex = 5;
            this.btnFindWindows.Text = "Find windows";
            this.btnFindWindows.UseVisualStyleBackColor = true;
            this.btnFindWindows.Click += new System.EventHandler(this.btnFindWindows_Click);
            // 
            // frmMissionHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 407);
            this.Controls.Add(this.btnFindWindows);
            this.Controls.Add(this.picBox1);
            this.Controls.Add(this.txtBoxInfo);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.button1);
            this.Name = "frmMissionHelper";
            this.Text = "MissionHelper";
            ((System.ComponentModel.ISupportInitialize)(this.picBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtBoxInfo;
        private System.Windows.Forms.PictureBox picBox1;
        private System.Windows.Forms.Button btnFindWindows;
    }
}

