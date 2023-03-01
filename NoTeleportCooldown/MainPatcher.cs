using HarmonyLib;
using System.Reflection;

namespace NoTeleportCooldown;
public static class MainPatcher
{
    public static void Patch()
    {
        var harmony = new Harmony("org.Kupie.NoTeleportCooldown");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}



