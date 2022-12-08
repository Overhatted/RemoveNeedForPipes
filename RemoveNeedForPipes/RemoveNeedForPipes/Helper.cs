using Harmony;
using ICities;
using System;
using System.IO;
using System.Reflection;
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

    public static class EmptyFunctionClass
    {
        public static void EmptyFunction()
        {

        }
    }

    public class Loader : ILoadingExtension
    {
        public void OnCreated(ILoading loading)
        {
#if DEBUG
            Helper.PrintError("");
#endif
            WaterManagerMod.Init();

            var harmony = HarmonyInstance.Create("com.overhatted.removeneedforpipes");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            var postfix = typeof(EmptyFunctionClass).GetMethod(nameof(EmptyFunctionClass.EmptyFunction));

            var original = typeof(WaterManager).GetMethod("TryFetchWater", new Type[] { typeof(Vector3), typeof(int), typeof(int), typeof(byte).MakeByRefType() });
            var prefix = typeof(TryFetchWaterVector3Mod).GetMethod(nameof(TryFetchWaterVector3Mod.Prefix));
            harmony.Patch(original, new HarmonyMethod(prefix), new HarmonyMethod(postfix));

            original = typeof(WaterManager).GetMethod("TryFetchWater", new Type[] { typeof(ushort), typeof(int), typeof(int), typeof(byte).MakeByRefType() });
            prefix = typeof(TryFetchWaterUshortMod).GetMethod(nameof(TryFetchWaterUshortMod.Prefix));
            harmony.Patch(original, new HarmonyMethod(prefix), new HarmonyMethod(postfix));
        }

        public void OnReleased()
        {

        }

        public void OnLevelLoaded(LoadMode mode)
        {

        }

        public void OnLevelUnloading()
        {

        }
    }
}