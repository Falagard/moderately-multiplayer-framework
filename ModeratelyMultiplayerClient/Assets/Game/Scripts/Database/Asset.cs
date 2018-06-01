using System;
using LiteDB;
//using SQLite4Unity3d;

namespace MMF
{
    public class Asset
    {
        [LiteDB.BsonId] public LiteDB.ObjectId Id;

        public string Name;
        public string Path;
        public int AssetType;
        public int Size;
        public string LogicPrefabName;
        public string ColliderPrefabName;
        public string ViewHighPrefabName;
        public string ViewMediumPrefabName;
        public string ViewLowPrefabName;
        public string ViewVeryLowPrefabName;
    }
}