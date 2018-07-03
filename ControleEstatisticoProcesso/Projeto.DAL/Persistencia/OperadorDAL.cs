using Projeto.DAL.Conexoes;
using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Persistencia
{
    public class OperadorDAL:Conexao
    {
        public void CadastrarOperador(Operador o)
        {
            try
            {
                AbrirConexao();

                string query = "insert into Operador (setor, nome, ativo) values (@setor, @nome, @ativo)";
                cmd = new SqlCommand(query,con);
                cmd.Parameters.AddWithValue("@setor", o.Setor.ToString());
                cmd.Parameters.AddWithValue("@nome", o.Nome);
                cmd.Parameters.AddWithValue("@ativo", o.Ativo);
                cmd.ExecuteNonQuery();
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

        public Operador ConsultarOperador(int id)
        {
            try
            {
                AbrirConexao();

                string query = "select idOperador, setor, nome, ativo from Operador where idOperador = @id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                dr = cmd.ExecuteReader();

                return ObterOperadores(dr).FirstOrDefault();
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

        public List<Operador> ListarOperadores()
        {
            try
            {
                AbrirConexao();

                string query = "select idOperador, setor, nome, ativo from Operador";
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();

                return ObterOperadores(dr);
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

        public List<Operador> ListarOperadores(Setor setor)
        {
            try
            {
                AbrirConexao();

                string query = "select idOperador, setor, nome, ativo from Operador where setor = @setor and ativo = @ativo";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@setor", setor.ToString());
                cmd.Parameters.AddWithValue("@ativo", true);
                dr = cmd.ExecuteReader();

                return ObterOperadores(dr);
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

        private List<Operador> ObterOperadores(SqlDataReader dr)
        {
            try
            {
                var lista = new List<Operador>();

                while (dr.Read())
                {
                    var o = new Operador();
                    o.IdOperador = (int)dr["idOperador"];
                    o.Setor = (Setor)Enum.Parse(typeof(Setor), dr["setor"].ToString());
                    o.Nome = dr["nome"].ToString();
                    o.Ativo = (bool)dr["ativo"];

                    lista.Add(o);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AtualizarOperador(Operador o)
        {
            try
            {
                AbrirConexao();

                string query = "update Operador set setor = @setor, nome = @nome, ativo = @ativo where idOperador = @idOperador";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@setor", o.Setor.ToString());
                cmd.Parameters.AddWithValue("@nome", o.Nome);
                cmd.Parameters.AddWithValue("@ativo", o.Ativo);
                cmd.Parameters.AddWithValue("@idOperador", o.IdOperador);
                cmd.ExecuteNonQuery();
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
