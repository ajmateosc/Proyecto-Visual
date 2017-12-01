using MySql.Data.MySqlClient;
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
    public partial class Registrarse : Form
    {
        private Conexion c;

        public Registrarse()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            c = new Conexion();

            Boolean enc=false;

            if(usuario.Text.ToString().Length==0|| contrasena.Text.ToString().Length == 0|| repcontrasena.Text.ToString().Length == 0)
            {
                MessageBox.Show("No puedes dejar ningún campo vacío.");
            }
            else
            {
               MySqlDataReader dr=c.consulta("SELECT * FROM usuarios where usuario LIKE '"+usuario.Text.ToString()+"'");
                if(dr.Read())
                {
                    enc = true;
                }
                dr.Close();
                if(enc==true)
                {
                    MessageBox.Show("Este usuario ya existe, por favor introduzca otro.");
                }
                else
                {
                     if(contrasena.Text.ToString().Equals(repcontrasena.Text.ToString()))
                     {
                        c.insertar("INSERT INTO usuarios (usuario, contrasena) VALUES ('"+usuario.Text.ToString()+"', '"+contrasena.Text.ToString()+"')");

                        for(int i=1;i<=4;i++)
                        {
                            c.insertar("INSERT INTO `estadisticas`(`usuario`, `id_juego`, `puntuacion`) VALUES ('"+usuario.Text.ToString()+"',"+i+", 0)");

                        }

                        MessageBox.Show("Usuario "+usuario.Text.ToString()+" registrado correctamente.");
                     }
                     else
                     {
                        MessageBox.Show("Contraseña y repetir contraseña no coinciden.");
                     }
                }
            }

            c.cerrar();
        }
    }
}
