using CitiesHarmony.API;
using HarmonyLib;
using ICities;
using System;
using System.IO;
using UnityEngine;

namespace RemoveNeedForPipes
{
    public class RemoveNeedForPipes : IUserMod
    {
        public string Name
        {
            get
            {
                return "Remove Need For Pipes";
            }
        }

        public string Description
        {
            get
            {
                return "Removes need to place pipes (you still have to produce and treat the water)";
            }
        }
    }

    public static class Helper
    {
        public static void PrintError(string Message)
        {
#if DEBUG
            File.AppendAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
        "\\RNFP.txt", Message + Environment.NewLine);
#else
            Debug.Log("[Remove Need For Pipes] " + Message);
#endif
        }
    }

    public static class Patcher
    {
        private const string HarmonyId = "Overhatted.RemoveNeedForPipes";
        private static bool patched = false;

        public static void PatchAll()
        {
            if (patched) return;

            patched = true;
            var harmony = new Harmony(HarmonyId);
            harmony.PatchAll(typeof(Patcher).Assembly);
        }

        public static void UnpatchAll()
        {
            if (!patched) return;

            var harmony = new Harmony(HarmonyId);
            harmony.UnpatchAll(HarmonyId);
            patched = false;
        }
    }

    public class Loader : ILoadingExtension
    {
        public void OnCreated(ILoading loading)
        {
#if DEBUG
            Helper.PrintError("");
#endif
            if (HarmonyHelper.IsHarmonyInstalled) Patcher.PatchAll();

            WaterManagerMod.Init();
        }

        public void OnReleased()
        {
            if (HarmonyHelper.IsHarmonyInstalled) Patcher.UnpatchAll();
        }

        public void OnLevelLoaded(LoadMode mode)
        {

        }

        public void OnLevelUnloading()
        {

        }
    }
}