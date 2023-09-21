namespace MetaDataStringEditor {
    partial class EditForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.keepBtn = new System.Windows.Forms.Button();
            this.discardBtn = new System.Windows.Forms.Button();
            this.reverseBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 14);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(332, 220);
            this.textBox1.TabIndex = 0;
            // 
            // keepBtn
            // 
            this.keepBtn.Location = new System.Drawing.Point(108, 249);
            this.keepBtn.Name = "keepBtn";
            this.keepBtn.Size = new System.Drawing.Size(75, 25);
            this.keepBtn.TabIndex = 1;
            this.keepBtn.Text = "Replace";
            this.keepBtn.UseVisualStyleBackColor = true;
            this.keepBtn.Click += new System.EventHandler(this.keepBtn_Click);
            // 
            // discardBtn
            // 
            this.discardBtn.Location = new System.Drawing.Point(270, 249);
            this.discardBtn.Name = "discardBtn";
            this.discardBtn.Size = new System.Drawing.Size(75, 25);
            this.discardBtn.TabIndex = 2;
            this.discardBtn.Text = "Cancel";
            this.discardBtn.UseVisualStyleBackColor = true;
            this.discardBtn.Click += new System.EventHandler(this.discardBtn_Click);
            // 
            // reverseBtn
            // 
            this.reverseBtn.Location = new System.Drawing.Point(189, 249);
            this.reverseBtn.Name = "reverseBtn";
            this.reverseBtn.Size = new System.Drawing.Size(75, 25);
            this.reverseBtn.TabIndex = 3;
            this.reverseBtn.Text = "Undo";
            this.reverseBtn.UseVisualStyleBackColor = true;
            this.reverseBtn.Click += new System.EventHandler(this.reverseBtn_Click);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 287);
            this.Controls.Add(this.reverseBtn);
            this.Controls.Add(this.discardBtn);
            this.Controls.Add(this.keepBtn);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.ShowIcon = false;
            this.Text = "String Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button keepBtn;
        private System.Windows.Forms.Button discardBtn;
        private System.Windows.Forms.Button reverseBtn;
    }
}