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
    }
}
