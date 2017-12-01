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
    public partial class JuegoIngles : Form
    {

        private int[] puntuaciones = new int[3];
        private int tot = 0;
        String usuario;

        public JuegoIngles(String usu)
        {
            InitializeComponent();
            label3.Text = "Puntuación total: 0pts";
            for(int i=0;i<puntuaciones.Length;i++)
            {
                puntuaciones[i] = 0;
            }
            usuario = usu;
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            VentanadeJuego v = new VentanadeJuego(1, "paises", this);
            v.Hide();
            v.ShowDialog();
        }

        private void trabajos_Click(object sender, EventArgs e)
        {
            VentanadeJuego v = new VentanadeJuego(2, "trabajo", this);
            v.Hide();
            v.ShowDialog();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            VentanadeJuego v = new VentanadeJuego(3, "ropa", this);
            v.Hide();
            v.ShowDialog();
        }

        public int[] Puntuaciones { get => puntuaciones; set => puntuaciones = value; }
     

        public void actualizarlabel()
        {
            tot = 0;

            for(int i=0;i<puntuaciones.Length;i++)
            {
                tot = tot + puntuaciones[i];
            }

            label3.Text = "Puntuación total: " + tot+"pts";
            
        }

        private void JuegoIngles_FormClosed(object sender, FormClosedEventArgs e)
        {
            actualizarlabel();
            Conexion con = new Conexion();
            con.insertar("UPDATE estadisticas SET puntuacion="+tot+" WHERE id_juego=2 and usuario LIKE '"+usuario+"'");
            con.cerrar();
        }
    }
}
