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
            m_icon = AssetUtils.LoadSpriteFromFile("JotunnModStub/AssetsEmbedded/Mother.png"); // You already have the code for this in the ss you posted.  Otherwise  = put: null;
            m_time = 5; // 60sec * 5 = 5 mins. /previously ttl
            m_addMaxCarryWeight = 120; // Adds 120 to max weight
            m_cooldown = 10; // 10 mins 60 * 10
        }
    }
}

