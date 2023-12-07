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
    /* 
     * All speeds are calculated for diagonal runspeed.  Running in a cardinal direction will be ~1.42 times faster.
     * Default movespeed tops out at 5.5.  Subtracting 1.5 gives a range of -1.5 to 5.5, or 0-7.  Boots are rated 0-10.
     * 10 will give maximum movespeed, at 5.5.  ( .7 * 10 -1.5 = 5.5 )
     * 0 will give barefoot movespeed, at -1.5. ( .7 * 0 - 1.5 = -1.5 )
     * 2.14 will give old default value, at 0.  ( .7 * 2.14 - 1.5 = 0 )
     * 5 will give halfway point, at 2.5.       ( .7 * 5 - 1.5 = 2 ) -1.5 + 3.5 = 2 = -3.5 + 5.5
     * Sprinting gives roughly double value, and limits range.  Anything rated 5 or higher will hit top speed sprinting.
    */
    public class ClothingOverhaulModifierFunction : IClothingOverhaulModifierFunction
    {        
        public float CalculateModifier(User user)
        {
            float baseMovementSpeedReduction = 1.5f;           //Reducing base movespeed.
            float movespeedRatio = 0.7f;                       
            try
            {            
                float moveSpeedModifierSum = ClothingOverhaulMoveSpeedModifierCalcUtil.GetMovementSpeedModifierByBlockType(user);
                float totalMovespeedModifier = (movespeedRatio * moveSpeedModifierSum) - baseMovementSpeedReduction ;
                return totalMovespeedModifier;
            }
            catch {}
            return 0;                       
        }
    }
}