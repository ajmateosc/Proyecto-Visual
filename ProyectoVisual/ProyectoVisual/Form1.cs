using System;
using MySql.Data.MySqlClient;
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
    public partial class Form1 : Form
    {
        private Conexion c;
        private String user;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            c = new Conexion();

            if (comprobar()) {
                user = textBox1.Text;
                MessageBox.Show("Bienvenido "+user);
                textBox1.Text = "";
                textBox2.Text = "";
                VentanaPrincipal vp = new VentanaPrincipal(user);
                vp.ShowDialog();
            }
            c.cerrar();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Registrarse form = new Registrarse();
            form.ShowDialog();
        }

        private Boolean comprobar()
        {
            Boolean cond = true;
            if (textBox1.Text.Length == 0)
            {
                cond = false;
                MessageBox.Show(this, "El campo usuario está vacío");
            }
            else if (textBox1.Text.Length == 0)
            {
                cond = false;
                MessageBox.Show(this, "El campo contraseña está vacío");
            }else if (!comprobarLogin())
            {
                cond = false;
                MessageBox.Show(this, "La contraseña no coincide con el usuario");
            }
            return cond;
        }

        public Boolean comprobarLogin()
        {
            Boolean cond = false;
            MySqlDataReader mysql = c.consulta("select * from usuarios where usuario like '" + textBox1.Text + "' and contrasena like '" + textBox2.Text + "' ");
            if (mysql.Read())
            {
                cond = true;
            }
            mysql.Close();
            return cond;
        }
    }
}
