using HarmonyLib;

namespace UnbreakableItems;


[HarmonyPatch(typeof(HPActionComponent), nameof(HPActionComponent.DoAction))]
class HarmonyPatch_DurabilityState
{
    static void Prefix(WorldGameObject player_wgo)
    {
        Item equippedTool = player_wgo.GetEquippedTool();
        if (equippedTool.definition.durability_decrease_on_use)
        {
            equippedTool.definition.durability_decrease_on_use = false;
        }

    }
}
