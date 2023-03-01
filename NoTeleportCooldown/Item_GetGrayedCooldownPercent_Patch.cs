using HarmonyLib;

namespace NoTeleportCooldown;


[HarmonyPatch(typeof(Item), nameof(Item.GetGrayedCooldownPercent))]
class HarmonyPatch_IgnoreCooldownTeleportStone
{
    static bool Prefix(Item __instance, ref int __result)
    {
        //Harmony.DEBUG = true;
        //FileLog.Log(__instance.ToString());
        //Harmony.DEBUG = false;
        if (__instance.id == "hearthstone")
        {
            __result = 0;
            return false;
        }
        else
        {
            return true;
        }
    }
}
