using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Linq;
using System;

namespace GKSleepModFixed;

[HarmonyPatch(typeof(SleepGUI))]
[HarmonyPatch("<Open>b__18_0")]
internal class SleepGUI_delegate_Patcher
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {

        float sleepMult = System.Convert.ToSingle(ConfigReader.ReadConfig()["sleepMult"]);

        float num = 10f * sleepMult;
        float num2 = (1f * sleepMult) / 12f;
        int index = 0;
        int index2 = 0;
        List<CodeInstruction> list = new List<CodeInstruction>(instructions);
        for (int i = 1; i < list.Count; i++)
        {
            if (list[i].operand != null)
            {
                if (list[i].opcode == OpCodes.Call && list[i].operand.ToString() == "Void set_fixedDeltaTime(Single)" && list[i - 1].opcode == OpCodes.Ldc_R4)
                {
                    index2 = i - 1;
                }
                else if (list[i].opcode == OpCodes.Call && list[i].operand.ToString() == "Void set_timeScale(Single)" && list[i - 1].opcode == OpCodes.Ldc_R4)
                {
                    index = i - 1;
                }
            }
        }
        list[index2].operand = num2;
        list[index].operand = num;
        return list.AsEnumerable();
    }
}