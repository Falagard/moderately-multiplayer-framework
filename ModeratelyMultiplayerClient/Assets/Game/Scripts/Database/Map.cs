using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteDB;


namespace MMF
{
    public class Map
    {
        [LiteDB.BsonId] public LiteDB.ObjectId Id;

        public string Name;

        public HexGridData HexGridData;

        public bool IsDeleted; 
    }

}

