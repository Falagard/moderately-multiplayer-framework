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
        //Debug.Log("Got here");
        var assetDatas = EditorHelper.GetAtPath<AssetData>("Game/Data");

        foreach (var assetData in assetDatas)
        {
            Debug.Log(assetData.name + " " + assetData.Id);
        }
    }
}