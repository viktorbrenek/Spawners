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
            m_icon = null; // You already have the code for this in the ss you posted. 
            m_ttl = 60 * 5; // 60sec * 5 = 5 mins.
            m_addMaxCarryWeight = 120; // Adds 300 to max weight
        }
    }
}

