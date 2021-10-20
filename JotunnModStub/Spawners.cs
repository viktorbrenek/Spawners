// JotunnModStub
// a Valheim mod skeleton using Jötunn
// 
// File:    JotunnModStub.cs
// Project: JotunnModStub

using BepInEx;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;
using HarmonyLib;
using Jotunn.Utils;


namespace JotunnModStub
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
            _embeddedResourceBundle = AssetUtils.LoadAssetBundleFromResources("spawnerbossone", typeof(Spawners).Assembly);
             
                // Create and add a custom item
            var eikthyrSpawner = _embeddedResourceBundle.LoadAsset<GameObject>("eikthyrspawner");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(eikthyrSpawner, true)); 
        }

        private void UnloadAssetBundle()
        {
            _embeddedResourceBundle.Unload(false);
        }
    }
}