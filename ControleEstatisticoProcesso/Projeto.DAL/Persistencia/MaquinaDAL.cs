using Projeto.DAL.Conexoes;
using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Persistencia
{
    public class MaquinaDAL:Conexao
    {
        public List<Maquina>ConsultarMaquina()
        {
            try
            {
                AbrirConexao();

                string query = "select idMaquina,codInterno,modelo,fabricante,setor from maquina";
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();

                var lista = new List<Maquina>();

                while (dr.Read())
                {
                    var m = new Maquina();
                    m.IdMaquina = (int)dr["idMaquina"];
                    m.CodInterno = dr["codInterno"].ToString();
                    m.Modelo = dr["modelo"].ToString();
                    m.Fabricante = dr["fabricante"].ToString();

                    lista.Add(m);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }
    }
}
