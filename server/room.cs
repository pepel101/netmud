using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server{
    public class Room{

        public enum Terrain{
            Inside,
            Outside
        }

        public enum roomType{
            Safe,
            Dungeon
        }

        public string name {get;set;}

        public string area{get;set;}
        public int areaID{get;set;}

        public List<Exit> exits {get;set;}

        public Terrain terrain {get;set;}

        public List<ClientObject> clients{get;set;}

    }


}