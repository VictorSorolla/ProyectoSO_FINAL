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
    public partial class EstadisticasForm : Form
    {
        LoginForm LF = new LoginForm();
        int code;
        string contrincante;
        delegate void DelegadorParaEstadisticas(string mensaje);

        public EstadisticasForm()
        {
            InitializeComponent();
        }


        
        private void EstadisticasForm_Load(object sender, EventArgs e)
        {
            string mensaje = "10/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            LoginForm.server.Send(msg);
        }

        //
        //Pone en la matriz todos los jugadores registrados
        //
        public void PonInvokeTodosJugadores(string pieces)
        {
            DelegadorParaEstadisticas delegadoEstad = new DelegadorParaEstadisticas(PonTodosLosJugadoresEnMatriz);
            matrizUsuarios.Invoke(delegadoEstad, new object[] { pieces });
        }
        //
        //Pone en la matriz todos los jugadores con quien se ha jugado alguna aprtida
        //
        public void PonInvokeJugador(string pieces)
        {
            DelegadorParaEstadisticas delegadoEstad = new DelegadorParaEstadisticas(PonJugadoresEnMatriz);
            matrizJugadores.Invoke(delegadoEstad, new object[] { pieces });
        }
        //
        //Escribe en un label cuantas partidas has ganado contra el jugador especificado
        //
        public void PonInvokeGanadas(string pieces)
        {
            DelegadorParaEstadisticas delegadoEstad = new DelegadorParaEstadisticas(PonPartidasGanadas);
            GanadasLbl.Invoke(delegadoEstad, new object[] { pieces });
        }
        //
        //Pone en la matriz el historial de partidas ganadas
        //
        public void PonInvokePartidas(string pieces)
        {
            DelegadorParaEstadisticas delegadoEstad = new DelegadorParaEstadisticas(PonResultadosPartida);
            matrizResultados.Invoke(delegadoEstad, new object[] { pieces });
        }
        //
        //Pone en la matriz todos los jugadores registrados
        //
        public void PonTodosLosJugadoresEnMatriz(string mensaje)
        {
            matrizUsuarios.ColumnCount = 1;
            matrizUsuarios.ColumnHeadersVisible = true;
            matrizUsuarios.RowHeadersVisible = false;
            matrizUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            matrizUsuarios.Columns[0].HeaderText = "Esta es la lista de jugadores registrados: ";
            string[] piezas = mensaje.Split('/');
            code = Convert.ToInt32(piezas[0]);
            matrizUsuarios.RowCount = code;
            int i = 0;
            while (i < code)
            {
                string[] nombres = new string[10];
                nombres[i] = piezas[i + 1];
                matrizUsuarios.Rows[i].Cells[0].Value = nombres[i];

                i++;
            }
            matrizUsuarios.Refresh();
        }
        //
        //Pone en la matriz el historial de partidas ganadas
        //
        public void PonResultadosPartida(string mensaje)
        {
            string[] piezas = mensaje.Split('/');
            code = Convert.ToInt32(piezas[0]);
            if (code == 0)
                MessageBox.Show("NO has jugado ninguna partida con el jugador seleccionado");
            else
            {
                matrizResultados.ColumnCount = 2;
                matrizResultados.ColumnHeadersVisible = true;
                matrizResultados.RowHeadersVisible = false;
                matrizResultados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                matrizResultados.RowCount = code;
                int i = 0;
                while (i < code)
                {
                    string[] nombres = new string[10];
                    nombres[i] = piezas[i + 1];
                    matrizResultados.Rows[i].Cells[0].Value = "Ganador: ";
                    matrizResultados.Rows[i].Cells[1].Value = nombres[i];

                    i++;
                }
                matrizResultados.Refresh();
            }
        }
        //
        //Escribe en un label cuantas partidas has ganado contra el jugador especificado
        //
        public void PonPartidasGanadas(string numero)
        {
            string imprime = "El jugador " + jugador.Text + " ha ganado " + numero + " partida(s)!!";
            GanadasLbl.Text = imprime;
        }
        //
        //Pone en la matriz todos los jugadores con quien se ha jugado alguna partida
        //
        public void PonJugadoresEnMatriz(string mensaje)
        {
            string[] piezas = mensaje.Split('/');
            code = Convert.ToInt32(piezas[0]);
            if (code == 0)
                MessageBox.Show("NO has jugado ninguna partida con NINGÚN jugador");
            else
            {
                matrizJugadores.ColumnCount = 1;
                matrizJugadores.ColumnHeadersVisible = true;
                matrizJugadores.RowHeadersVisible = false;
                matrizJugadores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                matrizJugadores.Columns[0].HeaderText = "Estos son los jugadores con los que has jugado una partida ";
                matrizJugadores.RowCount = code;
                int i = 0;
                while (i < code)
                {
                    string[] nombres = new string[10];
                    nombres[i] = piezas[i + 1];
                    matrizJugadores.Rows[i].Cells[0].Value = nombres[i];

                    i++;
                }
                matrizJugadores.Refresh();
            }
        }
        //
        // Botón para ver los nombres de usuarios de los jugadores con los que he jugado alguna partida.
        //
        private void JugadoresBTN_Click(object sender, EventArgs e)
        {
            string mensaje = "9/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            LoginForm.server.Send(msg);
        }
        //
        // Función para seleccionar el jugador del que quiero ver los resultados de las partidas jugadas contra mi.
        //
        private void matrizUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            jugadoresBox.AppendText(Convert.ToString(matrizUsuarios.CurrentRow.Cells[0].Value) + Environment.NewLine);
            contrincante = Convert.ToString(matrizUsuarios.CurrentRow.Cells[0].Value);
        }
        //
        // Botón para ver los resultados de las partidas jugadas contra mi. 
        //
        private void ResultadosBTN_Click(object sender, EventArgs e)
        {
            string mensaje = "11/" + contrincante;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            LoginForm.server.Send(msg);
        }

        private void CloseBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //
        // Botón para ver cuantas partidas has ganado contra el jugador especificado
        //
        private void PartidasGanadasBtn_Click(object sender, EventArgs e)
        {
            string mensaje = "12/" + jugador.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            LoginForm.server.Send(msg);
        }
    }
}
