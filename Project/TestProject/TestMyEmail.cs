using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace UnitTests
{
    [TestClass]
    public class TestMyEmail
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
            String sqlcommand = "create table testMyEmail ( MyEmailID int PRIMARY KEY, email dbo.MyEmail);"
                              + "insert into testMyEmail(MyEmailID, email) values (1, 'alancarry@gmail.com');"
                              + "insert into testMyEmail(MyEmailID, email) values (2, 'alancarry@ms.com');";
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
            String sqlcommand = "DROP TABLE testMyEmail;";
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
        public void TestMyEmailToString()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select email.ToString() as test_email from testMyEmail where MyEmailID = 1;";
            String expected = "dddalancarry@gmail.com";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreNotEqual(expected, datareader["test_email"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestMyEmailUsername()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select email.Username as value from testMyEmail where MyEmailID = 1;";
            String expected = "alancarry";
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
        public void TestMyEmailHost()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select email.Host as value from testMyEmail where MyEmailID = 1;";
            String expected = "gmail.com";
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
        public void TestMyEmailSameUser()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select MyEmailID, email.ToString() as email, email.isSameUser('aaaaalancarry@op.com') as isSameUser from testMyEmail where MyEmailID = 1;";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.IsFalse((bool)datareader["isSameUser"]);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestMyEmailChangeHost()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "update testMyEmail set email.changeHost('ms.com') where MyEmailID = 1;"
                                + "select email.ToString() as email from testMyEmail where MyEmailID = 1;";
            String expected = "alancarry@ms.com";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["email"].ToString());
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