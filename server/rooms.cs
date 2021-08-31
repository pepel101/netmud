using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server{

public class Rooms{
    public static List<Room> rooms = new List<Room>();
    public static void Test(){
        
         rooms = Serializer.DeserializeRooms("/home/hermeneut/netmud/server/rooms.json");
         Console.WriteLine(rooms[0].exits[0].direction);

    }


}



}