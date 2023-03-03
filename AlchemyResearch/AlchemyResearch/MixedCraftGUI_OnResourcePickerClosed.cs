using System;
using HarmonyLib;
using UnityEngine;

namespace AlchemyResearch;

[HarmonyPatch(typeof(MixedCraftGUI), "OnResourcePickerClosed", new Type[] { typeof(Item) })]
public class MixedCraftGUI_OnResourcePickerClosed
{
	[HarmonyPostfix]
	public static void Patch(MixedCraftGUI __instance, Item item)
	{
		string obj_id = __instance.GetCrafteryWGO().obj_id;
		Transform crafteryTransform = MixedCraftGUI_OpenAsAlchemy.GetCrafteryTransform(__instance.transform, obj_id);
		Transform resultPreview = __instance.transform.Find("ingredient container result");
		MixedCraftGUI_OpenAsAlchemy.ResultPreviewDrawUnknown(resultPreview);
		if (!crafteryTransform)
		{
			return;
		}
		Transform transform = crafteryTransform.transform.Find("ingredients/ingredient container/Base Item Cell");
		Transform transform2 = crafteryTransform.transform.Find("ingredients/ingredient container (1)/Base Item Cell");
		Transform transform3 = crafteryTransform.transform.Find("ingredients/ingredient container (2)/Base Item Cell");
		if (!transform || !transform2)
		{
			return;
		}
		BaseItemCellGUI component = transform.GetComponent<BaseItemCellGUI>();
		BaseItemCellGUI component2 = transform2.GetComponent<BaseItemCellGUI>();
		BaseItemCellGUI baseItemCellGUI = null;
		if ((bool)transform3)
		{
			baseItemCellGUI = transform3.GetComponent<BaseItemCellGUI>();
		}
		if (!component || !component2)
		{
			MixedCraftGUI_OpenAsAlchemy.ResultPreviewDrawUnknown(resultPreview);
			return;
		}
		string id = component.item.id;
		string id2 = component2.item.id;
		string text = "empty";
		if ((bool)baseItemCellGUI)
		{
			text = baseItemCellGUI.item.id;
		}
		if (id == "empty" || id2 == "empty" || (text == "empty" && obj_id == "mf_alchemy_craft_03"))
		{
			MixedCraftGUI_OpenAsAlchemy.ResultPreviewDrawUnknown(resultPreview);
			return;
		}
		string itemID = ResearchedAlchemyRecipes.IsRecipeKnown(id, id2, text);
		MixedCraftGUI_OpenAsAlchemy.ResultPreviewDrawItem(resultPreview, itemID);
	}
}
