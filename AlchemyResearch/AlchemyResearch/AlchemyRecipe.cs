namespace AlchemyResearch;

public class AlchemyRecipe
{
	public static int WorkstationUnityID = -1;

	public static string WorkstationObjectID = string.Empty;

	public static string Ingredient1 = "empty";

	public static string Ingredient2 = "empty";

	public static string Ingredient3 = "empty";

	public static string Result = "empty";

	public const string ItemEmpty = "empty";

	public const string ItemUnknown = "unknown";

	public static void Initialize()
	{
		WorkstationUnityID = -1;
		WorkstationObjectID = string.Empty;
		Ingredient1 = "empty";
		Ingredient2 = "empty";
		Ingredient3 = "empty";
		Result = "empty";
	}

	public static bool HasValidRecipe()
	{
		if (Ingredient1 != "empty" && !string.IsNullOrEmpty(Ingredient1) && Ingredient2 != "empty" && !string.IsNullOrEmpty(Ingredient2))
		{
			if (Result != "empty")
			{
				return !string.IsNullOrEmpty(Result);
			}
			return false;
		}
		return false;
	}

	public static bool MatchesResult(int CraftingStationUnityID, string CraftingStationObjectID, string ResultItemID)
	{
		if (WorkstationUnityID != CraftingStationUnityID || WorkstationObjectID != CraftingStationObjectID)
		{
			return false;
		}
		if (!(ResultItemID == Result))
		{
			if (ResultItemID.StartsWith("goo_"))
			{
				return Result.StartsWith("goo_");
			}
			return false;
		}
		return true;
	}

	public static string AlchemyRecipeToString(string Ingredient1, string Ingredient2, string Ingredient3, string Result)
	{
		return $"{ResearchedAlchemyRecipe.GetKey(Ingredient1, Ingredient2, Ingredient3)}|{Result}";
	}

	public static string GetCurrentRecipe()
	{
		return $"{Ingredient1}|{Ingredient2}|{Ingredient3}|{Result}|{WorkstationUnityID}|{WorkstationObjectID}";
	}
}
