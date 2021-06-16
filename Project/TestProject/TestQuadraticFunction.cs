using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTests
{
    [TestClass]
    public class TestQuadraticFunction
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
            String sqlcommand = "create table testQuadraticFunction ( QuadFunID int PRIMARY KEY, QuadFun dbo.QuadraticFunction, MaxMin text );"
                                + "insert into testQuadraticFunction(QuadFunID, QuadFun) values (1, '1,3,3');"
                                + "insert into testQuadraticFunction(QuadFunID, QuadFun) values (2, '4.3,-0.05,7');";
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
            String sqlcommand = "DROP TABLE testQuadraticFunction;";
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
        public void TestQFToString()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select QuadFun.ToString() as qf from testQuadraticFunction where QuadFunID = 1;";
            String expected = "1x^2 + 3x + 3";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["qf"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestQFCountMaxMin()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "update dbo.testQuadraticFunction set MaxMin = QuadFun.countMaxMin();"              
                                + "select QuadFunID, QuadFun.ToString() as QuadFun, MaxMin from testQuadraticFunction where QuadFunID = 1;";
            String expected = "Maxvalue = Infinity; Minvalue = 0,75";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["MaxMin"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestQFGetA()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select QuadFun.A as QuadFunA where QuadFunID = 1;";
            String expected = "1";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["QuadFunA"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestQFGetB()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select QuadFun.B as QuadFunB where QuadFunID = 1;";
            String expected = "3";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["QuadFunB"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestQFGetC()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select QuadFun.C as QuadFunC where QuadFunID = 1;";
            String expected = "3";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["QuadFunC"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestQFIsEquals()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select QuadFunID, QuadFun.ToString() as QuadFun, QuadraticFunction::[isEquals](QuadFun, '4.3,-0.05,7') as isEquals from testQuadraticFunction where QuadFunID = 2;";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.IsTrue((bool)datareader["isEquals"]);
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