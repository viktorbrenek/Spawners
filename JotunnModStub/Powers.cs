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
            float raisevalue = 1.5f;
            ModifySkillLevel(Skills.SkillType.Sneak, ref raisevalue);
            m_raiseSkillModifier = 1;
            m_damageModifier = 15;
        }

    }

}
