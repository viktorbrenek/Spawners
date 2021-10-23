using BepInEx;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System.Reflection;
using UnityEngine;

namespace Shroomer
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    internal class Shroomer: BaseUnityPlugin
    {
        public const string PluginGUID = "com.you.shroomer";
        public const string PluginName = "shroomer";
        public const string PluginVersion = "0.0.2";

        private AssetBundle viktorshroom;

        private void Awake()
        {
            LoadAssets();
            ShroomerSpear();
        }

        private void LoadAssets()
        {
            viktorshroom= AssetUtils.LoadAssetBundleFromResources("viktorshroom", Assembly.GetExecutingAssembly());
        }

        //custom item addition example
        private void ShroomerSpear()
        {
            var shroomerspear_prefab = viktorshroom.LoadAsset<GameObject>("ShroomerSpear");
            var shroomerspear = new CustomItem(shroomerspear_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Shroom Spear",
                    Amount = 1,
                    CraftingStation = "piece_workbench",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Resin", Amount = 25, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Crystal", Amount = 10, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(shroomerspear);
        }

    }
}