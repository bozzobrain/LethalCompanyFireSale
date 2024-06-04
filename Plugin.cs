using BepInEx;
using FireSale.Configuration;
using HarmonyLib;
using System.Reflection;

namespace FireSale
{
	[BepInPlugin(GUID, NAME, VERSION)]
	internal class FireSale : BaseUnityPlugin
	{
		public static FireSale instance;
		private const string GUID = "FireSale";
		private const string NAME = "FireSale";
		private const string VERSION = "1.0.1";

		public static void Log(string message)
		{
			instance.Logger.LogInfo((object)message);
		}

		private void Awake()
		{
			instance = this;
			ConfigSettings.BindConfigSettings();

			// Plugin startup logic
			Logger.LogInfo($"Plugin {GUID} is loaded!");

			Harmony harmony = new Harmony(GUID);
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
	}
}