using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;

namespace NMDManagement
{
    public partial class FrmImgFinger : Form
    {
        public FrmImgFinger(int pTipo)
        {
            InitializeComponent();
            picFinger.Visible = false;
            if (pTipo == 1)
            {
                this.Size = new Size(803, 601);
                Bitmap img = new Bitmap(Properties.Resources.ImgFondoHuella);
                picFondo = new PictureBox();
                picFondo.Image = img;
            }
            else
            {
                this.Size = new Size(262, 377);
                Bitmap img = new Bitmap(Properties.Resources.ImgHuella);
                picFondo=new PictureBox();
                picFondo.Image = img;

            }

        }
        public void fingerPrintVisible()
        {
            
            picFinger.Visible = true;
        }
        public void fingerPrintNotVisible()
        {
            picFinger.Visible = false;
        }

        private void FrmImgFinger_Shown(object sender, EventArgs e)
        {
            BringToFront();
        }
    }
}
