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

    //public List<HitData.DamageModPair> m_mods = new List<HitData.DamageModPair>();

    public class GP_Gwyrn : SE_Stats 
    {
        public GP_Gwyrn()
        {
            name = $"GP_Gwyrn";
            m_name = $"Gwyrn's blessing";
            m_tooltip = $"Tooltip Text";
            float raisevalue = 1.5f;
            ModifySkillLevel(Skills.SkillType.Sneak, ref raisevalue);
            m_raiseSkillModifier = 1;
            m_damageModifier = 15;
        }

    }
    public class GP_Infestation : SE_Stats
    {
        public GP_Infestation()
        {
            name = $"GP_Infestation";
            m_name = $"Infestation's blessing";
            m_tooltip = $"Tooltip Text"; 
            m_time = 5; // 60sec * 5 = 5 mins. /previously ttl
            m_addMaxCarryWeight = 50; // Adds 120 to max weight
            m_cooldown = 100; // 10 mins 60 * 10
            m_ttl = 60;
            var myDamageModifier = new HitData.DamageModifiers();
            myDamageModifier.m_poison = HitData.DamageModifier.Resistant;
            myDamageModifier.m_fire = HitData.DamageModifier.Weak;
            var mod = new HitData.DamageModPair { m_modifier = HitData.DamageModifier.Resistant, m_type = HitData.DamageType.Poison };
            m_mods.Add(mod);
        }
    }


}
