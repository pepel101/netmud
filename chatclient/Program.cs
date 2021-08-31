using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
 
namespace chatclient
{
    class Program
    {
        static string userName;
        static string host;
        static int port ;
        static TcpClient client;
        static NetworkStream stream;
 
        static void Main(string[] args)
        {
            Console.Write("Enter the IP adress, including 'localhost': ");
            Program.host = Console.ReadLine();
            Console.Write("enter the port: ");
            Program.port=Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter your username: ");
            userName = Console.ReadLine();
            client = new TcpClient();
            try
            {
                client.Connect(host, port); 
                stream = client.GetStream(); 
 
                string message = userName;
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
 
                
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); 
                Console.WriteLine("Welcome, {0}", userName);
                SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }
       
        static void SendMessage()
        {
            Console.WriteLine("Enter your message: ");
             
            while (true)
            {
                string message = Console.ReadLine();
                

                byte[] data = Encoding.UTF8.GetBytes(message);
                
                
                stream.Write(data, 0, data.Length);

                if (String.Equals(message.ToLower(),"disconnect")){
                    
                    Disconnect();
                }
            }
        }
        
        static void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; 
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
 
                    string message = builder.ToString();
                    Console.WriteLine(message);
                    if (String.Equals(message.ToLower(), "server closing")){
                        Disconnect();
                    }
                }
                catch
                {
                    Console.WriteLine("Connection closed!"); 
                    Console.ReadLine();
                    Disconnect();
                }
            }
        }
 
        static void Disconnect()
        {
            if(stream!=null)
                stream.Close();
            if(client!=null)
                client.Close();
            Environment.Exit(0); 
        }
    }
}