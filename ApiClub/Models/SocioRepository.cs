using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ApiClub.Models
{
    public class SocioRepository
    {
        string strCon = @"user id = CLUB; password = P2ssw0rd; Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = tcp)(HOST = 192.168.1.60)(PORT = 1521))(CONNECT_DATA = (SERVICE_NAME = ORADNET)))";

        public SocioRepository()
        {
            ;
        }

        private OracleConnection Connect(string connectStr)
        {
            OracleConnection con = new OracleConnection(connectStr);
            try
            {
                con.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            return con;
        }

        public List<Socio> ListaSocios()
        {
            List<Socio> listaSocios = new List<Socio>();
            Socio socio = null;

            OracleConnection con = Connect(strCon);

            OracleCommand cmd = new OracleCommand("PKG_SOCIO_CRUD.PRC_LISTA_SOCIOS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            //P_ERRCOD OUT NUMBER, P_ERRMSG OUT VARCHAR2

            cmd.Parameters.Add("P_ERRCOD", OracleDbType.Int32).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("P_ERRMSG", OracleDbType.Varchar2).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("P_DATA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader;

            try
            {
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    socio = new Socio();

                    socio.IdSocio = Convert.ToInt32(reader["IDSOCIO"]);
                    socio.Rut = Convert.ToInt32(reader["RUT"]);
                    socio.Dv = Convert.ToString(reader["DV"]);
                    socio.Nombres = Convert.ToString(reader["NOMBRES"]);
                    socio.Apaterno = Convert.ToString(reader["APATERNO"]);
                    socio.Amaterno = Convert.ToString(reader["AMATERNO"]);

                    listaSocios.Add(socio);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Dispose OracleCommand object
                cmd.Dispose();

                // Close and Dispose OracleConnection object
                con.Close();
                con.Dispose();
            }

            return listaSocios;
        }
        public Socio ObtenerSocio(int rut)
        {
            List<Socio> listaSocios = new List<Socio>();
            Socio socio = null;

            OracleConnection con = Connect(strCon);

            OracleCommand cmd = new OracleCommand("PKG_SOCIO_CRUD.PRC_OBTIENE_SOCIO_X_RUT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            //P_ERRCOD OUT NUMBER, P_ERRMSG OUT VARCHAR2

            cmd.Parameters.Add("P_RUT", OracleDbType.Int32, rut, ParameterDirection.Input);
            cmd.Parameters.Add("P_DATA", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader;

            try
            {
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    socio = new Socio();

                    socio.IdSocio = Convert.ToInt32(reader["IDSOCIO"]);
                    socio.Rut = Convert.ToInt32(reader["RUT"]);
                    socio.Dv = Convert.ToString(reader["DV"]);
                    socio.Nombres = Convert.ToString(reader["NOMBRES"]);
                    socio.Apaterno = Convert.ToString(reader["APATERNO"]);
                    socio.Amaterno = Convert.ToString(reader["AMATERNO"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Dispose OracleCommand object
                cmd.Dispose();

                // Close and Dispose OracleConnection object
                con.Close();
                con.Dispose();
            }

            return socio;
        }
        public void CrearSocio(Socio socio)
        {
            OracleConnection con = Connect(strCon);

            OracleCommand cmd = new OracleCommand("PKG_SOCIO_CRUD.PRC_CREAR_SOCIO_EXT", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("P_RUT", OracleDbType.Int32, socio.Rut, ParameterDirection.Input);
            cmd.Parameters.Add("P_DV", OracleDbType.Varchar2, socio.Dv, ParameterDirection.Input);
            cmd.Parameters.Add("P_NOMBRES", OracleDbType.NVarchar2, socio.Nombres, ParameterDirection.Input);
            cmd.Parameters.Add("P_APATERNO", OracleDbType.NVarchar2, socio.Apaterno, ParameterDirection.Input);
            cmd.Parameters.Add("P_AMATERNO", OracleDbType.NVarchar2, socio.Amaterno, ParameterDirection.Input);
            cmd.Parameters.Add("P_SEXO", OracleDbType.Varchar2, "M", ParameterDirection.Input);
            cmd.Parameters.Add("P_FENAC", OracleDbType.Date, socio.FechaNacimiento, ParameterDirection.Input);
            cmd.Parameters.Add("P_CORREO", OracleDbType.NVarchar2, System.DBNull.Value, ParameterDirection.Input);
            cmd.Parameters.Add("P_NROCELULAR", OracleDbType.Decimal, System.DBNull.Value, ParameterDirection.Input);

            //cmd.Parameters.Add("P_ERRCOD", OracleDbType.Int32).Direction = ParameterDirection.Output;
            //cmd.Parameters.Add("P_ERRMSG", OracleDbType.Varchar2).Direction = ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Dispose OracleCommand object
                cmd.Dispose();

                // Close and Dispose OracleConnection object
                con.Close();
                con.Dispose();
            }
        }

        public void ModificarSocio(int rut, Socio socio)
        {
            OracleConnection con = Connect(strCon);

            OracleCommand cmd = new OracleCommand("PKG_SOCIO_CRUD.PRC_MODIFICAR_SOCIO", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("P_RUT", OracleDbType.Int32, rut, ParameterDirection.Input);            
            cmd.Parameters.Add("P_NOMBRES", OracleDbType.NVarchar2, socio.Nombres, ParameterDirection.Input);
            cmd.Parameters.Add("P_APATERNO", OracleDbType.NVarchar2, socio.Apaterno, ParameterDirection.Input);
            cmd.Parameters.Add("P_AMATERNO", OracleDbType.NVarchar2, socio.Amaterno, ParameterDirection.Input);
            cmd.Parameters.Add("P_SEXO", OracleDbType.Varchar2, "M", ParameterDirection.Input);
            cmd.Parameters.Add("P_FENAC", OracleDbType.Date, socio.FechaNacimiento, ParameterDirection.Input);
            cmd.Parameters.Add("P_CORREO", OracleDbType.NVarchar2, System.DBNull.Value, ParameterDirection.Input);
            cmd.Parameters.Add("P_NROCELULAR", OracleDbType.Decimal, System.DBNull.Value, ParameterDirection.Input);

            //cmd.Parameters.Add("P_ERRCOD", OracleDbType.Int32).Direction = ParameterDirection.Output;
            //cmd.Parameters.Add("P_ERRMSG", OracleDbType.Varchar2).Direction = ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Dispose OracleCommand object
                cmd.Dispose();

                // Close and Dispose OracleConnection object
                con.Close();
                con.Dispose();
            }
        }

        public void EliminarSocio(int rut)
        {
            OracleConnection con = Connect(strCon);

            OracleCommand cmd = new OracleCommand("PKG_SOCIO_CRUD.PRC_ELIMINAR_SOCIO", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("P_RUT", OracleDbType.Int32, rut, ParameterDirection.Input);

            //cmd.Parameters.Add("P_ERRCOD", OracleDbType.Int32).Direction = ParameterDirection.Output;
            //cmd.Parameters.Add("P_ERRMSG", OracleDbType.Varchar2).Direction = ParameterDirection.Output;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Dispose OracleCommand object
                cmd.Dispose();

                // Close and Dispose OracleConnection object
                con.Close();
                con.Dispose();
            }
        }
    }
}