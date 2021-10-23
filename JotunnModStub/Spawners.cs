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
    private GameObject _shroomerSpear;

    // ReSharper disable twice IdentifierTypo
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

    #region Prefabs

    private void AddShroomerSpear()
    {
      // ReSharper disable once StringLiteralTypo
      _shroomerSpear = _assetBundle.LoadAsset<GameObject>("ShroomerSpear");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"_shroomerSpear == null : {_shroomerSpear == null}"); // This is null?
#endif

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
            Item = "Crystal"
            , Amount = 10
            , AmountPerLevel = 10
          }
        }
      }));

      // ItemManager.Instance.AddItem(new CustomItem(_shroomerSpear, false)); // Non Craftable version
    }

    #endregion

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
