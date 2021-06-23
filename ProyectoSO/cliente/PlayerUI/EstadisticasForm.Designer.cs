
namespace ProyectoSO
{
    partial class EstadisticasForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EstadisticasForm));
            this.JugadoresBTN = new Guna.UI2.WinForms.Guna2Button();
            this.matrizJugadores = new System.Windows.Forms.DataGridView();
            this.matrizUsuarios = new System.Windows.Forms.DataGridView();
            this.jugadoresBox = new System.Windows.Forms.TextBox();
            this.ResultadosBTN = new Guna.UI2.WinForms.Guna2Button();
            this.matrizResultados = new System.Windows.Forms.DataGridView();
            this.CloseBTN = new System.Windows.Forms.Button();
            this.jugador = new System.Windows.Forms.TextBox();
            this.PartidasGanadasBtn = new System.Windows.Forms.Button();
            this.GanadasLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.matrizJugadores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.matrizUsuarios)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.matrizResultados)).BeginInit();
            this.SuspendLayout();
            // 
            // JugadoresBTN
            // 
            this.JugadoresBTN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("JugadoresBTN.BackgroundImage")));
            this.JugadoresBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.JugadoresBTN.BorderColor = System.Drawing.Color.Transparent;
            this.JugadoresBTN.BorderRadius = 8;
            this.JugadoresBTN.BorderThickness = 3;
            this.JugadoresBTN.CheckedState.Parent = this.JugadoresBTN;
            this.JugadoresBTN.CustomImages.Parent = this.JugadoresBTN;
            this.JugadoresBTN.DisabledState.Parent = this.JugadoresBTN;
            this.JugadoresBTN.FillColor = System.Drawing.Color.Transparent;
            this.JugadoresBTN.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.JugadoresBTN.ForeColor = System.Drawing.Color.White;
            this.JugadoresBTN.HoverState.Parent = this.JugadoresBTN;
            this.JugadoresBTN.Location = new System.Drawing.Point(38, 276);
            this.JugadoresBTN.Margin = new System.Windows.Forms.Padding(2);
            this.JugadoresBTN.Name = "JugadoresBTN";
            this.JugadoresBTN.ShadowDecoration.Parent = this.JugadoresBTN;
            this.JugadoresBTN.Size = new System.Drawing.Size(109, 39);
            this.JugadoresBTN.TabIndex = 12;
            this.JugadoresBTN.Click += new System.EventHandler(this.JugadoresBTN_Click);
            // 
            // matrizJugadores
            // 
            this.matrizJugadores.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.matrizJugadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.matrizJugadores.Location = new System.Drawing.Point(23, 124);
            this.matrizJugadores.Margin = new System.Windows.Forms.Padding(2);
            this.matrizJugadores.Name = "matrizJugadores";
            this.matrizJugadores.RowHeadersWidth = 51;
            this.matrizJugadores.RowTemplate.Height = 24;
            this.matrizJugadores.Size = new System.Drawing.Size(141, 141);
            this.matrizJugadores.TabIndex = 13;
            // 
            // matrizUsuarios
            // 
            this.matrizUsuarios.BackgroundColor = System.Drawing.Color.MistyRose;
            this.matrizUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.matrizUsuarios.Location = new System.Drawing.Point(213, 124);
            this.matrizUsuarios.Margin = new System.Windows.Forms.Padding(2);
            this.matrizUsuarios.Name = "matrizUsuarios";
            this.matrizUsuarios.RowHeadersWidth = 51;
            this.matrizUsuarios.Size = new System.Drawing.Size(184, 191);
            this.matrizUsuarios.TabIndex = 37;
            this.matrizUsuarios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.matrizUsuarios_CellClick);
            // 
            // jugadoresBox
            // 
            this.jugadoresBox.Location = new System.Drawing.Point(464, 84);
            this.jugadoresBox.Margin = new System.Windows.Forms.Padding(2);
            this.jugadoresBox.Multiline = true;
            this.jugadoresBox.Name = "jugadoresBox";
            this.jugadoresBox.Size = new System.Drawing.Size(99, 43);
            this.jugadoresBox.TabIndex = 38;
            // 
            // ResultadosBTN
            // 
            this.ResultadosBTN.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ResultadosBTN.BackgroundImage")));
            this.ResultadosBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ResultadosBTN.BorderColor = System.Drawing.Color.Transparent;
            this.ResultadosBTN.BorderRadius = 8;
            this.ResultadosBTN.BorderThickness = 3;
            this.ResultadosBTN.CheckedState.Parent = this.ResultadosBTN;
            this.ResultadosBTN.CustomImages.Parent = this.ResultadosBTN;
            this.ResultadosBTN.DisabledState.Parent = this.ResultadosBTN;
            this.ResultadosBTN.FillColor = System.Drawing.Color.Transparent;
            this.ResultadosBTN.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ResultadosBTN.ForeColor = System.Drawing.Color.White;
            this.ResultadosBTN.HoverState.Parent = this.ResultadosBTN;
            this.ResultadosBTN.Location = new System.Drawing.Point(249, 327);
            this.ResultadosBTN.Margin = new System.Windows.Forms.Padding(2);
            this.ResultadosBTN.Name = "ResultadosBTN";
            this.ResultadosBTN.ShadowDecoration.Parent = this.ResultadosBTN;
            this.ResultadosBTN.Size = new System.Drawing.Size(109, 35);
            this.ResultadosBTN.TabIndex = 39;
            this.ResultadosBTN.Click += new System.EventHandler(this.ResultadosBTN_Click);
            // 
            // matrizResultados
            // 
            this.matrizResultados.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.matrizResultados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.matrizResultados.Location = new System.Drawing.Point(431, 124);
            this.matrizResultados.Margin = new System.Windows.Forms.Padding(2);
            this.matrizResultados.Name = "matrizResultados";
            this.matrizResultados.RowHeadersWidth = 51;
            this.matrizResultados.RowTemplate.Height = 24;
            this.matrizResultados.Size = new System.Drawing.Size(173, 214);
            this.matrizResultados.TabIndex = 40;
            // 
            // CloseBTN
            // 
            this.CloseBTN.FlatAppearance.BorderSize = 0;
            this.CloseBTN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(42)))), ((int)(((byte)(83)))));
            this.CloseBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.CloseBTN.ForeColor = System.Drawing.Color.White;
            this.CloseBTN.Location = new System.Drawing.Point(704, 14);
            this.CloseBTN.Name = "CloseBTN";
            this.CloseBTN.Size = new System.Drawing.Size(24, 30);
            this.CloseBTN.TabIndex = 41;
            this.CloseBTN.Text = "X";
            this.CloseBTN.UseVisualStyleBackColor = true;
            this.CloseBTN.Click += new System.EventHandler(this.CloseBTN_Click);
            // 
            // jugador
            // 
            this.jugador.Location = new System.Drawing.Point(41, 414);
            this.jugador.Margin = new System.Windows.Forms.Padding(2);
            this.jugador.Name = "jugador";
            this.jugador.Size = new System.Drawing.Size(113, 20);
            this.jugador.TabIndex = 42;
            // 
            // PartidasGanadasBtn
            // 
            this.PartidasGanadasBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PartidasGanadasBtn.BackgroundImage")));
            this.PartidasGanadasBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PartidasGanadasBtn.FlatAppearance.BorderSize = 0;
            this.PartidasGanadasBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PartidasGanadasBtn.Location = new System.Drawing.Point(170, 407);
            this.PartidasGanadasBtn.Margin = new System.Windows.Forms.Padding(2);
            this.PartidasGanadasBtn.Name = "PartidasGanadasBtn";
            this.PartidasGanadasBtn.Size = new System.Drawing.Size(140, 31);
            this.PartidasGanadasBtn.TabIndex = 43;
            this.PartidasGanadasBtn.UseVisualStyleBackColor = true;
            this.PartidasGanadasBtn.Click += new System.EventHandler(this.PartidasGanadasBtn_Click);
            // 
            // GanadasLbl
            // 
            this.GanadasLbl.AutoSize = true;
            this.GanadasLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GanadasLbl.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.GanadasLbl.Location = new System.Drawing.Point(334, 410);
            this.GanadasLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.GanadasLbl.Name = "GanadasLbl";
            this.GanadasLbl.Size = new System.Drawing.Size(0, 24);
            this.GanadasLbl.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(219, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 13);
            this.label3.TabIndex = 49;
            this.label3.Text = "Selecciona a un jugador para ver el ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(45, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Mira contra quienes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(35, 383);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(316, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Pon el nombre de un jugador para ver las partidas que ha ganado";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(235, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 13);
            this.label4.TabIndex = 52;
            this.label4.Text = "historial de partidas contra él";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(38, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "has jugado una partida";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(42)))), ((int)(((byte)(83)))));
            this.label6.Location = new System.Drawing.Point(209, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(223, 33);
            this.label6.TabIndex = 54;
            this.label6.Text = "ESTADÍSTICAS";
            // 
            // EstadisticasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(762, 571);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GanadasLbl);
            this.Controls.Add(this.PartidasGanadasBtn);
            this.Controls.Add(this.jugador);
            this.Controls.Add(this.CloseBTN);
            this.Controls.Add(this.matrizResultados);
            this.Controls.Add(this.ResultadosBTN);
            this.Controls.Add(this.jugadoresBox);
            this.Controls.Add(this.matrizUsuarios);
            this.Controls.Add(this.matrizJugadores);
            this.Controls.Add(this.JugadoresBTN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "EstadisticasForm";
            this.Text = "EstadisticasForm";
            this.Load += new System.EventHandler(this.EstadisticasForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.matrizJugadores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.matrizUsuarios)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.matrizResultados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button JugadoresBTN;
        private System.Windows.Forms.DataGridView matrizJugadores;
        private System.Windows.Forms.DataGridView matrizUsuarios;
        private System.Windows.Forms.TextBox jugadoresBox;
        private Guna.UI2.WinForms.Guna2Button ResultadosBTN;
        private System.Windows.Forms.DataGridView matrizResultados;
        private System.Windows.Forms.Button CloseBTN;
        private System.Windows.Forms.TextBox jugador;
        private System.Windows.Forms.Button PartidasGanadasBtn;
        private System.Windows.Forms.Label GanadasLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}