//Clothing Overhaul
//Copyright (C) 2023 Seth Reavis
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.using System;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.ModKit.Internal;
using Eco.Mods.TechTree;
using Eco.Shared.Gameplay;
using Eco.Shared.Math;
using Eco.Shared.Voxel;
using Eco.Simulation.WorldLayers;
using Eco.World.Blocks;
using System;
using System.Collections.Generic;

namespace ClothingOverhaul
{
    public partial class ClothingOverhaulBarefootDictionary : IClothingOverhaulBlockMovespeedDictionary
    {
        public Dictionary<Type, float> BlockMovespeedModifiers { get; } = new Dictionary<Type, float>()
        {
            { typeof(HewnLogCubeBlock),             2.0f          },  //  Hewn Log Blocks / Default Modifier 
            { typeof(BasaltBlock),                  2.0f          },  //  Solid Rock Type Blocks      
            { typeof(CrushedBasaltBlock),           1.0f          },  //  Crushed Type Blocks
            { typeof(TailingsBlock),                1.0f          },  //  Tailings Blocks
            { typeof(DirtBlock),                    2.0f          },  //  Dirt Blocks
            { typeof(ClayBlock),                    1.5f          },  //  Clay Blocks   
            { typeof(MudBlock),                     1.5f          },  //  Mud Blocks    
            { typeof(SandBlock),                    1.0f     /0.8f},  //  Sand Blocks    (0.8 Is Eco's built in Efficiency Multiplier)            
            { typeof(CompostBlock),                 1.0f          },  //  Compost Blocks
            { typeof(GarbageBlock),                 1.0f          },  //  Garbage Blocks
            { typeof(SewageBlock),                  1.0f          },  //  Sewage Blocks            
            { typeof(WetTailingsBlock),             1.0f          },  //  WetTailing Blocks            
            { typeof(CottonCarpetBlock),           10.0f          },  //  Carpet Blocks
            { typeof(WaterBlock),                   3.0f          },  //  Water Blocks
            { typeof(DirtRampBlock),                3.0f          },  //  Dirt Ramp Blocks
            { typeof(DirtRoadBlock),                3.0f          },  //  Dirt Road Blocks            
            { typeof(StoneRoadCubeBlock),           3.5f     /1.1f},  //  Stone Roads    (1.1 Is Eco's built in Efficiency Multiplier)
            { typeof(AsphaltConcreteCubeBlock),     4.0f     /1.2f},  //  Asphalt Roads  (1.2 Is Eco's built in Efficiency Multiplier)
        };
    }
}

