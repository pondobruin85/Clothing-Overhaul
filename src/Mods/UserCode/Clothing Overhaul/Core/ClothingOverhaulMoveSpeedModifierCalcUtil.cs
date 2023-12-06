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
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.ModKit.Internal;
using Eco.Mods.TechTree;
using Eco.Shared.Gameplay;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Utils;
using Eco.Shared.Voxel;
using Eco.Simulation.WorldLayers;
using Eco.World.Blocks;
using System;
using User = Eco.Gameplay.Players.User;

namespace ClothingOverhaul
{
    public sealed class ClothingOverhaulMoveSpeedModifierCalcUtil
    {        
        public static float GetMovementSpeedModifierByBlockType(User user)
        {
            float moveSpeedModifierSum = 0;
            bool isWearingShoeItem = false;
            Block blockUnderPlayer = Eco.World.World.GetBlock((user.Position.XYZi() - new Vector3i(0, 1, 0)));                                  // Get the block under the player by subracting 1 from the Y Axis.
            Type blockUnderPlayerBlockType = blockUnderPlayer.GetType();       
            
            if (blockUnderPlayerBlockType == typeof(EncasedWaterBlock)) { blockUnderPlayerBlockType = typeof(WaterBlock); }                     // Water Blocks become water blocks.
            if (blockUnderPlayer.ToString().Contains("Crushed")) { blockUnderPlayerBlockType = typeof(CrushedBasaltBlock); }                    // Any blocks with "Crushed" in the name become CrushedBasaltBlock to reduce dictionary entries.
            if (blockUnderPlayer.ToString().Contains("AsphaltConcrete")) { blockUnderPlayerBlockType = typeof(AsphaltConcreteCubeBlock); }      // Any blocks with "AsphaltConcrete" in the name become AsphaltConcreteCubeBlock to reduce dictionary entries.
            if (blockUnderPlayer.ToString().Contains("StoneRoad")) { blockUnderPlayerBlockType = typeof(StoneRoadCubeBlock); }                  // Any blocks with "StoneRoad" in the name become StoneRoadCubeBlock to reduce dictionary entries.
            if (blockUnderPlayer.ToString().Contains("Carpet")) { blockUnderPlayerBlockType = typeof(CottonCarpetBlock); }                      // Any blocks with "Carpet" in the name become CottonCarpetBlock to reduce dictionary entries.
            if (blockUnderPlayer.ToString().Contains("DirtRamp")) { blockUnderPlayerBlockType = typeof(DirtRampBlock); }                        // Any blocks with "DirtRamp" in the name become DirtRampBlock to reduce dictionary entries.
            if (blockUnderPlayer.ToString().Contains("Garbage")) { blockUnderPlayerBlockType = typeof(GarbageBlock); }                          // Any blocks with "Garbage" in the name become GarbageBlock to reduce dictionary entries.
            if (blockUnderPlayer.ToString().Contains("Concentrate")) { blockUnderPlayerBlockType = typeof(SandBlock ); }                        // Any blocks with "Concentrate" in the name become SandBlock to reduce dictionary entries.
            if (blockUnderPlayer is DirtBlock) { blockUnderPlayerBlockType = typeof(DirtBlock); }                                               // Any blocks that inherit from Dirt Block become Dirt Block to reduce dictionary entries..
            foreach (string blockUnderPlayerTagNames in blockUnderPlayer.TagNames())
            { if (blockUnderPlayerTagNames.Contains("Minable")) { blockUnderPlayerBlockType = typeof(BasaltBlock); } }                          // Any blocks with "Minable" Tag become BasaltBlock to reduce dictionary entries.
                        
            foreach (ItemStack playerInventoryItem in user.Inventory.Clothing.NonEmptyStacks)                                                   //Go through the player's clothing and sum the movespeed modifiers to moveSpeedModifierSum;
            {
                ClothingItem? clothingItem = playerInventoryItem.Item as ClothingItem;
                if (clothingItem != null && clothingItem.Slot == AvatarAppearanceSlots.Shoes){isWearingShoeItem = true;}                        // Check if player is wearing any shoe type.

                IClothingOverhaulBlockMovespeedDictionary? movespeedClothing = playerInventoryItem.Item as IClothingOverhaulBlockMovespeedDictionary;           

                if (movespeedClothing != null)
                {
                    if (movespeedClothing.BlockMovespeedModifiers.ContainsKey(blockUnderPlayerBlockType))
                    {
                        moveSpeedModifierSum += movespeedClothing.BlockMovespeedModifiers[blockUnderPlayerBlockType];                           // Get the value for the block modifier from the clothing item's dictionary and add it to the modifier value.
                    }
                    else
                    {
                        moveSpeedModifierSum+= movespeedClothing.BlockMovespeedModifiers[typeof(HewnLogCubeBlock)];                             // Any block not explicitly defined in dictionary is defined as a HewnLogCubeBlock and returns that key value.
                    }                    
                }
            }
            if (isWearingShoeItem) { return moveSpeedModifierSum; }                                                                             // If the Player is wearing shoes, return the moveSpeedModifierSum.
            else 
            { 
                ClothingOverhaulBarefootDictionary BarefootDictionary = new ClothingOverhaulBarefootDictionary();
                
                if (BarefootDictionary.BlockMovespeedModifiers.ContainsKey(blockUnderPlayerBlockType))
                {
                    moveSpeedModifierSum += BarefootDictionary.BlockMovespeedModifiers[blockUnderPlayerBlockType];                              // Get the value for the block modifier from the clothing item's dictionary and add it to the modifier value.
                }
                else
                {
                    moveSpeedModifierSum += BarefootDictionary.BlockMovespeedModifiers[typeof(HewnLogCubeBlock)];                               // Any block not explicitly defined in dictionary is defined as a HewnLogCubeBlock and returns that key value.
                }
                return moveSpeedModifierSum;                                                                                                    // If the Player is not wearing shoes, return the moveSpeedModifierSum.
            }                 
        }
    }
}