using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace TerminalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.SetWindowSize((int)(Console.LargestWindowWidth/1.05), (int)(Console.LargestWindowHeight/1.3));

            string sqlconnection = @"DATA SOURCE=MSSQLSERVER39;"
                       + "INITIAL CATALOG=project; INTEGRATED SECURITY=SSPI;";

            string sqlcommand = "";

            SqlConnection connection = null;

            while (true)
            {
                Console.WriteLine("\n\n");

                int option;

                Console.Write(
@"
####################################" + "\n"
+ "   Baza danych kierowców firmy" +
"\n####################################"
+
@"
#                                  #
#       Wybierz opcję:             #
# 1. Wyświetl wszystkie rekordy.   #
# 2. Wyświetl wszystkie rekordy    #
#    posortowane według            #
#    województw.                   #
# 3. Znajdź rekord według email.   #
#                                  #
# 4. Dodaj nowego kierowcę.        #
# 5. Dodaj procent do kary.        #
# 6. Oznacz naruszenie jako        #
#    opłacone.                     #
#                                  #
# 0. Wyjście z aplikacji.          #
#                                  #
####################################
                                  
Wprowadź opcję (0-6): "
            );

                option = -1;
                try
                {
                    option = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                }
                catch (FormatException)
                {
                    continue;
                }

                switch (option)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        sqlcommand = "select id, email.ToString() as email, licence.ToString() as licence, color.ToString() as color,"
                                    + "plate.ToString() as plate, motion_equation.ToString() as motion_equation, violation.ToString() as violation from Driver;";


                        connection = new SqlConnection(sqlconnection);

                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlcommand, connection);
                            SqlDataReader datareader = command.ExecuteReader();

                            Console.WriteLine(String.Format("|{0,2}|{1,22}|{2,40}|{3,13}|{4,8}|{5,23}|{6,110}|", "id", "email", "prawo jazdy", "kolor",
                                    "numer", "opis ruchu", "naruszenie"));

                            while (datareader.Read())
                            {
                                
                                Console.Write(String.Format("|{0,2}|{1,22}|{2,40}|{3,13}|{4,8}|{5,23}|{6,110}|", datareader[0].ToString(), datareader[1].ToString(),
                                    datareader[2].ToString(), datareader[3].ToString(), datareader[4].ToString(), datareader[5].ToString(), datareader[6].ToString()));
                                Console.Write("\n");
                            }
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine("\nError!");
                            String mess = ex.Message.Split(new string[] { "Exception: " }, StringSplitOptions.None)[1].Split('\n')[0];
                            Console.WriteLine(mess);
                        }
                        finally { connection.Close(); }
                        break;
                    case 2:
                        sqlcommand = "select id, email.ToString() as email,"
                                            + "plate.ToString() as plate, plate.Province as province,"
                                            + "DENSE_RANK() OVER (ORDER BY plate.Province) AS province_group from Driver where plate is not NULL;";


                        connection = new SqlConnection(sqlconnection);

                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlcommand, connection);
                            SqlDataReader datareader = command.ExecuteReader();

                            Console.WriteLine(String.Format("|{0,2}|{1,22}|{2,8}|{3,15}|{4,5}|", "id", "email",
                                    "numer", "województwo", "grupa"));

                            while (datareader.Read())
                            {

                                Console.Write(String.Format("|{0,2}|{1,22}|{2,8}|{3,15}|{4,5}|", datareader[0].ToString(), datareader[1].ToString(),
                                    datareader[2].ToString(), datareader[3].ToString(), datareader[4].ToString()));
                                Console.Write("\n");
                            }
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine("\nError!");
                            String mess = ex.Message.Split(new string[] { "Exception: " }, StringSplitOptions.None)[1].Split('\n')[0];
                            Console.WriteLine(mess);
                        }
                        finally { connection.Close(); }
                        break;
                    case 3:
                        Console.Write("Wprowadź email: ");
                        string email = Console.ReadLine();
                        Console.Write("\n");

                        sqlcommand = "select id, email.ToString() as email, licence.ToString() as licence, color.ToString() as color,"
                                    + "plate.ToString() as plate, plate.Province as province, motion_equation.ToString() as motion_equation,"
                                    + "violation.ToString() as violation from Driver where email.ToString() = '" + email + "';";


                        connection = new SqlConnection(sqlconnection);

                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlcommand, connection);
                            SqlDataReader datareader = command.ExecuteReader();

                            while (datareader.Read())
                            {
                                Console.WriteLine("id: " + datareader[0].ToString());
                                Console.WriteLine("email: " + datareader[1].ToString());
                                Console.WriteLine("prawo jazdy: " + datareader[2].ToString());
                                Console.WriteLine("kolor samochodu: " + datareader[3].ToString());
                                Console.WriteLine("numer samochodu: " + datareader[4].ToString());
                                Console.WriteLine("województwo: " + datareader[5].ToString());
                                Console.WriteLine("opis ruchu: " + datareader[6].ToString());
                                Console.WriteLine("naruszenie: " + datareader[7].ToString());
                                Console.Write("\n");
                            }
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine("\nError!");
                            String mess = ex.Message.Split(new string[] { "Exception: " }, StringSplitOptions.None)[1].Split('\n')[0];
                            Console.WriteLine(mess);
                        }
                        finally { connection.Close(); }
                        break;
                    case 4:
                        Console.WriteLine("Wprowadź email np. (user@host.com) ");
                        email = Console.ReadLine();

                        Console.WriteLine("Wprowadź dane prawa jazdy np. D,03/25/2013 ");
                        string licence = Console.ReadLine();

                        Console.WriteLine("Wprowadź kolor samochodu np. (153, 190, 227) ");
                        string color = Console.ReadLine();

                        Console.WriteLine("Wprowadź numer samochodu np. WA6642E ");
                        string plate = Console.ReadLine();

                        Console.WriteLine("Wprowadź opis ruchu np. -2.5,5, ");
                        string qf = Console.ReadLine();

                        Console.WriteLine("Wprowadź naruszenie np. Przekroczony limit prędkości, 11/11/2020 01:22:54, 500, Nie ");
                        string violation = Console.ReadLine();

                        Console.Write("\n");

                        sqlcommand = "insert into Driver (email, licence, color, plate, motion_equation, violation) values ('"
                            + email + "', '" + licence + "', '" + color + "', '" + plate + "', '" + qf + "', '" + violation +"')";


                        connection = new SqlConnection(sqlconnection);

                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlcommand, connection);
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine("\nError!");
                            String mess = ex.Message.Split(new string[] { "Exception: " }, StringSplitOptions.None)[1].Split('\n')[0];
                            Console.WriteLine(mess);
                        }
                        finally { connection.Close(); }
                        break;
                    case 5:
                        Console.Write("Wprowadź id: ");
                        string id = Console.ReadLine();
                        Console.Write("Wprowadź procent naliczki: ");
                        string perc = Console.ReadLine();

                        sqlcommand = "update Driver set violation.ToPay = Violation.surcharge(" + perc + ") where id = " + id + " and violation.IsPaid = 'false';";

                        connection = new SqlConnection(sqlconnection);

                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlcommand, connection);
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine("\nError!");
                            String mess = ex.Message.Split(new string[] { "Exception: " }, StringSplitOptions.None)[1].Split('\n')[0];
                            Console.WriteLine(mess);
                        }
                        finally { connection.Close(); }
                        break;
                    case 6:
                        Console.Write("Wprowadź id: ");
                        id = Console.ReadLine();

                        sqlcommand = "update Driver set violation.IsPaid = 'true' where id = " + id + ";";

                        connection = new SqlConnection(sqlconnection);

                        try
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(sqlcommand, connection);
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine("\nError!");
                            String mess = ex.Message.Split(new string[] { "Exception: " }, StringSplitOptions.None)[1].Split('\n')[0];
                            Console.WriteLine(mess);
                        }
                        finally { connection.Close(); }
                        break;
                    default:
                        break;
                }   
            }
        }
    }
}