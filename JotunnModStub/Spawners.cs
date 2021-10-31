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
    public const string PluginVersion = "0.0.3";
    private Harmony _harmony;
    // ReSharper disable once MemberCanBePrivate.Global
    [UsedImplicitly] public static Shroomer Instance;
    private AssetBundle _assetBundle;

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

      _assetBundle.Unload(false);
      _harmony = Harmony.CreateAndPatchAll(typeof(Shroomer).Assembly, PluginGuid);
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
      ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<GP_Mother>(), false));
    }

    // TADY SE MUSÍ UDĚLAT JAKOBY PROMĚNÁ PRO VŠECHNY PŘEDMĚTY KTERÝ CHCEŠ PŘIDÁVAT = TYPU BUILDING 
    private void LoadPieces()
    {
      AddShroomerSpawner();
      AddAltarCrystal();
      // AddRecaller(); // ToDo: Reenable
    }

    // TADY SE MUSÍ UDĚLAT JAKOBY PROMĚNÁ PRO VŠECHNY PŘEDMĚTY KTERÝ CHCEŠ PŘIDÁVAT = TYPU CRAFTING/NON CRAFTING 
    //tady zkouším přidat special effekty - jako např boss efekt

    private void LoadItems()
    {
      AddShroomerSpear();
      AddRedCrystal();
      AddShroomie();
      AddRedBullet();
      AddCrystalGun();
      AddBattleaxeMother();
      AddMotherHelmet();
      AddMotherCape();
      AddMotherLegs();
      AddMotherChest();
      AddSaddleFLox();
      AddTrophyShroomer();
      // AddFloxHorn(); // ToDo: Missing from AssetBundle
      AddFTrophyLox();
      AddFTrophyLoxMother();
      AddFLoxPelt();
      AddFLoxMeat();
      AddTrophyGrawl();
    }

    private void LoadPrefabs()
    {
      // Tady = přidání "itemu" - non craftable - odkaz na původní proměnou nahoře + vynecháme vytvoření crafting configu. = neobjeví se jako craftable
      // tady zkusím ještě přehodit případně pořadí = nevím jestli půjde referovat na crystal do receptu před tím, než ho přidám jako item = takže ho dám před ten recept

      // TADY SE PŘIDÁVAJÍ INGREDIENCE = V PODSTATĚ PODOBNÉ JAKO CONFIGY V RRR

      // TADY ZKOUŠÍM LOADNOUT SHROOMERA JAKO PREFAB, ABY ŠEL VYVOLAT KDYŽ POTŘEBUJU, NEBO SPAWNOVAT PŘES SPAWN THAT _assetBundle SE VZTAHUJE K LINKU ÚPLNĚ NAHOŘE

      // MONSTRA
      var Shroomer = _assetBundle.LoadAsset<GameObject>("Shroomer");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(Shroomer, true));

      var grawlenemy = _assetBundle.LoadAsset<GameObject>("GrawlEnemy");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(grawlenemy, true));

      var grawboss = _assetBundle.LoadAsset<GameObject>("GrawlBoss");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(grawboss, true));

      var loxfor = _assetBundle.LoadAsset<GameObject>("ForestLox");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(loxfor, true));

      var loxforc = _assetBundle.LoadAsset<GameObject>("FLox_Calf");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(loxforc, true));

      var loxform = _assetBundle.LoadAsset<GameObject>("ForestLoxMother");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(loxform, true));

      // DUNGEONY

      var crydung = _assetBundle.LoadAsset<GameObject>("IslandDungeon");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(crydung, true));

      var cryschestt = _assetBundle.LoadAsset<GameObject>("crystalchesttwo");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(cryschestt, true));

      var cryschest = _assetBundle.LoadAsset<GameObject>("crystalchest");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(cryschest, true));

      var cryboss = _assetBundle.LoadAsset<GameObject>("BossAltarOne");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(cryboss, true));

      var magaltar = _assetBundle.LoadAsset<GameObject>("MagicalAnvil");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(magaltar, true));

      var motalt = _assetBundle.LoadAsset<GameObject>("BossStone_Mother");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(motalt, true));

      // NPCČKA

      var grugnpc = _assetBundle.LoadAsset<GameObject>("Grug");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(grugnpc, true));

      var grawlnpc = _assetBundle.LoadAsset<GameObject>("Grawl");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(grawlnpc, true));

      var gruwlnpc = _assetBundle.LoadAsset<GameObject>("Gruwl");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(gruwlnpc, true));

      var blakenpc = _assetBundle.LoadAsset<GameObject>("Blake");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(blakenpc, true));

      var bobnpc = _assetBundle.LoadAsset<GameObject>("Bob");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(bobnpc, true));

      var grimnpc = _assetBundle.LoadAsset<GameObject>("Grim");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(grimnpc, true));

      var pedronpc = _assetBundle.LoadAsset<GameObject>("Pedro");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(pedronpc, true));

      var pepenpc = _assetBundle.LoadAsset<GameObject>("Pepe");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(pepenpc, true));

      var zeldannpc = _assetBundle.LoadAsset<GameObject>("Zeldan");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(zeldannpc, true));

      var zhaonpc = _assetBundle.LoadAsset<GameObject>("Zhao");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(zhaonpc, true));

      // RESOURCES - STROMY ATP

      var redtree = _assetBundle.LoadAsset<GameObject>("CrystalTree");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(redtree, true));

      var redtreelog = _assetBundle.LoadAsset<GameObject>("CrystalLog");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(redtreelog, true));

      var redtreehalf = _assetBundle.LoadAsset<GameObject>("CrystalLogHalf");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(redtreehalf, true));
    }

    #region Items

    // TADY SE PŘIDÁVAJÍ VĚCI NA CRAFTING

    // TADY JE CELEJ VOID = KTEREJ V SOBĚ DRŽÍ LOAD ITEMŮ CO SE CRAFTÍ - ODKAZUJE SE NA NĚJ NA ZAČÁTKU A JE TO ASI FUNKCE

    private void AddShroomerSpear()
    {
      //ZÁLOHA TADY NAČÍTÁM NOVEJ PŘEDMĚT CO SE NECRAFTÍ = MALINY, RESOURCES CO PADADJÍ Z MONSTER ATP 
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("ShroomerSpear");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      // ItemManager.Instance.AddItem(new CustomItem(_shroomerSpear, false)); // Non Craftable version ZÁLOHA - tady zkouším RedCrystal přidat tak, aby šel vyhodit z INV
      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Shroom Spear"
        , Amount = 1
        , CraftingStation = "piece_workbench"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Resin"
            , Amount = 25
            , AmountPerLevel = 10
          }
          , new RequirementConfig
          {
            Item = "Wood"
            , Amount = 10
            , AmountPerLevel = 10
          }
          , new RequirementConfig
          {
            Item = "RedCrystal"
            , Amount = 5
            , AmountPerLevel = 5
          }
        }
      }));
    }

    private void AddRedCrystal()
    {
      // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém

      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("RedCrystal");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
    }

    private void AddShroomie()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("Shroomie");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
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
        Name = "Red Crystal Bullet"
        , Amount = 5
        , CraftingStation = "piece_workbench"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Resin"
            , Amount = 1
            , AmountPerLevel = 10
          }

          , new RequirementConfig
          {
            Item = "RedCrystal"
            , Amount = 1
            , AmountPerLevel = 5
          }
        }
      }));
    }

    private void AddCrystalGun()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("CrystalGun");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Crystal Gun"
        , Amount = 1
        , CraftingStation = "piece_workbench"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Resin"
            , Amount = 25
            , AmountPerLevel = 10
          }
          , new RequirementConfig
          {
            Item = "Wood"
            , Amount = 10
            , AmountPerLevel = 10
          }
          , new RequirementConfig
          {
            Item = "RedCrystal"
            , Amount = 5
            , AmountPerLevel = 5
          }
        }
      }));
    }

    #region mother armor set

    private void AddBattleaxeMother()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("BattleaxeMother");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Mother Battleaxe"
        , Amount = 1
        , CraftingStation = "piece_workbench"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Bronze"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "FLoxPelt"
            , Amount = 1
            , AmountPerLevel = 1
          }
          // , new RequirementConfig
          // {
          //   Item = "FloxHorn"
          //   , Amount = 4
          //   , AmountPerLevel = 3
          // }
        }
      }));
    }

    private void AddMotherHelmet()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("MotherHelmet");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Mother Helm"
        , Amount = 1
        , CraftingStation = "piece_workbench"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Bronze"
            , Amount = 1
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "FLoxPelt"
            , Amount = 1
            , AmountPerLevel = 2
          }
          // , new RequirementConfig
          // {
          //   Item = "FloxHorn"
          //   , Amount = 2
          //   , AmountPerLevel = 2
          // }
        }
      }));
    }

    private void AddMotherCape()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("MotherCape");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Mother Cape"
        , Amount = 1
        , CraftingStation = "piece_workbench"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "FLoxPelt"
            , Amount = 3
            , AmountPerLevel = 3
          }
          // , new RequirementConfig
          // {
          //   Item = "FloxHorn"
          //   , Amount = 1
          //   , AmountPerLevel = 1
          // }
        }
      }));
    }

    private void AddMotherLegs()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("MotherLegs");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Mother Legs"
        , Amount = 1
        , CraftingStation = "piece_workbench"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "FLoxPelt"
            , Amount = 2
            , AmountPerLevel = 1
          }
          // , new RequirementConfig
          // {
          //   Item = "FloxHorn"
          //   , Amount = 3
          //   , AmountPerLevel = 3
          // }
        }
      }));
    }

    private void AddMotherChest()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("MotherChest");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Mother Chest"
        , Amount = 1
        , CraftingStation = "piece_workbench"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Bronze"
            , Amount = 1
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "FLoxPelt"
            , Amount = 3
            , AmountPerLevel = 3
          }
          // , new RequirementConfig
          // {
          //   Item = "FloxHorn"
          //   , Amount = 1
          //   , AmountPerLevel = 2
          // }
        }
      }));
    }

    #endregion

    private void AddSaddleFLox()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("SaddleFLox");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Forest Lox Saddle"
        , Amount = 1
        , CraftingStation = "piece_workbench"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Shroomie"
            , Amount = 25
            , AmountPerLevel = 10
          }
          , new RequirementConfig
          {
            Item = "FineWood"
            , Amount = 10
            , AmountPerLevel = 10
          }
          , new RequirementConfig
          {
            Item = "FLoxPelt"
            , Amount = 5
            , AmountPerLevel = 5
          }
        }
      }));
    }

    private void AddTrophyShroomer()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("TrophyShroomer");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddFloxHorn()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("FloxHorn");   // ToDo: "FloxHorn" is missing from AssetBundle in the GitHub Repo

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddFTrophyLox()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("FTrophyLox");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddFTrophyLoxMother()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("FTrophyLoxMother");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddFLoxPelt()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("FLoxPelt");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddFLoxMeat()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("FLoxMeat");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddTrophyGrawl()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("TrophyGrawl");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    #endregion

    #region Pieces
    // TADY SE PŘIDÁVAJÍ VĚCI NA BUILDING

    // ReSharper disable twice IdentifierTypo
    private void AddShroomerSpawner()
    {
      // ReSharper disable once StringLiteralTypo
      var prefab = _assetBundle.LoadAsset<GameObject>("shroomerspawner");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

      // ReSharper disable twice IdentifierTypo
      var shroomerSpawner = new CustomPiece(prefab,
        false,
        new PieceConfig
        {
          PieceTable = "_HammerPieceTable"
          , CraftingStation = ""
          , Enabled = true
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Wood"
              , Amount = 1
              , Recover = false
            }
          }
        });

      PieceManager.Instance.AddPiece(shroomerSpawner);

      //_shroomerRecaller = _assetBundle.LoadAsset<GameObject>("Recaller");
    }

    private void AddRecaller()
    {
      // další
      var prefab = _assetBundle.LoadAsset<GameObject>("Recaller");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

      // tady zkusím přidat další 

      PieceManager.Instance.AddPiece(new CustomPiece(prefab,
        false,
        new PieceConfig
        {
          PieceTable = "_HammerPieceTable"
          , CraftingStation = ""
          , Enabled = true
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Wood"
              , Amount = 1
              , Recover = false
            }
          }
        }));
    }

    private void AddAltarCrystal()
    {
      // další
      var prefab = _assetBundle.LoadAsset<GameObject>("CrystalAltar");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

      PieceManager.Instance.AddPiece(new CustomPiece(prefab,
        false,
        new PieceConfig
        {
          PieceTable = "_HammerPieceTable"
          , CraftingStation = ""
          , Enabled = true
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Wood"
              , Amount = 1
              , Recover = false
            }
          }
        }));
      // tady je konec sekce asi
    }

    #endregion

    #region Patches

    public void OnPatchSpawnAreaAwake(ref SpawnArea spawnArea)
    {
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} ZNetView.m_forceDisableInit : {ZNetView.m_forceDisableInit}");
      if (ZNetView.m_forceDisableInit)
      {
        Destroy(spawnArea);
      }
    }

    #endregion
  }
}
