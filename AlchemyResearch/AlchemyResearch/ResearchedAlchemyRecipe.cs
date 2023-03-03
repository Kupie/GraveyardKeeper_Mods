using System.Collections.Generic;

namespace AlchemyResearch;

public class ResearchedAlchemyRecipe
{
	public string ingredient1 = "empty";

	public string ingredient2 = "empty";

	public string ingredient3 = "empty";

	public string result = "empty";

	public ResearchedAlchemyRecipe(string Ingredient1, string Ingredient2, string Ingredient3, string Result)
	{
		ingredient1 = Ingredient1;
		ingredient2 = Ingredient2;
		ingredient3 = Ingredient3;
		result = Result;
	}

	public string GetKey()
	{
		return GetKey(ingredient1, ingredient2, ingredient3);
	}

	public static string GetKey(string Ingredient1, string Ingredient2, string Ingredient3)
	{
		List<string> list = new List<string>(3);
		list.Add(Ingredient1);
		list.Add(Ingredient2);
		list.Add(Ingredient3);
		list.Sort();
		return $"{list[0]}|{list[1]}|{list[2]}";
	}

	public string AlchemyRecipeToString(string Ingredient1, string Ingredient2, string Ingredient3, string Result)
	{
		return $"{GetKey(Ingredient1, Ingredient2, Ingredient3)}|{Result}";
	}
}
