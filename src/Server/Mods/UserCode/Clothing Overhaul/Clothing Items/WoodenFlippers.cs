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
//along with this program.  If not, see <http://www.gnu.org/licenses/>

namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Core.Items;
    using Eco.Shared.Gameplay;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items.Recipes;
    using ClothingOverhaul;
    using Eco.World.Blocks;
    using System;
    
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Wooden Flippers")] // Defines the localized name of the item.
    [LocDescription("Rudimentary flippers that allow traveling through the water at a much faster pace.")] //The tooltip description for this clothing item.
    [Weight(100)] // Defines how heavy the TallBoots is.
    [Tag("Clothes")]
    [Ecopedia("Items", "Clothing", createAsSubPage: true)]
    public partial class WoodenFlippersItem :
        ClothingItem , IClothingOverhaulBlockMovespeedDictionary
    {
        /// <summary>Slot this clothing type belongs to</summary>
        public override string Slot                     { get { return AvatarAppearanceSlots.Shoes; } }

        public override bool Starter                    { get { return false ; } }

        public Dictionary<Type, float> BlockMovespeedModifiers { get; } = new Dictionary<Type, float>()
        {
            { typeof(HewnLogCubeBlock),           -1.0f },  //  Hewn Log Blocks / Default Modifier
            { typeof(DirtBlock),                  -1.0f },  //  Dirt Blocks                                                            
            { typeof(BasaltBlock),                -1.0f },  //  Solid Rock Type Blocks      
            { typeof(DirtRoadBlock),              -1.0f },  //  Dirt Road Blocks            
            { typeof(StoneRoadCubeBlock),         -1.0f },  //  Stone Roads    
            { typeof(AsphaltConcreteCubeBlock),   -1.0f },  //  Asphalt Roads  
            { typeof(CrushedBasaltBlock),         -1.0f },  //  Crushed Type Blocks            
            { typeof(ClayBlock),                  -1.0f },  //  Clay Blocks            
            { typeof(SandBlock),                   1.0f },  //  Sand Blocks   
            { typeof(CottonCarpetBlock),          -1.0f },  //  Carpet Blocks
            { typeof(WaterBlock),                  5.0f },  //  Water Blocks            
        };
    }
    
    [RequiresSkill(typeof(TailoringSkill), 1)]
    public partial class WoodenFlippersRecipe : RecipeFamily
    {
        public WoodenFlippersRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Wooden Flippers",  //noloc
                displayName: Localizer.DoStr("Wooden Flippers"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("WoodBoard", 2, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                    new IngredientElement(typeof(LinenFabricItem), 2, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                    new IngredientElement(typeof(CelluloseFiberItem), 2, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<WoodenFlippersItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 3; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(150, typeof(TailoringSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(1);

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Tall Boots"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Wooden Flippers"), recipeType: typeof(WoodenFlippersRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(TailoringTableObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
