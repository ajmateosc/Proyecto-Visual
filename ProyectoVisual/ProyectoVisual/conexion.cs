using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login
{
    class Conexion
    {
        private MySqlConnection con;
        public Conexion()
        {
            con = new MySqlConnection();
            con.ConnectionString = "Server=localhost;Database=juegoeso;Uid=root;Pwd=12345";
            try
            {
                con.Open();
                //System.Windows.Forms.MessageBox.Show("Conexion exitosa", "Informacion", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al conectar", "Informacion", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }
        public MySqlDataReader consulta(String sql)
        {
            MySqlCommand comando = new MySqlCommand(sql, con);
            MySqlDataReader mysqldr = comando.ExecuteReader();
            return mysqldr;
        }
        public void insertar(String sql)
        {
            MySqlCommand comando = new MySqlCommand(sql, con);
            comando.ExecuteNonQuery();
        }
        protected void cerrar()
        {
            try
            {
                con.Close();
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }


    }
}
