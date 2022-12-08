using Harmony;
using System;
using UnityEngine;

namespace RemoveNeedForPipes
{
    [HarmonyPatch(typeof(WaterManager))]
    [HarmonyPatch(nameof(WaterManager.CheckHeating))]
    public class CheckHeatingMod
    {
        public static bool Prefix(out bool heating)
        {
            WaterManagerMod.CheckHeating(out heating);

            return false;
        }
    }

    [HarmonyPatch(typeof(WaterManager))]
    [HarmonyPatch(nameof(WaterManager.CheckWater))]
    public class CheckWaterMod
    {
        public static bool Prefix(out bool water, out bool sewage, out byte waterPollution)
        {
            WaterManagerMod.CheckWater(out water, out sewage, out waterPollution);

            return false;
        }
    }

    //Heating Plant
    [HarmonyPatch(typeof(WaterManager))]
    [HarmonyPatch(nameof(WaterManager.TryDumpHeating))]
    public class TryDumpHeatingMod
    {
        public static bool Prefix(int rate, int max, ref int __result)
        {
            __result = WaterManagerMod.DumpHeating(Math.Min(rate, max));

            return false;
        }
    }

    //Buildings
    [HarmonyPatch(typeof(WaterManager))]
    [HarmonyPatch(nameof(WaterManager.TryDumpSewage))]
    [HarmonyPatch(new Type[] { typeof(Vector3), typeof(int), typeof(int) })]
    public class TryDumpSewageVector3Mod
    {
        public static bool Prefix(int rate, int max, ref int __result)
        {
            __result = WaterManagerMod.DumpSewage(Math.Min(rate, max));

            return false;
        }
    }

    //Pumping station (the one that sends trucks to clear the roads)
    [HarmonyPatch(typeof(WaterManager))]
    [HarmonyPatch(nameof(WaterManager.TryDumpSewage))]
    [HarmonyPatch(new Type[] { typeof(ushort), typeof(int), typeof(int) })]
    public class TryDumpSewageUshortMod
    {
        public static bool Prefix(int rate, int max, ref int __result)
        {
            __result = WaterManagerMod.DumpSewage(Math.Min(rate, max));

            return false;
        }
    }

    //Water reservouir, Water intake
    [HarmonyPatch(typeof(WaterManager))]
    [HarmonyPatch(nameof(WaterManager.TryDumpWater))]
    public class TryDumpWaterMod
    {
        public static bool Prefix(int rate, int max, byte waterPollution, ref int __result)
        {
            __result = WaterManagerMod.DumpWater(Math.Min(rate, max), waterPollution);

            return false;
        }
    }

    //Buildings
    [HarmonyPatch(typeof(WaterManager))]
    [HarmonyPatch(nameof(WaterManager.TryFetchHeating))]
    public class TryFetchHeatingMod
    {
        public static bool Prefix(int rate, int max, out bool connected, ref int __result)
        {
            __result = WaterManagerMod.FetchHeating(Math.Min(rate, max), out connected);

            return false;
        }
    }

    //Sewage outlet
    [HarmonyPatch(typeof(WaterManager))]
    [HarmonyPatch(nameof(WaterManager.TryFetchSewage))]
    public class TryFetchSewageMod
    {
        public static bool Prefix(int rate, int max, ref int __result)
        {
            __result = WaterManagerMod.FetchSewage(Math.Min(rate, max));

            return false;
        }
    }

    //Buildings
    //[HarmonyPatch(typeof(WaterManager))]
    //[HarmonyPatch(nameof(WaterManager.TryFetchWater))]
    //[HarmonyPatch(new Type[] { typeof(Vector3), typeof(int), typeof(int), typeof(byte) })]
    public class TryFetchWaterVector3Mod
    {
        public static bool Prefix(int rate, int max, ref byte waterPollution, ref int __result)
        {
            __result = WaterManagerMod.FetchWater(Math.Min(rate, max), ref waterPollution);

            return false;
        }
    }

    //Water reservouir and Water outtake
    //[HarmonyPatch(typeof(WaterManager))]
    //[HarmonyPatch(nameof(WaterManager.TryFetchWater))]
    //[HarmonyPatch(new Type[] { typeof(ushort), typeof(int), typeof(int), typeof(byte) })]
    public class TryFetchWaterUshortMod
    {
        public static bool Prefix(int rate, int max, ref byte waterPollution, ref int __result)
        {
            __result = WaterManagerMod.FetchWater(Math.Min(rate, max), ref waterPollution);

            return false;
        }
    }
}