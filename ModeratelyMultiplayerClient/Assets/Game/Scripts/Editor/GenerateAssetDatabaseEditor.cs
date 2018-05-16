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
        var ds = new DataService("game.db");
        ds.CreateDb();

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
                    var asset = new Asset()
                    {
                        Name = assetData.name,
                        Id = assetData.Id
                    };

                    collection.Insert(asset);
                }

            }
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }
        
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