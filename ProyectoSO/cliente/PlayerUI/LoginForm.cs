using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ProyectoSO
{
    public partial class LoginForm : Form
    {

        public static Socket server;

        public LoginForm()
        {
            InitializeComponent();
        }
        //
        //Timer para mover de login a registro
        //
        private void timer1_Tick(object sender, EventArgs e) 
        {
            if (PanelLogin.Location.X > -737)
            {
                PanelLogin.Location = new Point(PanelLogin.Location.X - 20, PanelLogin.Location.Y);
            }
            else
            {
                timer1.Stop();
                LoginParteBtn.Enabled = true;
                RegistroParteBtn.Enabled = true;
            }
        }
        //
        //Timer para mover registro a login
        //
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (PanelLogin.Location.X < 0)
            {
                PanelLogin.Location = new Point(PanelLogin.Location.X + 20, PanelLogin.Location.Y);
            }
            else
            {
                timer2.Stop();
                LoginParteBtn.Enabled = true;
                RegistroParteBtn.Enabled = true;
            }
        }
        //
        //Boton para mover de registro a login
        //
        private void RegistroParteBtn_Click(object sender, EventArgs e)
        {
            timer1.Start();
            LoginParteBtn.Enabled = false;
            RegistroParteBtn.Enabled = false;
        }
        //
        //Boton para mover de login a registro
        //
        private void LoginParteBtn_Click(object sender, EventArgs e)
        {
            timer2.Start();
            LoginParteBtn.Enabled = false;
            RegistroParteBtn.Enabled = false;
        }
        //
        //Boton para LOGIN
        //
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(Password.Text)) || (string.IsNullOrEmpty(Username.Text)))
                MessageBox.Show("Es necesario añadir el usuario y el password");
            else
            {
                IPAddress direc = IPAddress.Parse("147.83.117.22");
                //192.168.56.101
                //147.83.117.22
                IPEndPoint ipep = new IPEndPoint(direc, 50068);

                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                PrincipalForm PF = new PrincipalForm(server);
                try
                {
                    server.Connect(ipep); //Intentamos conectar el socket
                }
                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }

                //pongo en marcha el thread que atenderá los mensajes del servidor

                string mensaje = "1/" + Username.Text + "/" + Password.Text;
                // Enviamos al servidor el nombre tecleado.
                byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                this.Hide();
                //PrincipalForm PF = new PrincipalForm(server);
                PrincipalForm.user = Username.Text;
                PF.ShowDialog();
                this.Show();
            }
        }
        //
        //Boton para el REGISTRO
        //
        private void RegistrarBtn_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(pass2.Text)) || (string.IsNullOrEmpty(user.Text)) || (string.IsNullOrEmpty(pass1.Text)))
                MessageBox.Show("Es necesario añadir el usuario y el password");
            else
            {
                IPAddress direc = IPAddress.Parse("147.83.117.22");
                IPEndPoint ipep = new IPEndPoint(direc, 50068);

                //Creamos el socket 
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    server.Connect(ipep); //Intentamos conectar el socket

                    int res;
                    res = string.Compare(pass1.Text, pass2.Text);
                    if (res == 0)
                    {
                        string mensaje = "3/" + user.Text + "/" + pass1.Text;
                        // Enviamos al servidor el nombre tecleado
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);

                        MessageBox.Show("Usuario registrado! Usted ya puede iniciar sesión con el usuario creado.");
                    }
                    else
                        MessageBox.Show("Las contraseñas no coinciden. Por favor vuelva a teclearlas y clique al botón 'SignUp'");
                }
                catch (SocketException ex)
                {
                    //Si hay excepcion imprimimos error y salimos del programa con return 
                    MessageBox.Show("No he podido conectar con el servidor");
                    return;
                }
            }
        }
        //
        //Boton para cerrar la aplicacion
        //
        private void CloseBTN_Click(object sender, EventArgs e) 
        {
            Application.Exit();
        }
    }
}
