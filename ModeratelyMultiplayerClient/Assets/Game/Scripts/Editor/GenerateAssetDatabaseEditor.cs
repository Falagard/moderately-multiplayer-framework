using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using MMF;

public class GenerateAssetDatabaseEditor : EditorWindow
{
    [MenuItem("MMF/Generate Asset Database")]
    public static void Init()
    {
        var ds = new DataService("game.db");
        ds.CreateDb();

        //Debug.Log("Got here");
        var assetDatas = EditorHelper.GetAtPath<AssetDataScriptableObject>("Game/Data");

        foreach (var assetData in assetDatas)
        {
            //Debug.Log(assetData.name + " " + assetData.Id);
            var asset = new Asset()
            {
                Name = assetData.name,
                Id = assetData.Id
            };

            ds.CreateAsset(asset);
        }
    }
}