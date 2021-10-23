// ViktorSpawners
// a Valheim mod skeleton using Jötunn
// 
// File:    ViktorSpawners.cs
// Project: ViktorSpawners

using BepInEx;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;
using HarmonyLib;
using Jotunn.Utils;


namespace Spawners
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    //[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    internal class Spawners : BaseUnityPlugin
    {
        public const string PluginGUID = "viktorbrenek.spawners";
        public const string PluginName = "Spawners";
        public const string PluginVersion = "0.0.1";
        
        // Use this class to add your own localization to the game
        // https://valheim-modding.github.io/Jotunn/tutorials/localization.html
        public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();

        private AssetBundle _embeddedResourceBundle;

        private void Awake()
        {
            // Jotunn comes with MonoMod Detours enabled for hooking Valheim's code
            // https://github.com/MonoMod/MonoMod
            On.FejdStartup.Awake += FejdStartup_Awake;
            
            // Jotunn comes with its own Logger class to provide a consistent Log style for all mods using it
            Jotunn.Logger.LogInfo("ModStub has landed");

            // To learn more about Jotunn's features, go to
            // https://valheim-modding.github.io/Jotunn/tutorials/overview.html

            LoadAssetBundle();


            UnloadAssetBundle();

            

        }

        private void FejdStartup_Awake(On.FejdStartup.orig_Awake orig, FejdStartup self)
        {
            // This code runs before Valheim's FejdStartup.Awake
            Jotunn.Logger.LogInfo("FejdStartup is going to awake");

            // Call this method so the original game method is invoked
            orig(self);

            // This code runs after Valheim's FejdStartup.Awake
            Jotunn.Logger.LogInfo("FejdStartup has awoken");
        }
         private void LoadAssetBundle()
        {
            // Load asset bundle from embedded resources
           
            Jotunn.Logger.LogInfo($"Embedded resources: {string.Join(",", typeof(Spawners).Assembly.GetManifestResourceNames())}");
            _embeddedResourceBundle = AssetUtils.LoadAssetBundleFromResources("shroomerspawner", typeof(Spawners).Assembly);
             
                // Create and add a custom item
            
            var shroomerSpawner = _embeddedResourceBundle.LoadAsset<GameObject>("shroomerspawner");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(shroomerSpawner, true)); 

            var Shroomer = _embeddedResourceBundle.LoadAsset<GameObject>("Shroomer");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(Shroomer, true)); 

            var Shroomer_ragdoll = _embeddedResourceBundle.LoadAsset<GameObject>("Shroomer_ragdoll");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(Shroomer_ragdoll, true)); 

            var Shroomie = _embeddedResourceBundle.LoadAsset<GameObject>("Shroomie");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(Shroomie, true)); 

            var RedCrystal = _embeddedResourceBundle.LoadAsset<GameObject>("RedCrystal");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(RedCrystal, true)); 

            var Shroomer_throw_projectile = _embeddedResourceBundle.LoadAsset<GameObject>("Shroomer_throw_projectile");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(Shroomer_throw_projectile, true)); 

            var TrophyShroomer = _embeddedResourceBundle.LoadAsset<GameObject>("TrophyShroomer");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(TrophyShroomer, true)); 

            var ShroomerSpear = _embeddedResourceBundle.LoadAsset<GameObject>("ShroomerSpear");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(ShroomerSpear, true)); 

            var Recipe_ShroomerSpear = _embeddedResourceBundle.LoadAsset<GameObject>("Recipe_ShroomerSpear");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(Recipe_ShroomerSpear, true)); 

            var sfx_shroomer_alerted = _embeddedResourceBundle.LoadAsset<GameObject>("sfx_shroomer_alerted");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(sfx_shroomer_alerted, true)); 

            var sfx_shroomer_idle = _embeddedResourceBundle.LoadAsset<GameObject>("sfx_shroomer_idle");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(sfx_shroomer_idle, true)); 

        }

        ////////////////// Tady přidávám recept do Workbenche

        private static void RecipeShroomerSpear(ItemDrop itemDrop)
        {
            // Create and add a recipe for the copied item
            Recipe recipe = ScriptableObject.CreateInstance<Recipe>();
            recipe.name = "Recipe_ShroomerSpear";
            recipe.m_item = itemDrop;
            recipe.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench");
            recipe.m_resources = new Piece.Requirement[]
            {
                    new Piece.Requirement()
                    {
                        m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("RedCrystal"),
                        m_amount = 4
                    },
                    new Piece.Requirement()
                    {
                        m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("Wood"),
                        m_amount = 10
                    },
                    new Piece.Requirement()
                    {
                        m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>("LeatherScraps"),
                        m_amount = 2
                    }
            };

            // Since we got the prefabs from the cache, no referencing is needed
            CustomRecipe CR = new CustomRecipe(recipe, fixReference: false, fixRequirementReferences: false);
            ItemManager.Instance.AddRecipe(CR);
        }

        ////////////////// Tady přidávám do Hammeru Spawnery
        
        

        private void UnloadAssetBundle()
        {
            _embeddedResourceBundle.Unload(false);
        }
    }
}