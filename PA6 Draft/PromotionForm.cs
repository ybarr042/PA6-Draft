using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PA6_Draft
{
    
    public partial class PromotionForm : Form
    {
        //remove the entire system menu:
        private const int WS_SYSMENU = 0x80000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style &= ~WS_SYSMENU;
                return cp;
            }
        }
        private int selection = -1;
        public PromotionForm()
        {
            InitializeComponent();
        }

        public int getSelection()
        {
            return selection;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void QueenButton_Click(object sender, EventArgs e)
        {
            selection = 1;
            this.Close();
        }

        private void Rook_Click(object sender, EventArgs e)
        {
            selection = 2;
            this.Close();
        }

        private void Bishop_Click(object sender, EventArgs e)
        {
            selection = 3;
            this.Close();
        }

        private void KnightButton_Click(object sender, EventArgs e)
        {
            selection = 4;
            this.Close();
        }
    }
}
