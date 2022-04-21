using BepInEx;
using HarmonyLib;
using JetBrains.Annotations;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.Reflection;
using UnityEngine;

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

        private void LoadStatusEffects()
        {
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<GP_Gwyrn>(), false));
        }


        // TADY SE MUSÍ UDĚLAT JAKOBY PROMĚNÁ PRO VŠECHNY PŘEDMĚTY KTERÝ CHCEŠ PŘIDÁVAT = TYPU BUILDING 
        private void LoadPieces()
        {
            AddDungDoorOne();
            AddDungDoorTwo();
            AddDungDoorThree();
            AddDungDoorFour();
            AddDungDoorFive();
            AddDungDoorSix();
            AddDungDoorSeven();
            AddDungDoorEight();
        }

        // TADY SE MUSÍ UDĚLAT JAKOBY PROMĚNÁ PRO VŠECHNY PŘEDMĚTY KTERÝ CHCEŠ PŘIDÁVAT = TYPU CRAFTING/NON CRAFTING 
        //tady zkouším přidat special effekty - jako např boss efekt

        private void LoadItems()
        {
            AddDungKeyOne();
            AddDungKeyTwo();
            AddDungKeyThree();
            AddDungKeyFour();
            AddDungKeyFive();
            AddDungKeySix();
            AddDungKeySeven();
            AddDungKeyEight();
            AddCageHammer();
            AddGwyrnsBelt();
        }

        private void LoadPrefabs()
        {
            var blakenpc = _assetBundle.LoadAsset<GameObject>("Blake");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(blakenpc, true));

            var drakenpc = _assetBundle.LoadAsset<GameObject>("Drake");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(drakenpc, true));

            var AddGuildHouse = _assetBundle.LoadAsset<GameObject>("VillageHouseOne");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(AddGuildHouse, true));

            var AddIslandDung = _assetBundle.LoadAsset<GameObject>("IslandDungeon");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(AddIslandDung, true));


            // BOSSES
            var bossone = _assetBundle.LoadAsset<GameObject>("BossA");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(bossone, true));
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