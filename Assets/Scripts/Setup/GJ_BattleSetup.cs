using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam.Setup
{
    public static class GJ_BattleSetup
    {
        public enum BattleWinner
        {
            Player = 0,
            Rival = 1,
            Tie = 2,
            None = 3
        }
        
        public enum BattleChoices
        {
            MAIN = 0,   // Select from main group: Move - Run
            MOVE = 1,   // Select from learned moves
            BAG = 2     // Select from bag
        }

        public enum BattleStatus
        {
            CALM = 0,
            NORMAL = 1,
            UPSET = 2,
            FAINTED = 3
        }

        public static int GetLevelExperience(int _currentLevel)
        {
            int exp = 0;
            if (_currentLevel > 100)
            {
                _currentLevel = 100;
            }

            exp = ExperienceTable[_currentLevel - 1];

            return exp;
        }

        private static int[] ExperienceTable = new int[]
        {
                0, 4, 13, 32, 65, 112, 178, 276, 393, 540,
                745, 967, 1230, 1591, 1957, 2457, 3046, 3732, 4526, 5440,
                6482, 7666, 9003, 10506, 12187, 14060, 16140, 18439, 20974, 23760,
                26811, 30146, 33780, 37731, 42017, 46656, 50653, 55969, 60505, 66560,
                71677, 78533, 84277, 91998, 98415, 107069, 114205, 123863, 131766, 142500,
                151222, 163105, 172697, 185807, 196322, 210739, 222231, 238036, 250562, 267840,
                281456, 300293, 315059, 335544, 351520, 373744, 390991, 415050, 433631, 459620,
                479600, 507617, 529063, 559209, 582187, 614566, 639146, 673863, 700115, 737280,
                765275, 804997, 834809, 877201, 908905, 954084, 987754, 1035837, 1071552, 1122660,
                1160499, 1214753, 1254796, 1312322, 1354652, 1415577, 1460276, 1524731, 1571884, 1640000
        };

        public static void AddExperience(int _expAdded, GameJam.SaveData.GJ_PlayerData data)
        {
            if (data.m_level < GJ_GameSetup.Core.MAX_LEVEL)
            {
                data.m_experience += _expAdded;
                while (data.m_experience >= data.m_nextLevelExperience)
                {
                    data.m_level += 1;
                    data.m_nextLevelExperience = GetLevelExperience(data.m_level + 1);
                    CalculateStats(data);
                }
            }
        }


        public static void CalculateStats(GameJam.SaveData.GJ_PlayerData data)
        {
            //return new int[] {baseStatsHP, baseStatsATK, baseStatsDEF, baseStatsSPA, baseStatsSPD, baseStatsSPE};
            if (data.m_baseStatsLife == 1)
            {
                data.m_maxLife = 1;
            }
            else
            {
                int prevMaxHP = data.m_maxLife;
                data.m_maxLife = Mathf.FloorToInt(((31 + (2 * data.m_baseStatsLife) + (31 / 4) + 100) * data.m_level) / 100 + 10);
                data.m_life = (data.m_life + (data.m_maxLife - prevMaxHP) < data.m_maxLife)
                    ? data.m_life + (data.m_maxLife - prevMaxHP)
                    : data.m_maxLife;
            }
            if (data.m_baseStatsDefenses == 1)
            {
                data.m_defense = 1;
            }
            else
            {
                data.m_defense = Mathf.FloorToInt(Mathf.FloorToInt(((31 + (2 * data.m_baseStatsDefenses) + (31 / 4)) * data.m_level) / 100 + 5) * 2);
            }

            if (data.m_baseStatsReflexes == 1)
            {
                data.m_reflexes = 1;
            }
            else
            {
                data.m_reflexes = Mathf.FloorToInt(Mathf.FloorToInt(((31 + (2 * data.m_baseStatsReflexes) + (31 / 4)) * data.m_level) / 100 + 5) * 3);
            }
        }

        public static void FullHeal(SaveData.GJ_PlayerData data)
        {
            data.m_life = data.m_maxLife;
        }

        public static int HealHP(float amount, SaveData.GJ_PlayerData data)
        {
            int excess = 0;
            int intAmount = Mathf.RoundToInt(amount);
            data.m_life += intAmount;
            if (data.m_life > data.m_maxLife)
            {
                excess = data.m_life - data.m_maxLife;
                data.m_life = data.m_maxLife;
            }
            return intAmount - excess;
        }

        public static void RemoveHP(float amount, SaveData.GJ_PlayerData data)
        {
            int intAmount = Mathf.RoundToInt(amount);
            data.m_life -= intAmount;
            if (data.m_life <= 0)
                data.m_life = 0;
        }

        public static float GetPercentHP(SaveData.GJ_PlayerData data)
        {
            return 1f - (((float)data.m_maxLife - (float)data.m_life) / (float)data.m_maxLife);
        }
    }
}
