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

        public List<Socio> ListaSocios1()
        {
            List<Socio> listaSocios = new List<Socio>();
            Socio socio = null;

            OracleConnection con = Connect(strCon);

            OracleCommand cmd = new OracleCommand("PKG_SOCIO_CRUD.PRC_LISTA_SOCIOS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            //P_ERRCOD OUT NUMBER, P_ERRMSG OUT VARCHAR2

            OracleParameter p1 = cmd.Parameters.Add("P_ERRCOD", OracleDbType.Int32);
            p1.Direction = ParameterDirection.Output;

            OracleParameter p2 = cmd.Parameters.Add("P_ERRMSG", OracleDbType.Varchar2);
            p2.Direction = ParameterDirection.Output;

            OracleParameter p3 = cmd.Parameters.Add("P_DATA", OracleDbType.RefCursor);
            p3.Direction = ParameterDirection.Output;

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
    }
}