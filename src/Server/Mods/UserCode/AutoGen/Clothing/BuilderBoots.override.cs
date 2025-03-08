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
    /// Server side item definition for the "BuilderBoots" clothing item. 
    /// </para>
    /// <para>More information about ClothingItem objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.ClothingItem.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Builder Boots")] // Defines the localized name of the item.
    [LocDescription("Whether you're building a bear or a bob, these work boots decrease calories consumed when using tools by 30\u0025)")] //The tooltip description for this clothing item.
    [Weight(100)] // Defines how heavy the BuilderBoots is.
    [Tag("Clothes")]
    [Ecopedia("Items", "Clothing", createAsSubPage: true)]
    public partial class BuilderBootsItem :
        ClothingItem , IClothingOverhaulBlockMovespeedDictionary
    {

        /// <summary>Slot this clothing type belongs to</summary>
        public override string Slot                     { get { return AvatarAppearanceSlots.Shoes; } }

        public override bool Starter                    { get { return false ; } }

        public Dictionary<Type, float> BlockMovespeedModifiers { get; } = new Dictionary<Type, float>()
        {
            { typeof(HewnLogCubeBlock),            7.0f },  //  Hewn Log Blocks / Default Modifier
            { typeof(DirtBlock),                   7.0f },  //  Dirt Blocks                                                            
            { typeof(BasaltBlock),                 7.0f },  //  Solid Rock Type Blocks      
            { typeof(DirtRoadBlock),               7.0f },  //  Dirt Road Blocks            
            { typeof(StoneRoadCubeBlock),          8.0f },  //  Stone Roads    
            { typeof(AsphaltConcreteCubeBlock),    9.0f },  //  Asphalt Roads  
            { typeof(CrushedBasaltBlock),          7.0f },  //  Crushed Type Blocks            
            { typeof(ClayBlock),                   6.5f },  //  Clay Blocks            
            { typeof(SandBlock),                   6.0f },  //  Sand Blocks   
            { typeof(CottonCarpetBlock),           6.0f },  //  Carpet Blocks
            { typeof(WaterBlock),                  0.0f },  //  Water Blocks               
            };
    }
    

    /// <summary>
    /// <para>Server side recipe definition for "BuilderBoots".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(TailoringSkill), 6)]
    public partial class BuilderBootsRecipe : RecipeFamily
    {
        public BuilderBootsRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "BuilderBoots",  //noloc
                displayName: Localizer.DoStr("Builder Boots"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(SteelPlateItem), 2, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                    new IngredientElement(typeof(LeatherHideItem), 2, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                    new IngredientElement(typeof(NylonFabricItem), 2, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<BuilderBootsItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 5; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(1200, typeof(TailoringSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(1);

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Builder Boots"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Builder Boots"), recipeType: typeof(BuilderBootsRecipe));
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
