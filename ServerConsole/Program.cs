using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using System.Net;

namespace ServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            MySqlDatabase conn = new MySqlDatabase();
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                // Set the TcpListener on port 13000.
                Int32 port = 5000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                conn.OpenConnection();

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        string query = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                        Console.WriteLine("Received query: {0}", query);

                        // Process the data sent by the client.
                        var ds = conn.getDataSet(query);

                        // Send back a response.
                        binaryFormatter.Serialize(stream, ds);

                        Console.WriteLine("Sent.");
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();

                conn.CloseConnection();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}
