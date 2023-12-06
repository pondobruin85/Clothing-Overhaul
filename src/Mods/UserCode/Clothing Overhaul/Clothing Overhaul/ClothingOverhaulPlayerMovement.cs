﻿//Clothing Overhaul
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
using ClothingOverhaul;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Utils;
using Eco.Simulation;
using System;

namespace ClothingOverhaul
{
    [Benefit]
    public partial class ClothingOverhaulPlayerMovement : ClothingOverhaulBase
    {
        protected virtual ClothingBasedStatModifiersRegister ClothingModifiersRegister { get; } = new ClothingBasedStatModifiersRegister();

        public ClothingOverhaulPlayerMovement()
        {           
            ClothingOverhaulModifierFunction = new ClothingOverhaulModifierFunction();     
        }
        public override void ApplyClothingOverhaulToUser(User user)
        {  
            IDynamicValue modifier = new ClothingOverhaulDynamicValue (ClothingOverhaulModifierFunction);

            Action updatePlayerLocation = user.ChangedMovementSpeed;
            ClothingModifiersRegister.AddModifierToUser(user, UserStatType.MovementSpeed, modifier, updatePlayerLocation);
        }
        public override void RemoveClothingOverhaulFromUser(User user)
        {
        }
    }
}
