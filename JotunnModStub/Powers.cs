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
    public class GP_Mother : SE_Stats
    {
        public GP_Mother()
        {
            name = $"GP_Mother";
            m_name = $"Mother Power";
            m_tooltip = $"Tooltip Text";
            m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("FTrophyLoxMother").m_itemData.GetIcon(); // m_icon = AssetUtils.LoadSpriteFromFile("JotunnModStub/AssetsEmbedded/Mother.png"); Toto by bylo kdybych to dal do složky s pluginem
            m_time = 5; // 60sec * 5 = 5 mins. /previously ttl
            m_addMaxCarryWeight = 120; // Adds 120 to max weight
            m_cooldown = 10; // 10 mins 60 * 10
        }
    }

    public class SetEffect_MotherArmor : SE_Stats
    {
        public SetEffect_MotherArmor()
        {   
            
            name = $"SetEffect_MotherArmor";
            m_name = $"Mother Set";
            m_tooltip = $"Tooltip Text";
            m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("MotherHelmet").m_itemData.GetIcon(); // You already have the code for this in the ss you posted.  Otherwise  = put: null;
            m_raiseSkillModifier = 1;
        }
    }

    public class SetEffect_SageArmor : SE_Stats
    {
        public SetEffect_SageArmor()
        {   
            
            name = $"SetEffect_SageArmor";
            m_name = $"Sage Set";
            m_tooltip = $"Tooltip Text";
            m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("LinenHelmet").m_itemData.GetIcon(); // You already have the code for this in the ss you posted.  Otherwise  = put: null;
            m_raiseSkillModifier = 1;
            m_damageModifier = 25;
        }
    }

    public class SetEffect_SkyrionArmor : SE_Stats
    {
        public SetEffect_SkyrionArmor()
        {   
            
            name = $"SetEffect_SkyrionArmor";
            m_name = $"Skyrion Set";
            m_tooltip = $"Tooltip Text";
            m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("SkyrionHelmet").m_itemData.GetIcon(); // You already have the code for this in the ss you posted.  Otherwise  = put: null;
            m_raiseSkillModifier = 1;
        }
    }

    public class SE_INT : SE_Stats
    {
        public SE_INT()
        {   
            
            name = $"SE_INT";
            m_name = $"Intelligence";
            m_tooltip = $"Tooltip Text";
            m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("LinenHelmet").m_itemData.GetIcon(); // You already have the code for this in the ss you posted.  Otherwise  = put: null;
            m_damageModifier = 1;
        }
    }
    
    
   

}

