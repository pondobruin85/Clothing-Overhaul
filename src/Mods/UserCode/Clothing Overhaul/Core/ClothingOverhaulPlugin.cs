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
using Eco.Core.Plugins.Interfaces;
using Eco.Core.Utils;
using Eco.Core.Utils.Threading;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClothingOverhaul
{
    /// <summary>
    /// Alter clothing bonuses to have unique modifiers according to the type of block the player is standing on.
    /// The clothing and blocktype can have a positive or negative benefit to movespeed, maximum calories, calorie burn rate, carry capacity, carry stack size, and animal detection range.  
    /// </summary>
    public partial class ClothingOverhaulPlugin : IModKitPlugin, IInitializablePlugin
    {
        public string GetCategory() => "Mod";
        public string GetStatus() => (ClothingOverhaulMod.Any() ? "Loaded Clothing Overhaul:" + string.Concat(ClothingOverhaulMod.Select(overhaul => " " + overhaul.GetType().Name)) : "No Clothing Overhaul loaded");
        public List<ILoggedInClothingOverhaul> ClothingOverhaulMod { get; } = new List<ILoggedInClothingOverhaul>();
        public void Initialize(TimedTask timer)
        {
            ClothingOverhaulMod.AddRange(DiscoverILoggedInClothingOverhaul());
            Log.WriteLine(Localizer.DoStr("Clothing Overhaul Status: " + GetStatus()));
            ModsChangeBenefits();
            UserManager.OnUserLoggedIn.Add(OnUserLoggedIn);
            UserManager.OnUserLoggedOut.Add(OnUserLoggedOut);
            PeriodicWorkerFactory.CreateWithInterval(TimeSpan.FromSeconds(2), SwimSpeedUpdater.ChangeGlobalSwimSpeed).Start();  //100 for linux servers, 1 for windows servers.
        }
        partial void ModsChangeBenefits();
        private IEnumerable<ILoggedInClothingOverhaul> DiscoverILoggedInClothingOverhaul()
        {
            var types = Assembly.GetExecutingAssembly().DefinedTypes.Where(type => type.IsAssignableTo(typeof(ILoggedInClothingOverhaul)) && Attribute.GetCustomAttributes(type).Any(attribute => attribute is BenefitAttribute));
            var constructors = types.Select(type => type.DeclaredConstructors.FirstOrDefault(constructor => constructor.GetParameters().Length == 0)).NonNull();
            var classes = constructors.Select(c => c.Invoke(null)).NonNull();
            return classes.OfType<ILoggedInClothingOverhaul>();
        }
        private void OnUserLoggedIn(User user)
        {
            foreach(ILoggedInClothingOverhaul modifier in ClothingOverhaulMod)
            {
                modifier.ApplyClothingOverhaulToUser(user);
            }
        }
        private void OnUserLoggedOut(User user)
        {
            foreach(ILoggedInClothingOverhaul modifier in ClothingOverhaulMod)
            {
                modifier.RemoveClothingOverhaulFromUser(user);
            }
        }
    }
}