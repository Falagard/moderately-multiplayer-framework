#region Using Statements
using MarkLight.Views.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using MarkLight;
using MarkLight.Views;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MMF.UI
{

    public class MapItem
    {
        public string Name;
    }

    /// <summary>
    /// Examples demonstrating the UI views.
    /// </summary>
    public class RuntimeEditor : UIView
    {
        const int mapFileVersion = 4;

        private HexMapEditor _controller;
        private HexMapGenerator _generator;
        public _int BrushSize;
        public _bool ElevationEnabled;
        public _int Elevation;
        public _bool WaterEnabled;
        public _int WaterLevel;
        private int _terrainTypeIndex;
        public _bool RiverModeIgnore;
        public _bool RiverModeYes;
        public _bool RiverModeNo;
        public _bool RoadModeIgnore;
        public _bool RoadModeYes;
        public _bool RoadModeNo;
        public _bool WalledModeIgnore;
        public _bool WalledModeYes;
        public _bool WalledModeNo;
        public _bool UrbanEnabled;
        public _int UrbanLevel;
        public _bool FarmEnabled;
        public _int FarmLevel;
        public _bool PlantEnabled;
        public _int PlantLevel;
        public _bool SpecialEnabled;
        public _int SpecialLevel;
        public _bool GenerateMaps;
        public _string MapItemName;

        public MarkLight.Views.UI.List TexturesList;
        public MarkLight.Views.UI.Label OpenSaveTitle;

        public ViewSwitcher ContentViewSwitcher;

        public ObservableList<MapItem> MapItems;

        public MarkLight.Views.UI.Button SaveMapButton;
        public MarkLight.Views.UI.Button OpenMapButton;

        public override void Initialize()
        {
            MapItems = new ObservableList<MapItem>();
        }

        void Start()
        {
            GameObject controllerGO = GameObject.Find("RuntimeEditorController");

            if (controllerGO != null)
            {
                _controller = controllerGO.GetComponent<HexMapEditor>();
                //_controller.AddHandler(gameObject);
            }

            GameObject generatorGO = GameObject.Find("Hex Map Generator");

            if (generatorGO != null)
            {
                _generator = generatorGO.GetComponent<HexMapGenerator>();
            }

            //defaults
            RiverModeIgnore.Value = true;
            RoadModeIgnore.Value = true;
            WalledModeIgnore.Value = true;

            TexturesList.SelectItem(0);
        }

        void FillMapItems()
        {
            MapItems.Clear();
            string[] paths =
                Directory.GetFiles(Application.persistentDataPath, "*.map");
            Array.Sort(paths);
            for (int i = 0; i < paths.Length; i++)
            {
                MapItem item = new MapItem()
                {
                    Name = Path.GetFileNameWithoutExtension(paths[i])
                };
                MapItems.Add(item);
            }
        }

        public void NewClicked()
        {
            ContentViewSwitcher.SwitchTo("NewMapRegion");
        }

        public void SaveClicked()
        {
            FillMapItems();
            ContentViewSwitcher.SwitchTo("OpenSaveMapRegion");
            OpenSaveTitle.Text.Value = "Save Map";
            SaveMapButton.IsActive.Value = true;
            OpenMapButton.IsActive.Value = false;
        }

        public void OpenClicked()
        {
            FillMapItems();
            ContentViewSwitcher.SwitchTo("OpenSaveMapRegion");
            OpenSaveTitle.Text.Value = "Open Map";
            SaveMapButton.IsActive.Value = false;
            OpenMapButton.IsActive.Value = true;
        }

        public void CancelOpenSaveMapClicked()
        {
            ContentViewSwitcher.SwitchTo("MainRegion");
        }

        public void CancelNewMapClicked()
        {
            ContentViewSwitcher.SwitchTo("MainRegion");
        }

        public void OpenMapClicked()
        {
            string path = GetSelectedPath();

            if (!File.Exists(path))
            {
                Debug.LogError("File does not exist " + path);
                return;
            }
            using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
            {
                int header = reader.ReadInt32();
                if (header <= mapFileVersion)
                {
                    _controller.hexGrid.Load(reader, header);
                    HexMapCamera.ValidatePosition();
                }
                else
                {
                    Debug.LogWarning("Unknown map format " + header);
                }
            }
            ContentViewSwitcher.SwitchTo("MainRegion");
        }

        public void MapItemListItemSelected(ItemSelectionActionData itemSelectData)
        {
            string text = itemSelectData.ItemView.Text;
            MapItemName.Value = text;
        }

        public void SaveMapClicked()
        {
            string path = GetSelectedPath();

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                writer.Write(mapFileVersion);
                _controller.hexGrid.Save(writer);
            }
            ContentViewSwitcher.SwitchTo("MainRegion");
        }

        public void DeleteMapClicked()
        {
            string path = GetSelectedPath();
            if (path == null)
            {
                return;
            }
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            MapItemName.Value = "";
            FillMapItems();
        }

        string GetSelectedPath()
        {
            if (string.IsNullOrEmpty(MapItemName.Value))
            {
                return null;
            }
            return Path.Combine(Application.persistentDataPath, MapItemName.Value + ".map");
        }

        public void SmallMapClicked()
        {
            CreateMap(20, 15);
        }

        public void MediumMapClicked()
        {
            CreateMap(40, 30);
        }

        public void LargeMapClicked()
        {
            CreateMap(80, 60);
        }

        void CreateMap(int x, int z)
        {
            if (GenerateMaps.Value)
            {
                _generator.GenerateMap(x, z);
            }
            else
            {
                _controller.hexGrid.CreateMap(x, z);
            }
            //HexMapCamera.ValidatePosition();

            ContentViewSwitcher.SwitchTo("MainRegion");
        }

        public void BrushSizeValueChanged()
        {
            Debug.Log("Value changed: " + BrushSize.Value);
            
            _controller.SetBrushSize(BrushSize.Value);
        }

        public void TextureListItemSelected(ItemSelectionActionData itemSelectData)
        {
            string text = itemSelectData.ItemView.Text;

            int index = 0;

            switch (text)
            {
                case "Sand":
                    index = 0;
                    break;
                case "Grass":
                    index = 1;
                    break;
                case "Mud":
                    index = 3;
                    break;
                case "Stone":
                    index = 2;
                    break;
                case "Snow":
                    index = 4;
                    break;
            }

            _controller.SetTerrainTypeIndex(index);

            _terrainTypeIndex = index;
            
            Debug.Log("SetTerrainTypeIndex " + index);
        }
        
        public void ElevationCheckboxClicked()
        {
            //Debug.Log("Elevation Checkbox Clicked: " + ElevationEnabled.Value);
            _controller.SetApplyElevation(ElevationEnabled.Value);
        }

        public void SpecialCheckboxClicked()
        {
            _controller.SetApplySpecialIndex(SpecialEnabled.Value);
        }

        public void PlantCheckboxClicked()
        {
            _controller.SetApplyPlantLevel(PlantEnabled.Value);
        }

        public void UrbanCheckboxClicked()
        {
            _controller.SetApplyUrbanLevel(UrbanEnabled.Value);
        }

        public void FarmCheckboxClicked()
        {
            _controller.SetApplyFarmLevel(FarmEnabled.Value);
        }

        public void ElevationValueChanged()
        {
            _controller.SetElevation(Elevation.Value);
        }

        public void WaterCheckboxClicked()
        {
            //Debug.Log("Elevation Checkbox Clicked: " + ElevationEnabled.Value);
            _controller.SetApplyWaterLevel(WaterEnabled.Value);
        }

        public void WaterLevelValueChanged()
        {
            _controller.SetWaterLevel(WaterLevel.Value);
        }

        public void SpecialLevelValueChanged()
        {
            _controller.SetSpecialIndex(SpecialLevel.Value);
        }

        public void FarmLevelValueChanged()
        {
            _controller.SetFarmLevel(FarmLevel.Value);
        }

        public void UrbanLevelValueChanged()
        {
            _controller.SetUrbanLevel(UrbanLevel.Value);
        }

        public void PlantLevelValueChanged()
        {
            _controller.SetPlantLevel(PlantLevel.Value);
        }

        public void RiverModeClicked()
        {
            UpdateRiverMode();
        }

        private void UpdateRiverMode()
        {
            if (RiverModeIgnore.Value)
            {
                _controller.SetRiverMode(0);
            }
            else if (RiverModeYes.Value)
            {
                _controller.SetRiverMode(1);
            }
            else if (RiverModeNo.Value)
            {
                _controller.SetRiverMode(2);
            }
        }

        public void RoadModeClicked()
        {
            UpdateRoadMode();
        }

        private void UpdateRoadMode()
        {
            if (RoadModeIgnore.Value)
            {
                _controller.SetRoadMode(0);
            }
            else if (RoadModeYes.Value)
            {
                _controller.SetRoadMode(1);
            }
            else if (RoadModeNo.Value)
            {
                _controller.SetRoadMode(2);
            }
        }

        public void WalledModeClicked()
        {
            UpdateWalledMode();
        }

        private void UpdateWalledMode()
        {
            if (WalledModeIgnore.Value)
            {
                _controller.SetWalledMode(0);
            }
            else if (WalledModeYes.Value)
            {
                _controller.SetWalledMode(1);
            }
            else if (WalledModeNo.Value)
            {
                _controller.SetWalledMode(2);
            }
        }

        private void DisableTexturesMode()
        {
            _controller.SetTerrainTypeIndex(-1);
        }

        private void DisableFeaturesMode()
        {
            _controller.SetApplyFarmLevel(false);
            _controller.SetApplyPlantLevel(false);
            _controller.SetApplySpecialIndex(false);
            _controller.SetApplyUrbanLevel(false);
            _controller.SetWalledMode(0);
        }

        private void DisableSculptMode()
        {
            _controller.SetApplyElevation(false);
            _controller.SetApplyWaterLevel(false);
            _controller.SetRiverMode(0);
            _controller.SetRoadMode(0);
        }

        //public void ShowHideLeftSidebarToggleClicked()
        //{
        //    if (ShowHideLeftSidebarToggleButton.Text == "Hide")
        //    {
        //        SlideLeftMenuInAnimation.StartAnimation();
        //        ShowHideLeftSidebarToggleButton.Text.Value = "Show";
        //    }
        //    {
        //        SlideLeftMenuInAnimation.ReverseAnimation();
        //        ShowHideLeftSidebarToggleButton.Text.Value = "Hide";
        //    }

        //}

        public void TerrainTabSelected(TabSelectionActionData actionData)
        {
            if (actionData.TabView.Text == "Textures")
            {
                DisableSculptMode();
                DisableFeaturesMode();
                _controller.SetTerrainTypeIndex(_terrainTypeIndex);
            }
            else if (actionData.TabView.Text == "Sculpt")
            {
                DisableTexturesMode();
                DisableFeaturesMode();
                _controller.SetApplyElevation(ElevationEnabled.Value);
                _controller.SetApplyWaterLevel(WaterEnabled.Value);
                UpdateRiverMode();
                UpdateRoadMode();
            } else if (actionData.TabView.Text == "Features")
            {
                DisableTexturesMode();
                DisableSculptMode();
                _controller.SetApplyUrbanLevel(UrbanEnabled.Value);
                _controller.SetApplyFarmLevel(FarmEnabled.Value);
                _controller.SetApplyPlantLevel(PlantEnabled.Value);
                _controller.SetApplySpecialIndex(SpecialEnabled.Value);
                UpdateWalledMode();
            }
        }
    }
}

