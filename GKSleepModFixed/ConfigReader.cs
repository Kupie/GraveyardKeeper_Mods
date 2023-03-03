using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using HarmonyLib;

namespace GKSleepModFixed;



internal class ConfigReader
{

    public static IDictionary<string, string> ReadConfig()
    {
        IDictionary<string, string> configVars = new Dictionary<string, string>();

        string _assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        // C:\Games\Graveyard Keeper\QMods\GKSleepModFixed
        string _configPath = Path.Combine(_assemblyFolder, "config.ini");
        

        // Yoinked this from p1xel8ted
        foreach (var line in File.ReadAllLines(_configPath))
        {
            bool isCommentOrBlank = string.IsNullOrWhiteSpace(line) || line.StartsWith("#");
            if (!isCommentOrBlank)
            {
                var splitString = line.Split('=');
                configVars.Add(splitString[0].Trim(), splitString[1].Trim());
            }


        }
        if (configVars["debug"] == "true") {
            Debug.Log("CONFIG PATH OUTPUT:");
            Debug.Log(_configPath);
        }
        return configVars;

    }



    //hooks into the time of day update and saves if the K key was pressed
    //[HarmonyPrefix]
    //[HarmonyPatch(typeof(TimeOfDay), nameof(TimeOfDay.Update))]
    //public static void TimeOfDay_Update()
    //{
    //    if (Input.GetKeyUp(ReadConfig()["reloadConfigKey"]))
    //    {
    //        var harmony = new Harmony("org.Kupie.GKSleepModFixed");
    //        harmony.PatchAll(Assembly.GetExecutingAssembly());
    //        Debug.Log("GKSleepModFixed: Updated Timescale variables from config.ini");
    //    }
    //
    //}


}
