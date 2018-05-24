using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using LiteDB;

namespace MMF
{
    [CreateAssetMenu(fileName = "AssetData", menuName = "ModeratelyMultiplayer/AssetData", order = 1)]
    public class AssetDataScriptableObject : ScriptableObject
    {
        public string Id = ObjectId.NewObjectId().ToString();
        public Collider Collider;
        public GameObject ViewPrefab;
    }
}