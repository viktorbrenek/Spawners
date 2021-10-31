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
        //private GameObject _trollback;
        private GameObject _redbullet;
        private GameObject _crystalgun;
        private GameObject _motaxe;
        private GameObject _mothelm;
        private GameObject _motchest;
        private GameObject _motlegs;
        private GameObject _motcape;
        private GameObject _floxsaddle;
        private GameObject _redcrystal;
        private GameObject _trophyshroomer;
        private GameObject _floxhorn;
        private GameObject _trophyflox;
        private GameObject _trophyfmlox;
        private GameObject _trophyfloxpelt;
        private GameObject _trophyfloxmeat;
        private GameObject _trophygrawl;
        private GameObject _shroomie;
        

        // ReSharper disable twice IdentifierTypo
        // TADY SE MUSÍ UDĚLAT JAKOBY PROMĚNÁ PRO VŠECHNY PŘEDMĚTY KTERÝ CHCEŠ PŘIDÁVAT = TYPU BUILDING 
        private GameObject _shroomerSpawnerPiece;
        private GameObject _shroomerCrystalAltar;
        //private GameObject _shroomerRecaller;


        private AssetBundle _assetBundle;

        


        // ReSharper disable once IdentifierTypo
        public Shroomer()
        {
            Instance = this;
        }

        
        

        [UsedImplicitly]
        private void Awake()
        {

            On.SpawnArea.Awake += SpawnArea_Awake;

            // ReSharper disable once StringLiteralTypo
            _assetBundle = AssetUtils.LoadAssetBundleFromResources("viktorshroom", typeof(Shroomer).Assembly);

            Jotunn.Managers.ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<GP_Mother>(), false));

            


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

        private void SpawnArea_Awake(On.SpawnArea.orig_Awake orig, SpawnArea self)
        {
            if (ZNetView.m_forceDisableInit)
            {
                Destroy(self);
                return;
            }
            orig(self);
        }

        private void LoadPieces()
        {
            AddShroomerSpawner();
        }

        //tady zkouším přidat special effekty - jako např boss efekt


       

        


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

            //_trollback = _assetBundle.LoadAsset<GameObject>("CapeBackpack");

            _redbullet = _assetBundle.LoadAsset<GameObject>("RedBullet");

            _crystalgun = _assetBundle.LoadAsset<GameObject>("CrystalGun");

            _motaxe = _assetBundle.LoadAsset<GameObject>("BattleaxeMother");

            _mothelm = _assetBundle.LoadAsset<GameObject>("MotherHelmet");

            _motcape = _assetBundle.LoadAsset<GameObject>("MotherCape");

            _motchest = _assetBundle.LoadAsset<GameObject>("MotherChest");

            _motlegs = _assetBundle.LoadAsset<GameObject>("MotherLegs");

            _floxsaddle = _assetBundle.LoadAsset<GameObject>("SaddleFLox");

            _trophyshroomer = _assetBundle.LoadAsset<GameObject>("TrophyShroomer");

            _floxhorn = _assetBundle.LoadAsset<GameObject>("FloxHorn");

            _trophyflox = _assetBundle.LoadAsset<GameObject>("FTrophyLox");

            _trophyfmlox = _assetBundle.LoadAsset<GameObject>("FTrophyLoxMother");

            _trophyfloxpelt = _assetBundle.LoadAsset<GameObject>("FLoxPelt");

            _trophyfloxmeat = _assetBundle.LoadAsset<GameObject>("FLoxMeat");

            _trophygrawl = _assetBundle.LoadAsset<GameObject>("TrophyGrawl");

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

            ItemManager.Instance.AddItem(new CustomItem(_floxhorn, false));

            ItemManager.Instance.AddItem(new CustomItem(_trophyflox, false));

            ItemManager.Instance.AddItem(new CustomItem(_trophyfmlox, false));

            ItemManager.Instance.AddItem(new CustomItem(_trophyfloxpelt, false));

            ItemManager.Instance.AddItem(new CustomItem(_trophyfloxmeat, false));

            ItemManager.Instance.AddItem(new CustomItem(_trophygrawl, false));

            //ItemManager.Instance.AddItem(new CustomItem(_trollback, false));

            ItemManager.Instance.AddItem(new CustomItem(_shroomerSpear, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Shroom Spear"
              ,
                Amount = 1
              ,
                CraftingStation = "piece_workbench"
              ,
                Requirements = new[]
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

            ItemManager.Instance.AddItem(new CustomItem(_redbullet, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Red Crystal Bullet"
              ,
                Amount = 5
              ,
                CraftingStation = "piece_workbench"
              ,
                Requirements = new[]
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

            ItemManager.Instance.AddItem(new CustomItem(_crystalgun, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Crystal Gun"
              ,
                Amount = 1
              ,
                CraftingStation = "piece_workbench"
              ,
                Requirements = new[]
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

            ItemManager.Instance.AddItem(new CustomItem(_floxsaddle, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Forest Lox Saddle"
              ,
                Amount = 1
              ,
                CraftingStation = "piece_workbench"
              ,
                Requirements = new[]
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

            //mother armor set

            ItemManager.Instance.AddItem(new CustomItem(_motaxe, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Mother Battleaxe"
              ,
                Amount = 1
              ,
                CraftingStation = "piece_workbench"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "Bronze"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "FloxPelt"
            , Amount = 1
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "FloxHorn"
            , Amount = 4
            , AmountPerLevel = 3
          }
        }
            }));

            ItemManager.Instance.AddItem(new CustomItem(_mothelm, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Mother Helm"
              ,
                Amount = 1
              ,
                CraftingStation = "piece_workbench"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "Bronze"
            , Amount = 1
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "FloxPelt"
            , Amount = 1
            , AmountPerLevel = 2
          }
          , new RequirementConfig
          {
            Item = "FloxHorn"
            , Amount = 2
            , AmountPerLevel = 2
          }
        }
            }));

            ItemManager.Instance.AddItem(new CustomItem(_motchest, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Mother Chest"
              ,
                Amount = 1
              ,
                CraftingStation = "piece_workbench"
              ,
                Requirements = new[]
              {
          new RequirementConfig
          {
            Item = "Bronze"
            , Amount = 1
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "FloxPelt"
            , Amount = 3
            , AmountPerLevel = 3
          }
          , new RequirementConfig
          {
            Item = "FloxHorn"
            , Amount = 1
            , AmountPerLevel = 2
          }
        }
            }));

            ItemManager.Instance.AddItem(new CustomItem(_motcape, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Mother Cape"
              ,
                Amount = 1
              ,
                CraftingStation = "piece_workbench"
              ,
                Requirements = new[]
              {
          
            new RequirementConfig
          {
            Item = "FloxPelt"
            , Amount = 3
            , AmountPerLevel = 3
          }
          , new RequirementConfig
          {
            Item = "FloxHorn"
            , Amount = 1
            , AmountPerLevel = 1
          }
        }
            }));

            ItemManager.Instance.AddItem(new CustomItem(_motlegs, false, new ItemConfig
            {
                // ReSharper disable once StringLiteralTypo
                Name = "Mother Legs"
              ,
                Amount = 1
              ,
                CraftingStation = "piece_workbench"
              ,
                Requirements = new[]
              {

            new RequirementConfig
          {
            Item = "FloxPelt"
            , Amount = 2
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "FloxHorn"
            , Amount = 3
            , AmountPerLevel = 3
          }
        }
            }));
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

        #endregion

        // TADY SE PŘIDÁVAJÍ VĚCI NA BUILDING
        #region Pieces

        // ReSharper disable twice IdentifierTypo
        private void AddShroomerSpawner()
        {
            // ReSharper disable once StringLiteralTypo
            _shroomerSpawnerPiece = _assetBundle.LoadAsset<GameObject>("shroomerspawner");
            
            _shroomerCrystalAltar = _assetBundle.LoadAsset<GameObject>("CrystalAltar");

            //_shroomerRecaller = _assetBundle.LoadAsset<GameObject>("Recaller");
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
                ,
                  CraftingStation = ""
                ,
                  Enabled = true
                ,
                  Requirements = new[]
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

            // tady zkusím přidat další 

            /*var RecallShroom = new CustomPiece(_shroomerRecaller,
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
              Item = "Wood"
              , Amount = 1
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(RecallShroom);*/

            // další
            var AltarCrystal = new CustomPiece(_shroomerCrystalAltar,
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
              Item = "Wood"
              , Amount = 1
              , Recover = false
            }
                }
              });

            PieceManager.Instance.AddPiece(AltarCrystal);
            // tady je konec sekce asi
        }

        #endregion
    }
}

