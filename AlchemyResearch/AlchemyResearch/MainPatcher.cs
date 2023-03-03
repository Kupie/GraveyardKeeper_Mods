using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace AlchemyResearch;

[HarmonyAfter(new string[] { "com.graveyardkeeper.urbanvibes.alchemyresearch" })]
public class MainPatcher
{
	public static string ResultPreviewText = "Result";

	public static readonly string configFilePathAndName = Application.dataPath + "/../QMods/AlchemyResearch/config.txt";

	public static readonly string KnownRecipesFilePathAndName = Application.dataPath + "/../QMods/AlchemyResearch/Known Recipes.txt";

	public static readonly char ParameterSeparator = '|';

	public static readonly string ParameterComment = "#";

	public static readonly string ParameterSectionBegin = "[";

	public static readonly string ParameterSectionEnd = "]";

	public const string ParameterResultPreviewText = "ResultPreviewText";

	public static void Patch()
	{
		try
		{
			new Harmony("com.graveyardkeeper.urbanvibes.alchemyresearch").PatchAll(Assembly.GetExecutingAssembly());
		}
		catch (Exception ex)
		{
			Logg.Log("[AlchemyResearch]: " + ex.Message + ", " + ex.Source + ", " + ex.StackTrace);
		}
		Logg.Log("Initialization");
		Reflection.Initialization();
		Logg.Log("ReadParametersFromFile");
		ReadParametersFromFile();
		Logg.Log("Patch done");
	}

	public static void ReadParametersFromFile()
	{
		string[] array = null;
		string empty = string.Empty;
		try
		{
			array = File.ReadAllLines(configFilePathAndName);
		}
		catch (Exception)
		{
			return;
		}
		if (array == null || array.Length == 0)
		{
			return;
		}
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			empty = array2[i].Trim();
			if (!string.IsNullOrEmpty(empty) && !empty.StartsWith(ParameterComment))
			{
				string[] array3 = empty.Split(ParameterSeparator);
				if (array3.Length >= 2 && array3[0].Trim() == "ResultPreviewText" && !string.IsNullOrEmpty(array3[1].Trim()))
				{
					ResultPreviewText = array3[1].Trim();
					break;
				}
			}
		}
	}
}
