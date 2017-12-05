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

namespace ProyectoVisual
{
    public partial class Biologia : Form
    {
        String usuario;
        private int[] puntuaciones = new int[4];
        private int total=0;
        
        public Biologia(String u)
        {
            InitializeComponent();

            usuario = u;

            for (int i = 0; i < puntuaciones.Length; i++)
            {
                puntuaciones[i] = 0;
            }
        }

        public int[] Puntuaciones { get => puntuaciones; set => puntuaciones = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            Biologia2 b2 = new Biologia2(1, this);
            b2.Hide();
            b2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Biologia2 b2 = new Biologia2(2, this);
            b2.Hide();
            b2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Biologia2 b2 = new Biologia2(3, this);
            b2.Hide();
            b2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Biologia2 b2 = new Biologia2(4, this);
            b2.Hide();
            b2.ShowDialog();
        }

        public void actualizarPuntuacion()
        {
            total = 0;

            for (int i = 0; i < puntuaciones.Length; i++)
            {
                total = total + puntuaciones[i];
            }

            label5.Text = "PUNTUACIÓN TOTAL: " + total;
        }

        private void Biologia_FormClosed(object sender, FormClosedEventArgs e)
        {
            Conexion con = new Conexion();
            con.insertar("UPDATE estadisticas SET puntuacion=" + total + " WHERE id_juego=4 and usuario LIKE '" + usuario + "'");
            con.cerrar();
        }
    }
}
