using System.Collections.Generic;
using System.IO;

namespace AlchemyResearch;

public class ResearchedAlchemyRecipes
{
	public static Dictionary<string, ResearchedAlchemyRecipe> ResearchedRecipes = new Dictionary<string, ResearchedAlchemyRecipe>();

	public static void AddCurrentRecipe(string ItemResult)
	{
		ResearchedAlchemyRecipe researchedAlchemyRecipe = new ResearchedAlchemyRecipe(AlchemyRecipe.Ingredient1, AlchemyRecipe.Ingredient2, AlchemyRecipe.Ingredient3, ItemResult);
		Logg.Log($"Adding Recipe: {AlchemyRecipe.Ingredient1}|{AlchemyRecipe.Ingredient2}|{AlchemyRecipe.Ingredient3} => {AlchemyRecipe.Result} | WGO: {AlchemyRecipe.WorkstationUnityID} / {AlchemyRecipe.WorkstationObjectID}");
		string key = researchedAlchemyRecipe.GetKey();
		if (!ResearchedRecipes.ContainsKey(key))
		{
			ResearchedRecipes.Add(researchedAlchemyRecipe.GetKey(), researchedAlchemyRecipe);
		}
		AlchemyRecipe.Initialize();
	}

	public static string IsRecipeKnown(string Ingredient1 = "empty", string Ingredient2 = "empty", string Ingredient3 = "empty")
	{
		string key = ResearchedAlchemyRecipe.GetKey(Ingredient1, Ingredient2, Ingredient3);
		if (ResearchedRecipes.ContainsKey(key))
		{
			return ResearchedRecipes[key].result;
		}
		return "unknown";
	}

	public static Dictionary<string, Dictionary<string, ResearchedAlchemyRecipe>> ReadRecipesFromFile()
	{
		Dictionary<string, Dictionary<string, ResearchedAlchemyRecipe>> dictionary = new Dictionary<string, Dictionary<string, ResearchedAlchemyRecipe>>();
		string[] array = null;
		string empty = string.Empty;
		string key = "1";
		if (!File.Exists(MainPatcher.KnownRecipesFilePathAndName))
		{
			File.CreateText(MainPatcher.KnownRecipesFilePathAndName);
		}
		try
		{
			array = File.ReadAllLines(MainPatcher.KnownRecipesFilePathAndName);
		}
		catch
		{
			return dictionary;
		}
		if (array == null || array.Length == 0)
		{
			return dictionary;
		}
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			empty = array2[i].Trim();
			if (empty.StartsWith(MainPatcher.ParameterComment) || string.IsNullOrEmpty(empty))
			{
				continue;
			}
			if (empty.StartsWith(MainPatcher.ParameterSectionBegin) && empty.EndsWith(MainPatcher.ParameterSectionEnd))
			{
				key = empty.Replace(MainPatcher.ParameterSectionBegin, string.Empty).Replace(MainPatcher.ParameterSectionEnd, string.Empty).Trim();
				if (!dictionary.ContainsKey(key))
				{
					dictionary.Add(key, new Dictionary<string, ResearchedAlchemyRecipe>());
				}
				continue;
			}
			string[] array3 = empty.Split(MainPatcher.ParameterSeparator);
			if (array3.Length >= 4)
			{
				ResearchedAlchemyRecipe researchedAlchemyRecipe = new ResearchedAlchemyRecipe(array3[0].Trim(), array3[1].Trim(), array3[2].Trim(), array3[3].Trim());
				dictionary[key].Add(researchedAlchemyRecipe.GetKey(), researchedAlchemyRecipe);
			}
		}
		using Dictionary<string, Dictionary<string, ResearchedAlchemyRecipe>>.Enumerator enumerator = dictionary.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Logg.Log($"Loaded Recipes for Savegame [{enumerator.Current.Key}]: {enumerator.Current.Value.Count}");
		}
		return dictionary;
	}

	public static void WriteRecipesToFile(string SaveGameName)
	{
		List<string> list = new List<string>();
		Dictionary<string, Dictionary<string, ResearchedAlchemyRecipe>> dictionary = ReadRecipesFromFile();
		if (!dictionary.ContainsKey(SaveGameName))
		{
			dictionary.Add(SaveGameName, new Dictionary<string, ResearchedAlchemyRecipe>());
		}
		using (Dictionary<string, ResearchedAlchemyRecipe>.Enumerator enumerator = ResearchedRecipes.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!dictionary[SaveGameName].ContainsKey(enumerator.Current.Key))
				{
					dictionary[SaveGameName].Add(enumerator.Current.Key, enumerator.Current.Value);
				}
			}
		}
		Dictionary<string, Dictionary<string, ResearchedAlchemyRecipe>>.Enumerator enumerator2 = dictionary.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			list.Add($"[{enumerator2.Current.Key}]");
			Dictionary<string, ResearchedAlchemyRecipe>.Enumerator enumerator3 = enumerator2.Current.Value.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				string item = enumerator3.Current.Value.AlchemyRecipeToString(enumerator3.Current.Value.ingredient1, enumerator3.Current.Value.ingredient2, enumerator3.Current.Value.ingredient3, enumerator3.Current.Value.result);
				list.Add(item);
			}
		}
		try
		{
			File.WriteAllLines(MainPatcher.KnownRecipesFilePathAndName, list);
		}
		catch
		{
		}
	}
}
