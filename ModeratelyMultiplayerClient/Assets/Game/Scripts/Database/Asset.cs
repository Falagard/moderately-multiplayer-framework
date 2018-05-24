using System;
using LiteDB;
//using SQLite4Unity3d;

namespace MMF
{
    public class Asset
    {
        [LiteDB.BsonId] public LiteDB.ObjectId Id;

        public string Name;
        public string ColliderPrefabName;
        public string ViewPrefabName;

    }
}