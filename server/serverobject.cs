using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
 
namespace server
{
    public class ServerObject
    {
        static TcpListener tcpListener; 
        List<ClientObject> clients = new List<ClientObject>(); 
 
        protected internal void AddConnection(ClientObject clientObject)
        {
            clients.Add(clientObject);
        }
        protected internal void RemoveConnection(string id)
        {
            
            ClientObject client = clients.FirstOrDefault(c => c.Id == id);
           
            if (client != null)
                clients.Remove(client);
        }
        
        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                Console.WriteLine("Server started, awaiting connections...");
 
                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
 
                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Handle));
                    clientThread.Start();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }
 
        
        protected internal void BroadcastMessage(string message, string id, Room room)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            for (int i = 0; i < room.clients.Count; i++)
            {
                if (room.clients[i].Id!= id)
                {
                    room.clients[i].Stream.Write(data, 0, data.Length); 
                }
            }
        }
        protected internal void EchoMessage(string message, string id){
            byte[] data = Encoding.UTF8.GetBytes(message);
            foreach(var cl in this.clients){
                if(String.Equals(id,cl.Id)){
                    cl.Stream.Write(data,0, data.Length);
                }
            }
        }
        
        protected internal void Disconnect()
        {
            tcpListener.Stop(); 
 
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close(); 
            }
            Environment.Exit(0); 
        }
    }
}