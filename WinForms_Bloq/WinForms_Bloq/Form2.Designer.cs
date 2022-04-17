
namespace WinForms_Bloq
{
    partial class Form2
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
            this.Form2_OKbutton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // Form2_OKbutton
            // 
            this.Form2_OKbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Form2_OKbutton.Location = new System.Drawing.Point(12, 86);
            this.Form2_OKbutton.MaximumSize = new System.Drawing.Size(258, 29);
            this.Form2_OKbutton.MinimumSize = new System.Drawing.Size(258, 29);
            this.Form2_OKbutton.Name = "Form2_OKbutton";
            this.Form2_OKbutton.Size = new System.Drawing.Size(258, 29);
            this.Form2_OKbutton.TabIndex = 0;
            this.Form2_OKbutton.Text = "OK";
            this.Form2_OKbutton.UseVisualStyleBackColor = true;
            this.Form2_OKbutton.Click += new System.EventHandler(this.NewSchema_draw);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(102, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "Szerokość";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(12, 52);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(109, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "Wysokość";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Location = new System.Drawing.Point(120, 10);
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownWidth.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownWidth.TabIndex = 5;
            this.numericUpDownWidth.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // numericUpDownHeight
            // 
            this.numericUpDownHeight.Location = new System.Drawing.Point(120, 50);
            this.numericUpDownHeight.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownHeight.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownHeight.Name = "numericUpDownHeight";
            this.numericUpDownHeight.Size = new System.Drawing.Size(150, 27);
            this.numericUpDownHeight.TabIndex = 6;
            this.numericUpDownHeight.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 127);
            this.Controls.Add(this.numericUpDownHeight);
            this.Controls.Add(this.numericUpDownWidth);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Form2_OKbutton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(400, 400);
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nowy Schemat";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Form2_OKbutton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
    }
}