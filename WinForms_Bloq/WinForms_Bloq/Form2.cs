using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_Bloq
{
    public partial class Form2 : Form
    {
        private Form1 parent;
        public (int height, int width) NewSize;
        public int GetHight { get { return (int)numericUpDownHeight.Value; } }
        public int GetWidth { get { return (int)numericUpDownWidth.Value; } }
        public Form2(Form1 form1)
        {
            InitializeComponent();
            parent = form1;
        }

        private void NewSchema_draw(object sender, EventArgs e)
        {
            int height =(int)numericUpDownHeight.Value;
            int width =(int)numericUpDownWidth.Value;
            NewSize = (height, width);
        }
    }
}
