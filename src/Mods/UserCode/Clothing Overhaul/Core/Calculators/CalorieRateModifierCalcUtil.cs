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
    public sealed class CalorieRateModifierCalcUtil
    {
        
        public static float GetCalorieRateModifier(User user)
        {
            float userTemp = WorldLayerManager.Obj.ClimateSim.Temperature.AverageOverBoundaryAlignedWorldPos(user.Position.XZi());

            //foreach (ItemStack playerInventoryItem in user.Inventory.Clothing.NonEmptyStacks)                                                   //Go through the player's clothing and sum the movespeed modifiers to moveSpeedModifierSum;
            //{
            //    ClothingItem? clothingItem = playerInventoryItem.Item as ClothingItem;
            //    IClothingOverhaulBlockMovespeedDictionary? movespeedClothing = playerInventoryItem.Item as IClothingOverhaulBlockMovespeedDictionary;

            //    if (movespeedClothing != null)
            //    {
                    
            //        moveSpeedModifierSum += movespeedClothing.BlockMovespeedModifiers[blockType];                           // Get the value for the block modifier from the clothing item's dictionary and add it to the modifier value.
            //        if (clothingItem != null && clothingItem.Slot == AvatarAppearanceSlots.Shoes && clothingItem is IClothingOverhaulBlockMovespeedDictionary)
            //        {
            //            isWearingShoeItem = true;
            //        }
            //    }
            //}


            return 0f;
        }
    }           
}

/*
 * 
 * 
            for each clothing piece = shirt and shoes
            check value compared - each shirt, pants and hat need a rate (property)?
            add calorie rate reducers.  shirt gives 20%, pants 20%, hat 10% for proper temps only.  bad value in wrong climate.
            temp is float 0 to 1.
            breakpoints are .35, .65.  cold clothes get max value at .35, warm clothes get max value at .65  above .65.  cold clothes provide no benefit.
            give clothing piece float value, .16, .5, or .84.  |value - temp| = x < .17, full benefit.  .17<x<.34, partial benefit.  x > .34, no benefit.  
            hat give max -.1. pants -.2, shirt -.2.  so   -.2 
                

 * 
 * 
 * 
 * 
 * 
 * 
 */
