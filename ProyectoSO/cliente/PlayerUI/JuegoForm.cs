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
    public partial class JuegoForm : Form
    {
        int nForm;
        Socket server; 
        bool le_toca; // DE QUIEN ES EL TURONO
        string casilla;
        string jug1; // Username del jugador amarillo
        string jug2; // Username del jugador rojo
        int jug; // QUIEN ESTA HACIENDO CLICK: Si '1': tira el rojo || Si '2': tira el amarillo
        int[,] tablero = new int[6, 7];
        string[] vector = new string[7];
        SoundPlayer playerganador = new SoundPlayer();
        SoundPlayer playerperdedor = new SoundPlayer();
        SoundPlayer playerficha = new SoundPlayer();


        public JuegoForm(int nForm, Socket server, string jug1, string jug2, int jug, bool le_toca)
        {
            this.jug = jug;
            this.jug1 = jug1;
            this.jug2 = jug2;
            InitializeComponent();
            UserLbl.Text = jug1;
            InvitadoLbl.Text = jug2;
            this.nForm = nForm;
            //nFormLbl.Text = Convert.ToString(jug);
            this.server = server;
            this.le_toca = le_toca;

            btn00.Click += new EventHandler(this.Btn_Click);
            btn01.Click += new EventHandler(this.Btn_Click);
            btn02.Click += new EventHandler(this.Btn_Click);
            btn03.Click += new EventHandler(this.Btn_Click);
            btn04.Click += new EventHandler(this.Btn_Click);
            btn05.Click += new EventHandler(this.Btn_Click);
            btn06.Click += new EventHandler(this.Btn_Click);
            btn10.Click += new EventHandler(this.Btn_Click);
            btn11.Click += new EventHandler(this.Btn_Click);
            btn12.Click += new EventHandler(this.Btn_Click);
            btn13.Click += new EventHandler(this.Btn_Click);
            btn14.Click += new EventHandler(this.Btn_Click);
            btn15.Click += new EventHandler(this.Btn_Click);
            btn16.Click += new EventHandler(this.Btn_Click);
            btn20.Click += new EventHandler(this.Btn_Click);
            btn21.Click += new EventHandler(this.Btn_Click);
            btn22.Click += new EventHandler(this.Btn_Click);
            btn23.Click += new EventHandler(this.Btn_Click);
            btn24.Click += new EventHandler(this.Btn_Click);
            btn25.Click += new EventHandler(this.Btn_Click);
            btn26.Click += new EventHandler(this.Btn_Click);
            btn30.Click += new EventHandler(this.Btn_Click);
            btn31.Click += new EventHandler(this.Btn_Click);
            btn32.Click += new EventHandler(this.Btn_Click);
            btn33.Click += new EventHandler(this.Btn_Click);
            btn34.Click += new EventHandler(this.Btn_Click);
            btn35.Click += new EventHandler(this.Btn_Click);
            btn36.Click += new EventHandler(this.Btn_Click);
            btn40.Click += new EventHandler(this.Btn_Click);
            btn41.Click += new EventHandler(this.Btn_Click);
            btn42.Click += new EventHandler(this.Btn_Click);
            btn43.Click += new EventHandler(this.Btn_Click);
            btn44.Click += new EventHandler(this.Btn_Click);
            btn45.Click += new EventHandler(this.Btn_Click);
            btn46.Click += new EventHandler(this.Btn_Click);
            btn50.Click += new EventHandler(this.Btn_Click);
            btn51.Click += new EventHandler(this.Btn_Click);
            btn52.Click += new EventHandler(this.Btn_Click);
            btn53.Click += new EventHandler(this.Btn_Click);
            btn54.Click += new EventHandler(this.Btn_Click);
            btn55.Click += new EventHandler(this.Btn_Click);
            btn56.Click += new EventHandler(this.Btn_Click);
        }

        //
        //Boton que recibe la casilla presionada, pone la ficha y envia al otro jugador la ficha que se ha puesto.
        //Si alguien ha ganado, envia al servidor toda la informacion correspondiente a la partida y envia a cada jugador los mensajes correspondientes
        //
        void Btn_Click(Object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            casilla = (string)clickedButton.Tag;
            string fila = casilla.Substring(0, 1);
            string columna = casilla.Substring(1, 1);
            int f = Convert.ToInt32(fila);
            int c = Convert.ToInt32(columna);
            playerficha.SoundLocation = "ficha.wav";

            bool tirada_ok = false;
            bool puede_tirar = false;
            // comprobar si se puede tirar
            if (f== 0 || tablero[f-1, c] != 0)
            {
                puede_tirar = true;
            }


            if (le_toca && jug==1 && puede_tirar)
            {
                TurnoLbl.Text = "NO ES TU TURNO";
                clickedButton.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                tirada_ok = true;
                tablero[f, c] = 1;// ' jugador 1': amarillo
                string jugada = "7/" + nForm + "/" + "1/" + f + "/" + c + "/" + jug1 + "/" + jug2;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(jugada);
                server.Send(msg);
            }
            else if(le_toca && jug==2 && puede_tirar)
            {
                TurnoLbl.Text = "NO ES TU TURNO";
                clickedButton.BackgroundImage = Image.FromFile("ficha_roja.png");
                tirada_ok = true;
                tablero[f, c] = 2; // 'jugador 2': rojo
                string jugada = "7/" + nForm + "/" + "2/" + f + "/" + c + "/" + jug1 + "/" + jug2;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(jugada);
                server.Send(msg);
            }
            if (tirada_ok)
            {
                le_toca = false;
                playerficha.Play();

                int ganador = jugador_ganador(f, c);
                if (ganador != -1)
                {
                    timer3.Start();
                   if(ganador==1)
                    {
                        string fecha = DateTime.Now.ToString("dd-MM-yyyy");
                        string enviar_ganador = "8/" + jug1 + "/" + jug2 + "/" + nForm + "/" + fecha;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(enviar_ganador);
                        server.Send(msg);
                    }
                   else if(ganador==2)
                    {
                        string fecha = DateTime.Now.ToString("dd-MM-yyyy");
                        string enviar_ganador = "8/" + jug2 + "/" + jug1 + "/" + nForm + "/" + fecha;
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(enviar_ganador);
                        server.Send(msg);
                    }
                }

            }
            
        }

        //
        //Comprueba todas las combinaciones para hacer 4 en raya y en caso de hacer 4 en raya retorna el jugador que ha ganado
        //
        private int jugador_ganador(int fila, int columna)
        {
            int jugador = tablero[fila, columna];
            int cont = 0;
            int i = 0;

            // Comprobar horizontal
            for (int c=0; c<7; c++)
            {
                if (tablero[fila, c] == jugador)
                {
                    cont++;
                    vector[i] = Convert.ToString(fila) + Convert.ToString(c);
                    i++;                    
                }
                else
                {
                    cont = 0;
                    vector = new string[7];
                    i = 0;
                }
                if (cont >= 4)
                {
                    return jugador;
                }
            }

            // Comprobar vertical
            cont = 0;
            for (int f = 0; f < 6; f++)
            {
                if (tablero[f, columna] == jugador)
                {                    
                    cont++;
                    vector[i] = Convert.ToString(f) + Convert.ToString(columna);
                    i++;
                }
                else
                {
                    cont = 0;
                    vector = new string[7];
                    i = 0;
                }
                if (cont >= 4)
                {
                    return jugador;
                }
            }

            // Comprobar diagonal 1
            cont = 0;
            vector = new string[7];
            i = 0;
            for (int d=0; d<3; d++)// Diagonal derecha arriba
            {
                if (fila + d <= 5 && columna + d <= 6 && tablero[fila+d, columna+d] == jugador)
                {
                    cont++;
                    vector[i] = Convert.ToString(fila + d) + Convert.ToString(columna + d);
                    i++;
                }
                else
                {
                    break;
                }
            }
            for (int d = 1; d <= 3; d++)// Diagonal izquierda abajo
            {
                if (fila - d >= 0 && columna - d >= 0 && tablero[fila - d, columna - d] == jugador)
                {
                    cont++;
                    vector[i] = Convert.ToString(fila - d) + Convert.ToString(columna - d);
                    i++;
                }
                else
                {
                    break;
                }
            }
            if (cont >= 4)
            {
                return jugador;
            }
            
            // Comprobar diagonal 2
            cont = 0;
            vector = new string[7];
            i = 0;
            for (int d = 0; d < 3; d++)// Diagonal izquirda arriba
            {
                if (fila + d <= 5 && columna - d >= 0 && tablero[fila + d, columna - d] == jugador)
                {
                    cont++;
                    vector[i] = Convert.ToString(fila + d) + Convert.ToString(columna - d);
                    i++;
                }
                else
                {
                    break;
                }
            }
            for (int d = 1; d <= 3; d++)// Diagonal derecha abajo
            {
                if (fila - d >= 0 && columna + d <= 6 && tablero[fila - d, columna + d] == jugador)
                {
                    cont++;
                    vector[i] = Convert.ToString(fila - d) + Convert.ToString(columna + d);
                    i++;
                }
                else
                {
                    break;
                }
            }
            if (cont >= 4)
            {
                return jugador;
            }

            return -1;
        }

        //
        //Pone el mensaje y el audio correspondiente al ganador o perdedor
        //
        public void TomaGanador(string ganador)
        {
            playerganador.SoundLocation = "ganador.wav";
            playerperdedor.SoundLocation = "derrota.wav";

            if (jug == 1 && String.Compare(jug1, ganador)==0)
            {
                GPLbl.Text = "HAS GANADOO!!!";
                playerganador.Play();
            }
            else if(jug ==2 && String.Compare(jug1, ganador) == 0)
            {
                GPLbl.Text = "HAS PERDIDO :(";
                playerperdedor.Play();
            }

            else if(jug == 1 && String.Compare(jug2, ganador) == 0)
            {
                GPLbl.Text = "HAS PERDIDO :(";
                playerperdedor.Play();
            }
            else if (jug == 2 && String.Compare(jug2, ganador) == 0)
            {
                GPLbl.Text = "HAS GANAOOO!!!";
                playerganador.Play();
            }
        }
        //
        // Recibe la ficha del servidor y la pone en el lugar correspondiente
        //
        public void TomaRespuesta(int jug, int fila, int columna)
        {

            playerficha.SoundLocation = "ficha.wav";
            if (jug == 2 && le_toca == false) //Jugador 1
            {

                playerficha.Play();
                tablero[fila, columna] = 1;
                le_toca = true;
                TurnoLbl.Text = "ES TU TURNO";

                if (fila == 0 && columna == 0)
                {
                    btn00.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 0 && columna == 1)
                {
                    btn01.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 0 && columna == 2)
                {
                    btn02.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 0 && columna == 3)
                {
                    btn03.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 0 && columna == 4)
                {
                    btn04.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 0 && columna == 5)
                {
                    btn05.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 0 && columna == 6)
                {
                    btn06.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 1 && columna == 0)
                {
                    btn10.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 1 && columna == 1)
                {
                    btn11.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 1 && columna == 2)
                {
                    btn12.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 1 && columna == 3)
                {
                    btn13.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 1 && columna == 4)
                {
                    btn14.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 1 && columna == 5)
                {
                    btn15.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 1 && columna == 6)
                {
                    btn16.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }
                if (fila == 2 && columna == 0)
                {
                    btn20.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 2 && columna == 1)
                {
                    btn21.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 2 && columna == 2)
                {
                    btn22.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 2 && columna == 3)
                {
                    btn23.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 2 && columna == 4)
                {
                    btn24.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 2 && columna == 5)
                {
                    btn25.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 2 && columna == 6)
                {
                    btn26.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }
                if (fila == 3 && columna == 0)
                {
                    btn30.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 3 && columna == 1)
                {
                    btn31.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 3 && columna == 2)
                {
                    btn32.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 3 && columna == 3)
                {
                    btn33.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 3 && columna == 4)
                {
                    btn34.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 3 && columna == 5)
                {
                    btn35.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 3 && columna == 6)
                {
                    btn36.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }
                if (fila == 4 && columna == 0)
                {
                    btn40.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 4 && columna == 1)
                {
                    btn41.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 4 && columna == 2)
                {
                    btn42.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 4 && columna == 3)
                {
                    btn43.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 4 && columna == 4)
                {
                    btn44.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 4 && columna == 5)
                {
                    btn44.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 4 && columna == 6)
                {
                    btn46.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }
                if (fila == 5 && columna == 0)
                {
                    btn50.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 5 && columna == 1)
                {
                    btn51.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 5 && columna == 2)
                {
                    btn52.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 5 && columna == 3)
                {
                    btn53.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 5 && columna == 4)
                {
                    btn54.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 5 && columna == 5)
                {
                    btn55.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }

                if (fila == 5 && columna == 6)
                {
                    btn56.BackgroundImage = Image.FromFile("ficha_amarilla.png");
                }
            }
            else if (jug == 1 && le_toca == false) //Jugador 2
            {
                playerficha.Play();
                tablero[fila, columna] = 2;
                le_toca = true;
                TurnoLbl.Text = "ES TU TURNO";

                if (fila == 0 && columna == 0) 
                {
                    btn00.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 0 && columna == 1)
                {
                    btn01.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 0 && columna == 2)
                {
                    btn02.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 0 && columna == 3)
                {
                    btn03.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 0 && columna == 4)
                {
                    btn04.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 0 && columna == 5)
                {
                    btn05.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 0 && columna == 6)
                {
                    btn06.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 1 && columna == 0)
                {
                    btn10.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 1 && columna == 1)
                {
                    btn11.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 1 && columna == 2)
                {
                    btn12.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 1 && columna == 3)
                {
                    btn13.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 1 && columna == 4)
                {
                    btn14.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 1 && columna == 5)
                {
                    btn15.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 1 && columna == 6)
                {
                    btn16.BackgroundImage = Image.FromFile("ficha_roja.png");
                }
                if (fila == 2 && columna == 0)
                {
                    btn20.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 2 && columna == 1)
                {
                    btn21.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 2 && columna == 2)
                {
                    btn22.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 2 && columna == 3)
                {
                    btn23.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 2 && columna == 4)
                {
                    btn24.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 2 && columna == 5)
                {
                    btn25.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 2 && columna == 6)
                {
                    btn26.BackgroundImage = Image.FromFile("ficha_roja.png");
                }
                if (fila == 3 && columna == 0)
                {
                    btn30.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 3 && columna == 1)
                {
                    btn31.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 3 && columna == 2)
                {
                    btn32.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 3 && columna == 3)
                {
                    btn33.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 3 && columna == 4)
                {
                    btn34.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 3 && columna == 5)
                {
                    btn35.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 3 && columna == 6)
                {
                    btn36.BackgroundImage = Image.FromFile("ficha_roja.png");
                }
                if (fila == 4 && columna == 0)
                {
                    btn40.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 4 && columna == 1)
                {
                    btn41.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 4 && columna == 2)
                {
                    btn42.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 4 && columna == 3)
                {
                    btn43.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 4 && columna == 4)
                {
                    btn44.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 4 && columna == 5)
                {
                    btn44.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 4 && columna == 6)
                {
                    btn46.BackgroundImage = Image.FromFile("ficha_roja.png");
                }
                if (fila == 5 && columna == 0)
                {
                    btn50.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 5 && columna == 1)
                {
                    btn51.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 5 && columna == 2)
                {
                    btn52.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 5 && columna == 3)
                {
                    btn53.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 5 && columna == 4)
                {
                    btn54.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 5 && columna == 5)
                {
                    btn55.BackgroundImage = Image.FromFile("ficha_roja.png");
                }

                if (fila == 5 && columna == 6)
                {
                    btn56.BackgroundImage = Image.FromFile("ficha_roja.png");
                }
            }
        
        }

        public void JuegoForm_Load(object sender, EventArgs e)
        {
            timer3.Interval = 300;
            timer3.Enabled = false;

            if (jug == 1)
            {
                TurnoLbl.Text = "ES TU TURNO";
            }
            if (jug == 2)
            {
                TurnoLbl.Text = "NO ES TU TURNO";
            }
        }

        public void timer3_Tick(object sender, EventArgs e)
        {
            {
                int j = vector.Length;

                for (int i = 0; i < j; i++)
                {
                    casilla = vector[i];

                    if (String.Compare(casilla, "00") == 0)
                    {
                        btn00.Visible = !btn00.Visible;
                    }
                    else if (String.Compare(casilla, "01") == 0)
                    {
                        btn01.Visible = !btn01.Visible;
                    }
                    if (String.Compare(casilla, "02") == 0)
                    {
                        btn02.Visible = !btn02.Visible;
                    }
                    if (String.Compare(casilla, "03") == 0)
                    {
                        btn03.Visible = !btn03.Visible;
                    }
                    if (String.Compare(casilla, "04") == 0)
                    {
                        btn04.Visible = !btn04.Visible;
                    }
                    if (String.Compare(casilla, "05") == 0)
                    {
                        btn05.Visible = !btn05.Visible;
                    }
                    if (String.Compare(casilla, "06") == 0)
                    {
                        btn06.Visible = !btn06.Visible;
                    }
                    if (String.Compare(casilla, "10") == 0)
                    {
                        btn10.Visible = !btn10.Visible;
                    }
                    if (String.Compare(casilla, "11") == 0)
                    {
                        btn11.Visible = !btn11.Visible;
                    }
                    if (String.Compare(casilla, "12") == 0)
                    {
                        btn12.Visible = !btn12.Visible;
                    }
                    if (String.Compare(casilla, "13") == 0)
                    {
                        btn13.Visible = !btn13.Visible;
                    }
                    if (String.Compare(casilla, "14") == 0)
                    {
                        btn14.Visible = !btn14.Visible;
                    }
                    if (String.Compare(casilla, "15") == 0)
                    {
                        btn15.Visible = !btn15.Visible;
                    }
                    if (String.Compare(casilla, "16") == 0)
                    {
                        btn16.Visible = !btn16.Visible;
                    }
                    if (String.Compare(casilla, "20") == 0)
                    {
                        btn20.Visible = !btn20.Visible;
                    }
                    if (String.Compare(casilla, "21") == 0)
                    {
                        btn21.Visible = !btn21.Visible;
                    }
                    if (String.Compare(casilla, "22") == 0)
                    {
                        btn22.Visible = !btn22.Visible;
                    }
                    if (String.Compare(casilla, "23") == 0)
                    {
                        btn23.Visible = !btn23.Visible;
                    }
                    if (String.Compare(casilla, "24") == 0)
                    {
                        btn24.Visible = !btn24.Visible;
                    }
                    if (String.Compare(casilla, "25") == 0)
                    {
                        btn25.Visible = !btn25.Visible;
                    }
                    if (String.Compare(casilla, "26") == 0)
                    {
                        btn26.Visible = !btn26.Visible;
                    }
                    if (String.Compare(casilla, "30") == 0)
                    {
                        btn30.Visible = !btn30.Visible;
                    }
                    if (String.Compare(casilla, "31") == 0)
                    {
                        btn31.Visible = !btn31.Visible;
                    }
                    if (String.Compare(casilla, "32") == 0)
                    {
                        btn32.Visible = !btn32.Visible;
                    }
                    if (String.Compare(casilla, "33") == 0)
                    {
                        btn33.Visible = !btn33.Visible;
                    }
                    if (String.Compare(casilla, "34") == 0)
                    {
                        btn34.Visible = !btn34.Visible;
                    }
                    if (String.Compare(casilla, "35") == 0)
                    {
                        btn35.Visible = !btn35.Visible;
                    }
                    if (String.Compare(casilla, "36") == 0)
                    {
                        btn36.Visible = !btn36.Visible;
                    }
                    if (String.Compare(casilla, "40") == 0)
                    {
                        btn40.Visible = !btn40.Visible;
                    }
                    if (String.Compare(casilla, "41") == 0)
                    {
                        btn41.Visible = !btn41.Visible;
                    }
                    if (String.Compare(casilla, "42") == 0)
                    {
                        btn42.Visible = !btn42.Visible;
                    }
                    if (String.Compare(casilla, "43") == 0)
                    {
                        btn43.Visible = !btn43.Visible;
                    }
                    if (String.Compare(casilla, "44") == 0)
                    {
                        btn44.Visible = !btn44.Visible;
                    }
                    if (String.Compare(casilla, "45") == 0)
                    {
                        btn45.Visible = !btn45.Visible;
                    }
                    if (String.Compare(casilla, "46") == 0)
                    {
                        btn46.Visible = !btn46.Visible;
                    }
                    if (String.Compare(casilla, "50") == 0)
                    {
                        btn50.Visible = !btn50.Visible;
                    }
                    if (String.Compare(casilla, "51") == 0)
                    {
                        btn51.Visible = !btn51.Visible;
                    }
                    if (String.Compare(casilla, "52") == 0)
                    {
                        btn52.Visible = !btn52.Visible;
                    }
                    if (String.Compare(casilla, "53") == 0)
                    {
                        btn53.Visible = !btn53.Visible;
                    }
                    if (String.Compare(casilla, "54") == 0)
                    {
                        btn54.Visible = !btn54.Visible;
                    }
                    if (String.Compare(casilla, "55") == 0)
                    {
                        btn55.Visible = !btn55.Visible;
                    }
                    if (String.Compare(casilla, "56") == 0)
                    {
                        btn56.Visible = !btn56.Visible;
                    }
                }
            }
        }

        //
        //Timer para el parpadeo de las fichas
        //
        public void timer1_Tick(object sender, EventArgs e)
        {
            {
                int j = vector.Length;

                for (int i = 0; i < j; i++)
                {
                    casilla = vector[i];

                    if (String.Compare(casilla, "00") == 0)
                    {
                        btn00.Visible = !btn00.Visible;
                    }
                    else if (String.Compare(casilla, "01") == 0)
                    {
                        btn01.Visible = !btn01.Visible;
                    }
                    if (String.Compare(casilla, "02") == 0)
                    {
                        btn02.Visible = !btn02.Visible;
                    }
                    if (String.Compare(casilla, "03") == 0)
                    {
                        btn03.Visible = !btn03.Visible;
                    }
                    if (String.Compare(casilla, "04") == 0)
                    {
                        btn04.Visible = !btn04.Visible;
                    }
                    if (String.Compare(casilla, "05") == 0)
                    {
                        btn05.Visible = !btn05.Visible;
                    }
                    if (String.Compare(casilla, "06") == 0)
                    {
                        btn06.Visible = !btn06.Visible;
                    }
                    if (String.Compare(casilla, "10") == 0)
                    {
                        btn10.Visible = !btn10.Visible;
                    }
                    if (String.Compare(casilla, "11") == 0)
                    {
                        btn11.Visible = !btn11.Visible;
                    }
                    if (String.Compare(casilla, "12") == 0)
                    {
                        btn12.Visible = !btn12.Visible;
                    }
                    if (String.Compare(casilla, "13") == 0)
                    {
                        btn13.Visible = !btn13.Visible;
                    }
                    if (String.Compare(casilla, "14") == 0)
                    {
                        btn14.Visible = !btn14.Visible;
                    }
                    if (String.Compare(casilla, "15") == 0)
                    {
                        btn15.Visible = !btn15.Visible;
                    }
                    if (String.Compare(casilla, "16") == 0)
                    {
                        btn16.Visible = !btn16.Visible;
                    }
                    if (String.Compare(casilla, "20") == 0)
                    {
                        btn20.Visible = !btn20.Visible;
                    }
                    if (String.Compare(casilla, "21") == 0)
                    {
                        btn21.Visible = !btn21.Visible;
                    }
                    if (String.Compare(casilla, "22") == 0)
                    {
                        btn22.Visible = !btn22.Visible;
                    }
                    if (String.Compare(casilla, "23") == 0)
                    {
                        btn23.Visible = !btn23.Visible;
                    }
                    if (String.Compare(casilla, "24") == 0)
                    {
                        btn24.Visible = !btn24.Visible;
                    }
                    if (String.Compare(casilla, "25") == 0)
                    {
                        btn25.Visible = !btn25.Visible;
                    }
                    if (String.Compare(casilla, "26") == 0)
                    {
                        btn26.Visible = !btn26.Visible;
                    }
                    if (String.Compare(casilla, "30") == 0)
                    {
                        btn30.Visible = !btn30.Visible;
                    }
                    if (String.Compare(casilla, "31") == 0)
                    {
                        btn31.Visible = !btn31.Visible;
                    }
                    if (String.Compare(casilla, "32") == 0)
                    {
                        btn32.Visible = !btn32.Visible;
                    }
                    if (String.Compare(casilla, "33") == 0)
                    {
                        btn33.Visible = !btn33.Visible;
                    }
                    if (String.Compare(casilla, "34") == 0)
                    {
                        btn34.Visible = !btn34.Visible;
                    }
                    if (String.Compare(casilla, "35") == 0)
                    {
                        btn35.Visible = !btn35.Visible;
                    }
                    if (String.Compare(casilla, "36") == 0)
                    {
                        btn36.Visible = !btn36.Visible;
                    }
                    if (String.Compare(casilla, "40") == 0)
                    {
                        btn40.Visible = !btn40.Visible;
                    }
                    if (String.Compare(casilla, "41") == 0)
                    {
                        btn41.Visible = !btn41.Visible;
                    }
                    if (String.Compare(casilla, "42") == 0)
                    {
                        btn42.Visible = !btn42.Visible;
                    }
                    if (String.Compare(casilla, "43") == 0)
                    {
                        btn43.Visible = !btn43.Visible;
                    }
                    if (String.Compare(casilla, "44") == 0)
                    {
                        btn44.Visible = !btn44.Visible;
                    }
                    if (String.Compare(casilla, "45") == 0)
                    {
                        btn45.Visible = !btn45.Visible;
                    }
                    if (String.Compare(casilla, "46") == 0)
                    {
                        btn46.Visible = !btn46.Visible;
                    }
                    if (String.Compare(casilla, "50") == 0)
                    {
                        btn50.Visible = !btn50.Visible;
                    }
                    if (String.Compare(casilla, "51") == 0)
                    {
                        btn51.Visible = !btn51.Visible;
                    }
                    if (String.Compare(casilla, "52") == 0)
                    {
                        btn52.Visible = !btn52.Visible;
                    }
                    if (String.Compare(casilla, "53") == 0)
                    {
                        btn53.Visible = !btn53.Visible;
                    }
                    if (String.Compare(casilla, "54") == 0)
                    {
                        btn54.Visible = !btn54.Visible;
                    }
                    if (String.Compare(casilla, "55") == 0)
                    {
                        btn55.Visible = !btn55.Visible;
                    }
                    if (String.Compare(casilla, "56") == 0)
                    {
                        btn56.Visible = !btn56.Visible;
                    }
                }
            }
        }
    }
}
