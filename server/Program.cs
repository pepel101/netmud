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

        public static World world;
        static void Main(string[] args)
        
        {
           // string test = Serializer.SerializeRoom(Rooms.Test());
           // Console.WriteLine(test);
            
            try
            {
                world = new World{};
                world.initWorld();

                server = new ServerObject();
                
                listenThread = new Thread(new ThreadStart(server.Listen));
                listenThread.Start(); //старт потока

                if (Console.ReadLine().ToLower().Equals("disconnect")){
                    for (int i = 0; i<world.rooms.Count; i++){
                        server.BroadcastMessage("Server closing", "0", world.rooms[i]);
                   
                    }
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
