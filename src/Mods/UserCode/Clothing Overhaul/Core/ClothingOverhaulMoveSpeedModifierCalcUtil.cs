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
using System.Numerics;
using User = Eco.Gameplay.Players.User;

namespace ClothingOverhaul
{
    public sealed class ClothingOverhaulMoveSpeedModifierCalcUtil
    {
        private static bool IsWearingShoeItem(User user)
        {
            foreach (ItemStack playerInventoryItem in user.Inventory.Clothing.NonEmptyStacks)                                                   
            {
                ClothingItem? clothingItem = playerInventoryItem.Item as ClothingItem;
                if (clothingItem != null && clothingItem.Slot == AvatarAppearanceSlots.Shoes)
                {
                    return true;
                }                
            }
            return false;
        }
        private static Type BlockTypeAffectingUserMovement(User user)
        {
            Block blockAtPlayer = Eco.World.World.GetBlock(user.Position.XYZi());
            Type blockAtPlayerBlockType = typeof(HewnLogCubeBlock);
            if (blockAtPlayer.GetType() == typeof(EmptyBlock))
            {
                blockAtPlayer = Eco.World.World.GetBlock(user.Position.XYZi() - new Vector3i(0, 1, 0));            
            }
            if (blockAtPlayer is DirtBlock) { return typeof(DirtBlock); }                                               // Any blocks that inherit from Dirt Block become Dirt Block to reduce dictionary entries..
            if (blockAtPlayer is IWaterBlock) { return typeof(WaterBlock); }                                            // Water Blocks become water blocks.
            if (blockAtPlayer.ToString().Contains("AsphaltConcrete")) { return typeof(AsphaltConcreteCubeBlock); }      // Any blocks with "AsphaltConcrete" in the name become AsphaltConcreteCubeBlock to reduce dictionary entries.
            if (blockAtPlayer.ToString().Contains("StoneRoad")) { return typeof(StoneRoadCubeBlock); }                  // Any blocks with "StoneRoad" in the name become StoneRoadCubeBlock to reduce dictionary entries.
            if (blockAtPlayer.ToString().Contains("DirtRamp")) { return typeof(DirtRoadBlock); }                        // Any blocks with "DirtRamp" in the name become DirtRoadBlock to reduce dictionary entries.
            if (blockAtPlayer.ToString().Contains("Crushed")) { return typeof(CrushedBasaltBlock); }                    // Any blocks with "Crushed" in the name become CrushedBasaltBlock to reduce dictionary entries.
            if (blockAtPlayer.ToString().Contains("Concentrate")) { return typeof(SandBlock); }                         // Any blocks with "Concentrate" in the name become SandBlock to reduce dictionary entries.
            if (blockAtPlayer.ToString().Contains("Garbage")) { return typeof(SandBlock); }                             // Any blocks with "Garbage" in the name become SandBlock to reduce dictionary entries.
            if (blockAtPlayer.ToString().Contains("Carpet")) { blockAtPlayerBlockType = typeof(CottonCarpetBlock); }    // Any blocks with "Carpet" in the name become CottonCarpetBlock to reduce dictionary entries.
            foreach (string blockAtPlayerTagNames in blockAtPlayer.TagNames())
            { if (blockAtPlayerTagNames.Contains("Minable")) { return typeof(BasaltBlock); } }                          // Any blocks with "Minable" Tag become BasaltBlock to reduce dictionary entries.
            return blockAtPlayerBlockType;
        }

        public static float GetMovementSpeedModifierByBlockType(User user)
        {
            float moveSpeedModifierSum = 0;    
            Type blockType = BlockTypeAffectingUserMovement(user);
            
            if(IsWearingShoeItem(user))
            {
                foreach (ItemStack playerInventoryItem in user.Inventory.Clothing.NonEmptyStacks)                                                   //Go through the player's clothing and sum the movespeed modifiers to moveSpeedModifierSum;
                {
                    ClothingItem? clothingItem = playerInventoryItem.Item as ClothingItem;
                    IClothingOverhaulBlockMovespeedDictionary? movespeedClothing = playerInventoryItem.Item as IClothingOverhaulBlockMovespeedDictionary;

                    if (movespeedClothing != null)
                    {
                        moveSpeedModifierSum += movespeedClothing.BlockMovespeedModifiers[blockType];                           // Get the value for the block modifier from the clothing item's dictionary and add it to the modifier value.    
                    }
                }
                return moveSpeedModifierSum;
            }                                                                                                                                     // If the Player is wearing shoes, return the moveSpeedModifierSum.
            else 
            { 
                ClothingOverhaulBarefootDictionary BarefootDictionary = new ClothingOverhaulBarefootDictionary();

                foreach (ItemStack playerInventoryItem in user.Inventory.Clothing.NonEmptyStacks)                                                   //Go through the player's clothing and sum the movespeed modifiers to moveSpeedModifierSum;
                {
                    ClothingItem? clothingItem = playerInventoryItem.Item as ClothingItem;
                    IClothingOverhaulBlockMovespeedDictionary? movespeedClothing = playerInventoryItem.Item as IClothingOverhaulBlockMovespeedDictionary;

                    if (movespeedClothing != null)
                    {
                        moveSpeedModifierSum += movespeedClothing.BlockMovespeedModifiers[blockType];                           // Get the value for the block modifier from the clothing item's dictionary and add it to the modifier value.    
                    }
                }
                moveSpeedModifierSum += BarefootDictionary.BlockMovespeedModifiers[blockType];                              // Get the value for the block modifier from the clothing item's dictionary and add it to the modifier value.
                return moveSpeedModifierSum;                                                                                            // If the Player is not wearing shoes, return the moveSpeedModifierSum.
            }                 
        }
    }
}