

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
    using static System.Runtime.InteropServices.JavaScript.JSType;

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
    [LocDisplayName("Rubber Flippers")] // Defines the localized name of the item.
    [LocDescription("These flippers allow moving through the water at remarkable speed.")] //The tooltip description for this clothing item.
    [Weight(100)] // Defines how heavy the BuilderBoots is.
    [Tag("Clothes")]
    [Ecopedia("Items", "Clothing", createAsSubPage: true)]
    public partial class RubberFlippersItem :
        ClothingItem , IClothingOverhaulBlockMovespeedDictionary
    {

        /// <summary>Slot this clothing type belongs to</summary>
        public override string Slot                     { get { return AvatarAppearanceSlots.Shoes; } }

        public override bool Starter                    { get { return false ; } }

        public Dictionary<Type, float> BlockMovespeedModifiers { get; } = new Dictionary<Type, float>()
        {
            { typeof(HewnLogCubeBlock),             2.0f          },  //  Hewn Log Blocks / Default Modifier 
            { typeof(BasaltBlock),                  2.0f          },  //  Solid Rock Type Blocks      
            { typeof(CrushedBasaltBlock),           2.0f          },  //  Crushed Type Blocks
            { typeof(TailingsBlock),                2.0f          },  //  Tailings Blocks
            { typeof(DirtBlock),                    2.0f          },  //  Dirt Blocks
            { typeof(ClayBlock),                    2.0f          },  //  Clay Blocks   
            { typeof(MudBlock),                     2.0f          },  //  Mud Blocks    
            { typeof(SandBlock),                    2.0f     /0.8f},  //  Sand Blocks    (0.8 Is Eco's built in Efficiency Multiplier)            
            { typeof(CompostBlock),                 2.0f          },  //  Compost Blocks
            { typeof(GarbageBlock),                 2.0f          },  //  Garbage Blocks
            { typeof(SewageBlock),                  2.0f          },  //  Sewage Blocks            
            { typeof(WetTailingsBlock),             2.0f          },  //  WetTailing Blocks            
            { typeof(CottonCarpetBlock),            2.0f          },  //  Carpet Blocks
            { typeof(WaterBlock),                  10.0f          },  //  Water Blocks
            { typeof(DirtRampBlock),                2.0f          },  //  Dirt Ramp Blocks
            { typeof(DirtRoadBlock),                2.0f          },  //  Dirt Road Blocks            
            { typeof(StoneRoadCubeBlock),           2.0f     /1.1f},  //  Stone Roads    (1.1 Is Eco's built in Efficiency Multiplier)
            { typeof(AsphaltConcreteCubeBlock),     2.0f     /1.2f},  //  Asphalt Roads  (1.2 Is Eco's built in Efficiency Multiplier)
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
    [RequiresSkill(typeof(TailoringSkill), 5)]
    public partial class RubberFlippersRecipe : RecipeFamily
    {
        public RubberFlippersRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Rubber Flippers",  //noloc
                displayName: Localizer.DoStr("Rubber Flippers"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(SteelBarItem), 1, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                    new IngredientElement(typeof(SyntheticRubberItem), 5, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                    new IngredientElement(typeof(NylonFabricItem), 5, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                    new IngredientElement(typeof(LinenFabricItem), 5, typeof(TailoringSkill), typeof(TailoringLavishResourcesTalent)),
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<RubberFlippersItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 5; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(1200, typeof(TailoringSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(1);

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Builder Boots"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Rubber Flippers"), recipeType: typeof(RubberFlippersRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(AdvancedTailoringTableObject), recipe: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
