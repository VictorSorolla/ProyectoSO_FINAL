
namespace ProyectoSO
{
    partial class EliminarForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EliminarForm));
            this.label3 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.TextBox();
            this.EliminarBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CloseBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(121, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 26);
            this.label3.TabIndex = 12;
            this.label3.Text = "Password";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(235, 161);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(164, 20);
            this.Password.TabIndex = 13;
            this.Password.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(119, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 26);
            this.label2.TabIndex = 10;
            this.label2.Text = "Username";
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(235, 128);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(164, 20);
            this.Username.TabIndex = 11;
            // 
            // EliminarBtn
            // 
            this.EliminarBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("EliminarBtn.BackgroundImage")));
            this.EliminarBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.EliminarBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EliminarBtn.Location = new System.Drawing.Point(259, 200);
            this.EliminarBtn.Margin = new System.Windows.Forms.Padding(2);
            this.EliminarBtn.Name = "EliminarBtn";
            this.EliminarBtn.Size = new System.Drawing.Size(114, 55);
            this.EliminarBtn.TabIndex = 14;
            this.EliminarBtn.UseVisualStyleBackColor = true;
            this.EliminarBtn.Click += new System.EventHandler(this.EliminarBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(42)))), ((int)(((byte)(83)))));
            this.label1.Location = new System.Drawing.Point(220, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 31);
            this.label1.TabIndex = 15;
            this.label1.Text = "DAR DE BAJA";
            // 
            // CloseBTN
            // 
            this.CloseBTN.FlatAppearance.BorderSize = 0;
            this.CloseBTN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(42)))), ((int)(((byte)(83)))));
            this.CloseBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.CloseBTN.ForeColor = System.Drawing.Color.LightGray;
            this.CloseBTN.Location = new System.Drawing.Point(608, -1);
            this.CloseBTN.Name = "CloseBTN";
            this.CloseBTN.Size = new System.Drawing.Size(24, 30);
            this.CloseBTN.TabIndex = 27;
            this.CloseBTN.Text = "X";
            this.CloseBTN.UseVisualStyleBackColor = true;
            this.CloseBTN.Click += new System.EventHandler(this.CloseBTN_Click);
            // 
            // EliminarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(30)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(659, 450);
            this.Controls.Add(this.CloseBTN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EliminarBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Username);
            this.Name = "EliminarForm";
            this.Text = "EliminarForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Button EliminarBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CloseBTN;
    }
}