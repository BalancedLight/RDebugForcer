using System;
using System.Collections.Generic;
using System.Linq;
using MelonLoader;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using RDLevelEditor;
using System.Reflection;

namespace RDebugForcer
{
    public class RDebugForceMod : MelonMod
    {
        public override void OnApplicationStart()
        {
            var harmony = new HarmonyLib.Harmony("com.balancedlight.rdebugforcer");
            harmony.PatchAll();
            LoggerInstance.Msg("You're good to go, Intern! I patched up your game to enable some debug functions. Type \"DESPACIT0\" to access level debugging features. Have fun, Intern!");
            LoggerInstance.Warning("Note: This game was patched with a mod which allows developer only functions. Do NOT report any issues to 7th Beat Games!");
        }
    }

    [HarmonyPatch(typeof(RDBase), "isDev", MethodType.Getter)]
    public static class RDBase_isDev_Patch
    {
        public static bool Prefix(ref bool __result)
        {
            __result = true; // Force isDev to always return true
            return false; // Skip the original getter (tm)
        }
    }

    [HarmonyPatch(typeof(RDLevelEditor.SelectLevelEventPanel), "Awake")]
    public static class SelectLevelEventPanel_Awake_Patch
    {
        private static readonly LevelEventType[] hiddenEvents = new LevelEventType[]
        {
        };
        //no more hidden events for u lol

        public static void Prefix(RDLevelEditor.SelectLevelEventPanel __instance)
        {
            var field = typeof(RDLevelEditor.SelectLevelEventPanel).GetField("hiddenEvents", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(__instance, hiddenEvents);
                Console.WriteLine("Hidden events force enabled");
            }
        }
    }
}
