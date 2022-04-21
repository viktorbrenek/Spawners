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
    public class GP_Gwyrn : SE_Stats 
    {
        public GP_Gwyrn()
        {
            name = $"GP_Gwyrn";
            m_name = $"Gwyrn's blessing";
            m_tooltip = $"Tooltip Text";
            //m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("FTrophyLoxMother").m_itemData.GetIcon(); // m_icon = AssetUtils.LoadSpriteFromFile("JotunnModStub/AssetsEmbedded/Mother.png"); Toto by bylo kdybych to dal do složky s pluginem
            //m_time = 5; // 60sec * 5 = 5 mins. /previously ttl
            m_skillLevel = (Skills.SkillType)17; // 
            m_skillLevelModifier = 1;
            //m_cooldown = 10; // 10 mins 60 * 10

        }

        /*public override void ModifySkillLevel(Skills.SkillType skill, ref float value)
        {
            if (this.m_skillLevel == Skills.SkillType.None)
            {
                return;
            }
            if (this.m_skillLevel == Skills.SkillType.All || this.m_skillLevel == skill)
            {
                value += this.m_skillLevelModifier;
            }
        }*/
    }

}

/*
    public class GP_Gwyrn : SE_Stats
{
    public GP_Gwyrn()
    {
        name = $"GP_Gwyrn";
        m_name = $"Gwyrn's blessing";
        m_tooltip = $"Tooltip Text";
        //m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("FTrophyLoxMother").m_itemData.GetIcon(); // m_icon = AssetUtils.LoadSpriteFromFile("JotunnModStub/AssetsEmbedded/Mother.png"); Toto by bylo kdybych to dal do složky s pluginem
        //m_time = 5; // 60sec * 5 = 5 mins. /previously ttl
        m_raiseSkill = (Skills.SkillType)17; // 
        m_raiseSkillModifier = 1;
        //m_cooldown = 10; // 10 mins 60 * 10
    }
}
*/