using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Conexion
{
    public class Acceso : Conexion
    {
            #region Propiedades
            SqlConnection connection = new SqlConnection();

            private string _SelectCommandText;
            private string _executeCommandText;
            private SqlCommand _ExecuteParameters = new SqlCommand();
            internal readonly DbProviderFactory BaseFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");

            protected string SelectCommandText
            {
                get { return _SelectCommandText; }
                set { _SelectCommandText = value; }
            }
            protected string ExecuteCommandText
            {
                get { return _executeCommandText; }
                set { _executeCommandText = value; }
            }
            protected SqlCommand ExecuteParameters
            {
                get { return _ExecuteParameters; }
                set { _ExecuteParameters = value; }
            }
            #endregion

            #region Conexión
            private void Conectar()
            {
                connection.ConnectionString = conexion;
                connection.Open();
            }
            private void Desconectar()
            {
                connection.Close();
            }
            #endregion

            #region Métodos
            public virtual void ExecuteNonQuery()
            {
                Conectar();

                SqlTransaction TR = connection.BeginTransaction();
                SqlCommand command = new SqlCommand(ExecuteCommandText, connection, TR);

                command.CommandType = CommandType.Text;
                command.Parameters.Clear();

                foreach (SqlParameter p in ExecuteParameters.Parameters)
                {
                    command.Parameters.AddWithValue(p.ParameterName, p.SqlValue);
                }

                try
                {
                    command.ExecuteNonQuery();
                    TR.Commit();
                }
                catch (SqlException exc)
                {
                    TR.Rollback();
                    throw new Exception("Ocurrió un error en BD: " + exc.Message);
                }
                catch (Exception exc2)
                {
                    TR.Rollback();
                    throw new Exception("Ocurrió un Error: " + exc2.Message);
                }
                finally
                {
                    Desconectar();
                }
            }

            public virtual int ExecuteNonEscalar()
            {
                Conectar();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = new SqlCommand(ExecuteCommandText, connection, transaction);

                command.CommandType = CommandType.Text;
                command.Parameters.Clear();

                foreach (SqlParameter p in ExecuteParameters.Parameters)
                {
                    command.Parameters.AddWithValue(p.ParameterName, p.SqlValue);
                }

                SqlParameter sp_return = new SqlParameter();
                sp_return.Direction = ParameterDirection.ReturnValue;
                command.Parameters.Add(sp_return);

                int outputId = 0;

                try
                {
                    outputId = (int)command.ExecuteScalar();
                    transaction.Commit();
                }
                catch (SqlException exc)
                {
                    transaction.Rollback();
                    throw new Exception("Ocurrió un error en BD: " + exc.Message);
                }
                catch (Exception exc2)
                {
                    transaction.Rollback();
                    throw new Exception("Ocurrió un error: " + exc2.Message);
                }
                finally
                {
                    Desconectar();
                }

                return outputId;
            }

            public virtual DataSet ExecuteNonReader()
            {
                if (this.SelectCommandText == "")
                    throw new Exception("You must provide SelectCommandText first. Review Framework documentation.");

                using (connection)
                {
                    DbDataAdapter da = this.BaseFactory.CreateDataAdapter();
                    da.SelectCommand = this.BaseFactory.CreateCommand();
                    da.SelectCommand.CommandText = this.SelectCommandText;
                    da.SelectCommand.Connection = connection;

                    DataSet ds = new DataSet();
                    try
                    {
                        Conectar();
                        da.Fill(ds);
                    }
                    catch (SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        Desconectar();
                    }

                    return ds;
                }
            }
            #endregion
        }
}
