using login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoVisual
{
    public partial class GeoHistoria : Form
    {
        private string usuario;
        private int numeroPregunta = 0, correctas=0, incorrectas=0;
        private string[] preguntas;
        private string[] marcada = new string[8];
        private string[] seleccionadas = new string[8];
        private RadioButton opcion1, opcion2, opcion3, opcion4;
        public GeoHistoria(string user)
        {
            usuario = user;
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            label1.Text = "Hola " + usuario + ", Tu puntaje: " + obtenerPuntajeUsuario(usuario) + " puntos.";

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private int obtenerPuntajeUsuario(string user)
        {
            int ptos=0;
            MySql.Data.MySqlClient.MySqlDataReader resultado;
            Conexion c = new Conexion();
            resultado = c.consulta("SELECT SUM(puntuacion) FROM estadisticas WHERE usuario LIKE '"+user+"' ");
            //
            while (resultado.Read())
            {
                ptos = resultado.GetInt32("SUM(puntuacion)");
            }
            c.cerrar();
            return ptos;

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
       

        private void generarVentanaInicialRespondePreguntas(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(label2);
            panel2.Controls.Add(comboBox1);
            panel2.Controls.Add(button4);

        }
        private string[] tratarDataset(MySql.Data.MySqlClient.MySqlDataReader datos)
        {
            int i = 0;
            string[] pregunta = new string[8];
            while (datos.Read())
            {
                //pregunta;opcion1;opcion2;opcion3;opcion4;correcta
                pregunta[i] = datos.GetString("pregunta")+";"+datos.GetString("opcion1") + ";" +datos.GetString("opcion2") + ";" +datos.GetString("opcion3") + ";" +datos.GetString("opcion4") + ";" +datos.GetString("correcta");
                i++;
            }
            return pregunta;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int opcionSeleccionada = 0;
            string a = comboBox1.SelectedIndex.ToString().Substring(0, 1);
            if (!a.Equals("T"))
            {
                opcionSeleccionada = int.Parse(a);
            }

            MySql.Data.MySqlClient.MySqlDataReader dr = obtenerPreguntas(opcionSeleccionada);
            preguntas = tratarDataset(dr);
            muestraPregunta(preguntas, numeroPregunta);
           


        }
        private void guardarPreferenciaPregunta(string[] matrizPreguntas, int nro_pregunta)
        {

        }

        private void siguiente_Click(object sender, EventArgs e)
        {
            if (opcion1.Checked==true)
            {
                seleccionadas[numeroPregunta] = "opcion1";
            }
            else if (opcion2.Checked==true)
            {
                seleccionadas[numeroPregunta] = "opcion2";
            }
            else if (opcion3.Checked==true)
            {
                seleccionadas[numeroPregunta] = "opcion3";
            }
            else if (opcion4.Checked==true)
            {
                seleccionadas[numeroPregunta] = "opcion4";
            }
            switch (preguntas[numeroPregunta].Split(';')[5])
            {
                case "opcion1":
                    if (opcion1.Checked== true)
                    {
                        marcada[numeroPregunta] = "CORRECTA";
                        correctas++;
                    }
                    else if(opcion2.Checked==true || opcion3.Checked==true || opcion4.Checked==true)
                    {
                        marcada[numeroPregunta] = "INCORRECTA";
                        incorrectas++;
                    }
                    else
                    {
                        marcada[numeroPregunta] = "EN BLANCO";
                    }
                    break;
                case "opcion2":
                    if (opcion2.Checked == true)
                    {
                        marcada[numeroPregunta] = "CORRECTA";
                        correctas++;
                    }
                    else if (opcion1.Checked == true || opcion3.Checked == true || opcion4.Checked == true)
                    {
                        marcada[numeroPregunta] = "INCORRECTA";
                        incorrectas++;
                    }
                    else
                    {
                        marcada[numeroPregunta] = "EN BLANCO";
                    }
                    break;
                case "opcion3":
                    if (opcion3.Checked == true)
                    {
                        marcada[numeroPregunta] = "CORRECTA";
                        correctas++;
                    }
                    else if (opcion1.Checked == true || opcion2.Checked == true || opcion4.Checked == true)
                    {
                        marcada[numeroPregunta] = "INCORRECTA";
                        incorrectas++;
                    }
                    else
                    {
                        marcada[numeroPregunta] = "EN BLANCO";
                    }
                    break;
                case "opcion4":
                    if (opcion4.Checked == true)
                    {
                        marcada[numeroPregunta] = "CORRECTA";
                        correctas++;
                    }
                    else if (opcion2.Checked == true || opcion3.Checked == true || opcion1.Checked == true)
                    {
                        marcada[numeroPregunta] = "INCORRECTA";
                        incorrectas++;
                    }
                    else
                    {
                        marcada[numeroPregunta] = "EN BLANCO";
                    }
                    break;
            }
            numeroPregunta++;
            if (numeroPregunta < 8)
            {
                muestraPregunta(preguntas, numeroPregunta);
            }
            else
            {
               // Limpia el panel
                panel2.Controls.Clear();
                mostrarNota();

                numeroPregunta = 0;
                seleccionadas = new String[8];
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void mostrarNota()
        {
            Label tituloResultado = new Label();
            tituloResultado.Text = "Resultados del Quiz";
            tituloResultado.Location = new Point(327,9);
            tituloResultado.Size = new Size(407,26);
            tituloResultado.Font = new Font("Microsoft Sans Serif",16, FontStyle.Bold);

            Label pregunta1 = new Label();
            pregunta1.Text = "P1: "+marcada[0];
            pregunta1.Location = new Point(57, 76);
            if (marcada[0]=="INCORRECTA")
            {
                pregunta1.ForeColor = Color.Red;
            }
            pregunta1.Size = new Size(124, 18);
            pregunta1.Font = new Font("Trebuchet MS", 12, FontStyle.Regular);

            Label pregunta2 = new Label();
            pregunta2.Text = "P2: "+marcada[1];
            pregunta2.Location = new Point(57,136);
            if (marcada[1] == "INCORRECTA")
            {
                pregunta2.ForeColor = Color.Red;
            }
            pregunta2.Size = new Size(124,18);
            pregunta2.Font = new Font("Trebuchet MS", 12, FontStyle.Regular);

            Label pregunta3 = new Label();
            pregunta3.Text = "P3: " + marcada[2];
            pregunta3.Location = new Point(57, 196);
            if (marcada[2] == "INCORRECTA")
            {
                pregunta3.ForeColor = Color.Red;
            }
            pregunta3.Size = new Size(124, 18);
            pregunta3.Font = new Font("Trebuchet MS", 12, FontStyle.Regular);
            
            Label pregunta4 = new Label();
            pregunta4.Text = "P4: " + marcada[3];
            pregunta4.Location = new Point(57, 256);
            if (marcada[3] == "INCORRECTA")
            {
                pregunta4.ForeColor = Color.Red;
            }
            pregunta4.Size = new Size(124, 18);
            pregunta4.Font = new Font("Trebuchet MS", 12, FontStyle.Regular);

  



            Label pregunta5 = new Label();
            pregunta5.Text = "P5: " + marcada[4];
            pregunta5.Location = new Point(459, 76);
            if (marcada[4] == "INCORRECTA")
            {
                pregunta5.ForeColor = Color.Red;
            }
            pregunta5.Size = new Size(124, 18);
            pregunta5.Font = new Font("Trebuchet MS", 12, FontStyle.Regular);

            Label pregunta6 = new Label();
            pregunta6.Text = "P6: " + marcada[5];
            pregunta6.Location = new Point(459, 136);
            if (marcada[5] == "INCORRECTA")
            {
                pregunta6.ForeColor = Color.Red;
            }
            pregunta6.Size = new Size(124, 18);
            pregunta6.Font = new Font("Trebuchet MS", 12, FontStyle.Regular);

            Label pregunta7 = new Label();
            pregunta7.Text = "P7: " + marcada[6];
            pregunta7.Location = new Point(459, 196);
            if (marcada[6] == "INCORRECTA")
            {
                pregunta7.ForeColor = Color.Red;
            }
            pregunta7.Size = new Size(124, 18);
            pregunta7.Font = new Font("Trebuchet MS", 12, FontStyle.Regular);

            Label pregunta8 = new Label();
            pregunta8.Text = "P8: "+ marcada[7];
            pregunta8.Location = new Point(459, 256);
            if (marcada[7] == "INCORRECTA")
            {
                pregunta8.ForeColor = Color.Red;
            }
            pregunta8.Size = new Size(124, 18);
            pregunta8.Font = new Font("Trebuchet MS", 12, FontStyle.Regular);

            int notafinal = correctas - (incorrectas / 4);
            sumarPuntos(usuario, notafinal);
            
            Label puntuacion = new Label();
            puntuacion.Text = "Nota: "+notafinal+"(Correctas: "+correctas+", Incorrectas: "+incorrectas+")";
            puntuacion.Location = new Point(227,350);
            puntuacion.Size = new Size(500, 50);
            puntuacion.Font = new Font("Trebuchet MS", 18, FontStyle.Bold);
            puntuacion.ForeColor = Color.BlueViolet;
            correctas = 0;incorrectas = 0;notafinal = 0;

            Button reiniciar = new Button();
            reiniciar.Text = "VOLVER A COMENZAR";
            reiniciar.Location = new Point(300,400);
            reiniciar.Size = new Size(300,50);
            reiniciar.Font = new Font("Calibri",18,FontStyle.Bold);
            reiniciar.BackColor = Color.BlueViolet;
            reiniciar.ForeColor = Color.White;





            panel2.Controls.Add(tituloResultado);
            panel2.Controls.Add(pregunta1);
            //panel2.Controls.Add(bPregunta1);
            panel2.Controls.Add(pregunta2);
            //panel2.Controls.Add(bPregunta2);
            panel2.Controls.Add(pregunta3);
            //panel2.Controls.Add(bPregunta3);
            panel2.Controls.Add(pregunta4);
            //panel2.Controls.Add(bPregunta4);
            panel2.Controls.Add(pregunta5);
            //panel2.Controls.Add(bPregunta5);
            panel2.Controls.Add(pregunta6);
            //panel2.Controls.Add(bPregunta6);
            panel2.Controls.Add(pregunta7);
            //panel2.Controls.Add(bPregunta7);
            panel2.Controls.Add(pregunta8);
            //panel2.Controls.Add(bPregunta8);
            //panel2.Controls.Add(pregunta9);
            //panel2.Controls.Add(bPregunta9);
            //panel2.Controls.Add(pregunta10);
            //panel2.Controls.Add(bPregunta10);
            panel2.Controls.Add(puntuacion);
            panel2.Controls.Add(reiniciar);
            reiniciar.Click += new EventHandler(generarVentanaInicialRespondePreguntas);

        }
        private void sumarPuntos(string usuario, int puntaje)
        {
            Conexion c = new Conexion();
            c.insertar("INSERT INTO estadisticas (usuario,id_juego,puntuacion) VALUES ('"+usuario+"',3,"+puntaje+")");
            c.cerrar();
            label1.Text = "Hola " + usuario + ", Tu puntuación: " + obtenerPuntajeUsuario(usuario) + " Puntos.";
            //
        }
        private void GeoHistoria_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("saliendo");
        }

        private MySql.Data.MySqlClient.MySqlDataReader obtenerPreguntas(int nivel)
        {
            MySql.Data.MySqlClient.MySqlDataReader resultado;
            Conexion c = new Conexion();
            if (nivel!=0)
            {
                resultado = c.consulta("SELECT * FROM preguntashistoria WHERE categoria = "+nivel+" ORDER BY RAND() LIMIT 8");
            }
            else
            {
                resultado = c.consulta("SELECT * FROM preguntashistoria ORDER BY RAND() LIMIT 8");
            }
            return resultado;
        }
        private void muestraPregunta(String[] preguntas, int numero)
        {
            // Frase de la Pregunta
            Label frasePregunta = new Label();
            frasePregunta.Text = preguntas[numero].Split(';')[0];
            frasePregunta.Location = new Point(panel2.Width / 8, panel2.Height / 8);
            frasePregunta.Size = new Size(3 * panel2.Width / 4, panel2.Height / 8);
            frasePregunta.Font = new Font("Calibri", 12, FontStyle.Bold);
            frasePregunta.ForeColor = Color.Black;
            panel2.Controls.Clear();
            // Opcion1
            opcion1 = new RadioButton();
            opcion1.Location = new Point(2 * panel2.Width / 8, panel2.Height / 4);
            opcion1.Text = preguntas[numero].Split(';')[1];
            opcion1.Size = new Size(3 * panel2.Width / 4, panel2.Height / 8);
            opcion1.Font = new Font("Calibri", 10, FontStyle.Regular);
            // Opcion2
            opcion2 = new RadioButton();
            opcion2.Location = new Point(2 * panel2.Width / 8, 3 * panel2.Height / 8);
            opcion2.Size = new Size(3 * panel2.Width / 4, panel2.Height / 8);
            opcion2.Text = preguntas[numero].Split(';')[2];
            opcion2.Font = new Font("Calibri", 10, FontStyle.Regular);
            // Opcion3
            opcion3 = new RadioButton();
            opcion3.Location = new Point(2 * panel2.Width / 8, panel2.Height / 2);
            opcion3.Size = new Size(3 * panel2.Width / 4, panel2.Height / 8);
            opcion3.Text = preguntas[numero].Split(';')[3];
            opcion3.Font = new Font("Calibri", 10, FontStyle.Regular);
            // Opcion4
            opcion4 = new RadioButton();
            opcion4.Location = new Point(2 * panel2.Width / 8, 5 * panel2.Height / 8);
            opcion4.Size = new Size(3 * panel2.Width / 4, panel2.Height / 8);
            opcion4.Text = preguntas[numero].Split(';')[4];
            opcion4.Font = new Font("Calibri", 10, FontStyle.Regular);
            // Botón volver
            Button volver = new Button();
            if (numeroPregunta != 0 && numeroPregunta!=8)
            {
                volver.Text = "Pregunta Anterior";
                volver.Font = new Font("Trebuchet MS", 10, FontStyle.Bold);
                volver.Location = new Point(0, 7 * panel2.Height / 8);
                volver.Size = new Size(3 * panel2.Width / 8, panel2.Height / 8);
            }
            // Selecciona la opción si el jugador ha elegido anteriormente una respuesta
            switch (seleccionadas[numeroPregunta])
            {
                case "opcion1":
                    opcion1.Checked = true;
                    break;
                case "opcion2":
                    opcion2.Checked = true;
                    break;
                case "opcion3":
                    opcion3.Checked = true;
                    break;
                case "opcion4":
                    opcion4.Checked = true;
                    break;
                default:
                    break;
            }
            // Botón Continuar
            Button siguiente = new Button();
            if (numeroPregunta == 7)
            {
                siguiente.Text = "Ver resultados";
            }
            else
            {
                siguiente.Text = "Siguiente Pregunta";
            }
           
            siguiente.Font = new Font("Trebuchet MS", 10, FontStyle.Bold);
            siguiente.Location = new Point(5 * panel2.Width / 8, 7 * panel2.Height / 8);
            siguiente.Size = new Size();
            siguiente.Size = new Size(3 * panel2.Width / 8, panel2.Height / 8);
            // Agregar los elementos

            panel2.Controls.Add(frasePregunta);
            panel2.Controls.Add(opcion1);
            panel2.Controls.Add(opcion2);
            panel2.Controls.Add(opcion3);
            panel2.Controls.Add(opcion4);
            if (numeroPregunta != 0)
            {
                panel2.Controls.Add(volver);
            }
            
            panel2.Controls.Add(siguiente);
            siguiente.Click += new EventHandler(siguiente_Click);
            volver.Click += new EventHandler(volver_Click);
        }

        private void volver_Click(object sender, EventArgs e)
        {
            // dar puntaje si es correcta
            numeroPregunta--;
            if (numeroPregunta>=0 && numeroPregunta < 8)
            {
                muestraPregunta(preguntas, numeroPregunta);
            }
            else
            {
                // Mostrar pantalla inicial
                panel2.Controls.Clear();

            }
        }
    }
}
