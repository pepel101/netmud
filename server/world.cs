using System;
using System.Collections.Generic;
namespace server{
    public class World{
        List<ClientObject> clients = new List<ClientObject>();

        List<Room> rooms = new List<Room>();

        public World newWorld(){
            return (new World{});
        }

        public void initWorld(){
            rooms = Serializer.DeserializeRooms("/home/hermeneut/netmud/server/rooms.json");
        }
        
    }
}