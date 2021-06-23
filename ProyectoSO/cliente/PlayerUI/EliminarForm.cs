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
    public partial class EliminarForm : Form
    {
        LoginForm eliminar = new LoginForm();
        public EliminarForm()
        {
            InitializeComponent();
        }
        //
        // Botón para eliminar el usuario.
        //
        private void EliminarBtn_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(Password.Text)) || (string.IsNullOrEmpty(Username.Text)))
                MessageBox.Show("Es necesario añadir el usuario y password para dar de baja al usuario");
            else
            {
                string mensaje = "2/" + Username.Text + "/" + Password.Text;
                // Enviamos al servidor la petición.
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                LoginForm.server.Send(msg);
            }

        }

        private void CloseBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
