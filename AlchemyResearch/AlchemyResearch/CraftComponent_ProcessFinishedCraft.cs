using System;
using HarmonyLib;

namespace AlchemyResearch;

[HarmonyPatch(typeof(CraftComponent), "ProcessFinishedCraft", new Type[] { })]
public class CraftComponent_ProcessFinishedCraft
{
	[HarmonyPrefix]
	public static void Patch(CraftComponent __instance)
	{
		string text = "empty";
		if (__instance.wgo.data != null && __instance.wgo.data.id.StartsWith("mf_alchemy_craft_"))
		{
			if (__instance.current_craft.output.Count > 0)
			{
				text = __instance.current_craft.output[0].id;
			}
			else if (__instance.current_craft.needs.Count > 0)
			{
				text = __instance.current_craft.needs[0].id;
			}
			if (!text.Equals("empty") && AlchemyRecipe.MatchesResult(__instance.wgo.GetInstanceID(), __instance.wgo.obj_id, text) && AlchemyRecipe.HasValidRecipe())
			{
				ResearchedAlchemyRecipes.AddCurrentRecipe(text);
			}
		}
	}
}
