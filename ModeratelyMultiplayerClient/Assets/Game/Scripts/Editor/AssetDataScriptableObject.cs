using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace MMF
{
    [CreateAssetMenu(fileName = "AssetData", menuName = "ModeratelyMultiplayer/AssetData", order = 1)]
    public class AssetDataScriptableObject : ScriptableObject
    {
        public string Id = System.Guid.NewGuid().ToString();
        public Collider Collider;
        public GameObject ViewPrefab;
    }
}