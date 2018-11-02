using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Mvc_CRUD.DAO
{
    public class Connection
    {
        protected SqlConnection ConexaoDados =>
            new SqlConnection(
                    ConfigurationManager.ConnectionStrings["ConexaoConnectionString"].ConnectionString) ;

        public void ExecuteNonQuery(string sql, List<SqlParameter> parametros)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            using(var conexao = ConexaoDados)
            {
                try
                {

                    conexao.Open();
                    using(var comando= conexao.CreateCommand()) 
                    {
                        comando.CommandType = CommandType.Text;
                        comando.CommandText = sql;

                        foreach(var parametro in parametros)
                        {
                            comando.Parameters.Add(parametro);
                        }

                        logger.Info("Executando a seguinte instrução " + sql);
                        comando.ExecuteNonQuery();
                    }
                }
                catch(SqlException e)
                {
                    logger.Error("Erro ao Executar a instrução " + e.Message);
                    throw new Exception(e.Message);
                }
                catch(Exception e)
                {
                    logger.Error("Erro ao Executar a instrução " + e.Message);
                    throw new Exception(e.Message);
                }
                finally
                {
                    logger.Info("Executando o fechamento do Banco de dados");
                    conexao.Dispose();
                    conexao.Close();
                }
            }
        }
    }
}