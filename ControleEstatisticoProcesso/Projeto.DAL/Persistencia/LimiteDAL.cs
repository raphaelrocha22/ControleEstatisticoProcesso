using Projeto.DAL.Conexoes;
using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Persistencia
{
    public class LimiteDAL : Conexao
    {
        public void CadastrarLimiteControle(LimiteControle l)
        {
            try
            {
                AbrirConexao();

                tr = con.BeginTransaction();

                new SqlCommand("update LimiteControle set ativo = 0", con, tr).ExecuteNonQuery();

                string query = "insert into LimiteControle (dataCalculo,LSC,LC,LIC,idUsuarioAprovacao,ativo,tipoCarta) values (@dataCalculo,@LSC,@LC,@LIC,@idUsuarioAprovacao,@ativo,@tipoCarta); " +
                    "SELECT SCOPE_IDENTITY()";
                cmd = new SqlCommand(query, con, tr);
                cmd.Parameters.AddWithValue("@dataCalculo", l.DataCalculo);
                cmd.Parameters.AddWithValue("@LSC", l.LSC);
                cmd.Parameters.AddWithValue("@LC", l.LC);
                cmd.Parameters.AddWithValue("@LIC", l.LIC);
                cmd.Parameters.AddWithValue("@idUsuarioAprovacao", l.Usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@ativo", l.Ativo);
                cmd.Parameters.AddWithValue("@tipoCarta", l.TipoCarta);
                l.IdLimite = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var item in l.Lotes)
                {
                    query = "update Lote set idLimite = @idLimite where idLote = @idLote";
                    cmd = new SqlCommand(query, con, tr);
                    cmd.Parameters.AddWithValue("@idLimite", l.IdLimite);
                    cmd.Parameters.AddWithValue("@idLote", item.IdLote);
                    cmd.ExecuteNonQuery();
                }

                new SqlCommand("delete from TempLote", con, tr).ExecuteNonQuery();

                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }

        public List<LimiteControle> ConsultarLimiteControle(bool? ativo = null, int? idLimite = null)
        {
            try
            {
                AbrirConexao();

                var lista = new List<LimiteControle>();

                string query = "select lc.idLimite, lc.dataCalculo, lc.LSC, lc.LC, lc.LIC, u.nome, lc.ativo from LimiteControle lc " +
                    "inner join Usuario u on lc.idUsuarioAprovacao = u.idUsuario " +
                    "where (lc.ativo = @ativo or @ativo is null) and (lc.idLimite = @idLimite or @idLimite is null)";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithNullValue("@ativo", ativo);
                cmd.Parameters.AddWithNullValue("@idLimite", idLimite);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    var l = new LimiteControle();
                    l.Usuario = new Usuario();

                    l.IdLimite = (int)dr["idLimite"];
                    l.DataCalculo = (DateTime)dr["dataCalculo"];
                    l.LSC = (decimal)dr["LSC"];
                    l.LC = (decimal)dr["LC"];
                    l.LIC = (decimal)dr["LIC"];
                    l.Usuario.Nome = dr["nome"].ToString();
                    l.Ativo = (bool)dr["ativo"];

                    lista.Add(l);
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

        public void DefinirLimiteAtivo(int idLimite)
        {
            try
            {
                AbrirConexao();

                tr = con.BeginTransaction();

                new SqlCommand("update LimiteControle set ativo = 0", con, tr).ExecuteNonQuery();

                string query = "update LimiteControle set ativo = 1 where idLimite = @idLimite";
                cmd = new SqlCommand(query, con, tr);
                cmd.Parameters.AddWithValue("@idLimite",idLimite);
                cmd.ExecuteNonQuery();

                tr.Commit();
            }
            catch (Exception e)
            {
                tr.Rollback();
                throw e;
            }
            finally
            {
                FecharConexao();
            }
        }

        

    }
}
