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
using System.Media;

namespace ProyectoSO
{
    public partial class PrincipalForm : Form
    {
        
        //Variables globales
        Socket server;
        public static string user;
        public static string jugador1;
        public static string jugador2;
        Thread atender;
        string message;
        public static string invitado;
        string texto;
        string jug1;
        string jug2;
        int cont;
        int forms_s;

        List<string> invitados = new List<string>();
        List<JuegoForm> formularios = new List<JuegoForm>();

        EstadisticasForm EF = new EstadisticasForm();

        delegate void DelegadoParaEscribir(int code, string[] trozos);
        delegate void DelegadoParaChat(string[] trozos);
        delegate void DelegadorParaEstadisticas(string mensaje);

        public PrincipalForm(Socket s)
        {
            InitializeComponent();
            this.server = s;
            hideSubMenu();
        }

        private void PrincipalForm_Load(object sender, EventArgs e)
        {
            ThreadStart ts = delegate { AtenderServidor(); };
            atender = new Thread(ts);
            atender.Start();
        }

        private void hideSubMenu()
        {
            panelMediaSubMenu.Visible = false;
            panelToolsSubMenu.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }
        //
        //Función para atender las peticiones del servidor.
        //
        private void AtenderServidor()
        {
            while (true)
            {
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                string mssg = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                string[] trozos = mssg.Split('-');
                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje = trozos[1];
                int cont_f = formularios.Count;

                switch (codigo)
                {
                    case 1: //LOGIN
                        string[] piezas = mensaje.Split('/');
                        int code = Convert.ToInt32(piezas[0]);
                        if (code == 11)
                        {
                            MessageBox.Show("El usuario y la contraseña no coinciden. Por favor intente INCIAR SESIÓN de nuevo.");
                            this.Close();
                        }
                        else
                        {
                            DelegadoParaEscribir delegado = new DelegadoParaEscribir(PonConectado);
                            matriz.Invoke(delegado, new object[] { code, piezas });
                        }
                        break;

                    case 2: //ELIMINAR
                        MessageBox.Show(mensaje);
                    break;

                    case 4: // El usuario invitado recibe la invitacion y envia su respuesta correspondiente

                        string[] msg_form = mensaje.Split('/');
                        forms_s = Convert.ToInt32(msg_form[1]);
                        DialogResult resultado;
                        resultado = MessageBox.Show(msg_form[0] + " te ha enviado una invitación.", "Invitación", MessageBoxButtons.OKCancel);
                        if (resultado == DialogResult.OK)
                        {
                            string message = "5/" + msg_form[0] + "/0";   // + "/" + nform
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                            server.Send(msg);
                        }
                        else
                        {
                            string message = "5/" + msg_form[0] + "/1";
                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(message);
                            server.Send(msg);
                        }

                    break;

                    case 5: //El usuario que ha invitado recibe si el invitado ha aceptado o no

                        string[] pieces = mensaje.Split('/');
                        int codigoAcepta = Convert.ToInt32(pieces[1]);
                        if (codigoAcepta == 0)
                        {
                            MessageBox.Show(pieces[0] + " ha ACEPTADO tu invitación");
                            //invitado = pieces[0];
                        }
                        else
                            MessageBox.Show(pieces[0] + " ha RECHAZADO tu invitación");

                    break;

                    case 6: //Recibe los mensajes del chat
                        pieces = mensaje.Split('/');
                        DelegadoParaChat delegadoChat = new DelegadoParaChat(PonMensaje);
                        listBox1.Invoke(delegadoChat, new object[] { pieces });

                    break;

                    case 7: //Pone en marcha los formularios de la partida 
                        
                        string[] jugadores = mensaje.Split('/');
                        jug1 = jugadores[0];
                        jug2 = jugadores[1];
                        ThreadStart ts = delegate { PonerEnMarchaFormulario(); };
                        Thread T = new Thread(ts);
                        T.Start();

                    break;
                        
                    case 8: //Recibe la tirada y la pone en el tablero del otro jugador
                        
                        string[] jugada = mensaje.Split('/');
                        int nform = Convert.ToInt32(jugada[0]);
                        int jug = Convert.ToInt32(jugada[1]);
                        int fila = Convert.ToInt32(jugada[2]);
                        int columna = Convert.ToInt32(jugada[3]);
                        CheckForIllegalCrossThreadCalls = false;
                        formularios[nform].TomaRespuesta(jug, fila, columna);

                    break;
                        
                    case 9: //Recibe el ganador

                        string[] ganadors = mensaje.Split('/');
                        string ganador = (ganadors[0]);
                        int nform_g = Convert.ToInt32(ganadors[1]);
                        formularios[nform_g].TomaGanador(ganador);

                    break;

                    case 10: //Ver jugadores con los que has jugado
                        EF.PonInvokeJugador(mensaje);
                    break;

                    case 11://Ver todos los jugadores registrados en la base de datos
                        EF.PonInvokeTodosJugadores(mensaje);
                    break;
                    case 12://Ver jugadores con quien has jugado
                        EF.PonInvokePartidas(mensaje);
                    break;
                    case 13://Ver las partidas que ha ganado un determiando jugador
                        EF.PonInvokeGanadas(mensaje);
                    break;

                }
            }
        }
        //
        //Botón para desplegar las opciones de usuario.
        //
        private void UserBtn_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMediaSubMenu);
        }

        #region MediaSubMenu
        //
        //Boton que abre el formulario para darse de baja
        //
        private void EliminarBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new EliminarForm());
            hideSubMenu();
        }
        #endregion
        //
        //Botón que abre el formulario para ver las estadísticas.
        //
        private void EstadisticasBtn_Click(object sender, EventArgs e)
        {
            EF.StartPosition = FormStartPosition.Manual;
            EF.Left = 310;
            EF.Top = 240;
            EF.ShowDialog();           
        }
        //
        //Botón que abre el formulario para ver las estadísticas.
        //
        private void OpcionBtn_Click(object sender, EventArgs e)
        {
            showSubMenu(panelToolsSubMenu);
        }
        #region ToolsSubMenu
        //
        //Boton que abre el formulario sobre ayuda del juego
        //
        private void AyudaBtn_Click(object sender, EventArgs e)
        {
            openChildForm(new AyudaForm());
            hideSubMenu();
        }
        #endregion
        //
        //Botón para cerrar la aplicación y desconectarse.
        //
        private void btnExit_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Gray;
            ConectarLbl.Text = "DESCONECTADO";
            ConectarLbl.ForeColor = Color.FromArgb(235, 42, 83);
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            atender.Abort();
            Application.Exit();

        }
        //
        //Función para uso de childforms.
        //
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        //
        //Función para poner el usuario logueado en la matriz de conectados.
        //
        public void PonConectado(int code, string[] trozos)
        {
            matriz.ColumnCount = 1;
            matriz.RowCount = code;
            matriz.ColumnHeadersVisible = true;
            matriz.RowHeadersVisible = false;
            matriz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            matriz.Columns[0].HeaderText = " Conectados ";

            int i = 0;
            while (i < code)
            {
                string[] nombres = new string[10];
                nombres[i] = trozos[i + 1];
                matriz.Rows[i].Cells[0].Value = nombres[i];

                i++;
            }
            matriz.Refresh();
        }
        //
        //Función para poner el mensaje recibido por el servidor en el chat.
        //
        public void PonMensaje(string[] trozos)
        {
            invitado = trozos[0];
            texto = trozos[1];
            texto = trozos[0] + " : " + texto;
            listBox1.Items.Add(texto);
        }
        //
        //Función para seleccionar la persona para invitar.
        //
        private void matriz_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            invitadosBox.AppendText(Convert.ToString(matriz.CurrentRow.Cells[0].Value) + Environment.NewLine);
            invitados.Add(Convert.ToString(matriz.CurrentRow.Cells[0].Value));
        }
        //
        //Función para desconectarse.
        //
        private void DesconectarBtn_Click(object sender, EventArgs e)
        {
            matriz.Hide();
        
            //Mensaje de desconexión
            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            this.BackColor = Color.Gray;
            ConectarLbl.Text = "DESCONECTADO";
            ConectarLbl.ForeColor = Color.FromArgb(235,42,83);
            server.Shutdown(SocketShutdown.Both);
            server.Close();
            atender.Abort();
        }
        //
        //Función enviar la invitación.
        //
        private void invitarBTN_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < invitados.Count; i++)
            {
                message = string.Concat(invitados[i]);
                
            }
            cont = formularios.Count;
            if (string.IsNullOrEmpty(message))
                MessageBox.Show("Selecciona a algún jugador antes de invitar");
            else
            {
                string mensaje = "4/" + message + "/" + cont;

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                invitadosBox.Text = null;
                mensaje = null;
            }
        }
        //
        //Función para inicializar el formulario el juego.
        //
        private void PonerEnMarchaFormulario()
        {
            int jug;
            int res;
            bool le_toca;
            res = string.Compare(user, jug1);
            if (res==0) //Cambiamos el turno 
            {
                jug = 1;
                le_toca = true;
            }
            else
            {
                jug = 2;
                le_toca = false;
            }
            for (int i = cont; i < forms_s; i++)
            {
                formularios.Add(null);
            }
            cont = formularios.Count;

            JuegoForm fj = new JuegoForm(cont, server, jug1, jug2, jug, le_toca);
            formularios.Add(fj);
            fj.ShowDialog();
        }
        //
        //Función para enviar el mensaje del chat en el servidor.
        //
        private void chatBTN_Click(object sender, EventArgs e)
        {
            string mensaje = "6/" + chatBox.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
    }
}
