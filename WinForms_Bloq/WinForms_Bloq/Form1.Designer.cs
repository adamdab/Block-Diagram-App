
namespace WinForms_Bloq
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FileBox = new System.Windows.Forms.GroupBox();
            this.LOADbutton = new System.Windows.Forms.Button();
            this.SAVEbutton = new System.Windows.Forms.Button();
            this.NEWbutton = new System.Windows.Forms.Button();
            this.EditionBox = new System.Windows.Forms.GroupBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.EditionTextBox = new System.Windows.Forms.TextBox();
            this.DELETEbutton = new System.Windows.Forms.RadioButton();
            this.LINK_button = new System.Windows.Forms.RadioButton();
            this.ENDbutton = new System.Windows.Forms.RadioButton();
            this.STARTbutton = new System.Windows.Forms.RadioButton();
            this.DECbutton = new System.Windows.Forms.RadioButton();
            this.OPbutton = new System.Windows.Forms.RadioButton();
            this.LanguageBox = new System.Windows.Forms.GroupBox();
            this.ENbutton = new System.Windows.Forms.Button();
            this.PLbutton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.FileBox.SuspendLayout();
            this.EditionBox.SuspendLayout();
            this.LanguageBox.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.FileBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.EditionBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.LanguageBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // FileBox
            // 
            this.FileBox.Controls.Add(this.LOADbutton);
            this.FileBox.Controls.Add(this.SAVEbutton);
            this.FileBox.Controls.Add(this.NEWbutton);
            resources.ApplyResources(this.FileBox, "FileBox");
            this.FileBox.Name = "FileBox";
            this.FileBox.TabStop = false;
            // 
            // LOADbutton
            // 
            resources.ApplyResources(this.LOADbutton, "LOADbutton");
            this.LOADbutton.Name = "LOADbutton";
            this.LOADbutton.UseVisualStyleBackColor = true;
            this.LOADbutton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LOADfrom);
            // 
            // SAVEbutton
            // 
            resources.ApplyResources(this.SAVEbutton, "SAVEbutton");
            this.SAVEbutton.Name = "SAVEbutton";
            this.SAVEbutton.UseVisualStyleBackColor = true;
            this.SAVEbutton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SaveAS);
            // 
            // NEWbutton
            // 
            resources.ApplyResources(this.NEWbutton, "NEWbutton");
            this.NEWbutton.Name = "NEWbutton";
            this.NEWbutton.UseVisualStyleBackColor = true;
            this.NEWbutton.Click += new System.EventHandler(this.NewScheme);
            // 
            // EditionBox
            // 
            this.EditionBox.Controls.Add(this.textBox);
            this.EditionBox.Controls.Add(this.EditionTextBox);
            this.EditionBox.Controls.Add(this.DELETEbutton);
            this.EditionBox.Controls.Add(this.LINK_button);
            this.EditionBox.Controls.Add(this.ENDbutton);
            this.EditionBox.Controls.Add(this.STARTbutton);
            this.EditionBox.Controls.Add(this.DECbutton);
            this.EditionBox.Controls.Add(this.OPbutton);
            resources.ApplyResources(this.EditionBox, "EditionBox");
            this.EditionBox.Name = "EditionBox";
            this.EditionBox.TabStop = false;
            // 
            // textBox
            // 
            resources.ApplyResources(this.textBox, "textBox");
            this.textBox.Name = "textBox";
            this.textBox.TextChanged += new System.EventHandler(this.Change_text);
            // 
            // EditionTextBox
            // 
            this.EditionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.EditionTextBox, "EditionTextBox");
            this.EditionTextBox.Name = "EditionTextBox";
            this.EditionTextBox.ReadOnly = true;
            // 
            // DELETEbutton
            // 
            resources.ApplyResources(this.DELETEbutton, "DELETEbutton");
            this.DELETEbutton.Name = "DELETEbutton";
            this.DELETEbutton.UseVisualStyleBackColor = true;
            // 
            // LINK_button
            // 
            resources.ApplyResources(this.LINK_button, "LINK_button");
            this.LINK_button.Name = "LINK_button";
            this.LINK_button.UseVisualStyleBackColor = true;
            // 
            // ENDbutton
            // 
            resources.ApplyResources(this.ENDbutton, "ENDbutton");
            this.ENDbutton.Name = "ENDbutton";
            this.ENDbutton.UseVisualStyleBackColor = true;
            // 
            // STARTbutton
            // 
            resources.ApplyResources(this.STARTbutton, "STARTbutton");
            this.STARTbutton.Name = "STARTbutton";
            this.STARTbutton.UseVisualStyleBackColor = true;
            // 
            // DECbutton
            // 
            resources.ApplyResources(this.DECbutton, "DECbutton");
            this.DECbutton.Name = "DECbutton";
            this.DECbutton.UseVisualStyleBackColor = true;
            // 
            // OPbutton
            // 
            resources.ApplyResources(this.OPbutton, "OPbutton");
            this.OPbutton.Checked = true;
            this.OPbutton.Name = "OPbutton";
            this.OPbutton.TabStop = true;
            this.OPbutton.UseVisualStyleBackColor = true;
            // 
            // LanguageBox
            // 
            this.LanguageBox.Controls.Add(this.ENbutton);
            this.LanguageBox.Controls.Add(this.PLbutton);
            resources.ApplyResources(this.LanguageBox, "LanguageBox");
            this.LanguageBox.Name = "LanguageBox";
            this.LanguageBox.TabStop = false;
            // 
            // ENbutton
            // 
            resources.ApplyResources(this.ENbutton, "ENbutton");
            this.ENbutton.Name = "ENbutton";
            this.ENbutton.UseVisualStyleBackColor = true;
            // 
            // PLbutton
            // 
            resources.ApplyResources(this.PLbutton, "PLbutton");
            this.PLbutton.Name = "PLbutton";
            this.PLbutton.UseVisualStyleBackColor = true;
            this.PLbutton.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.Canvas);
            this.panel1.Name = "panel1";
            this.tableLayoutPanel1.SetRowSpan(this.panel1, 3);
            // 
            // Canvas
            // 
            resources.ApplyResources(this.Canvas, "Canvas");
            this.Canvas.Name = "Canvas";
            this.Canvas.TabStop = false;
            this.Canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Clicked_to_draw);
            this.Canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Mouse_move_event);
            this.Canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Mouse_up_event);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.FileBox.ResumeLayout(false);
            this.EditionBox.ResumeLayout(false);
            this.EditionBox.PerformLayout();
            this.LanguageBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox FileBox;
        private System.Windows.Forms.GroupBox EditionBox;
        private System.Windows.Forms.GroupBox LanguageBox;
        private System.Windows.Forms.Button PLbutton;
        private System.Windows.Forms.Button ENbutton;
        private System.Windows.Forms.Button LOADbutton;
        private System.Windows.Forms.Button SAVEbutton;
        private System.Windows.Forms.Button NEWbutton;
        private System.Windows.Forms.RadioButton DECbutton;
        private System.Windows.Forms.RadioButton OPbutton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.RadioButton DELETEbutton;
        private System.Windows.Forms.RadioButton LINK_button;
        private System.Windows.Forms.RadioButton ENDbutton;
        private System.Windows.Forms.RadioButton STARTbutton;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.TextBox EditionTextBox;
    }
}

