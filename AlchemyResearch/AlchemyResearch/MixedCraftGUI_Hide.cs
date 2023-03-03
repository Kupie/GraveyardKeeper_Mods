using System;
using HarmonyLib;
using UnityEngine;

namespace AlchemyResearch;

[HarmonyPatch(typeof(MixedCraftGUI), "Hide", new Type[] { typeof(bool) })]
public class MixedCraftGUI_Hide
{
	[HarmonyPostfix]
	public static void Postfix(MixedCraftGUI __instance)
	{
		Transform transform = __instance.transform.Find("ingredient container result");
		if ((bool)transform && (bool)transform.gameObject)
		{
			UnityEngine.Object.Destroy(transform.gameObject);
		}
	}
}
