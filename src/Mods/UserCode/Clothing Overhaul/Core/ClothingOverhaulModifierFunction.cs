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
using Eco.Gameplay.Players;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Utils;
using Eco.Simulation;
using System;

namespace ClothingOverhaul
{
    public class ClothingOverhaulModifierFunction : IClothingOverhaulModifierFunction
    {        
        public float CalculateModifier(User user)
        {
            float baseMovementSpeedReduction = 1.8f;           // Reducing base movespeed to 1.5.
            float movespeedRatio = 0.698529f;                  // Convert from 10 Rating to proper scale.
            try
            {            
                float dictionaryValue = ClothingOverhaulMoveSpeedModifierCalcUtil.GetMovementSpeedModifierByBlockType(user);
                float totalMovespeedModifier = (movespeedRatio * dictionaryValue) - baseMovementSpeedReduction ;
                return totalMovespeedModifier;
            }
            catch {}
            return 0;                       
        }
    }
}