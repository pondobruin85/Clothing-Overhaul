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
            float calorieReduction = 0f;
            bool isSwimming = Eco.World.World.GetBlock(user.Position.XYZi()).IsWater();

            foreach (ItemStack playerInventoryItem in user.Inventory.Clothing.NonEmptyStacks)                                                   //Go through the player's clothing and sum the movespeed modifiers to moveSpeedModifierSum;
            {

                if (playerInventoryItem.Item is IClothingOverhaulCalorieRateVars calorieClothing)
                {
                    if (isSwimming && calorieClothing.IsSwimwear)
                    {
                        calorieReduction += calorieClothing.MaxCalorieRateBonus;
                    }
                    if (!isSwimming && !calorieClothing.IsSwimwear)
                    {
                        float clothingTemp = calorieClothing.TemperatureValue;
                        float temperatureAbsoluteDifference = Math.Abs(clothingTemp - userTemp);
                        if (temperatureAbsoluteDifference <= 0.17f)
                        {
                            calorieReduction += calorieClothing.MaxCalorieRateBonus;
                        }
                        if (temperatureAbsoluteDifference > 0.17f && temperatureAbsoluteDifference <= 0.34f)
                        {
                            calorieReduction += calorieClothing.MaxCalorieRateBonus * (.34f - temperatureAbsoluteDifference) / .17f;         //this gives a 0-100% multiplier to the max reduction bonus.
                        }
                    }
                }
            }
            return calorieReduction;
        }
    }           
}