using System;
using System.Net.Sockets;
using System.Text;
using System.Linq;
 
namespace server
{
    public class ClientObject
    {
        protected internal string Id { get; private set; }
        protected internal NetworkStream Stream {get; private set;}
        string userName;
        TcpClient client;
        ServerObject server; 
        Character character;

        Room room;
        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);

        }
 
        public void Process()
        {
            Program.world.rooms[0].clients.Add(this);
            try
            {
                
                if ((Program.world.rooms[0].clients[0].Id).Equals(this.Id)){
                    Console.WriteLine("added to room"+Program.world.rooms[0].name);
                    }
                this.room = Program.world.rooms[0];
                Stream = client.GetStream();
                
                string message = GetMessage();
                userName = message;
 
                message = userName + " entered the room";
                
                server.BroadcastMessage(message, this.Id, this.room);
                Console.WriteLine(message);
                
                while (true)
                {
                    try
                    {
                        message = GetMessage();
                        var command = message.Split(" ").First();
                        switch(command){
                            //case "go":  Program.world.handleMoved(this,this.room.exits[0]);
                            //break;
                            //case "say":

                                
                        }

                        message = String.Format("{0}: {1}", userName, message);
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.Id, this.room);
                    }
                    catch
                    {
                        message = String.Format("{0}: left the chat", userName);
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, this.Id, this.room);
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                
                server.RemoveConnection(this.Id);
                Close();
            }
        }
 
        
        private string GetMessage()
        {
            byte[] data = new byte[64]; 
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                if (String.Equals(builder.ToString().ToLower(),"disconnect")){
                    server.RemoveConnection(this.Id);
                    Close();

                }
            }
            while (Stream.DataAvailable);
 
            return builder.ToString();
        }
 
       
        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }
}