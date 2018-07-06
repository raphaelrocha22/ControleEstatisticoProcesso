using Projeto.DAL.Conexoes;
using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using Projeto.Entidades.Enuns;
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

                string query = "insert into Lote (idLote, dataHora, qtdTotal, qtdReprovada, percentualReprovado, comentario, idUsuarioAnalise, tipoLote,idMaquina) " +
                    "values (@idLote, @dataHora, @qtdTotal, @qtdReprovada, @percentualReprovado, @comentario, @idUsuarioAnalise, @tipoLote,@idMaquina); " +
                    "insert into TempLote values (@idLote)";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idLote", l.IdLote);
                cmd.Parameters.AddWithValue("@dataHora", l.DataHora);
                cmd.Parameters.AddWithValue("@qtdTotal", l.QtdTotal);
                cmd.Parameters.AddWithValue("@qtdReprovada", l.QtdReprovada);
                cmd.Parameters.AddWithValue("@percentualReprovado", l.PercentualReprovado);
                cmd.Parameters.AddWithNullValue("@comentario", l.Comentario);
                cmd.Parameters.AddWithValue("@idUsuarioAnalise", l.UsuarioAnalise.IdUsuario);
                cmd.Parameters.AddWithValue("@tipoLote", l.TipoLote.ToString());
                cmd.Parameters.AddWithValue("@idMaquina", l.Maquina.IdMaquina);
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


        public List<Lote> ConsultarAmostras()
        {
            try
            {
                AbrirConexao();

                string query = "select l.idLote, dataHora, qtdTotal, qtdReprovada, percentualReprovado,comentario,u.nome,l.tipoLote, m.idMaquina, m.codInterno from Lote l " +
                    "inner join Usuario u on l.idUsuarioAnalise = u.idUsuario " +
                    "inner join Maquina m on l.idMaquina = m.idMaquina " +
                    "inner join TempLote tp on l.idLote = tp.idLote " +
                    "order by dataHora";

                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();

                var lista = new List<Lote>();

                while (dr.Read())
                {
                    var l = new Lote();
                    l.UsuarioAnalise = new Usuario();
                    l.Maquina = new Maquina();

                    l.IdLote = (int)dr["idLote"];
                    l.DataHora = (DateTime)dr["dataHora"];
                    l.QtdTotal = (int)dr["qtdTotal"];
                    l.QtdReprovada = (int)dr["qtdReprovada"];
                    l.PercentualReprovado = (decimal)dr["percentualReprovado"];
                    l.Comentario = dr["comentario"].ToString();
                    l.UsuarioAnalise.Nome = dr["nome"].ToString();
                    l.TipoLote = (TipoLote)Enum.Parse(typeof(TipoLote), dr["tipoLote"].ToString());
                    l.Maquina.IdMaquina = (int)dr["idMaquina"];
                    l.Maquina.CodInterno = dr["codInterno"].ToString();

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

        
    }
}
