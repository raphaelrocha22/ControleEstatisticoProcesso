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

                tr = con.BeginTransaction();

                string query = "insert into Lote (idLote, dataHora, qtdTotal, qtdReprovada, percentualReprovado, comentario, idUsuarioAnalise, tipoLote,idMaquina) " +
                    "values (@idLote, @dataHora, @qtdTotal, @qtdReprovada, @percentualReprovado, @comentario, @idUsuarioAnalise, @tipoLote,@idMaquina)";

                cmd = new SqlCommand(query, con, tr);
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

                cmd = new SqlCommand("insert into TempLote values (@idLote)", con, tr);
                cmd.Parameters.AddWithValue("@idLote", l.IdLote);
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

        public List<Lote> ConsultarAmostras(int? idLimite = null)
        {
            try
            {
                AbrirConexao();

                string query =
                    "select l.idLote, dataHora, qtdTotal, qtdReprovada, percentualReprovado,comentario, " +
                    "u.nome,l.tipoLote, m.idMaquina, m.codInterno from Lote l " +
                    "inner join Usuario u on l.idUsuarioAnalise = u.idUsuario " +
                    "inner join Maquina m on l.idMaquina = m.idMaquina " +
                    "where " +
                    "(@idLimite is null or l.idLimite = @idLimite) " +
                    "and " +
                    "(@idLimite is not null or l.idLote in (select idLote from TempLote)) " +
                    "order by dataHora";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithNullValue("@idLimite", idLimite);
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

        public List<Lote> ConsultarLotesProducao(int? idLimite = null)
        {
            try
            {
                AbrirConexao();

                string query =
                    "select l.idLote, dataHora, qtdTotal, qtdReprovada, percentualReprovado, status, comentario, " +
                    "u_analise.idUsuario 'usuarioAnalise', u_aprovacao.idUsuario 'usuarioAprovacao', lc.LSC, lc.LC, lc.LIC, l.tipoLote, m.idMaquina, m.codInterno from Lote l " +
                    "inner join Usuario u_analise on l.idUsuarioAnalise = u_analise.idUsuario " +
                    "left join Usuario u_aprovacao on l.idUsuarioAnalise = u_aprovacao.idUsuario " +
                    "inner join Maquina m on l.idMaquina = m.idMaquina " +
                    "inner join LimiteControle lc on l.idLimite = lc.idLimite " +
                    "where " +
                    "(@idLimite is null or l.idLimite = @idLimite) " +
                    "and " +
                    "(@idLimite is not null or l.idLote in (select idLote from TempLote)) " +
                    "order by dataHora";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithNullValue("@idLimite", idLimite);
                dr = cmd.ExecuteReader();

                var lista = new List<Lote>();

                while (dr.Read())
                {
                    var l = new Lote();
                    l.UsuarioAnalise = new Usuario();
                    l.UsuarioAprovacao = new Usuario();
                    l.Maquina = new Maquina();
                    l.LimiteControle = new LimiteControle();

                    l.IdLote = (int)dr["idLote"];
                    l.DataHora = (DateTime)dr["dataHora"];
                    l.QtdTotal = (int)dr["qtdTotal"];
                    l.QtdReprovada = (int)dr["qtdReprovada"];
                    l.PercentualReprovado = (decimal)dr["percentualReprovado"];
                    l.Status = (Status)Enum.Parse(typeof(Status), dr["status"].ToString());
                    l.Comentario = dr["comentario"].ToString();
                    l.UsuarioAnalise.Nome = dr["usuarioAnalise"].ToString();
                    l.UsuarioAprovacao.Nome = dr["usuarioAprovacao"].ToString();
                    l.LimiteControle.LSC = (decimal)dr["LSC"];
                    l.LimiteControle.LC = (decimal)dr["LC"];
                    l.LimiteControle.LIC = (decimal)dr["LIC"];
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

        public void ExcluirLote(int idLote)
        {
            try
            {
                AbrirConexao();

                tr = con.BeginTransaction();

                cmd = new SqlCommand("delete from TempLote where idLote = @idLote", con, tr);
                cmd.Parameters.AddWithValue("@idLote", idLote);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("delete from Lote where idLote = @idLote", con, tr);
                cmd.Parameters.AddWithValue("@idLote", idLote);
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
