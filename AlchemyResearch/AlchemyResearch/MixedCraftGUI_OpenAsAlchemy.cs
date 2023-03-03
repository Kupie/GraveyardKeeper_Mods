using System;
using HarmonyLib;
using UnityEngine;

namespace AlchemyResearch;

[HarmonyPatch(typeof(MixedCraftGUI), "OpenAsAlchemy", new Type[]
{
	typeof(WorldGameObject),
	typeof(string)
})]
public class MixedCraftGUI_OpenAsAlchemy
{
	public const string AlchemyWorkbench1ObjID = "mf_alchemy_craft_02";

	public const string AlchemyWorkbench2ObjID = "mf_alchemy_craft_03";

	[HarmonyPostfix]
	public static void Patch(MixedCraftGUI __instance, WorldGameObject craftery_wgo, string preset_name)
	{
		AlchemyRecipe.Initialize();
		Transform crafteryTransform = GetCrafteryTransform(__instance.transform, craftery_wgo.obj_id);
		Transform transform = __instance.transform.Find("ingredient container result");
		if (!(transform != null))
		{
			Transform transform2 = crafteryTransform.transform.Find("ingredients/ingredient container (1)");
			GameObject gameObject = UnityEngine.Object.Instantiate(transform2.gameObject);
			gameObject.name = "ingredient container result";
			transform = gameObject.transform;
			transform.transform.SetParent(__instance.transform, worldPositionStays: false);
			transform.transform.position = Vector3.zero;
			transform.transform.localPosition = new Vector3(0f, -40f, 0f);
			if (craftery_wgo.obj_id == "mf_alchemy_craft_03")
			{
				transform.transform.localPosition = new Vector3(transform2.localPosition.x, -40f, 0f);
			}
			ResultPreviewDrawUnknown(gameObject.transform);
			GameObject gameObject2 = UnityEngine.Object.Instantiate(crafteryTransform.transform.Find("ingredients/ingredient container/Base Item Cell/x2 container/counter").gameObject);
			gameObject2.name = "label result";
			gameObject2.transform.SetParent(gameObject.transform, worldPositionStays: false);
			UILabel component = gameObject2.GetComponent<UILabel>();
			component.text = MainPatcher.ResultPreviewText;
			component.pivot = UIWidget.Pivot.Center;
			component.color = new Color(0.937f, 0.87f, 0.733f);
			component.overflowWidth = 0;
			component.overflowMethod = UILabel.Overflow.ShrinkContent;
			component.topAnchor.target = gameObject.transform;
			component.bottomAnchor.target = gameObject.transform;
			component.rightAnchor.target = gameObject.transform;
			component.leftAnchor.target = gameObject.transform;
			component.leftAnchor.relative = -10f;
			component.rightAnchor.relative = 10f;
			component.topAnchor.relative = -9f;
			component.bottomAnchor.relative = -10f;
		}
	}

	public static Transform GetCrafteryTransform(Transform CraftingStation, string CrafteryWGOObjectID)
	{
		if (CrafteryWGOObjectID == "mf_alchemy_craft_02")
		{
			return CraftingStation.Find("alchemy_craft_02");
		}
		if (CrafteryWGOObjectID == "mf_alchemy_craft_03")
		{
			return CraftingStation.Find("alchemy_craft_03");
		}
		return null;
	}

	public static void ResultPreviewDrawUnknown(Transform ResultPreview)
	{
		if ((bool)ResultPreview)
		{
			BaseItemCellGUI componentInChildren = ResultPreview.GetComponentInChildren<BaseItemCellGUI>();
			if ((bool)componentInChildren)
			{
				componentInChildren.DrawEmpty();
				componentInChildren.DrawUnknown();
			}
		}
	}

	public static void ResultPreviewDrawItem(Transform ResultPreview, string ItemID)
	{
		BaseItemCellGUI componentInChildren = ResultPreview.GetComponentInChildren<BaseItemCellGUI>();
		componentInChildren.DrawEmpty();
		componentInChildren.DrawItem(ItemID, 1);
	}
}
