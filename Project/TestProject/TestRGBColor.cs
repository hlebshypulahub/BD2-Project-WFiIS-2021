using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace UnitTests
{
    [TestClass]
    public class TestRGBColor
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
            String sqlcommand = "create table testRGBColor ( ColorID int PRIMARY KEY, Color dbo.RGBColor );"
                                + "insert into testRGBColor(ColorID, Color) values (1, '(153,190, 227)');"
                                + "insert into testRGBColor(ColorID, Color) values (2, '(22, 85,244 )');";
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
            String sqlcommand = "DROP TABLE testRGBColor;";
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
        public void TestRGBColorToString()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select Color.ToString() as color from testRGBColor where ColorID = 1;";
            String expected = "(153,190,227)";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["color"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestRGBColorR()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select Color.R as r from testRGBColor where ColorID = 2;";
            String expected = "22";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["r"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestRGBColorG()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select Color.G as g from testRGBColor where ColorID = 2;";
            String expected = "85";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["g"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestRGBColorB()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select Color.B as b from testRGBColor where ColorID = 2;";
            String expected = "244";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["b"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestRGBColorAdd()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "update testRGBColor set Color = RGBColor::[add]('(50,75,150)', '(50,75,150)') where ColorID = 1;"
                                + "select ColorID, Color.ToString() as Color from dbo.testRGBColor where ColorID = 1;";
            String expected = "(100,150,255)";
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlcommand, connection);
                SqlDataReader datareader = command.ExecuteReader();
                while (datareader.Read())
                {
                    Assert.AreEqual(expected, datareader["Color"].ToString());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
        }

        [TestMethod]
        public void TestRGBColorIsEquals()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            String sqlcommand = "select ColorID, Color.ToString() as Color, RGBColor::[isEquals]('(22,85,244)', Color) as isEquals from dbo.testRGBColor where ColorID = 2;";
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