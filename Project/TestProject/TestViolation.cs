using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace UnitTests
{
    [TestClass]
    public class TestViolation
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
            String sqlcommand = "create table testViolation ( ViolationID int PRIMARY KEY, Violtn dbo.Violation );"
                                + "insert into testViolation(ViolationID, Violtn) values (1, 'Przekroczony limit prędkości, 11/11/2020 01:22:54,500.1, Nie');";
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
            String sqlcommand = "DROP TABLE testViolation;";
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
        public void TestViolationToString()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select ViolationID, Violtn.ToString() as Violation from dbo.testViolation where ViolationID = 1;";
            String expected = "Przekroczony limit prędkości; Czas: 2020-11-11 01:22:54; Do zapłaty: 500,1; Opłacone: Nie";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["Violation"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestViolationSurcharge()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "update testViolation set Violtn.ToPay = Violtn.surcharge(50) where ViolationID = 1;"
                                + "select ViolationID, Violtn.ToString() as Violation from dbo.testViolation where ViolationID = 1;";
            String expected = "Przekroczony limit prędkości; Czas: 2020-11-11 01:22:54; Do zapłaty: 750,15; Opłacone: Nie";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["Violation"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestViolationSetIsPaid()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "update testViolation set Violtn.IsPaid = 'true' where ViolationID = 1;"
                                + "select ViolationID, Violtn.ToString() as Violation from dbo.testViolation where ViolationID = 1;";
            String expected = "Przekroczony limit prędkości; Czas: 2020-11-11 01:22:54; Do zapłaty: 750,15; Opłacone: Tak";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["Violation"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestViolationDescription()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select Violtn.Description as value from dbo.testViolation where ViolationID = 1;";
            String expected = "Przekroczony limit prędkości";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["value"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestViolationIsPaid()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select Violtn.IsPaid as value from dbo.testViolation where ViolationID = 1;";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.IsFalse((bool)datareader["value"]);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestViolationDate()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select Violtn.Date as value from dbo.testViolation where ViolationID = 1;";
            String expected = "2020-11-11 01:22:54";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["value"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestViolationToPay()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select Violtn.ToPay as value from dbo.testViolation where ViolationID = 1;";
            String expected = "750,15";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["value"].ToString());
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