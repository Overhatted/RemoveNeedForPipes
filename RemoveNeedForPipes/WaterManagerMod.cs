using ColossalFramework;
using System;
using UnityEngine;

namespace RemoveNeedForPipes
{
    public static class WaterManagerMod
    {
        public static int Current_Water;
        public static int Current_Sewage;
        public static int Current_Heating;
        public static int Current_Water_Total_Polution;

        public static int WaterCapacity;
        public static int HeatingCapacity;

        public static void Init()
        {
            Current_Water = 0;
            Current_Sewage = 0;
            Current_Heating = 0;
            Current_Water_Total_Polution = 0;
        }

        public static void CheckHeating(out bool Heating)
        {
            if (Current_Heating > 0)
            {
                Heating = true;
            }
            else
            {
                Heating = false;
            }
        }

        public static void CheckWater(out bool Water, out bool Sewage, out byte WaterPollution)
        {
            if (Current_Water > 0)
            {
                Water = true;
            }
            else
            {
                Water = false;
            }

            if (Current_Sewage < WaterCapacity)
            {
                Sewage = true;
            }
            else
            {
                Sewage = false;
            }

            if (Current_Water == 0)
            {
                WaterPollution = byte.MinValue;
            }
            else
            {
                WaterPollution = (byte)Mathf.Clamp(Current_Water_Total_Polution / Current_Water, byte.MinValue, byte.MaxValue);
            }
        }

        public static int DumpHeating(int Rate)
        {
            Rate = Math.Min(Rate, HeatingCapacity - Current_Heating);
            Rate = Math.Max(Rate, 0);
            Current_Heating += Rate;
            return Rate;
        }

        public static int DumpSewage(int Rate)
        {
            Rate = Math.Min(Rate, WaterCapacity - Current_Sewage);
            Rate = Math.Max(Rate, 0);
            Current_Sewage += Rate;
            return Rate;
        }

        public static int DumpWater(int Rate, byte WaterPollution)
        {
            Rate = Math.Min(Rate, WaterCapacity - Current_Water);
            Rate = Math.Max(Rate, 0);
            Current_Water += Rate;

            Current_Water_Total_Polution = Math.Min(Current_Water_Total_Polution + WaterPollution * Rate, Current_Water * byte.MaxValue);

            return Rate;
        }

        public static int FetchHeating(int Rate, out bool Connected)
        {
            if (Singleton<UnlockManager>.instance.Unlocked(InfoManager.InfoMode.Heating))
            {
                Rate = Math.Min(Rate, Current_Heating);
                Current_Heating -= Rate;
                Connected = true;
                return Rate;
            }
            else
            {
                Connected = false;
                return 0;
            }
        }

        public static int FetchSewage(int Rate)
        {
            Rate = Math.Min(Rate, Current_Sewage);
            Current_Sewage -= Rate;
            return Rate;
        }

        public static int FetchWater(int Rate, ref byte WaterPollution)
        {
            if (Current_Water == 0)
            {
                WaterPollution = byte.MinValue;
            }
            else
            {
                WaterPollution = (byte)Mathf.Clamp(Current_Water_Total_Polution / Current_Water, byte.MinValue, byte.MaxValue);
            }

            Rate = Math.Min(Rate, Current_Water);
            Current_Water -= Rate;

            Current_Water_Total_Polution = Math.Max(Current_Water_Total_Polution - WaterPollution * Rate, 0);

            return Rate;
        }
    }
}
 