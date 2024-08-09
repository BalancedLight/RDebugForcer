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
            var harmony = new HarmonyLib.Harmony("com.lightfall.rdebugforcer");
            harmony.PatchAll();
            LoggerInstance.Msg("You're good to go, Intern! I patched up your game to enable some debug functions. Type \"DESPACIT0\" to access level debugging features. Have fun, Intern!");
            LoggerInstance.Warning("Note: This game was patched which allows developer only functions. Do not report any issues to 7th Beat Games.");
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName == "scnMenu")
            {
                var menu = GameObject.FindObjectOfType<scnMenu>();
                if (menu != null)
                {
                    var field = typeof(scnMenu).GetField("debug_showTestPixels", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field != null)
                    {
                        field.SetValue(menu, true);
                    }
                }
            }
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

    [HarmonyPatch(typeof(scnMenu), "Awake")]
    public static class ScnMenu_Awake_Patch
    {
        public static void Prefix(scnMenu __instance)
        {
            var field = typeof(scnMenu).GetField("debug_showTestPixels", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(__instance, true);
            }
        }
    }

    [HarmonyPatch(typeof(RDLevelEditor.SelectLevelEventPanel), "Awake")]
    public static class SelectLevelEventPanel_Awake_Patch
    {
        private static readonly LevelEventType[] hiddenEvents = new LevelEventType[]
        {
            LevelEventType.AdvanceText,
            LevelEventType.ShowSubdivisionsRows,
            LevelEventType.CommentShow,
            LevelEventType.WindowResize,
            LevelEventType.ReadNarration,
            LevelEventType.NarrateRowInfo
        };

        public static void Prefix(RDLevelEditor.SelectLevelEventPanel __instance)
        {
            // Filter out hidden events from availableEvents
            //(idk why but this doesn't even work lmao)
            var field = typeof(RDLevelEditor.SelectLevelEventPanel).GetField("hiddenEvents", BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(__instance, hiddenEvents);
            }
        }
    }
}
