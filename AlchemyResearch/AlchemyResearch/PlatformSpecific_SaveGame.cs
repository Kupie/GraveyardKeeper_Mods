using System;
using HarmonyLib;

namespace AlchemyResearch;

[HarmonyPatch(typeof(PlatformSpecific), "SaveGame", new Type[]
{
	typeof(SaveSlotData),
	typeof(GameSave),
	typeof(PlatformSpecific.OnSaveCompleteDelegate)
})]
public class PlatformSpecific_SaveGame
{
	[HarmonyPostfix]
	public static void Patch(SaveSlotData slot, GameSave save, PlatformSpecific.OnSaveCompleteDelegate on_complete)
	{
		ResearchedAlchemyRecipes.WriteRecipesToFile(slot.filename_no_extension);
	}
}
