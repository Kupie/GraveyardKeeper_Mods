using HarmonyLib;
using System.Reflection;

namespace MuteOnUnfocus;
public static class MainPatcher
{
    public static void Patch()
    {
        var harmony = new Harmony("org.Kupie.MuteOnUnfocus");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}



