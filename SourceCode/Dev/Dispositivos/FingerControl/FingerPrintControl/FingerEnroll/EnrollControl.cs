using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prodem.Fingerprint.FingerPrintControl.Domain;

namespace Prodem.Fingerprint.FingerPrintControl.FingerEnroll
{
    public partial class EnrollControl : UserControl
    {
        public TypeEnroll typeEnroll { get; set; }

        public EnrollControl(TypeEnroll _typeEnroll)
        {
            InitializeComponent();
            typeEnroll = _typeEnroll;
        }

        private void EnrollControl_Load(object sender, EventArgs e)
        {
            FigersWrapper fw = new FigersWrapper(new RadioButton[] { F1, F2, F3, F4, F5, F6, F7, F8, F9, F10 }, new Panel[] { N1, N2, N3, N4 }, typeEnroll);
            fw.imageFinger = pbFingerprint;

        }


    }
}
