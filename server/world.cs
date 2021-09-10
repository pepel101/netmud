using System;
using System.Collections.Generic;
namespace server{
    public class World{
        public List<ClientObject> clients = new List<ClientObject>();

        public List<Room> rooms = new List<Room>();

        public World newWorld(){
            return (new World{});
        }

        public void initWorld(){
            rooms = Serializer.DeserializeRooms("/home/hermeneut/netmud/server/rooms.json");
        }

        public Room getRoomById(int aID){
            foreach (var r in this.rooms){
                if (r.areaID==aID){
                    return r;
                }

            }
            return null;
        }

        public void handleMoved(ClientObject client, string direction){
            foreach (var ex in client.room.exits){
                if(String.Equals(ex.direction,direction)){
                    client.room  = this.getRoomById(ex.areaID); 
                }
            }
        }
        
    }
}