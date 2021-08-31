using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using server;
using System.IO;
namespace server{

    
    public static class Serializer{
        static List<Room> _room = new List<Room>();

        

        

        public static string SerializeRoom(Room room){
            _room.Add(room);
            string json = JsonSerializer.Serialize(_room);
            File.WriteAllText("/home/hermeneut/netmud/server/rooms.json", json);
            return json;
        }

        public static List<Room> DeserializeRooms(string filepath){
            string json= File.ReadAllText(filepath);

            return JsonSerializer.Deserialize<List<Room>>(json);
            
        }

    }


}