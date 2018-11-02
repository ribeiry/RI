using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Mvc_CRUD.Models;
using System.Data;

namespace Mvc_CRUD.DAO
{
    public class CRUD : Connection
    {
        public void Incluir(Funcionario func)
        {

            var logger = NLog.LogManager.GetCurrentClassLogger();
            var sql = "Insert into Funcionario(FuncionarioId,Nome,Cidade,Departamento,Sexo) ";
            sql += " values(@FuncionarioId, @Nome, @Cidade, @Departamento, @Sexo)";
            var parametro = new List<SqlParameter>();

            parametro.Add(new SqlParameter("@FuncionarioID", BuscaMaiorId()+1));
            parametro.Add(new SqlParameter("@Nome", func.Nome));
            parametro.Add(new SqlParameter("@Cidade", func.Cidade));
            parametro.Add(new SqlParameter("@Departamento", func.Departamento));
            parametro.Add(new SqlParameter("@Sexo", func.Sexo));

            logger.Info("Executando a Inclusão do Funcionario");

            ExecuteNonQuery(sql, parametro);

        }
        public void Alterar(Funcionario func)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            var sql = "Update Funcionario set Nome = @Nome,Cidade = @Cidade,Departamento = @Departamento,Sexo = @Sexo  " +
                "where FuncionarioId = @FuncionarioID";
            var parametro = new List<SqlParameter>();

            parametro.Add(new SqlParameter("@FuncionarioID", func.FuncionarioId));
            parametro.Add(new SqlParameter("@Nome", func.Nome));
            parametro.Add(new SqlParameter("@Cidade", func.Cidade));
            parametro.Add(new SqlParameter("@Departamento", func.Departamento));
            parametro.Add(new SqlParameter("@Sexo", func.Sexo));

            logger.Info("Executando a Alteração do Funcionario");

            ExecuteNonQuery(sql, parametro);

        }
        public void Deletar(int id)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            var sql = "Delete Funcionario where FuncionarioId = @FuncionarioID";
            var parametro = new List<SqlParameter>();

            parametro.Add(new SqlParameter("@FuncionarioID", id));

            ExecuteNonQuery(sql, parametro);

        }

        public Funcionario BuscaPorId(int Id)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            var sql = "select FuncionarioId,Nome,Cidade,Departamento,Sexo from Funcionario where FuncionarioId = @FuncionarioID";
            var parametro = new SqlParameter();
            Funcionario func = new Funcionario();
            var conexao = ConexaoDados;

                try
                {
                    conexao.Open();
                    using (var commando = conexao.CreateCommand())
                    {
                        commando.CommandType = CommandType.Text;
                        commando.CommandText = sql;
                        commando.Parameters.AddWithValue("@FuncionarioID", Id);

                        using (var registro = commando.ExecuteReader())
                        {
                        logger.Info("Executando a Busca do Funcionario pelo ID");
                        logger.Info(sql);
                        if (registro.HasRows & registro.Read())
                            {
                                func.FuncionarioId = Convert.ToInt32(registro["FuncionarioId"].ToString().Trim());
                                func.Nome = registro["Nome"].ToString();
                                func.Cidade = registro["Cidade"].ToString();
                                func.Departamento = registro["Departamento"].ToString();
                                func.Sexo = registro["Sexo"].ToString();
                            }
                        }
                    }
                } 
                catch(SqlException e)
                {
                    logger.Error("Erro a efetuar a busca " + e.Message);
                    throw new Exception(e.Message);
                }
                catch(Exception e)
                {
                    logger.Error("Erro a efetuar a busca " + e.Message);
                    throw new Exception(e.Message);
                }
                finally
                {
                logger.Info("Executando o fechamento do Banco de dados");
                conexao.Dispose();
                    conexao.Close();
                }
            
            return func;
        }
        public int BuscaMaiorId()
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            var sql = "select Max(FuncionarioId) FuncionarioId from Funcionario ";
            var parametro = new SqlParameter();
            Funcionario func = new Funcionario();
            var conexao = ConexaoDados;
            int id=0;

            try
            {
                conexao.Open();
                using (var commando = conexao.CreateCommand())
                {
                    commando.CommandType = CommandType.Text;
                    commando.CommandText = sql;

                    logger.Info("Executando a Busca do Funcionario pelo ID");
                    logger.Info(sql);
                    using (var registro = commando.ExecuteReader())
                    {
                        if (registro.HasRows & registro.Read())
                        {
                            id = Convert.ToInt32(registro["FuncionarioId"].ToString().Trim());
                            
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                logger.Error("Erro a efetuar a busca " + e.Message);
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                logger.Error("Erro a efetuar a busca " + e.Message);
                throw new Exception(e.Message);
            }
            finally
            {
                logger.Info("Executando o fechamento do Banco de dados");
                conexao.Dispose();
                conexao.Close();
            }
            return id;
        }
        public List<Funcionario> ListaTodosFuncionarios()
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            var sql = "select FuncionarioId,Nome,Cidade,Departamento,Sexo from Funcionario";
            List<Funcionario> funcionarios = new List<Funcionario>();

            using (var conexao = ConexaoDados)
            {
                conexao.Open();

                using (var commando = conexao.CreateCommand())
                {
                    commando.CommandType = CommandType.Text;
                    commando.CommandText = sql;
                    using (var registro = commando.ExecuteReader())
                    {
                        if (registro.HasRows)
                        {
                            logger.Info("Executando a consulta dos Funcionarios ");
                            logger.Info(sql);
                            while (registro.Read())
                            {
                                Funcionario func = new Funcionario();

                                func.FuncionarioId = Convert.ToInt32(registro["FuncionarioId"]);
                                func.Nome = registro["Nome"].ToString();
                                func.Cidade = registro["Cidade"].ToString();
                                func.Departamento = registro["Departamento"].ToString();
                                func.Sexo = registro["Sexo"].ToString();

                                funcionarios.Add(func);
                            }
                        }
                    }
                }
                logger.Info("Executando o fechamento do Banco de dados");
                conexao.Dispose();
                conexao.Close();
                    
            }
            return funcionarios;
        }
    }
}
