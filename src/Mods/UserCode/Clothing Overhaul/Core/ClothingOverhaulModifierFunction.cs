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
    /// <summary>
    /// Scale the benefit by the amount of food and housing xp the player has
    /// in such a way as to require both sources of xp to give any benefit
    /// </summary>
    public class ClothingOverhaulModifierFunction : IClothingOverhaulModifierFunction
    {        
        public float CalculateModifier(User user)
        {
            float builtInBaseMovementSpeed = 3.3f;
            float movespeedRatio = 0.5f;
            try
            {            
                float moveSpeedModifierSum = ClothingOverhaulMoveSpeedModifierCalcUtil.GetMovementSpeedModifierByBlockType(user);
                float totalMovespeedModifier = (movespeedRatio * moveSpeedModifierSum) - builtInBaseMovementSpeed ;     //  -3.3 removes all player movement on all block types.
                return totalMovespeedModifier;
            }
            catch {}
            return 0;                       
        }
    }
}