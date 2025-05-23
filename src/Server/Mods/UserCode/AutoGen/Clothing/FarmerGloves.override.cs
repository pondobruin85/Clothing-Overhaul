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
    
    /// <summary>
    /// <para>
    /// Server side item definition for the "FarmerGloves" clothing item. 
    /// </para>
    /// <para>More information about ClothingItem objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.ClothingItem.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Modern Gloves")] // Defines the localized name of the item.
    [LocDescription("Textured palms provide increaded grip strength for the wearer.\n\n(Provides a 4kg increase in carrying capacity)")] //The tooltip description for this clothing item.
    [Weight(100)] // Defines how heavy the FarmerGloves is.
    [Tag("Clothes")]
    [Tag("Clothing - Hands")]
    [Ecopedia("Clothing", "Clothing - Hands", createAsSubPage: true)]
    public partial class FarmerGlovesItem :
        ClothingItem
    {

        /// <summary>Slot this clothing type belongs to</summary>
        public override string Slot                     { get { return AvatarAppearanceSlots.Hands; } }

        public override bool Starter                    { get { return false ; } }

        // Define our all custom UserStatTypes associated with this clothing item. These can include things such as custom
        // carry weight or movement speed.
        private static Dictionary<UserStatType, float> flatStats = new Dictionary<UserStatType, float>()
        {
            { UserStatType.MaxCarryWeight, 4000 },
        };

        // Custom GetFlatStats override to pass through our flatstats variable created above.
        public override Dictionary<UserStatType, float> GetFlatStats() { return flatStats; }

    }
    

    /// <summary>
    /// <para>Server side recipe definition for "FarmerGloves".</para>
    /// <para>More information about RecipeFamily objects can be found at https://docs.play.eco/api/server/eco.gameplay/Eco.Gameplay.Items.RecipeFamily.html</para>
    /// </summary>
    /// <remarks>
    /// This is an auto-generated class. Don't modify it! All your changes will be wiped with next update! Use Mods* partial methods instead for customization. 
    /// If you wish to modify this class, please create a new partial class or follow the instructions in the "UserCode" folder to override the entire file.
    /// </remarks>
    [RequiresSkill(typeof(TailoringSkill), 4)]
    public partial class FarmerGlovesRecipe : RecipeFamily
    {
        public FarmerGlovesRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "FarmerGloves",  //noloc
                displayName: Localizer.DoStr("Modern Gloves"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(LeatherHideItem), 2, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                    new IngredientElement(typeof(LinenFabricItem), 2, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                    new IngredientElement(typeof(IronPlateItem), 1, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<FarmerGlovesItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(800, typeof(TailoringSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(1);

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Farmer Gloves"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Modern Gloves"), recipeType: typeof(FarmerGlovesRecipe));
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
