using HarmonyLib;
using System.Reflection;

namespace UnbreakableItems;
public static class MainPatcher
{
    public static void Patch()
    {
        var harmony = new Harmony("org.Kupie.UnbreakableItems");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}



