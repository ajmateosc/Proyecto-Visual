using login;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProyectoVisual
{
    public partial class Mates : Form
    {
        private Timer time;
        private String usuario;
        private char sol;
        private Boolean crono;
        private int puntos;
        private int contador;
        public Mates(String user)
        {
            crono = true;
            puntos = 0;
            usuario = user;
            contador = 1;
            InitializeComponent();
        }

        private void Mates_Load(object sender, EventArgs e)
        {
            iniciar();
            

        }
        public void iniciar()
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            button3.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label1.Text = "Bienvenido " + usuario + ", soy Carlos Sobera y esta a punto de jugar a";
            label2.Text = "¿Quién quiere ser MATEMATICO?";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            label3.Text = usuario + " tienes que responder a 10 preguntas y dispones de 30 segundos por cada una";
            label4.Text = "Además dispones de 2 comodines,";
            label5.Text = "el del público, te mostrar las opcion que el publico ha responido";
            label6.Text = "y el comodín del 50% que eliminara la mitad de las respuestas";

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Button pulsado = (Button)sender;
            Conexion con = new Conexion();
            if (pulsado.Text.ToString().Equals("Reintentar"))
            {
                iniciar();
            }
            else if(pulsado.Text.ToString().Equals("Finalizar"))
            {
                if (puntos < 5)
                {
                    richTextBox1.Text = "El juego finalizó, y con " + puntos + " tristes puntos, dificilmente podrás ser matemático, pero recuerda un matemático no nace, se hace con esfuerzo";

                }
                else if (puntos < 8)
                {
                    richTextBox1.Text = "El juego finalizó, enhorabuena por tus " + puntos + " puntos, sigue así y recuerda en las matemáticas lo que más resultado da es la constancia";

                }
                else if (puntos < 10)
                {

                    richTextBox1.Text = "El juego finalizo, y lo hiciste de maravilla con tus " + puntos + " puntos. Enhorabuena, serás una de las mentes que guiará a la humanidad en el futuro";
                }
                else
                {

                    richTextBox1.Text = "El juego finalizó, pero lo que no finalizará nunca es tu genialidad, eres uno entre cien mil millones";
                }
                buttonA.Text = "";
                buttonB.Text = "";
                buttonC.Text = "";
                buttonD.Text = "";
                actualizarPtos(con);
                colorBoton(Color.DarkGray);
                pulsado.Text = "Reintentar";
                //   MessageBox.Show("Final: Su puntuacion fue " + puntos);

            }
            else
            {
                MessageBox.Show("Pregunta numero " + contador);
                int dif = 1;
                label9.Text = puntos.ToString();
                button3.Enabled = false;
                if (crono == true || label7.Text.ToString().Equals("0"))
                {
                    if (contador < 5)
                    {
                        dif = 1;
                    }
                    else if (contador < 9)
                    {
                        dif = 2;
                    }
                    else if (contador < 11)
                    {
                        if (contador == 10)
                        {
                            button3.Text = "Finalizar";

                        }
                        dif = 3;
                    }
                    else
                    {

                    }

                    MySqlDataReader mysql = con.consulta("SELECT * FROM preguntasmates WHERE dificultad = " + dif + " ORDER BY rand() LIMIT 1");
                    if (mysql.Read())
                    {

                        pregunta(mysql.GetString(1), mysql.GetString(2), mysql.GetString(3), mysql.GetString(4), mysql.GetString(5), mysql.GetChar(6));

                    }

                    con.cerrar();
                }

            }



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!label7.Text.ToString().Equals("0"))
            {
                if (label7.Text.ToString().Equals("20"))
                {
                    label7.ForeColor = Color.Yellow;
                }
                else if (label7.Text.ToString().Equals("10"))
                {
                    label7.ForeColor = Color.Red;
                }
                label7.Text = (int.Parse(label7.Text) - 1).ToString();

            }
            else
            {
                label7.Text = "0";
                button3.Visible = true;
                habilitarRespuestas(false);
                colorBoton(Color.DarkSlateGray);
            }

        }
        private void pregunta(String pregunta, String a, String b, String c, String d, char solu)
        {

            habilitarRespuestas(true);
            colorBoton(Color.White);
            panel2.Visible = false;
            panel3.Visible = true;
            panel4.Visible = true;
            panel5.Visible = true;
            label7.Text = "30";
            label7.ForeColor = Color.Green;
            contador++;
            time = new Timer();
            time.Tick += new EventHandler(timer1_Tick);
            time.Interval = 1000;
            time.Start();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.Text = pregunta;
            buttonA.Text = "a) " + a;
            buttonB.Text = "b) " + b;
            buttonC.Text = "c) " + c;
            buttonD.Text = "d) " + d;
            sol = solu;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void buttonA_Click(object sender, EventArgs e)
        {
            Button pulsado = (Button)sender;

            String texto = pulsado.Text.Substring(3);
            if (pulsado.Name.ToString().Equals("button" + sol))
            {
                
                label7.Text = "0";
                pulsado.BackColor = Color.Green;
                puntos++;
                label9.Text = puntos.ToString();
            }
            else
            {
    
                pulsado.BackColor = Color.Red;
                label7.Text = "0";
            }
            habilitarRespuestas(false);
            button3.Visible = true;
            time.Stop();
            time.Dispose();
            button3.Enabled = true;
        }
        private void habilitarRespuestas(Boolean cond)
        {
            buttonA.Enabled = cond;
            buttonB.Enabled = cond;
            buttonC.Enabled = cond;
            buttonD.Enabled = cond;
        }
        private void colorBoton(Color c)
        {
            buttonA.BackColor = c;
            buttonB.BackColor = c;
            buttonC.BackColor = c;
            buttonD.BackColor = c;
        }


        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            label10.Visible = true;
        }
        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            label10.Visible = false;
        }
        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            label11.Visible = true;
        }
        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            label11.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
         
            pictureBox4.Enabled = false;
            int cont = 0;
            if (!buttonC.Name.ToString().Equals("button" + sol.ToString()) && cont < 2)
            {
                buttonC.Enabled = false;
                buttonC.Text = "";
                cont++;
            }
            if (!buttonA.Name.ToString().Equals("button" + sol) && cont < 2)
            {
                buttonA.Enabled = false;
                buttonA.Text = "";
                cont++;
            }
            if (!buttonD.Name.ToString().Equals("button" + sol) && cont < 2)
            {
                buttonD.Enabled = false;
                buttonD.Text = "";
                cont++;
            }
            if (!buttonB.Name.ToString().Equals("button" + sol) && cont < 2)
            {
                buttonB.Enabled = false;
                buttonB.Text = "";
                cont++;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            /*         axWindowsMediaPlayer1.URL = "E:\\DAM\\2DAM\\Desarrollo de Interfaces\\Mates1\\efectos varios\\MLG Horns Sound Effect.mp3";
                     axWindowsMediaPlayer1.Ctlcontrols.play();*/
            pictureBox5.Enabled = false;
            panel6.Visible = true;
            int estadA = 0, estadB = 0, estadC = 0, estadD = 0;
            int max = 51;
            Random rnd = new Random();
            switch (sol)
            {
                case 'A':
                    estadA = 50;
                    break;
                case 'B':
                    estadB = 50;
                    break;
                case 'C':
                    estadC = 50;
                    break;
                case 'D':
                    estadD = 50;
                    break;
            }
            int n = rnd.Next(0, max);
            estadA += n;
            max -= n;
            n = rnd.Next(0, max);

            estadB += n;

            max -= n;

            n = rnd.Next(0, max);

            estadC += n;


            max -= n;

            estadD += max - 1;

            label12.Text = "a) " + estadA + " %";
            label13.Text = "b) " + estadB + " %";
            label14.Text = "c) " + estadC + " %";
            label15.Text = "d) " + estadD + " %";
        }

        public void actualizarPtos(Conexion c)
        {
            String ptosviejos="0";
            MySqlDataReader mysql = c.consulta("select * from estadisticas where usuario like '" + usuario + "' and id_juego = 1");
            if (mysql.Read())
            {
                ptosviejos = mysql.GetString(3);
               
            }
            mysql.Close();
            if (puntos > int.Parse(ptosviejos))
            {
                c.insertar("UPDATE estadisticas SET puntuacion = " + puntos + " WHERE usuario like '" + usuario + "' and id_juego = 1");
            }
        }
    }
   
}
