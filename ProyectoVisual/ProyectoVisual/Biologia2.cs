using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using login;
using System.IO;

namespace ProyectoVisual
{
    public partial class Biologia2 : Form
    {
        private int num;
        private Biologia bio;
        private int contTurno;
        private int contAciertos;
        private Boolean respondida;
        private String correcta;

        public Biologia2(int n, Biologia bio)
        {
            InitializeComponent();
            num = n;
            this.bio = bio;
            bio.Puntuaciones[num - 1] = 0;
            contTurno = 0;
            contAciertos = 0;
            respondida = false;
            correcta = "";
            siguiente();

            switch (num)
            {
                case 1:
                    label2.Text = "LA CÉLULA";
                    break;
                case 2:
                    label2.Text = "EVOLUCIÓN";
                    break;
                case 3:
                    label2.Text = "GENÉTICA";
                    break;
                case 4:
                    label2.Text = "ECOSISTEMAS";
                    break;
            }
        }

        public void siguiente()
        {
            label4.Text = "" + bio.Puntuaciones[num - 1];

            Conexion con = new Conexion();
            MySqlDataReader preguntas = con.consulta("SELECT * FROM preguntasbiologia WHERE categoria=" + num +" order by id_pregunta asc");

            int contPregunta = 0;

            while (preguntas.Read())
            {
                if (contPregunta == contTurno)
                {
                    richTextBox1.Text = preguntas.GetString(1);
                    a.Text = preguntas.GetString(2);
                    b.Text = preguntas.GetString(3);
                    c.Text = preguntas.GetString(4);
                    d.Text = preguntas.GetString(5);
                    a.BackColor = Color.LightSlateGray;
                    b.BackColor = Color.LightSlateGray;
                    c.BackColor = Color.LightSlateGray;
                    d.BackColor = Color.LightSlateGray;                    

                    correcta = preguntas.GetString(6);
                    respondida = false;
                }
                contPregunta++;

                if (contTurno ==10)
                {
                    MessageBox.Show("Juego finalizado. ¡Bien jugado! Puntuación: " + bio.Puntuaciones[num - 1]);
                    richTextBox1.Text = null;
                    a.Text = null;
                    b.Text = null;
                    c.Text = null;
                    d.Text = null;
                    a.BackColor = Color.LightSlateGray;
                    b.BackColor = Color.LightSlateGray;
                    c.BackColor = Color.LightSlateGray;
                    d.BackColor = Color.LightSlateGray;
                    bio.actualizarPuntuacion();
                    contTurno++;
                }
            }

            con.cerrar();
        }

        public void comprobar(Button pregunta)
        {
            if (pregunta.Name.Equals(correcta))
            {
                pregunta.BackColor = Color.Green;
                contAciertos++;
                bio.Puntuaciones[num - 1] = bio.Puntuaciones[num - 1] + 1;
            }
            else
            {
                pregunta.BackColor = Color.Red;
            }

            respondida = true;
            contTurno++;
            
        }

        private void a_Click(object sender, EventArgs e)
        {
            if (!respondida)
            {
                comprobar(a);
            }
        }

        private void b_Click(object sender, EventArgs e)
        {
            if (!respondida)
            {
                comprobar(b);
            }
        }

        private void c_Click(object sender, EventArgs e)
        {
            if (!respondida)
            {
                comprobar(c);
            }
        }

        private void d_Click(object sender, EventArgs e)
        {
            if (!respondida)
            {
                comprobar(d);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (respondida)
            {
                siguiente();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (respondida)
            {
                respondida = false;
                contTurno--;
                button2.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "cross.png"));
                button2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (respondida)
            {
                respondida = false;
                contTurno--;
                button3.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "cross.png"));
                button3.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (respondida)
            {
                respondida = false;
                contTurno--;
                button4.BackgroundImage = Image.FromFile(Path.Combine(Application.StartupPath, "cross.png"));
                button4.Enabled = false;
            }            
        }
    }
}
