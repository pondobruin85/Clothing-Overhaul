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
using Eco.Gameplay.Plants;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.ModKit.Internal;
using Eco.Mods.Organisms;
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
        public static Block GetBlockAffectingUserMovement(User user)
        {
            Block blockAtPlayer = Eco.World.World.GetBlock(user.Position.XYZi());
            if (blockAtPlayer.GetType() == typeof(EmptyBlock) || blockAtPlayer.GetType() == typeof(PlantBlock))
            {
                blockAtPlayer = Eco.World.World.GetBlock(user.Position.XYZi() - new Vector3i(0, 1, 0));
            }
            return blockAtPlayer;
        }
        private static Type GetBlockTypeAffectingUserMovement(Block blockAtPlayer)
        {         
            Type blockAtPlayerBlockType = typeof(HewnLogCubeBlock);            
            if (blockAtPlayer is DirtRoadBlock) { return typeof(DirtRoadBlock); }                                       // Return Dirt Roads first because they are also dirt blocks.
            if (blockAtPlayer is DirtBlock) { return typeof(DirtBlock); }                                               // Any blocks that inherit from Dirt Block become Dirt Block to reduce dictionary entries..
            if (blockAtPlayer is IWaterBlock) { return typeof(WaterBlock); }                                            // Water Blocks become water blocks.
            if (blockAtPlayer is TreeDebrisBlock) { return typeof(TreeDebrisBlock); }                                   // Tree Debris also treated like Sand.
            if (blockAtPlayer is TailingsBlock) { return typeof(CrushedBasaltBlock); }                                  // Tailings treated as Crushed Basalt.
            if (blockAtPlayer is CompostBlock) { return typeof(SandBlock); }                                            // Compost blocks treated as Sand.
            if (blockAtPlayer is SewageBlock) { return typeof(SandBlock); }                                             // Sewage Blocks treated as Sand Blocks.
            if (blockAtPlayer is WetTailingsBlock) { return typeof(SandBlock); }                                        // Wet Tailiings treated as Sand.
            if (blockAtPlayer is MudBlock) { return typeof(ClayBlock); }                                                // Mud treated as Clay.
            if (blockAtPlayer.ToString().Contains("AsphaltConcrete")) { return typeof(AsphaltConcreteCubeBlock); }      // Any blocks with "AsphaltConcrete" in the name become AsphaltConcreteCubeBlock to reduce dictionary entries.
            if (blockAtPlayer.ToString().Contains("StoneRoad")) { return typeof(StoneRoadCubeBlock); }                  // Any blocks with "StoneRoad" in the name become StoneRoadCubeBlock to reduce dictionary entries.
            if (blockAtPlayer.ToString().Contains("Crushed")) { return typeof(CrushedBasaltBlock); }                    // Any blocks with "Crushed" in the name become CrushedBasaltBlock to reduce dictionary entries.
            if (blockAtPlayer.ToString().Contains("Concentrate")) { return typeof(SandBlock); }                         // Any blocks with "Concentrate" in the name become SandBlock to reduce dictionary entries.
            if (blockAtPlayer.ToString().Contains("Garbage")) { return typeof(SandBlock); }                             // Any blocks with "Garbage" in the name become SandBlock to reduce dictionary entries.
            if (blockAtPlayer.ToString().Contains("Carpet")) { blockAtPlayerBlockType = typeof(CottonCarpetBlock); }    // Any blocks with "Carpet" in the name become CottonCarpetBlock to reduce dictionary entries.
            foreach (string blockAtPlayerTagNames in blockAtPlayer.TagNames())
            { if (blockAtPlayerTagNames.Contains("Minable")) { return typeof(BasaltBlock); } }                          // Any blocks with "Minable" Tag become BasaltBlock to reduce dictionary entries.
            return blockAtPlayerBlockType;
        }
        private static float FindSumOfMoveSpeedBonuses(User user, Type blockType)
        {
            float moveSpeedModifierSum = 0;
            bool isWearingShoeItem = false;

            foreach (ItemStack playerInventoryItem in user.Inventory.Clothing.NonEmptyStacks)                                                   //Go through the player's clothing and sum the movespeed modifiers to moveSpeedModifierSum;
            {
                ClothingItem? clothingItem = playerInventoryItem.Item as ClothingItem;
                IClothingOverhaulBlockMovespeedDictionary? movespeedClothing = playerInventoryItem.Item as IClothingOverhaulBlockMovespeedDictionary;

                if (movespeedClothing != null)
                {
                    moveSpeedModifierSum += movespeedClothing.BlockMovespeedModifiers[blockType];                           // Get the value for the block modifier from the clothing item's dictionary and add it to the modifier value.
                    if (clothingItem != null && clothingItem.Slot == AvatarAppearanceSlots.Shoes && clothingItem is IClothingOverhaulBlockMovespeedDictionary)
                    {
                        isWearingShoeItem = true;
                    }
                }
            }
            if (!isWearingShoeItem)
            {
                ClothingOverhaulBarefootDictionary BarefootDictionary = new ClothingOverhaulBarefootDictionary();
                moveSpeedModifierSum += BarefootDictionary.BlockMovespeedModifiers[blockType];                              // Get the value for the block modifier from the clothing item's dictionary and add it to the modifier value.
            }
            return moveSpeedModifierSum;
        }

        public static float GetBlockEfficiencyBonus(Block playerBlock, Type playerBlockType)
        {
            if (playerBlockType.GetRoadEfficiency() > 0)
            {
                return playerBlockType.GetRoadEfficiency();
            }
            else return playerBlock.Get<MoveEfficiency>()?.Efficiency ?? 1f;
        }
        public static float GetMovementSpeedModifier(User user)
        {
            Block playerBlock = GetBlockAffectingUserMovement(user);
            Type playerBlockType = GetBlockTypeAffectingUserMovement(playerBlock);            
            float sumOfMoveSpeedBonuses = FindSumOfMoveSpeedBonuses(user, playerBlockType);
            float blockEfficiencyBonus = GetBlockEfficiencyBonus(playerBlock, playerBlockType);
            float baseMoveSpeedReduction = 1.8f;                                                                    // conversion bonus converts 0-10 scale into 0-7 scale.  This is added to
            float conversionMultiplier = 0.7f;                                                                      // base move speed, which is 3.3. Then we subract 1.8, giving us a 1.5-8.5 
                                                                                                                    // scale (8.5 is needed bonus for max diagonal walking speed on asphalt roads.
            return (conversionMultiplier * sumOfMoveSpeedBonuses - baseMoveSpeedReduction) / blockEfficiencyBonus;  // Finally, efficiency bonus is divided out.
        }
    }           
}
