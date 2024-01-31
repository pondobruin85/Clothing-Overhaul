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
//along with this program.  If not, see <http://www.gnu.org/licenses/>
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
    public partial class BarefootMovespeedDictionary : IClothingOverhaulBlockMovespeedDictionary
    {
        public Dictionary<Type, float> BlockMovespeedModifiers { get; } = new Dictionary<Type, float>()
        {
            { typeof(HewnLogCubeBlock),            1.75f },  //  Hewn Log Blocks / Default Modifier
            { typeof(DirtBlock),                   0.25f },  //  Dirt Blocks                                                            
            { typeof(BasaltBlock),                 0.25f },  //  Solid Rock Type Blocks      
            { typeof(DirtRoadBlock),                2.0f },  //  Dirt Road Blocks            
            { typeof(StoneRoadCubeBlock),           2.5f },  //  Stone Roads    
            { typeof(AsphaltConcreteCubeBlock),    2.75f },  //  Asphalt Roads  
            { typeof(CrushedBasaltBlock),           0.0f },  //  Crushed Type Blocks            
            { typeof(ClayBlock),                    0.0f },  //  Clay Blocks            
            { typeof(SandBlock),                  -0.15f },  //  Sand Blocks   
            { typeof(CottonCarpetBlock),           10.0f },  //  Carpet Blocks
            { typeof(WaterBlock),                   0.5f },  //  Water Blocks            
        };
    }
}

