using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace UnitTests
{
    [TestClass]
    public class TestLicence
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        static string sqlconnection = @"DATA SOURCE=MSSQLSERVER39;"
                + "INITIAL CATALOG=project; INTEGRATED SECURITY=SSPI;";

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "create table testLicence ( LicenceID int PRIMARY KEY, DriverLicence dbo.Licence );"
                                + "insert into testLicence(LicenceID, DriverLicence) values (1, 'B,05/05/2005');"
                                + "insert into testLicence(LicenceID, DriverLicence) values (2, 'C1+E,10/16/1998');";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                datareader.Read();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "DROP TABLE testLicence;";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                datareader.Read();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestLicenceToString()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select DriverLicence.ToString() as licence from testLicence where LicenceID = 1;";
            String expected = "Kategoria: B; Data wydania: 2005-05-05";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreNotEqual(expected, datareader["licence"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestLicenceCategory()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select DriverLicence.Category as category from testLicence where LicenceID = 1;";
            String expected = "B";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["category"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestLicenceDate()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select DriverLicence.Date as date from testLicence where LicenceID = 1;";
            String expected = "2005-05-05 00:00:00";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["date"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestLicenceSetDate()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "update dbo.testLicence set DriverLicence.Date = '10/10/2001' where LicenceID = 1;"
                                + "select DriverLicence.Date as date from testLicence where LicenceID = 1;";
            String expected = "2001-10-10 00:00:00";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["date"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }
    }
}