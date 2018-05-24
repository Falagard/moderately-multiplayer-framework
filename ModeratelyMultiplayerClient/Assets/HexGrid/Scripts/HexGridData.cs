using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMF
{
    public class HexCellData
    {
        public int TerrainTypeIndex;
        public int Elevation;
        public int WaterLevel;
        public int UrbanLevel;
        public int FarmLevel;
        public int PlantLevel;
        public int SpecialIndex;
        public bool Walled;
        public bool HasIncomingRiver;
        public int IncomingRiver;
        public bool HasOutgoingRiver;
        public int OutgoingRiver;
        public int RoadFlags;
    }

    public class HexGridData
    {
        public int CellCountX;
        public int CellCountZ;

        public List<HexCellData> Cells;
    }    
}
