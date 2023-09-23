using System;
using System.Reflection;
using BepInEx;
using Epic.OnlineServices.AntiCheatClient;
using HarmonyLib;
using Landfall;
using TMPro;
using UnityEngine;

namespace BetterHealthBar
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll(typeof(HealthBarHandlerPatch));
            Logger.LogInfo($"Fixed that horrible health bar!");
        }
    }


    public class HealthBarHandlerPatch
    {
        [HarmonyPatch(typeof(HealthBarHandler), "Update")]
        [HarmonyPostfix]
        public static void HealthBarHandlerPostfix(HealthBarHandler __instance)
        {
            PlayerDeath playerDeath = Traverse.Create(__instance).Field("death").GetValue() as PlayerDeath;
            __instance.bar.fillAmount = playerDeath.health / 100;
        }
    }
}
