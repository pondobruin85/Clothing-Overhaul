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
            { typeof(HewnLogCubeBlock),             0.0f          },  //  Hewn Log Blocks / Default Modifier 
            { typeof(BasaltBlock),                  0.0f          },  //  Solid Rock Type Blocks      
            { typeof(CrushedBasaltBlock),          -0.2f          },  //  Crushed Type Blocks
            { typeof(TailingsBlock),               -0.2f          },  //  Tailings Blocks
            { typeof(DirtBlock),                    0.0f     /0.9f},  //  Dirt Blocks        (0.9 Is Eco's built in Efficiency Multiplier) 
            { typeof(ClayBlock),                   -0.1f          },  //  Clay Blocks   
            { typeof(MudBlock),                    -0.1f          },  //  Mud Blocks    
            { typeof(SandBlock),                  -0.15f     /0.8f},  //  Sand Blocks        (0.8 Is Eco's built in Efficiency Multiplier)            
            { typeof(CompostBlock),               -0.15f          },  //  Compost Blocks           
            { typeof(SewageBlock),                -0.15f          },  //  Sewage Blocks            
            { typeof(WetTailingsBlock),           -0.15f          },  //  WetTailing Blocks            
            { typeof(CottonCarpetBlock),           10.0f          },  //  Carpet Blocks
            { typeof(WaterBlock),                   2.0f          },  //  Water Blocks            
            { typeof(DirtRoadBlock),                2.0f    /1.05f},  //  Dirt Road Blocks   (1.05 Modded efficiency)         
            { typeof(StoneRoadCubeBlock),           2.5f     /1.1f},  //  Stone Roads        (1.1 Is Eco's built in Efficiency Multiplier)
            { typeof(AsphaltConcreteCubeBlock),     3.0f     /1.2f},  //  Asphalt Roads      (1.2 Is Eco's built in Efficiency Multiplier)
        };
    }
}

