using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Net.NetworkInformation;
using MMF;
using LiteDB;

public class GenerateAssetDatabaseEditor : EditorWindow
{
    [MenuItem("MMF/Generate Asset Database")]
    public static void Init()
    {
        var connectionString = string.Format(@"Assets/StreamingAssets/{0}", "lite.db");
        var assetDatas = EditorHelper.GetAtPath<AssetDataScriptableObject>("Game/Data");

        try
        {
            using (var database = new LiteDatabase(connectionString))
            {
                database.Shrink();
                
                var collection = database.GetCollection<Asset>();

                foreach (var assetData in assetDatas)
                {
                    Debug.Log("checking " + assetData.Id.ToString());
                    var id = new ObjectId(assetData.Id);

                    //check if asset exists
                    if (collection.FindById(id) == null)
                    {
                        Debug.Log("asset not found " + assetData.Id.ToString());
                        var asset = new Asset()
                        {
                            Name = assetData.name,
                            Id = new ObjectId(assetData.Id),
                            Path = null,
                            AssetType = 1,
                            ColliderPrefabName = assetData.ColliderPrefab != null ? AssetDatabase.GetAssetPath(assetData.ColliderPrefab) : null,
                            LogicPrefabName = assetData.LogicPrefab != null ? AssetDatabase.GetAssetPath(assetData.LogicPrefab) : null,
                            ViewHighPrefabName = assetData.ViewHighPrefab != null ? AssetDatabase.GetAssetPath(assetData.ViewHighPrefab) : null,
                            ViewMediumPrefabName = assetData.ViewMediumPrefab != null ? AssetDatabase.GetAssetPath(assetData.ViewMediumPrefab) : null,
                            ViewLowPrefabName = assetData.ViewLowPrefab != null ? AssetDatabase.GetAssetPath(assetData.ViewLowPrefab) : null,
                            ViewVeryLowPrefabName = assetData.ViewVeryLowPrefab != null ? AssetDatabase.GetAssetPath(assetData.ViewVeryLowPrefab) : null,
                        };

                        collection.Insert(asset);
                    }
                    else
                    {
                        Debug.Log("asset found " + assetData.Id.ToString());
                    }
                }

            }
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}