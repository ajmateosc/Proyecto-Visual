using MySql.Data.MySqlClient;
using ProyectoVisual;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login
{
    public partial class VentanaPrincipal : Form
    {
        String usuario;

        public VentanaPrincipal(String usu)
        {
            InitializeComponent();
            usuario = usu;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            JuegoIngles ji = new JuegoIngles(usuario);
            ji.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conexion con = new Conexion();
            MySqlDataReader consulta=con.consulta("SELECT SUM(puntuacion) FROM estadisticas WHERE usuario LIKE '"+usuario+"'");
            consulta.Read();
            label1.Text = "Hola " + usuario + ", tu puntuación es: " + consulta.GetInt16(0)+"." ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();   
        }

        private void button4_Click(object sender, EventArgs e)
        {
               Mates v = new Mates(usuario);
            v.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Biologia b = new Biologia(usuario);
            b.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GeoHistoria gh = new GeoHistoria(usuario);
            gh.ShowDialog();
        }
    }
}
