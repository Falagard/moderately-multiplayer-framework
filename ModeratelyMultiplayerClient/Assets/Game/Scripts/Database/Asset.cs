using SQLite4Unity3d;

namespace MMF
{
    public class Asset
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Name { get; set; }
        public string ColliderPrefabName { get; set; }
        public string ViewPrefabName { get; set; }

    }
}