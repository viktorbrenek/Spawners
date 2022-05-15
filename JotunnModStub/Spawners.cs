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
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<GP_Luvulv>(), false));
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
         
            
            AddLadder();
            AddBowl();
            AddGateOneWay();
            AddDungBlinder();
            AddDungSingle();
            AddDungTripod();
            AddWaterLiquid();
            AddEffectArea1();
            AddEffectArea2();
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
            AddHoraCube();
            //pokus
           
            AddValkyria();
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
            Addqitemone();
            Addqitemtwo();
            Addqitemthree();
            Addqitemfour();
            //npc items
            AddDwarfgun();
            
            AddTimeGear();
            AddTimeGearPiece();
            AddTimeGearHalf();
            AddRedBullet();
            AddHoralpiece();
            AddSumToken();
            AddSumTokenTwo();
            AddBossSumItOne();
            AddBossSumItTwo();
            AddBossSumItThree();
            AddBossSumItFour();
            AddBossSumItFive();
            // weapons
            AddShroomerSpear();
            AddSageStaff();
            AddAsianHat();
            AddBattleaxeMother();
            AddDwarfGunQuest();
            AddEyeSword();
            AddPickaxeToxicon();
            AddReaperScythe();
            AddStarSword();
            //boss items
            AddBossATrophy();
            AddBossBTrophy();
            AddBossBMeat();

            //loxes
            AddLoxSaddleOne();
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

            var drakenpc = _assetBundle.LoadAsset<GameObject>("VB_Drake");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(drakenpc, true));

            var blakenpc = _assetBundle.LoadAsset<GameObject>("VB_Blake");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(blakenpc, true));

            var grugnpc = _assetBundle.LoadAsset<GameObject>("VB_Grug");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(grugnpc, true));

            var shruknpc = _assetBundle.LoadAsset<GameObject>("VB_Shruk");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(shruknpc, true));

            var nomadnpc = _assetBundle.LoadAsset<GameObject>("VB_Dwarf");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(nomadnpc, true));

            var nomadsnpc = _assetBundle.LoadAsset<GameObject>("VB_DwarfFlamer");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(nomadsnpc, true));

            var vrack = _assetBundle.LoadAsset<GameObject>("VB_Vracka");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(vrack, true));

            var dwsumA = _assetBundle.LoadAsset<GameObject>("VB_SummAA");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(dwsumA, true));

            var dwsumB = _assetBundle.LoadAsset<GameObject>("VB_SummAB");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(dwsumB, true));

            //Guild

            //var AddGuildHouse = _assetBundle.LoadAsset<GameObject>("VillageHouseOne");
            //PrefabManager.Instance.AddPrefab(new CustomPrefab(AddGuildHouse, true));

            var AddIslandDung = _assetBundle.LoadAsset<GameObject>("VB_IslandDungeon");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(AddIslandDung, true));

            var AddInfostone = _assetBundle.LoadAsset<GameObject>("VB_InfoStone");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(AddInfostone, true));

            var AddWelcstone = _assetBundle.LoadAsset<GameObject>("VB_WelcomeStone");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(AddWelcstone, true));

            var Addquesstone = _assetBundle.LoadAsset<GameObject>("VB_QuestStone");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(Addquesstone, true));

            var sinquest = _assetBundle.LoadAsset<GameObject>("VB_SignQuest");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(sinquest, true));

            //Nature prefabs 

            var Addpazruda = _assetBundle.LoadAsset<GameObject>("Pazourek");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(Addpazruda, true));

            var jungletree = _assetBundle.LoadAsset<GameObject>("JungleEle");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(jungletree, true));

            var rockoone = _assetBundle.LoadAsset<GameObject>("RockOne");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(rockoone, true));

            var Addartespa = _assetBundle.LoadAsset<GameObject>("ArtefaktRune");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(Addartespa, true));


            // BOSSES
            var bossone = _assetBundle.LoadAsset<GameObject>("VB_BossA");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(bossone, true));

            var bosstwo = _assetBundle.LoadAsset<GameObject>("VB_BossB");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(bosstwo, true));

            var bossonealt = _assetBundle.LoadAsset<GameObject>("VB_BossAAltar");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(bossonealt, true));

            var bossonestn = _assetBundle.LoadAsset<GameObject>("VB_BossStone_A");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(bossonestn, true));

            var bosstwoalt = _assetBundle.LoadAsset<GameObject>("VB_BossBAltar");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(bosstwoalt, true));

            var bosstwostn = _assetBundle.LoadAsset<GameObject>("VB_BossStone_B");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(bosstwostn, true));

            var lightAOE = _assetBundle.LoadAsset<GameObject>("VB_lightningAOE 1");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(lightAOE, true));

            // LOXES
            var loxone = _assetBundle.LoadAsset<GameObject>("VB_Geju");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(loxone, true));

            var loxtwo = _assetBundle.LoadAsset<GameObject>("VB_Bibzu");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(loxtwo, true));

            // Spawners
            var spatA = _assetBundle.LoadAsset<GameObject>("SpawnerTrA");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(spatA, true));

            var spatB = _assetBundle.LoadAsset<GameObject>("SpawnerTrB");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(spatB, true));

            var spatC = _assetBundle.LoadAsset<GameObject>("SpawnerTrC");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(spatC, true));

            var spaoA = _assetBundle.LoadAsset<GameObject>("OceanSpaA");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(spaoA, true));

            var spaoB = _assetBundle.LoadAsset<GameObject>("OceanSpaB");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(spaoB, true));

            var spaoC = _assetBundle.LoadAsset<GameObject>("OceanSpaC");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(spaoC, true));


            // rostliny

            /*CustomVegetation Plants = new CustomVegetation(Jungletree, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Meadows,
                    BlockCheck = true
                });*/

            CustomVegetation Plants = new CustomVegetation(spatA, false,
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

            CustomVegetation Oceturtle = new CustomVegetation(spaoB, false,
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

            CustomVegetation RybaOne = new CustomVegetation(spaoA, false,
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

            CustomVegetation RybaThree = new CustomVegetation(spaoC, false,
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

            CustomVegetation Pazruda = new CustomVegetation(spatC, false,
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

            CustomVegetation ArteSpa = new CustomVegetation(spatB, false,
                new VegetationConfig
                {
                    Biome = Heightmap.Biome.Ocean,
                    Min = 1,
                    Max = 3,
                    GroupSizeMin = 1,
                    GroupSizeMax = 2,
                    GroupRadius = 80,
                    ScaleMin = 1,
                    ScaleMax = 2
                });
            ZoneManager.Instance.AddCustomVegetation(ArteSpa);

            
        }

        #region Items

        // KEYS

        private void AddDungKeyOne()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_DungeonKeyA");

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
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 250
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "Bronze"
             , Amount = 2
             , AmountPerLevel = 1
           }

        }
            }));
        }

        private void AddDungKeyTwo()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_DungeonKeyB");

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
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 444
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "Bronze"
             , Amount = 2
             , AmountPerLevel = 1
           }

        }
            }));
        }

        private void AddDungKeyThree()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_DungeonKeyC");

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
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 800
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "Iron"
             , Amount = 2
             , AmountPerLevel = 1
           }

        }
            }));
        }

        private void AddDungKeyFour()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_DungeonKeyD");

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
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 1111
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "Iron"
             , Amount = 2
             , AmountPerLevel = 1
           }

        }
            }));
        }

        private void AddDungKeyFive()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_DungeonKeyE");

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
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 1500
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "Silver"
             , Amount = 2
             , AmountPerLevel = 1
           }

        }
            }));
        }

        private void AddDungKeySix()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_DungeonKeyF");

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
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 2000
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "Silver"
             , Amount = 2
             , AmountPerLevel = 1
           }

        }
            }));
        }

        private void AddDungKeySeven()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_DungeonKeyG");

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
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 3000
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "BlackMetal"
             , Amount = 2
             , AmountPerLevel = 1
           }

        }
            }));
        }

        private void AddDungKeyEight()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_DungeonKeyH");

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
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 4444
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "BlackMetal"
             , Amount = 2
             , AmountPerLevel = 1
           }

        }
            }));
        }

        private void AddRedBullet()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("RedBullet");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Red bullet"
              ,
                Amount = 5
              ,
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "VB_QuestItem1"
            , Amount = 1
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "Coal"
            , Amount = 1
            , AmountPerLevel = 3
          }
        }
            }));
        }

        private void Addqitemthree()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_QuestItem3");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Quest item three"
              ,
                Amount = 1
              ,
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "TrophyDragonQueen"
            , Amount = 1
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_QuestItem2"
            , Amount = 20
            , AmountPerLevel = 3
          }
        }
            }));
        }

        private void AddTimeGear()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_TimeGear");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Artefakt"
              ,
                Amount = 1
              ,
                CraftingStation = "VB_HoraldicCube"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "VB_TimeGearHalf"
            , Amount = 2
            , AmountPerLevel = 1
          } 
        }
            }));
        }

        private void AddTimeGearHalf()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_TimeGearHalf");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Půlka Artefaktu"
              ,
                Amount = 1
              ,
                CraftingStation = "VB_HoraldicCube"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "VB_TimeGearPiece"
            , Amount = 4
            , AmountPerLevel = 1
          }
        }
            }));
        }


        
       


        private void AddCageHammer()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_CageHammer");

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
                CraftingStation = "VB_Blake"
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
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_GuildbeltA");

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
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 2000
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "Iron"
             , Amount = 2
             , AmountPerLevel = 1
           }

        }
            }));
        }
        

        private void AddWerdiBelt()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_GuildbeltB");

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
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 2000
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "Iron"
             , Amount = 2
             , AmountPerLevel = 1
           }

        }
            }));
        }

        private void AddSandoBelt()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_GuildbeltC");

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
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 2000
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "Iron"
             , Amount = 2
             , AmountPerLevel = 1
           }

        }
            }));
        }

        private void AddZinbuBelt()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_GuildbeltD");

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
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 2000
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "Iron"
             , Amount = 2
             , AmountPerLevel = 1
           }

        }
            }));
        }

        private void AddShroomerSpear()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_ShroomerSpear");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Krystalové kopí"
              ,
                Amount = 1
              ,
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "Crystal"
            , Amount = 3
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 400
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "FineWood"
             , Amount = 10
             , AmountPerLevel = 1
           }

        }
            }));
        }
        
        private void AddReaperScythe()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_ReaperScythe");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Smrť"
              ,
                Amount = 1
              ,
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "VB_QuestItem4"
            , Amount = 3
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 10000
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "Guck"
             , Amount = 100
             , AmountPerLevel = 1
           }
        }
            }));
        }
        private void AddStarSword()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_StarSword");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Hvězdný meč"
              ,
                Amount = 1
              ,
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "VB_QuestItem4"
            , Amount = 5
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 4444
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "BlackMetalScrap"
             , Amount = 10
             , AmountPerLevel = 1
           }
        }
            }));
        }
        private void AddPickaxeToxicon()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_PickaxeToxicon");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Krumpáč nákazy"
              ,
                Amount = 1
              ,
                CraftingStation = "VB_DwarfFlamer"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "VB_QuestItem4"
            , Amount = 1
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 4000
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "TrophyGreydwarfShaman"
             , Amount = 10
             , AmountPerLevel = 1
           }
        }
            }));
        }

        private void AddEyeSword()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_EyeSword");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Prokletý meč"
              ,
                Amount = 1
              ,
                CraftingStation = "VB_DwarfFlamer"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "VB_QuestItem4"
            , Amount = 4
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 4000
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "TrophySerpent"
             , Amount = 7
             , AmountPerLevel = 1
           }
        }
            }));
        }
        private void AddDwarfGunQuest()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_DwarfGunQuest");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Trpasličí pistole"
              ,
                Amount = 1
              ,
                CraftingStation = "VB_Dwarf"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "VB_QuestItem3"
            , Amount = 5
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 700
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "TrophyGreydwarfShaman"
             , Amount = 7
             , AmountPerLevel = 2
           }

        }
            }));
        }

        private void AddBattleaxeMother()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_BattleaxeMother");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Sekera z kostí loxů"
              ,
                Amount = 1
              ,
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "VB_QuestItem3"
            , Amount = 10
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 1000
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "TrophyLox"
             , Amount = 5
             , AmountPerLevel = 2
           }

        }
            }));
        }
        private void AddAsianHat()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_AsianHat");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Asijský klobouk"
              ,
                Amount = 1
              ,
                CraftingStation = "VB_Blake"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "VB_QuestItem3"
            , Amount = 10
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "VB_TimeGear"
            , Amount = 1000
            , AmountPerLevel = 3
          }

          , new RequirementConfig
          {
           Item = "FineWood"
             , Amount = 4
             , AmountPerLevel = 2
           }

        }
            }));
        }

        private void AddSageStaff()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_SageStaff");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Sage staff"
              ,
                Amount = 1
              ,
                CraftingStation = "VB_Blake"
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
            Item = "VB_TimeGear"
            , Amount = 2550
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "VB_QuestItem3"
             , Amount = 2
             , AmountPerLevel = 1
           }

        }
            }));
        }

        private void AddLoxSaddleOne()
        {
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_SaddleGeju");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Saddle Gejdzu"
              ,
                Amount = 1
              ,
                CraftingStation = "VB_Blake"
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
            Item = "VB_TimeGear"
            , Amount = 550
            , AmountPerLevel = 1
          }

          , new RequirementConfig
          {
           Item = "VB_QuestItem2"
             , Amount = 44
             , AmountPerLevel = 1
           }

        }
            }));
        }
       


        // BUILD

        private void AddDungDoorOne()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_dungeon_gateA");

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

            PieceManager.Instance.AddPiece(dundoor);

        }

        private void AddDungDoorTwo()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_dungeon_gateB");

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

            PieceManager.Instance.AddPiece(dundoor2);

        }

        private void AddDungDoorThree()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_dungeon_gateC");

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

            PieceManager.Instance.AddPiece(dundoor3);

        }

        private void AddDungDoorFour()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_dungeon_gateD");

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

            PieceManager.Instance.AddPiece(dundoor4);

        }

        private void AddDungDoorFive()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_dungeon_gateE");

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

            PieceManager.Instance.AddPiece(dundoor5);

        }

        private void AddDungDoorSix()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_dungeon_gateF");

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

            PieceManager.Instance.AddPiece(dundoor6);

        }

        private void AddDungDoorSeven()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_dungeon_gateG");

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

            PieceManager.Instance.AddPiece(dundoor7);

        }

        private void AddDungDoorEight()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_dungeon_gateH");

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

            PieceManager.Instance.AddPiece(dundoor8);

        }

        private void AddLadder()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_LadderUp");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var ladd = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_HammerPieceTable"
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
            },
            new RequirementConfig
            {
              Item = "VB_TimeGear"
              , Amount = 200
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(ladd);

        }

        private void AddBowl()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_Bowl");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_DungeonDoors");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_DungeonBlinder");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_DungeonSingle");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_DungeonTripod");

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

        private void AddWaterLiquid()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_WaterLiquid");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var wali = new CustomPiece(prefab,
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
              Item = "Tar"
              , Amount = 50
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "MeadTasty"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(wali);

        }

        private void AddEffectArea1()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_EffectArea1");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var effaone = new CustomPiece(prefab,
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
              Item = "SurtlingCore"
              , Amount = 50
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "MeadTasty"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(effaone);

        }

        private void AddEffectArea2()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_EffectArea2");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var effatwo = new CustomPiece(prefab,
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
              Item = "Guck"
              , Amount = 50
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "MeadTasty"
              , Amount = 20
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(effatwo);

        }

        private void AddFort()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_Fort");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_TownHouseOne");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_TownHouseTwo");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_TownHouseThree");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_TownHouseFour");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_TownHouseSix");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_TownHouseSeven");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_DesertHouseOne");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_DesertHouseTwo");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_DesertHouseThree");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_GuildHouse");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var guildho = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_HammerPieceTable"
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
              , Amount = 90
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "FineWood"
              , Amount = 200
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(guildho);

        }

        

       

        private void AddValkyria()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_HomeGo");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var hgome = new CustomPiece(prefab,
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

            PieceManager.Instance.AddPiece(hgome);

        }

        private void AddHoraCube()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_HoraldicCube");

#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

            // ReSharper disable twice IdentifierTypo
            var guhoracu = new CustomPiece(prefab,
              false,
              new PieceConfig
              {
                  PieceTable = "_HammerPieceTable"
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
              Item = "VB_ArteMat"
              , Amount = 10
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(guhoracu);

        }

        private void AddSiegeMachine()
        {
            // ReSharper disable once StringLiteralTypo
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_SiegeMachine");

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
            var prefab = _assetBundle.LoadAsset<GameObject>("VB_BossCanon");

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
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_TrophyInfestation");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void AddBossBTrophy()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_BossBTrophy");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void AddHoralpiece()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_ArteMat");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void AddSumToken()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_SumToken");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void AddSumTokenTwo()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_SumTokenTwo");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void AddBossSumItOne()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_BossTokenOne");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void AddBossSumItTwo()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_BossTokenTwo");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void AddBossSumItThree()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_BossTokenThree");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void AddBossSumItFour()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_BossTokenFour");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void AddBossSumItFive()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_BossTokenFive");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

       



        private void AddBossBMeat()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_FoxMeat");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void Addqitemone()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_QuestItem1");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void Addqitemtwo()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_QuestItem2");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void Addqitemfour()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_QuestItem4");
#if DEBUG
            // ReSharper disable once StringLiteralTypo
            Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif
            ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
        }

        private void AddTimeGearPiece()
        {
            // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
            // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
            var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_TimeGearPiece");
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
           var itemPrefab = _assetBundle.LoadAsset<GameObject>("VB_Dwarfgun");
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