using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoSO
{
    public partial class AyudaForm : Form //Form para mostrar ayuda sobre el juego
    {
        public AyudaForm()
        {
            InitializeComponent();
        }

        private void CloseBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
