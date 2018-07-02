using Projeto.DAL.Conexoes;
using Projeto.DAL.Repositorio;
using Projeto.Entidades;
using Projeto.Entidades.Enuns;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Projeto.DAL.Persistencia
{
    public class UsuarioDAL:Conexao
    {
        public void CadastrarUsuario(Usuario u)
        {
            try
            {
                AbrirConexao();

                string query = "insert into Usuario (dataCadastro,nome,login,senha,perfil,ativo) values (@dataCadastro,@nome,@login,@senha,@perfil,@ativo)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@dataCadastro", DateTime.Now);
                cmd.Parameters.AddWithValue("@nome", u.Nome);
                cmd.Parameters.AddWithValue("@login", u.Login);
                cmd.Parameters.AddWithValue("@senha", Criptografia.EncriptarSenha(u.Senha));
                cmd.Parameters.AddWithValue("@perfil", u.Perfil.ToString());
                cmd.Parameters.AddWithValue("@ativo", u.Ativo);
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

        private List<Usuario> ObterUsuarios(SqlDataReader dr)
        {
            try
            {
                var lista = new List<Usuario>();

                while (dr.Read())
                {
                    var u = new Usuario();
                    u.IdUsuario = (int)dr["idUsuario"];
                    u.Nome = dr["nome"].ToString();
                    u.Login = dr["login"].ToString();
                    u.Perfil = (PerfilUsuario)Enum.Parse(typeof(PerfilUsuario), dr["perfil"].ToString());
                    u.Ativo = (bool)dr["ativo"];

                    lista.Add(u);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Usuario ConsultarUsuario(string login, string senha)
        {
            try
            {
                AbrirConexao();

                string query = "select idUsuario,nome,login,senha,perfil,ativo from Usuario where login = @login and senha = @senha and ativo = @ativo";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ativo", true);
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@senha", Criptografia.EncriptarSenha(senha));
                dr = cmd.ExecuteReader();

                return ObterUsuarios(dr).FirstOrDefault();
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

        public Usuario ConsultarUsuario(int id)
        {
            try
            {
                AbrirConexao();

                string query = "select idUsuario,nome,login,senha,perfil,ativo from Usuario where idUsuario = @id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                dr = cmd.ExecuteReader();

                return ObterUsuarios(dr).FirstOrDefault();
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

        public List<Usuario> ListarUsuarios()
        {
            try
            {
                AbrirConexao();

                string query = "select idUsuario, nome, login, perfil, ativo from Usuario";
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();

                return ObterUsuarios(dr);
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

        public void AtualizarSenha(string senhaNova, int id)
        {
            try
            {
                AbrirConexao();

                string query = "update Usuario set senha = @senhaNova where idUsuario = @id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@senhaNova", Criptografia.EncriptarSenha(senhaNova));
                cmd.Parameters.AddWithValue("@id", id);
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

        public void AtualizarUsuario(Usuario u)
        {
            try
            {
                AbrirConexao();

                string query = "update Usuario set nome = @nome, login = @login, ativo = @ativo, perfil = @perfil where idUsuario = @id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nome", u.Nome);
                cmd.Parameters.AddWithValue("@login", u.Login);
                cmd.Parameters.AddWithValue("@ativo", u.Ativo);
                cmd.Parameters.AddWithValue("@perfil", u.Perfil.ToString());
                cmd.Parameters.AddWithValue("@id", u.IdUsuario);
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
