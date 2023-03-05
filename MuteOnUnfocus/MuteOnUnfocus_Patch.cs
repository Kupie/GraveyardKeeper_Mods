using HarmonyLib;
using UnityEngine;
namespace MuteOnUnfocus;

[HarmonyPatch(typeof(MainGame), nameof(MainGame.Update))]
class HarmonyPatch_Focus
{
    static void Postfix()
    {
        // If application is focused but Audio is paused then unpause audio
        if (Application.isFocused && AudioListener.pause)
        {
            //Console.WriteLine("Focused");
            AudioListener.pause = false;
            //Debug.Log("MuteOnUnfocus: Audio Auto Unpaused");

        }
        // If app is unfocused but Audio is not paused, then pause audio
        else if (!Application.isFocused && !AudioListener.pause)
        {
            //Console.WriteLine("Not Focused");
            AudioListener.pause = true;
            //Debug.Log("MuteOnUnfocus: Audio Auto Paused");
        }

    }
}
