namespace Aes
{
    partial class FormAes
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
            this.tbPlain = new System.Windows.Forms.TextBox();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnDecode = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClearCipher = new System.Windows.Forms.Button();
            this.btnClearPlain = new System.Windows.Forms.Button();
            this.btnNewKey = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPlain
            // 
            this.tbPlain.Location = new System.Drawing.Point(46, 14);
            this.tbPlain.Name = "tbPlain";
            this.tbPlain.Size = new System.Drawing.Size(386, 20);
            this.tbPlain.TabIndex = 0;
            this.tbPlain.Text = "Super secret message";
            // 
            // tbKey
            // 
            this.tbKey.Location = new System.Drawing.Point(59, 9);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(386, 20);
            this.tbKey.TabIndex = 1;
            this.tbKey.Text = "11223344556677889900aabbccddeeff";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "key";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Plain";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnTest.Location = new System.Drawing.Point(46, 39);
            this.btnTest.Margin = new System.Windows.Forms.Padding(2);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(179, 42);
            this.btnTest.TabIndex = 14;
            this.btnTest.Text = "▼";
            this.btnTest.UseVisualStyleBackColor = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(249, 40);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(179, 42);
            this.btnDecode.TabIndex = 15;
            this.btnDecode.Text = "▲";
            this.btnDecode.UseVisualStyleBackColor = true;
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Cipher";
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(46, 87);
            this.tbResult.Name = "tbResult";
            this.tbResult.Size = new System.Drawing.Size(382, 20);
            this.tbResult.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClearCipher);
            this.groupBox1.Controls.Add(this.btnClearPlain);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbResult);
            this.groupBox1.Controls.Add(this.btnTest);
            this.groupBox1.Controls.Add(this.tbPlain);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnDecode);
            this.groupBox1.Location = new System.Drawing.Point(13, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(501, 126);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Secret text";
            // 
            // btnClearCipher
            // 
            this.btnClearCipher.Location = new System.Drawing.Point(434, 87);
            this.btnClearCipher.Name = "btnClearCipher";
            this.btnClearCipher.Size = new System.Drawing.Size(51, 20);
            this.btnClearCipher.TabIndex = 21;
            this.btnClearCipher.Text = "X";
            this.btnClearCipher.UseVisualStyleBackColor = true;
            this.btnClearCipher.Click += new System.EventHandler(this.btnClearCipher_Click);
            // 
            // btnClearPlain
            // 
            this.btnClearPlain.Location = new System.Drawing.Point(438, 14);
            this.btnClearPlain.Name = "btnClearPlain";
            this.btnClearPlain.Size = new System.Drawing.Size(51, 20);
            this.btnClearPlain.TabIndex = 20;
            this.btnClearPlain.Text = "X";
            this.btnClearPlain.UseVisualStyleBackColor = true;
            this.btnClearPlain.Click += new System.EventHandler(this.btnClearPlain_Click);
            // 
            // btnNewKey
            // 
            this.btnNewKey.Location = new System.Drawing.Point(451, 9);
            this.btnNewKey.Name = "btnNewKey";
            this.btnNewKey.Size = new System.Drawing.Size(51, 20);
            this.btnNewKey.TabIndex = 19;
            this.btnNewKey.Text = "NEW";
            this.btnNewKey.UseVisualStyleBackColor = true;
            this.btnNewKey.Click += new System.EventHandler(this.btnNewKey_Click);
            // 
            // FormAes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 240);
            this.Controls.Add(this.btnNewKey);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbKey);
            this.Name = "FormAes";
            this.Text = "Don\'t eavesdrop please";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPlain;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNewKey;
        private System.Windows.Forms.Button btnClearCipher;
        private System.Windows.Forms.Button btnClearPlain;
    }
}

