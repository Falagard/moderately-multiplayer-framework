#region Using Statements
using MarkLight.Views.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MarkLight;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MMF.UI
{
    /// <summary>
    /// Examples demonstrating the UI views.
    /// </summary>
    public class RuntimeEditor : UIView
    {
        private HexMapEditor _controller;
        public _int BrushSize;
        public _bool ElevationEnabled;
        public _int Elevation;
        public _bool WaterEnabled;
        public _int WaterLevel;
        private int _terrainTypeIndex;
        public _bool RiverModeIgnore;
        public _bool RiverModeYes;
        public _bool RiverModeNo;
        
        void Start()
        {
            GameObject controllerGO = GameObject.Find("RuntimeEditorController");

            if (controllerGO != null)
            {
                _controller = controllerGO.GetComponent<HexMapEditor>();
                //_controller.AddHandler(gameObject);
            }
            else
            {
                Debug.Log("RuntimeEditorController GameObject not found");
            }

            //defaults
            RiverModeIgnore.Value = true;
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
        }

        public void TerrainTabSelected(TabSelectionActionData actionData)
        {
            if (actionData.TabView.Text == "Textures")
            {
                DisableSculptMode();
                DisableFeaturesMode();
                _controller.SetTerrainTypeIndex(_terrainTypeIndex);
                _controller.SetRiverMode(0);
            }
            else if (actionData.TabView.Text == "Sculpt")
            {
                DisableTexturesMode();
                DisableFeaturesMode();
                _controller.SetApplyElevation(ElevationEnabled.Value);
                _controller.SetApplyWaterLevel(WaterEnabled.Value);
                UpdateRiverMode();
            } else if (actionData.TabView.Text == "Features")
            {
                DisableTexturesMode();
                DisableSculptMode();

            }
        }
    }
}

