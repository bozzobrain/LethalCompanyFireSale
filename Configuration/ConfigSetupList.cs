using BepInEx.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FireSale.Configuration
{
	public class ConfigSetupList
	{
		public ConfigEntry<string> Key;

		public string KeyDisplayName;

		public string pluginName;

		public string SettingDescription;

		public string SettingName;

		public string SettingValue;

		public ConfigSetupList()
		{
		}

		public static ConfigEntry<string> CreateKey(ConfigSetupList c)
		{
			return FireSale.instance.Config.Bind(c.pluginName, c.SettingName, c.SettingValue, c.SettingDescription);
		}

		public Dictionary<string, ConfigEntryBase> Bind(Dictionary<string, ConfigEntryBase> toDictionary)
		{
			Key = CreateKey(this);
			toDictionary.Add(Key.Definition.Key, (ConfigEntryBase)(object)Key);
			return toDictionary;
		}

		public List<string> GetStrings(string configSetting)
		{
			List<string> results = new List<string>();
			Regex ItemMatches = new Regex("((?<item>[A-Za-z]+)[,]*)");
			var result = ItemMatches.Matches(configSetting);
			foreach (Match ItemMatch in result)
			{
				string item = ItemMatch.Groups["item"].ToString();
				results.Add(ItemMatch.Groups["item"].ToString());
				FireSale.Log($"Got configuration item {item}");
			}

			return results;
		}
	}
}