using System;
using System.Text;
using System.Threading;
using server;
using System.Collections.Generic;
 
namespace server
{
    class Program
    {
        static ServerObject server; // сервер
        static Thread listenThread; // потока для прослушивания
        static void Main(string[] args)
        
        {
           // string test = Serializer.SerializeRoom(Rooms.Test());
           // Console.WriteLine(test);
            
            try
            {
                

                server = new ServerObject();
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start(); //старт потока
                if (Console.ReadLine().ToLower().Equals("disconnect")){
                    server.BroadcastMessage("Server closing", "0");
                    server.Disconnect();
                }
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
