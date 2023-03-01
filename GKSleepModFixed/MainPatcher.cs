using HarmonyLib;
using System.Reflection;

namespace GKSleepModFixed;
public static class MainPatcher
{
    public static void Patch()
    {
        var harmony = new Harmony("org.Kupie.GKSleepModFixed");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}



