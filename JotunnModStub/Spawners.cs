using BepInEx;
using JetBrains.Annotations;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
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
    public const string PluginVersion = "0.0.2";

    // ReSharper disable once MemberCanBePrivate.Global
    [UsedImplicitly] public static Shroomer Instance;

    // ReSharper disable once IdentifierTypo
    // TADY SE MUSÍ UDĚLAT JAKOBY PROMĚNÁ PRO VŠECHNY PŘEDMĚTY KTERÝ CHCEŠ PŘIDÁVAT = TYPU CRAFTING/NON CRAFTING 
    private GameObject _shroomerSpear;
    private GameObject _redcrystal;
    private GameObject _trophyshroomer;
    private GameObject _shroomie;

    // ReSharper disable twice IdentifierTypo
    // TADY SE MUSÍ UDĚLAT JAKOBY PROMĚNÁ PRO VŠECHNY PŘEDMĚTY KTERÝ CHCEŠ PŘIDÁVAT = TYPU BUILDING 
    private GameObject _shroomerSpawnerPiece;
    private AssetBundle _assetBundle;


    // ReSharper disable once IdentifierTypo
    public Shroomer()
    {
      Instance = this;
    }


    [UsedImplicitly]
    private void Awake()
    {

      On.SpawnArea.Awake += 

      // ReSharper disable once StringLiteralTypo
      _assetBundle = AssetUtils.LoadAssetBundleFromResources("viktorshroom", typeof(Shroomer).Assembly);

#if DEBUG
      foreach (var assetName in _assetBundle.GetAllAssetNames())
      {
        Jotunn.Logger.LogInfo(assetName);
      }
#endif

      LoadPrefabs();
      LoadPieces();

      _assetBundle.Unload(false);
    }



    private void LoadPieces()
    {
      AddShroomerSpawner();
    }


    private void LoadPrefabs()
    {
      AddShroomerSpear();
    }

    // TADY SE PŘIDÁVAJÍ VĚCI NA CRAFTING

    #region Prefabs

    // TADY JE CELEJ VOID = KTEREJ V SOBĚ DRŽÍ LOAD ITEMŮ CO SE CRAFTÍ - ODKAZUJE SE NA NĚJ NA ZAČÁTKU A JE TO ASI FUNKCE

    private void AddShroomerSpear()
    {

      //ZÁLOHA TADY NAČÍTÁM NOVEJ PŘEDMĚT CO SE NECRAFTÍ = MALINY, RESOURCES CO PADADJÍ Z MONSTER ATP 
      // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém
      _redcrystal = _assetBundle.LoadAsset<GameObject>("RedCrystal");

      _shroomie = _assetBundle.LoadAsset<GameObject>("Shroomie");

      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      _shroomerSpear = _assetBundle.LoadAsset<GameObject>("ShroomerSpear");
        
      _trophyshroomer = _assetBundle.LoadAsset<GameObject>("TrophyShroomer");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"_shroomerSpear == null : {_shroomerSpear == null}"); // This is null?
#endif

      // ItemManager.Instance.AddItem(new CustomItem(_shroomerSpear, false)); // Non Craftable version ZÁLOHA - tady zkouším RedCrystal přidat tak, aby šel vyhodit z INV
      ItemManager.Instance.AddItem(new CustomItem(_redcrystal, false)); // Non Craftable version
      ItemManager.Instance.AddItem(new CustomItem(_shroomie, false));
      // Tady = přidání "itemu" - non craftable - odkaz na původní proměnou nahoře + vynecháme vytvoření crafting configu. = neobjeví se jako craftable
      // tady zkusím ještě přehodit případně pořadí = nevím jestli půjde referovat na crystal do receptu před tím, než ho přidám jako item = takže ho dám před ten recept

      ItemManager.Instance.AddItem(new CustomItem(_trophyshroomer, false));

      ItemManager.Instance.AddItem(new CustomItem(_shroomerSpear, false, new ItemConfig
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
       // TADY SE PŘIDÁVAJÍ INGREDIENCE = V PODSTATĚ PODOBNÉ JAKO CONFIGY V RRR

       // TADY ZKOUŠÍM LOADNOUT SHROOMERA JAKO PREFAB, ABY ŠEL VYVOLAT KDYŽ POTŘEBUJU, NEBO SPAWNOVAT PŘES SPAWN THAT _assetBundle SE VZTAHUJE K LINKU ÚPLNĚ NAHOŘE
       var Shroomer = _assetBundle.LoadAsset<GameObject>("Shroomer");
            PrefabManager.Instance.AddPrefab(new CustomPrefab(Shroomer, true)); 

      
    }

    #endregion
    
    // TADY SE PŘIDÁVAJÍ VĚCI NA BUILDING
    #region Pieces

    // ReSharper disable twice IdentifierTypo
    private void AddShroomerSpawner()
    {
      // ReSharper disable once StringLiteralTypo
      _shroomerSpawnerPiece = _assetBundle.LoadAsset<GameObject>("shroomerspawner");
#if DEBUG
      // ReSharper disable twice StringLiteralTypo
      Jotunn.Logger.LogDebug($"_shroomerSpawnerPiece == null : {_shroomerSpawnerPiece == null}"); // This is null?
#endif

      // ReSharper disable twice IdentifierTypo
      var shroomerSpawner = new CustomPiece(_shroomerSpawnerPiece,
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
    }

    #endregion
  }
}
