using Projeto.DAL.Conexoes;
using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Projeto.DAL.Persistencia
{
    public class LoteDAL : Conexao
    {
        public void CadastrarLoteAmostra(Lote l)
        {
            try
            {
                AbrirConexao();

                string query = "insert into Lote (idLote, dataHora, qtdTotal, qtdReprovada, percentualReprovado, status, comentario, idUsuarioAnalise, idUsuarioAprovacao, tipoLote) " +
                    "values (@idLote, @dataHora, @qtdTotal, @qtdReprovada, @percentualReprovado, @status, @comentario, @idUsuarioAnalise, @idUsuarioAprovacao, @tipoLote); " +
                    "insert into TempLote values (@idLote)";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idLote", l.IdLote);
                cmd.Parameters.AddWithValue("@dataHora", l.DataHora);
                cmd.Parameters.AddWithValue("@qtdTotal", l.QtdTotal);
                cmd.Parameters.AddWithValue("@qtdReprovada", l.QtdReprovada);
                cmd.Parameters.AddWithValue("@percentualReprovado", l.PercentualReprovado);
                cmd.Parameters.AddWithValue("@status", l.Status);
                cmd.Parameters.AddWithNullValue("@comentario", l.Comentario);
                cmd.Parameters.AddWithValue("@idUsuarioAnalise", l.UsuarioAnalise.IdUsuario);
                cmd.Parameters.AddWithNullValue("@idUsuarioAprovacao", l.UsuarioAprovacao.IdUsuario);
                cmd.Parameters.AddWithValue("@tipoLote", l.TipoLote);
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

        public void CadastrarLoteProducao(Lote l)
        {
            try
            {
                AbrirConexao();

                string query = "insert into Lote (idLote, dataHora, qtdTotal, qtdReprovada, percentualReprovado, status, comentario, idUsuarioAnalise, idUsuarioAprovacao, tipoLote, idLimite) " +
                    "values (@idLote, @dataHora, @qtdTotal, @qtdReprovada, @percentualReprovado, @status, @comentario, @idUsuarioAnalise, @idUsuarioAprovacao, @tipoLote, @idLimite)";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idLote", l.IdLote);
                cmd.Parameters.AddWithValue("@dataHora", l.DataHora);
                cmd.Parameters.AddWithValue("@qtdTotal", l.QtdTotal);
                cmd.Parameters.AddWithValue("@qtdReprovada", l.QtdReprovada);
                cmd.Parameters.AddWithValue("@percentualReprovado", l.PercentualReprovado);
                cmd.Parameters.AddWithValue("@status", l.Status);
                cmd.Parameters.AddWithValue("@comentario", l.Comentario);
                cmd.Parameters.AddWithValue("@idUsuarioAnalise", l.UsuarioAnalise.IdUsuario);
                cmd.Parameters.AddWithValue("@idUsuarioAprovacao", l.UsuarioAprovacao.IdUsuario);
                cmd.Parameters.AddWithValue("@tipoLote", l.TipoLote);
                cmd.Parameters.AddWithValue("@idLimite", l.LimiteControle.IdLimite);
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
