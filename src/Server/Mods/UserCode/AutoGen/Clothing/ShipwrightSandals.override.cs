﻿// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
// <auto-generated from ClothingTemplate.tt/>

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
    using System;
    using ClothingOverhaul;
    using Eco.World.Blocks;

    /// <summary>
    /// <para>
    /// Server side item definition for the "ShipwrightSandals" clothing item. 
    /// </para>
    /// <para>More information about ClothingItem objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.ClothingItem.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Modern Sandals")] // Defines the localized name of the item.
    [LocDescription("These fashionable kicks are perfect for zipping around city streets and other urban areas.  Provides modest speed in the wilderness.")] //The tooltip description for this clothing item.
    [Weight(100)] // Defines how heavy the ShipwrightSandals is.
    [Tag("Clothes")]
    [Ecopedia("Items", "Clothing", createAsSubPage: true)]
    public partial class ShipwrightSandalsItem :
        ClothingItem , IClothingOverhaulBlockMovespeedDictionary
    {

        /// <summary>Slot this clothing type belongs to</summary>
        public override string Slot                     { get { return AvatarAppearanceSlots.Shoes; } }

        public override bool Starter                    { get { return false ; } }

        public Dictionary<Type, float> BlockMovespeedModifiers { get; } = new Dictionary<Type, float>()
        {
            { typeof(HewnLogCubeBlock),            7.0f },  //  Hewn Log Blocks / Default Modifier
            { typeof(DirtBlock),                   6.5f },  //  Dirt Blocks                                                            
            { typeof(BasaltBlock),                 6.0f },  //  Solid Rock Type Blocks      
            { typeof(DirtRoadBlock),               8.0f },  //  Dirt Road Blocks            
            { typeof(StoneRoadCubeBlock),          9.0f },  //  Stone Roads    
            { typeof(AsphaltConcreteCubeBlock),   10.0f },  //  Asphalt Roads  
            { typeof(CrushedBasaltBlock),          6.0f },  //  Crushed Type Blocks            
            { typeof(ClayBlock),                   6.0f },  //  Clay Blocks            
            { typeof(SandBlock),                   5.5f },  //  Sand Blocks   
            { typeof(CottonCarpetBlock),           7.0f },  //  Carpet Blocks
            { typeof(WaterBlock),                  5.0f },  //  Water Blocks               
        };
    }
    

    /// <summary>
    /// <para>Server side recipe definition for "ShipwrightSandals".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(TailoringSkill), 6)]
    public partial class ShipwrightSandalsRecipe : RecipeFamily
    {
        public ShipwrightSandalsRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ShipwrightSandals",  //noloc
                displayName: Localizer.DoStr("Modern Sandals"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(LinenFabricItem), 2, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                    new IngredientElement(typeof(NylonFabricItem), 2, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                    new IngredientElement(typeof(SyntheticRubberItem), 2, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<ShipwrightSandalsItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 3; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(1200, typeof(TailoringSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(1);

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Shipwright Sandals"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Modern Sandals"), recipeType: typeof(ShipwrightSandalsRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(AdvancedTailoringTableObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
