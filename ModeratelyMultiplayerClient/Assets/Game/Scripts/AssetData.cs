using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[CreateAssetMenu(fileName = "AssetData", menuName = "ModeratelyMultiplayer/AssetData", order = 1)]
public class AssetData : ScriptableObject
{
    public string Id = System.Guid.NewGuid().ToString();
    public Collider Collider;
    public GameObject ViewPrefab;
}