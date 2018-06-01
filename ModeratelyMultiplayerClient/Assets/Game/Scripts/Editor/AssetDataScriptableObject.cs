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
        public string Path;
        public int AssetType;
        public int Size;
        public GameObject LogicPrefab;
        public Collider ColliderPrefab;
        public GameObject ViewHighPrefab;
        public GameObject ViewMediumPrefab;
        public GameObject ViewLowPrefab;
        public GameObject ViewVeryLowPrefab;
    }
}