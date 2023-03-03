using System.IO;
using System.Reflection;

namespace AlchemyResearch;

public class Logg
{
	private static string _assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    // C:\Games\Graveyard Keeper\QMods\GKSleepModFixed
    private static string path = Path.Combine(_assemblyFolder, "Mod log.txt");

    public static void Log(string Text)
	{
		if (!File.Exists(path))
		{
			using (StreamWriter streamWriter = File.CreateText(path))
			{
				streamWriter.WriteLine(Text);
				return;
			}
		}
		using StreamWriter streamWriter2 = File.AppendText(path);
		streamWriter2.WriteLine(Text);
	}

	public static void LogClear()
	{
		using (File.CreateText(path))
		{
		}
	}
}
