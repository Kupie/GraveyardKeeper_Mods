using System;
using System.Collections.Generic;
using HarmonyLib;

namespace AlchemyResearch;

[HarmonyPatch(typeof(MixedCraftGUI), "OnCraftPressed", new Type[] { })]
public class MixedCraftGUI_OnCraftPressed
{
	[HarmonyPrefix]
	public static void Patch(MixedCraftGUI __instance)
	{
		CraftDefinition craftDefinition = null;
		AlchemyRecipe.Initialize();
		if (!(bool)Reflection.MethodIsCraftAllowed.Invoke(__instance, new object[0]))
		{
			return;
		}
		MixedCraftPresetGUI mixedCraftPresetGUI = (MixedCraftPresetGUI)Reflection.FieldCurrentPreset.GetValue(__instance);
		craftDefinition = (CraftDefinition)Reflection.MethodGetCraftDefinition.Invoke(__instance, new object[2] { false, null });
		if (craftDefinition == null)
		{
			craftDefinition = (CraftDefinition)Reflection.MethodGetCraftDefinition.Invoke(__instance, new object[2] { true, null });
		}
		if (craftDefinition == null || !craftDefinition.id.StartsWith("mix:mf_alchemy"))
		{
			return;
		}
		List<Item> selectedItems = mixedCraftPresetGUI.GetSelectedItems();
		if (selectedItems.Count < 2)
		{
			return;
		}
		for (int i = 0; i < selectedItems.Count; i++)
		{
			switch (i)
			{
			case 0:
				AlchemyRecipe.Ingredient1 = selectedItems[i].id;
				break;
			case 1:
				AlchemyRecipe.Ingredient2 = selectedItems[i].id;
				break;
			case 2:
				AlchemyRecipe.Ingredient3 = selectedItems[i].id;
				break;
			}
			AlchemyRecipe.Result = craftDefinition.GetFirstRealOutput().id;
			AlchemyRecipe.WorkstationUnityID = __instance.GetCrafteryWGO().GetInstanceID();
			AlchemyRecipe.WorkstationObjectID = __instance.GetCrafteryWGO().obj_id;
		}
		Logg.Log($"Processed Recipe: {AlchemyRecipe.Ingredient1}|{AlchemyRecipe.Ingredient2}|{AlchemyRecipe.Ingredient3} => {AlchemyRecipe.Result} | WGO: {AlchemyRecipe.WorkstationUnityID} / {AlchemyRecipe.WorkstationObjectID}");
	}
}
