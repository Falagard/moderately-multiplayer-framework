using LiteDB;
using SQLite4Unity3d;

namespace MMF
{
    public class Asset
    {
        [PrimaryKey]
        [LiteDB.BsonId]
        public string Id { get; set; }

        public string Name { get; set; }
        public string ColliderPrefabName { get; set; }
        public string ViewPrefabName { get; set; }

    }
}