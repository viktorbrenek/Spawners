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
    
    //tady přidávám skill type 
    public static Skills.SkillType FireMagic = 0;
    public static Skills.SkillType IntellSkill = 0;

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
      LoadSkills();
      

      _assetBundle.Unload(false);
      _harmony = Harmony.CreateAndPatchAll(typeof(Shroomer).Assembly, PluginGuid);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        { // Set a breakpoint here to break on F6 key press
            Player.m_localPlayer.RaiseSkill(FireMagic, 1);
        }
    }
    
    //method by DigitalRoot = increaces a skill based on status effect of the weapon 
    
    /*
	public float m_damageModifier = 1f;
    public float m_statusEffects = "SE_INT";

    
    
   public void ModifyAttack(global::Skills.SkillType IntellSkill, ref global::HitData hitData)
    {
            foreach (global::StatusEffect SE_INT in this.m_statusEffects)
            {
              SE_INT.ModifyAttack(skill, ref hitData);
            }
            if (SkillType == Polearms || Bows || Pickaxes == global::Skills.SkillType.All)
            {
              hitData.m_damage.Modify(this.m_damageModifier);
            }

    }

    public override void ModifyAttack(global::Skills.SkillType IntellSkill, ref global::HitData hitData)
    {
            if (SkillType == Polearms || Bows || Pickaxes == global::Skills.SkillType.All)
            {
              hitData.m_damage.Modify(this.m_damageModifier);
            }
    }*/

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
      ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SetEffect_MotherArmor>(), false));
      ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SetEffect_SageArmor>(), false));
      ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SetEffect_SkyrionArmor>(), false));
      ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_INT>(), false));
    }

    private void LoadSkills()
    {
        // Test adding a skill with a texture
    
        
        FireMagic = SkillManager.Instance.AddSkill(new SkillConfig
        {
            Identifier = "com.jotunn.JotunnModExample.firemagic",
            Name = "Fire Magic",
            Description = "Basic magic of any Wizard!",
            Icon = _assetBundle.LoadAsset<Sprite>("NecromancerIcons_60_t"),
            IncreaseStep = 1f
        });

        IntellSkill = SkillManager.Instance.AddSkill(new SkillConfig
        {
            Identifier = "com.jotunn.JotunnModExample.intelligence",
            Name = "Intelligence",
            Description = "Basic attribute of any Wizard!",
            Icon = _assetBundle.LoadAsset<Sprite>("NecromancerIcons_60_t"),
            IncreaseStep = 1f
        });
    }



    // TADY SE MUSÍ UDĚLAT JAKOBY PROMĚNÁ PRO VŠECHNY PŘEDMĚTY KTERÝ CHCEŠ PŘIDÁVAT = TYPU BUILDING 
    private void LoadPieces()
    {
      AddShroomerSpawner();
      AddAltarCrystal();
      AddSpinWheel();
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
      AddSageHat();
      AddSageCloak();
      AddSageChest();
      AddSkyHat();
      AddSkyChest();
      AddSkyLegs();
      AddSageStaff();
      
      AddScholarHat();
      AddScholarTunic();
      AddWizardBelt();
      AddLanthernBelt();
      AddToxAxe();
      AddToxPick();
      AddWatPick();
      AddGoldPick();
      AddSaddleFLox();
      AddTrophyShroomer();
      AddFloxHorn(); // ToDo: Missing from AssetBundle
      AddFTrophyLox();
      AddFTrophyLoxMother();
      AddFLoxPelt();
      AddFLoxMeat();
      AddTrophyGrawl();
      AddHempFlower();
      AddLinen();
      AddFlamaxFlower();
      AddFlamaxLinen();
      AddBronzeLinen();
      AddGoldLinen();
      AddSilverLinen();
      AddDeepLinen();
      AddFlaxLin();
      AddFrostaxFlower();
      AddFrostaxLinen();
      AddShroomLinen();
      AddSilkSpider();
      AddSpiderLinen();
      AddSwampLinen();
      AddSwampFlower();
      AddWaterLinen();
      AddOreGold();
      AddGold();
      AddOreTox();
      AddTox();
      AddOreSky();
      AddSky();
      AddOreWat();
      AddWat();
      AddOreCor();
      AddCor();
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

      var corruptedone = _assetBundle.LoadAsset<GameObject>("Corrupted");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(corruptedone, true));
      
      // GUILDY

      var guildmo = _assetBundle.LoadAsset<GameObject>("VillageHouseOne");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(guildmo, true));

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

      var mobossal = _assetBundle.LoadAsset<GameObject>("BossAltarMother");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(mobossal, true));

      var corruptedspawner = _assetBundle.LoadAsset<GameObject>("CorruptionSpawner");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(corruptedspawner, true));

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

      //flowers

      var hemphar = _assetBundle.LoadAsset<GameObject>("Hemp");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(hemphar, true));

      var amanitahar = _assetBundle.LoadAsset<GameObject>("Amanita");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(amanitahar, true));

      var flamahar = _assetBundle.LoadAsset<GameObject>("Flamax");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(flamahar, true));

      var flosthar = _assetBundle.LoadAsset<GameObject>("Frostax");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(flosthar, true));

      var spidehar = _assetBundle.LoadAsset<GameObject>("SpiderWeb");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(spidehar, true));

      var Swamphar = _assetBundle.LoadAsset<GameObject>("Swamperia");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(Swamphar, true));

      var algyone = _assetBundle.LoadAsset<GameObject>("Algas");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(algyone, true));

      var algytwo = _assetBundle.LoadAsset<GameObject>("WaterAlgas");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(algytwo, true));

      var algythr = _assetBundle.LoadAsset<GameObject>("DarkAlgas");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(algythr, true));

      var veingo = _assetBundle.LoadAsset<GameObject>("GoldVein");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(veingo, true));

      var veinwa = _assetBundle.LoadAsset<GameObject>("WaterVein");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(veinwa, true));

      var veinsky = _assetBundle.LoadAsset<GameObject>("SkyVein");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(veinsky, true));

      var veintox = _assetBundle.LoadAsset<GameObject>("ToxicVein");
      PrefabManager.Instance.AddPrefab(new CustomPrefab(veintox, true));
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

    private void AddSageStaff()
    {
      //ZÁLOHA TADY NAČÍTÁM NOVEJ PŘEDMĚT CO SE NECRAFTÍ = MALINY, RESOURCES CO PADADJÍ Z MONSTER ATP 
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("SageStaff");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      // ItemManager.Instance.AddItem(new CustomItem(_shroomerSpear, false)); // Non Craftable version ZÁLOHA - tady zkouším RedCrystal přidat tak, aby šel vyhodit z INV
      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Sage Staff"
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
            Item = "Toxicon"
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

    private void AddOreGold()
    {
      // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém

      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("GoldOre");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
    }

    private void AddOreTox()
    {
      // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém

      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("ToxicOre");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
    }

    private void AddOreWat()
    {
      // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém

      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("WaterOre");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
    }

    private void AddOreSky()
    {
      // _redcrystal je proměná = načte se z asset bundlu a referuje na přesný název z UNITY = "RedCrystal" = velké písmena, můžou dělat problém

      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("SkyOre");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false)); // Non Craftable version
    }

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
        
    private void AddFlamaxFlower()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("FlamaxFlower");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddFrostaxFlower()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("FrostaxFlower");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddSilkSpider()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("SilkSpider");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddSwampFlower()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("SwamperiaFlower");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddHempFlower()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("HempFlower");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }
    
    private void AddGold()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("Gold");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddCor()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("Corrupton");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddWat()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("Waterion");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddSky()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("Skyrion");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }

    private void AddTox()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("Toxicon");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false));
    }
    

    private void AddLinen()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("Linen");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab,false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Linen"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "HempFlower"
            , Amount = 1
            , AmountPerLevel = 4
          }

        }
      }));
    }

    private void AddBronzeLinen()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("BronzeLinen");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab,false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Bronze Linen"
        , Amount = 3
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "HempFlower"
            , Amount = 1
            , AmountPerLevel = 4
          },
          new RequirementConfig
          {
            Item = "Bronze"
            , Amount = 1
            , AmountPerLevel = 4
          }

        }
      }));
    }

    private void AddGoldLinen()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("GoldLinen");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab,false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Gold Linen"
        , Amount = 3
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "HempFlower"
            , Amount = 1
            , AmountPerLevel = 4
          },
          new RequirementConfig
          {
            Item = "SurtlingCore"
            , Amount = 7
            , AmountPerLevel = 4
          }

        }
      }));
    }

    private void AddSilverLinen()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("SilverLinen");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab,false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Silver Linen"
        , Amount = 3
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "HempFlower"
            , Amount = 1
            , AmountPerLevel = 4
          },
          new RequirementConfig
          {
            Item = "Silver"
            , Amount = 1
            , AmountPerLevel = 4
          }

        }
      }));
    }

    private void AddDeepLinen()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("DeepLinen");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab,false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Deep Linen"
        , Amount = 10
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "HempFlower"
            , Amount = 1
            , AmountPerLevel = 4
          },
          new RequirementConfig
          {
            Item = "DragonTear"
            , Amount = 1
            , AmountPerLevel = 4
          }

        }
      }));
    }

    private void AddFlaxLin()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("FlaxLinen");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab,false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Flax Linen"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Flax"
            , Amount = 1
            , AmountPerLevel = 4
          }

        }
      }));
    }

    private void AddShroomLinen()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("Shroomhemp");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab,false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Shroom Linen"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Shroomie"
            , Amount = 2
            , AmountPerLevel = 4
          }

        }
      }));
    }

    private void AddSpiderLinen()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("SpiderSilk");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab,false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Spider Silk"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "SilkSpider"
            , Amount = 1
            , AmountPerLevel = 4
          }

        }
      }));
    }

    private void AddFlamaxLinen()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("FlamaxLinen");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab,false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Flamax Linen"
        , Amount = 2
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "FlamaxFlower"
            , Amount = 1
            , AmountPerLevel = 4
          }

        }
      }));
    }

    private void AddFrostaxLinen()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("FrostaxLinen");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab,false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Frostax Linen"
        , Amount = 2
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "FrostaxFlower"
            , Amount = 1
            , AmountPerLevel = 4
          }

        }
      }));
    }

    private void AddSwampLinen()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("Swampax");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab,false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Swampax Linen"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "SwamperiaFlower"
            , Amount = 1
            , AmountPerLevel = 4
          }

        }
      }));
    }

    private void AddWaterLinen()
    {
      // ReSharper disable once StringLiteralTypo - TADY TO NAČTU AŽ JAKO DRUHÝ
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("WaterLinen");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab,false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Water Linen"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "FishCooked"
            , Amount = 3
            , AmountPerLevel = 4
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
          , new RequirementConfig
          {
           Item = "FloxHorn"
             , Amount = 4
             , AmountPerLevel = 3
           }
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
        , CraftingStation = "SpinWheel"
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
           , new RequirementConfig
           {
             Item = "FloxHorn"
             , Amount = 2
             , AmountPerLevel = 2
           }
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
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "FLoxPelt"
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
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "FLoxPelt"
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
        , CraftingStation = "SpinWheel"
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
           , new RequirementConfig
           {
             Item = "FloxHorn"
             , Amount = 1
             , AmountPerLevel = 2
           }
        }
      }));
    }

     private void AddSageHat()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("LinenHelmet");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Wizard Hat"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Linen"
            , Amount = 1
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "BronzeLinen"
            , Amount = 3
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "Swampax"
             , Amount = 1
             , AmountPerLevel = 2
           }
        }
      }));
    }

    private void AddSageCloak()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("LinenCape");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Wizard Cloak"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Linen"
            , Amount = 1
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "BronzeLinen"
            , Amount = 3
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "Swampax"
             , Amount = 1
             , AmountPerLevel = 2
           }
        }
      }));
    }

    private void AddSageChest()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("LinenTunic");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Wizard Tunic"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Linen"
            , Amount = 1
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "BronzeLinen"
            , Amount = 3
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "Swampax"
             , Amount = 1
             , AmountPerLevel = 2
           }
        }
      }));
    }

    private void AddSkyChest()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("SkyrionCuirass");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Skyrion Cuirass"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "DeepLinen"
            , Amount = 10
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "FlamaxLinen"
            , Amount = 10
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "Skyrion"
             , Amount = 8
             , AmountPerLevel = 2
           }
        }
      }));
    }

    private void AddSkyHat()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("SkyrionHelmet");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Skyrion Helmet"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "DeepLinen"
            , Amount = 10
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "FlamaxLinen"
            , Amount = 10
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "Skyrion"
             , Amount = 8
             , AmountPerLevel = 2
           }
        }
      }));
    }

     private void AddSkyLegs()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("SkyrionGreaves");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Skyrion Greaves"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "DeepLinen"
            , Amount = 10
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "FlamaxLinen"
            , Amount = 10
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "Skyrion"
             , Amount = 8
             , AmountPerLevel = 2
           }
        }
      }));
    }

     private void AddScholarHat()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("ScholarHelmet");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Scholar Hat"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Swampax"
            , Amount = 10
            , AmountPerLevel = 3
          }
          , new RequirementConfig
          {
            Item = "Shroomhemp"
            , Amount = 10
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "Gold"
             , Amount = 16
             , AmountPerLevel = 2
           }
        }
      }));
    }

    private void AddScholarTunic()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("ScholarTunic");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Scholar Tunic"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Swampax"
            , Amount = 10
            , AmountPerLevel = 3
          }
          , new RequirementConfig
          {
            Item = "Shroomhemp"
            , Amount = 10
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "Gold"
             , Amount = 16
             , AmountPerLevel = 2
           }
        }
      }));
    }

    private void AddWizardBelt()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("WizardBelt");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Wizard Belt"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Swampax"
            , Amount = 10
            , AmountPerLevel = 3
          }
          , new RequirementConfig
          {
            Item = "Shroomhemp"
            , Amount = 10
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "FlaxLinen"
             , Amount = 16
             , AmountPerLevel = 2
           }
        }
      }));
    }

    private void AddLanthernBelt()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("LanthernBelt");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Lanthern Belt"
        , Amount = 1
        , CraftingStation = "SpinWheel"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "FlaxLinen"
            , Amount = 10
            , AmountPerLevel = 3
          }
          , new RequirementConfig
          {
            Item = "FlamaxLinen"
            , Amount = 10
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "Gold"
             , Amount = 16
             , AmountPerLevel = 2
           }
        }
      }));
    }

    private void AddToxAxe()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("ToxiconBronze");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Toxicon Axe"
        , Amount = 1
        , CraftingStation = "forge"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Toxicon"
            , Amount = 10
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "Gold"
            , Amount = 2
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "FineWood"
             , Amount = 8
             , AmountPerLevel = 2
           }
        }
      }));
    }

    private void AddToxPick()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("PickaxeToxicon");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Toxicon Pickaxe"
        , Amount = 1
        , CraftingStation = "forge"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Toxicon"
            , Amount = 10
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "Gold"
            , Amount = 2
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "FineWood"
             , Amount = 8
             , AmountPerLevel = 2
           }
        }
      }));
    }

    private void AddWatPick()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("PickaxeWaterion");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Waterion Pickaxe"
        , Amount = 1
        , CraftingStation = "forge"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Waterion"
            , Amount = 10
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "Gold"
            , Amount = 2
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "FineWood"
             , Amount = 8
             , AmountPerLevel = 2
           }
        }
      }));
    }

    private void AddGoldPick()
    {
      var itemPrefab = _assetBundle.LoadAsset<GameObject>("PickaxeGolden");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} itemPrefab == null : {itemPrefab == null}"); // This is null?
#endif

      ItemManager.Instance.AddItem(new CustomItem(itemPrefab, false, new ItemConfig
      {
        // ReSharper disable once StringLiteralTypo
        Name = "Gold Pickaxe"
        , Amount = 1
        , CraftingStation = "forge"
        , Requirements = new[]
        {
          new RequirementConfig
          {
            Item = "Gold"
            , Amount = 12
            , AmountPerLevel = 1
          }
          , new RequirementConfig
          {
            Item = "Copper"
            , Amount = 1
            , AmountPerLevel = 3
          }
           , new RequirementConfig
           {
             Item = "FineWood"
             , Amount = 8
             , AmountPerLevel = 2
           }
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

    private void AddSpinWheel()
    {
      // ReSharper disable once StringLiteralTypo
      var prefab = _assetBundle.LoadAsset<GameObject>("SpinWheel");

#if DEBUG
      // ReSharper disable once StringLiteralTypo
      Jotunn.Logger.LogDebug($"{MethodBase.GetCurrentMethod().Name} prefab == null : {prefab == null}"); // This is null?
#endif

      // ReSharper disable twice IdentifierTypo
      var spintowin = new CustomPiece(prefab,
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
              , Amount = 20
              , Recover = false
            },
            new RequirementConfig
            {
              Item = "Copper"
              , Amount = 5
              , Recover = false
            }
          }
        });

      PieceManager.Instance.AddPiece(spintowin);

      //_shroomerRecaller = _assetBundle.LoadAsset<GameObject>("Recaller");
    }

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
