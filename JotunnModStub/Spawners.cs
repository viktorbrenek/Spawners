using BepInEx;
using HarmonyLib;
using JetBrains.Annotations;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Logger = Jotunn.Logger;
using SimpleJson;
using static SimpleJson.SimpleJson;

namespace Sporelings
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency(Main.ModGuid)]
    [UsedImplicitly]
    // ReSharper disable once IdentifierTypo
    public class Shroomer : BaseUnityPlugin
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public const string PluginGuid = "viktor44.sporelings";
        public const string PluginName = "Sporelings";
        public const string PluginVersion = "0.0.4";
        private Harmony _harmony;
        // ReSharper disable once MemberCanBePrivate.Global
        [UsedImplicitly] public static Shroomer Instance;
        private AssetBundle _assetBundle;
        internal static ScriptableObject SE_Gwyrn;
        



        // ReSharper disable once IdentifierTypo
        public Shroomer()
        {
            Instance = this;
        }


        [UsedImplicitly]
        private void Awake()
        {
            // ReSharper disable once StringLiteralTypo
            _assetBundle = AssetUtils.LoadAssetBundleFromResources("viktorshroom", typeof(Shroomer).Assembly);
            SE_Gwyrn = _assetBundle.LoadAsset<ScriptableObject>("BeltA_stat");

            


#if DEBUG
            foreach (var assetName in _assetBundle.GetAllAssetNames())
            {
                Jotunn.Logger.LogInfo(assetName);
            }
#endif

            LoadItems();
            LoadPrefabs();
            LoadPieces();
            LoadStatusEffects();
            PrefabManager.OnVanillaPrefabsAvailable += LoadSEStat;
            _assetBundle.Unload(false);
            _harmony = Harmony.CreateAndPatchAll(typeof(Shroomer).Assembly, PluginGuid);
        }

        internal void LoadSEStat()
        {
            ObjectDB.instance.m_StatusEffects.Add(SE_Gwyrn as StatusEffect);
            

            PrefabManager.OnVanillaPrefabsAvailable -= LoadSEStat;
        }

        [UsedImplicitly]
        private void OnDestroy()
        {
            try
            {
                _harmony?.UnpatchSelf();
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError(e);
            }
        }

        // TADY JSEM PŘIDAL TO ČTENÍ CONFIGU
        /*public static string ReadEmbeddedFile(string embeddedName)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedName);
            if (stream == null) return null;
            TextReader tr = new StreamReader(stream);
            var fileContents = tr.ReadToEnd();
            return fileContents;
        }*/
       

        private void LoadStatusEffects()
        {
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<GP_Gwyrn>(), false));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<GP_Infestation>(), false));
        }


        // TADY SE MUSÍ UDĚLAT JAKOBY PROMĚNÁ PRO VŠECHNY PŘEDMĚTY KTERÝ CHCEŠ PŘIDÁVAT = TYPU BUILDING 
        private void LoadPieces()
        {
            //dung piece
            AddDungDoorOne();
            AddDungDoorTwo();
            AddDungDoorThree();
            AddDungDoorFour();
            AddDungDoorFive();
            AddDungDoorSix();
            AddDungDoorSeven();
            AddDungDoorEight();
            AddDwarfSpawn();
            AddLadder();
            AddBowl();
            AddGateOneWay();
            AddDungBlinder();
            AddDungSingle();
            AddDungTripod();
            // TOWN piece 
            AddFort();
            AddTownHouseOne();
            AddTownHouseTwo();
            AddTownHouseThree();
            AddTownHouseFour(); 
            AddTownHouseSix();
            AddTownHouseSeven();
            // DESERT piece
            AddTownDesertOne();
            AddTownDesertTwo();
            AddTownDesertThree();
            //boss piece
            AddSiegeMachine();
            AddBossCanon();
            //guild piece
            AddGuildHouse();
        }

        // TADY SE MUSÍ UDĚLAT JAKOBY PROMĚNÁ PRO VŠECHNY PŘEDMĚTY KTERÝ CHCEŠ PŘIDÁVAT = TYPU CRAFTING/NON CRAFTING 
        //tady zkouším přidat special effekty - jako např boss efekt

        private void LoadItems()
        {
            //dung items
            AddDungKeyOne();
            AddDungKeyTwo();
            AddDungKeyThree();
            AddDungKeyFour();
            AddDungKeyFive();
            AddDungKeySix();
            AddDungKeySeven();
            AddDungKeyEight();
            AddCageHammer();

            //guild items
            AddGwyrnsBelt();
            AddWerdiBelt();
            AddSandoBelt();
            AddZinbuBelt();
            //npc items
            AddDwarfgun();
            //boss items
            AddBossATrophy();

        }

        private void LoadPrefabs()
        {
            // OCEAN

            var chaluone = _assetBundle.LoadAsset<GameObject>("ChaluhaOne");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(chaluone, true));

            var chalutwo = _assetBundle.LoadAsset<GameObject>("ChaluhaTwo");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(chalutwo, true));

            var chaluthree = _assetBundle.LoadAsset<GameObject>("ChaluhaThree");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(chaluthree, true));

            var chalufour = _assetBundle.LoadAsset<GameObject>("Algas");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(chalufour, true));

            var chalufive = _assetBundle.LoadAsset<GameObject>("ChaluhaFour");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(chalufive, true));

            var turtleg = _assetBundle.LoadAsset<GameObject>("Turtle");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(turtleg, true));

            var fishone = _assetBundle.LoadAsset<GameObject>("FishOne");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(fishone, true));

            var fishthree = _assetBundle.LoadAsset<GameObject>("FishThree");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(fishthree, true));

            //NPC

            var drakenpc = _assetBundle.LoadAsset<GameObject>("Drake");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(drakenpc, true));

            var blakenpc = _assetBundle.LoadAsset<GameObject>("Blake");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(blakenpc, true));

            var nomadnpc = _assetBundle.LoadAsset<GameObject>("Dwarf");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(nomadnpc, true));

            var nomadsnpc = _assetBundle.LoadAsset<GameObject>("DwarfFlamer");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(nomadsnpc, true));

            //Guild

            //var AddGuildHouse = _assetBundle.LoadAsset<GameObject>("VillageHouseOne");
            //PrefabManager.Instance.AddPrefab(new CustomPrefab(AddGuildHouse, true));

            var AddIslandDung = _assetBundle.LoadAsset<GameObject>("IslandDungeon");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(AddIslandDung, true));

            //Nature prefabs 

            var Addpazruda = _assetBundle.LoadAsset<GameObject>("Pazourek");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(Addpazruda, true));

            var jungletree = _assetBundle.LoadAsset<GameObject>("JungleEle");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(jungletree, true));

            var rockoone = _assetBundle.LoadAsset<GameObject>("RockOne");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(rockoone, true));


            // BOSSES
            var bossone = _assetBundle.LoadAsset<GameObject>("BossA");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(bossone, true));

            var bossonealt = _assetBundle.LoadAsset<GameObject>("BossAAltar");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(bossonealt, true));

            var bossonestn = _assetBundle.LoadAsset<GameObject>("BossStone_A");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(bossonestn, true));

            var lightAOE = _assetBundle.LoadAsset<GameObject>("lightningAOE 1");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(lightAOE, true));


            // rostliny

            /*CustomVegetation Plants = new CustomVegetation(Jungletree, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Meadows,
                    BlockCheck = true
                });*/

            CustomVegetation Plants = new CustomVegetation(jungletree, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Meadows,
                    Min = 1,
                    Max = 5,
                    GroupSizeMin = 1,
                    GroupSizeMax = 2,
                    GroupRadius = 100,
                    GroundOffset = -1,
                    BiomeArea = Heightmap.BiomeArea.Median,
                    MinAltitude = 2
                }); ;
            ZoneManager.Instance.AddCustomVegetation(Plants);

            CustomVegetation Chaluhy = new CustomVegetation(chaluone, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Ocean,
                    Min = 3,
                    Max = 10,
                    GroupSizeMin = 3,
                    GroupSizeMax = 7,
                    GroupRadius = 40,
                    ScaleMin = 1,
                    ScaleMax = 2
                });
            ZoneManager.Instance.AddCustomVegetation(Chaluhy);

            CustomVegetation Chaluhatwo = new CustomVegetation(chalutwo, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Ocean,
                    Min = 1,
                    Max = 5,
                    GroupSizeMin = 1,
                    GroupSizeMax = 2,
                    GroupRadius = 40,
                    ScaleMin = 1,
                    ScaleMax = 2
                });
            ZoneManager.Instance.AddCustomVegetation(Chaluhatwo);

            CustomVegetation Oceturtle = new CustomVegetation(turtleg, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Ocean,
                    Min = 1,
                    Max = 5,
                    GroupSizeMin = 1,
                    GroupSizeMax = 2,
                    GroupRadius = 70
                });
            ZoneManager.Instance.AddCustomVegetation(Oceturtle);

            CustomVegetation Chaluhathree = new CustomVegetation(chaluthree, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Ocean,
                    Min = 1,
                    Max = 3,
                    GroupSizeMin = 1,
                    GroupSizeMax = 3,
                    GroupRadius = 40,
                    ScaleMin = 1,
                    ScaleMax = 2
                });
            ZoneManager.Instance.AddCustomVegetation(Chaluhathree);

            CustomVegetation RybaOne = new CustomVegetation(fishone, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Ocean,
                    Min = 1,
                    Max = 3,
                    GroupSizeMin = 1,
                    GroupSizeMax = 3,
                    GroupRadius = 40,
                    ScaleMin = 1,
                    ScaleMax = 2
                });
            ZoneManager.Instance.AddCustomVegetation(RybaOne);

            CustomVegetation RybaThree = new CustomVegetation(fishthree, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Ocean,
                    Min = 1,
                    Max = 3,
                    GroupSizeMin = 1,
                    GroupSizeMax = 3,
                    GroupRadius = 40,
                    ScaleMin = 1,
                    ScaleMax = 2
                });
            ZoneManager.Instance.AddCustomVegetation(RybaThree);

            CustomVegetation Chaluhafour = new CustomVegetation(chalufour, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Ocean,
                    Min = 1,
                    Max = 3,
                    GroupSizeMin = 1,
                    GroupSizeMax = 3,
                    GroupRadius = 40,
                    ScaleMin = 1,
                    ScaleMax = 2
                });
            ZoneManager.Instance.AddCustomVegetation(Chaluhafour);

            CustomVegetation Chaluhafive = new CustomVegetation(chalufive, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Ocean,
                    Min = 2,
                    Max = 7,
                    GroupSizeMin = 2,
                    GroupSizeMax = 6,
                    GroupRadius = 50,
                    ScaleMin = 1,
                    ScaleMax = 2
                });
            ZoneManager.Instance.AddCustomVegetation(Chaluhafive);

            CustomVegetation RockOne = new CustomVegetation(rockoone, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Ocean,
                    Min = 1,
                    Max = 3,
                    GroupSizeMin = 1,
                    GroupSizeMax = 3,
                    GroupRadius = 50,
                    ScaleMin = 1,
                    ScaleMax = 2
                });
            ZoneManager.Instance.AddCustomVegetation(RockOne);

            CustomVegetation Pazruda = new CustomVegetation(Addpazruda, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Ocean,
                    Min = 1,
                    Max = 5,
                    GroupSizeMin = 1,
                    GroupSizeMax = 2,
                    GroupRadius = 40,
                    ScaleMin = 1,
                    ScaleMax = 2
                });
            ZoneManager.Instance.AddCustomVegetation(Pazruda);
        }

        #region Items

        // KEYS

        private void AddDungKeyOne()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("DungeonKeyA");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Crystal dungeon key"
              ,
                Amount = 1
              ,
                CraftingStation = "Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "Crystal"
            , Amount = 20
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "TrophySGolem"
            , Amount = 2
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "Silver"
             , Amount = 10
             , AmountPerLevel = 2
           }

        }
            }));
        }

        private void AddDungKeyTwo()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("DungeonKeyB");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Fire dungeon key"
              ,
                Amount = 1
              ,
                CraftingStation = "Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 20
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "TrophySurtling"
            , Amount = 2
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "Bronze"
             , Amount = 10
             , AmountPerLevel = 2
           }

        }
            }));
        }

        private void AddDungKeyThree()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("DungeonKeyC");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Swamp dungeon key"
              ,
                Amount = 1
              ,
                CraftingStation = "Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "TrophyBonemass"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "TrophyDraugr"
            , Amount = 20
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "Iron"
             , Amount = 10
             , AmountPerLevel = 2
           }

        }
            }));
        }

        private void AddDungKeyFour()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("DungeonKeyD");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Sky dungeon key"
              ,
                Amount = 1
              ,
                CraftingStation = "Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "TrophyHatchling"
            , Amount = 20
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "TrophyDragonQueen"
            , Amount = 2
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "Silver"
             , Amount = 10
             , AmountPerLevel = 2
           }

        }
            }));
        }

        private void AddDungKeyFive()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("DungeonKeyE");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Frost dungeon key"
              ,
                Amount = 1
              ,
                CraftingStation = "Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "TrophyHatchling"
            , Amount = 10
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "TrophyWolf"
            , Amount = 20
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "Silver"
             , Amount = 10
             , AmountPerLevel = 2
           }

        }
            }));
        }

        private void AddDungKeySix()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("DungeonKeyF");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Tar dungeon key"
              ,
                Amount = 1
              ,
                CraftingStation = "Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "TrophyGrowth"
            , Amount = 20
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "TrophyGoblinShaman"
            , Amount = 10
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "BlackMetal"
             , Amount = 10
             , AmountPerLevel = 2
           }

        }
            }));
        }

        private void AddDungKeySeven()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("DungeonKeyG");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Sand dungeon key"
              ,
                Amount = 1
              ,
                CraftingStation = "Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "TrophyDeathsquito"
            , Amount = 15
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "TrophyGrowth"
            , Amount = 15
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "Copper"
             , Amount = 40
             , AmountPerLevel = 2
           }

        }
            }));
        }

        private void AddDungKeyEight()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("DungeonKeyH");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Water dungeon key"
              ,
                Amount = 1
              ,
                CraftingStation = "Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "TrophySerpent"
            , Amount = 10
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "FishingBait"
            , Amount = 20
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "Tin"
             , Amount = 40
             , AmountPerLevel = 2
           }

        }
            }));
        }


        private void AddCageHammer()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("CageHammer");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "The hammer of cage gods"
              ,
                Amount = 1
              ,
                CraftingStation = "Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "TrophyGoblinKing"
            , Amount = 100
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "Thunderstone"
            , Amount = 100
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "Silver"
             , Amount = 100
             , AmountPerLevel = 2
           }

        }
            }));
        }


        private void AddGwyrnsBelt()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("GuildbeltA");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Gwyrn clan belt"
              ,
                Amount = 1
              ,
                CraftingStation = "Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "Crystal"
            , Amount = 20
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "TrophySGolem"
            , Amount = 2
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "Silver"
             , Amount = 10
             , AmountPerLevel = 2
           }

        }
            }));
        }
        

        private void AddWerdiBelt()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("GuildbeltB");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Werdi clan belt"
              ,
                Amount = 1
              ,
                CraftingStation = "Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "Crystal"
            , Amount = 20
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "TrophySGolem"
            , Amount = 2
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "Silver"
             , Amount = 10
             , AmountPerLevel = 2
           }

        }
            }));
        }

        private void AddSandoBelt()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("GuildbeltC");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Sando clan belt"
              ,
                Amount = 1
              ,
                CraftingStation = "Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "Crystal"
            , Amount = 20
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "TrophySGolem"
            , Amount = 2
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "Silver"
             , Amount = 10
             , AmountPerLevel = 2
           }

        }
            }));
        }

        private void AddZinbuBelt()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("GuildbeltD");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Zinbu clan belt"
              ,
                Amount = 1
              ,
                CraftingStation = "Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "Crystal"
            , Amount = 20
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "TrophySGolem"
            , Amount = 2
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "Silver"
             , Amount = 10
             , AmountPerLevel = 2
           }

        }
            }));
        }












        // BUILD

        private void AddDungDoorOne()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("dungeon_gateA");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dundoor = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 10
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dundoor);

        }

        private void AddDungDoorTwo()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("dungeon_gateB");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dundoor2 = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 10
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dundoor2);

        }

        private void AddDungDoorThree()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("dungeon_gateC");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dundoor3 = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 10
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dundoor3);

        }

        private void AddDungDoorFour()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("dungeon_gateD");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dundoor4 = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 10
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dundoor4);

        }

        private void AddDungDoorFive()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("dungeon_gateE");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dundoor5 = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 10
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dundoor5);

        }

        private void AddDungDoorSix()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("dungeon_gateF");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dundoor6 = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 10
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dundoor6);

        }

        private void AddDungDoorSeven()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("dungeon_gateG");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dundoor7 = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 10
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dundoor7);

        }

        private void AddDungDoorEight()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("dungeon_gateH");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dundoor8 = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 10
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dundoor8);

        }


        private void AddDwarfSpawn()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("Dwarf_spawner");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dwspawn = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 150
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dwspawn);

        }


        private void AddLadder()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("LadderUp");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var ladd = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 2
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(ladd);

        }

        private void AddBowl()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("Bowl");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var bowlrena = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(bowlrena);

        }

        private void AddGateOneWay()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("DungeonDoors");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var oneway = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 2
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(oneway);

        }

        

        private void AddDungBlinder()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("DungeonBlinder");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dblind = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dblind);

        }

        private void AddDungSingle()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("DungeonSingle");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dsingle = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dsingle);

        }

        private void AddDungTripod()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("DungeonTripod");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dtri = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dtri);

        }

        private void AddFort()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("Fort");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dfort = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dfort);

        }

        private void AddTownHouseOne()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("TownHouseOne");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dhouseone = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dhouseone);

        }

        private void AddTownHouseTwo()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("TownHouseTwo");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dhousetwo = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dhousetwo);

        }

        private void AddTownHouseThree()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("TownHouseThree");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dhousethree = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dhousethree);

        }

        private void AddTownHouseFour()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("TownHouseFour");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dhousefour = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dhousefour);

        }

        private void AddTownHouseSix()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("TownHouseSix");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dhousesix = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dhousesix);

        }

        private void AddTownHouseSeven()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("TownHouseSeven");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var dhouseseven = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(dhouseseven);

        }

        private void AddTownDesertOne()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("DesertHouseOne");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var ddeseone = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(ddeseone);

        }

        private void AddTownDesertTwo()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("DesertHouseTwo");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var ddesetwo = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(ddesetwo);

        }

        private void AddTownDesertThree()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("DesertHouseThree");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var ddesethree = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(ddesethree);

        }

        private void AddGuildHouse()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("GuildHouse");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var guildho = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(guildho);

        }


        private void AddSiegeMachine()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("SiegeMachine");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var siegemach = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(siegemach);

        }

        private void AddBossCanon()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("BossCanon");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var bosscan = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_CageHammerPieceTable"
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
                {
            new RequirementConfig
            {
              Item = "Iron"
              , Amount = 999
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(bosscan);

        }










        private void AddBossATrophy()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("TrophyInfestation");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void AddDwarfgun()
       {
           // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
           // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
           var itemPrefab = _assetBundle.LoadAsset<GameObject>("Dwarfgun");
#if DEBUG
           // ReSharper disable once StringLiteralTypo
           Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
           ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
       }

        














        /*
        private void AddOreCor()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("CorruptedOre");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }
        */


        #endregion

        #region Patches

        /*public void OnPatchSpawnAreaAwake(ref SpawnArea spawnArea)
        {
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} ZNetView.m_forceDisableInit : {ZNetView.m_forceDisableInit}");
            if (ZNetView.m_forceDisableInit)
            {
                Destroy(spawnArea);
            }
        }*/

        #endregion
    }
}