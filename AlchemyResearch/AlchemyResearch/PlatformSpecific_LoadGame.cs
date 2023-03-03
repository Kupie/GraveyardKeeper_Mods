using System;
using System.Collections.Generic;
using HarmonyLib;

namespace AlchemyResearch;

[HarmonyPatch(typeof(PlatformSpecific), "LoadGame", new Type[]
{
	typeof(SaveSlotData),
	typeof(PlatformSpecific.OnGameLoadedDelegate)
})]
public class PlatformSpecific_LoadGame
{
	[HarmonyPostfix]
	public static void Patch(SaveSlotData slot, PlatformSpecific.OnGameLoadedDelegate on_lodaded)
	{
		using Dictionary<string, ResearchedAlchemyRecipe>.Enumerator enumerator = ResearchedAlchemyRecipes.ReadRecipesFromFile()[slot.filename_no_extension].GetEnumerator();
		while (enumerator.MoveNext())
		{
			ResearchedAlchemyRecipes.ResearchedRecipes.Add(enumerator.Current.Key, enumerator.Current.Value);
		}
	}
}
