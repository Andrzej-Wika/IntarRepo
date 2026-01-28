namespace IntarRepo
{
    partial class frmExecute
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExecute));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sprawdzanyBtn = new System.Windows.Forms.RadioButton();
            this.repozytoriumBtn = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Opis = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sprawdzanyBtn);
            this.groupBox1.Controls.Add(this.repozytoriumBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 75);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scieżka skryptu";
            // 
            // sprawdzanyBtn
            // 
            this.sprawdzanyBtn.AutoSize = true;
            this.sprawdzanyBtn.Location = new System.Drawing.Point(17, 44);
            this.sprawdzanyBtn.Name = "sprawdzanyBtn";
            this.sprawdzanyBtn.Size = new System.Drawing.Size(85, 17);
            this.sprawdzanyBtn.TabIndex = 1;
            this.sprawdzanyBtn.TabStop = true;
            this.sprawdzanyBtn.Text = "radioButton2";
            this.sprawdzanyBtn.UseVisualStyleBackColor = true;
            this.sprawdzanyBtn.CheckedChanged += new System.EventHandler(this.sprawdzanyBtn_CheckedChanged);
            // 
            // repozytoriumBtn
            // 
            this.repozytoriumBtn.AutoSize = true;
            this.repozytoriumBtn.Location = new System.Drawing.Point(17, 20);
            this.repozytoriumBtn.Name = "repozytoriumBtn";
            this.repozytoriumBtn.Size = new System.Drawing.Size(85, 17);
            this.repozytoriumBtn.TabIndex = 0;
            this.repozytoriumBtn.TabStop = true;
            this.repozytoriumBtn.Text = "radioButton1";
            this.repozytoriumBtn.UseVisualStyleBackColor = true;
            this.repozytoriumBtn.CheckedChanged += new System.EventHandler(this.repozytoriumBtn_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "BPSC",
            "TEST",
            "KPP"});
            this.comboBox1.Location = new System.Drawing.Point(541, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(301, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Wykonaj skrypt";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Opis
            // 
            this.Opis.BackColor = System.Drawing.SystemColors.Info;
            this.Opis.Location = new System.Drawing.Point(13, 94);
            this.Opis.Multiline = true;
            this.Opis.Name = "Opis";
            this.Opis.Size = new System.Drawing.Size(509, 112);
            this.Opis.TabIndex = 3;
            // 
            // frmExecute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 249);
            this.Controls.Add(this.Opis);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExecute";
            this.Text = "Wykonanie skryptu";
            this.Load += new System.EventHandler(this.frmExecute_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton sprawdzanyBtn;
        private System.Windows.Forms.RadioButton repozytoriumBtn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox Opis;
    }
}