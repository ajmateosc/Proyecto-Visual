using login;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoVisual
{
    public partial class VentanadeJuego : Form
    {
        private int num;
        private String nom;
        private int conta = 1;
        private int contc = 1;
        private JuegoIngles j;
       



        public VentanadeJuego(int n, String no, JuegoIngles jue)
        {
            InitializeComponent();
            num = n;
            nom = no;
            j = jue;
            j.Puntuaciones[num - 1] = 0;
            avanzar();
            
        }


        public void avanzar()
        {
            label2.Text ="Puntos: "+j.Puntuaciones[num-1];

            Conexion con = new Conexion();

            MySqlDataReader ava = con.consulta("SELECT * FROM preguntasingles WHERE categoria=" + num);
            int cont2 = 0;
            
            while (ava.Read())
            {
                cont2++;
                if (conta == cont2)
                {
                   
                        pictureBox1.Image = Image.FromFile(Path.Combine(Application.StartupPath + "\\imagenesingles\\" + nom, ava.GetString(1)));
                        label1.Text = ava.GetString(2);
                    

                }
                
            }
            conta++;
            con.cerrar();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            int cant=0;

            Conexion con = new Conexion();

            MySqlDataReader ava = con.consulta("SELECT COUNT(*) FROM preguntasingles WHERE categoria=" + num);
            ava.Read();
            cant = ava.GetInt16(0)+1;

                comprobar();
            if (cant != conta)
            {
                avanzar();

            }
            else
            {
                MessageBox.Show("Juego finalizado. ¡Bien jugado! Puntuación: "+j.Puntuaciones[num-1]);
                j.actualizarlabel();
                this.Close();
            }

            con.cerrar();

        }

        private void comprobar()
        {
            Conexion con = new Conexion();
            MySqlDataReader avan = con.consulta("SELECT * FROM preguntasingles WHERE categoria=" + num);
            int cont2 = 0;
            while (avan.Read())
            {
                cont2++;
                if (contc == cont2)
                {


                    if (textBox1.Text.ToString().ToLower().Equals(avan.GetString(3)))
                    {
                        MessageBox.Show("Correct answer!!!!!!");
                        j.Puntuaciones[num - 1] = j.Puntuaciones[num - 1] + 1;
                    }
                    else
                    {
                        MessageBox.Show("Wrong answer :( - ("+ avan.GetString(3)+")");
                    }
                }

            }
            textBox1.Text = "";
            contc++;
            con.cerrar();
        }

    }
}