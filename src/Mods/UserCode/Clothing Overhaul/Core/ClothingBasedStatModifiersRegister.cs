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
using Eco.Core.Controller;
using Eco.Core.Utils;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Utils;
using Eco.Shared.View;
using Eco.Simulation;
using System;
using System.Collections.Generic;


namespace ClothingOverhaul
{
    public class ClothingBasedStatModifiersRegister
    {
        /// <summary>
        /// Stores the skill rate change listeners
        /// </summary>;
        private IDictionary<(User, UserStatType), Action> ClothingOverhaulListeners { get; } = new ThreadSafeDictionary<(User, UserStatType), Action>();

        /// <summary>
        /// Add the benefit amount to the current stat in the form of an IDynamicValue
        /// </summary>
        /// <param name="user"></param>
        /// <param name="statType"></param>
        /// <param name="modifier"></param>
        /// <param name="callbackToUpdateStat">If not null, watch the skill rate property and call the callback to update the stat when the skill rate changes</param>
        public virtual void AddModifierToUser(User user, UserStatType statType, IDynamicValue modifier, Action callbackToUpdateStat)
        {
            //Rather than add and remove the modifier every log in/out, set it once when they first log in and it will last until the server shuts down
            if (!ClothingOverhaulListeners.ContainsKey((user, statType)))
            {
                UserStat stat = user.ModifiedStats.GetStat(statType);
                stat.ModifierSkill = new MultiDynamicValue(MultiDynamicOps.Sum, stat.ModifierSkill, modifier);

                //Eco will wait for the User's Position to change, then the callback will force the game to recalculate the stat
                user.Subscribe("Position", callbackToUpdateStat);
                ClothingOverhaulListeners.Add((user, statType), callbackToUpdateStat);
                callbackToUpdateStat();
            }
        }
        public virtual void RemoveClothingOverhaulFromUser(User user, UserStatType statType) { }
    }
}