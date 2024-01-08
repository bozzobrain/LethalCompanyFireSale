using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;
using FireSale.Configuration;
using static FireSale.Networking.NetworkFunctions;

namespace FireSale.HelperFunctions
{
	public static class ScrapHelperFunctions
	{
		/// <summary>
		/// Get the highest value of the loot in the ship.
		/// </summary>
		/// <returns>The value of the highest valued loot on the ship.</returns>
		public static float CalculateHighestScrapValue(List<GrabbableObject> objects)
		{
			float highestScrap = 0;

			foreach (GrabbableObject obj in objects)
			{
				if (obj.scrapValue > highestScrap)
				{
					highestScrap = obj.scrapValue;
				}
			}

			return highestScrap;
		}

		public static void PlaceObjectAtPlayer(GrabbableObject obj)
		{
			Vector3 playerPosition = Keybinds.localPlayerController.gameplayCamera.transform.position;
			Vector3 targetPosition = new(playerPosition.x - 1f, playerPosition.y + 0.2f, playerPosition.z);

			NetworkingObjectManager.MakeObjectFallRpc(obj, targetPosition, true);
		}

		/// <summary>
		/// Sort List of GrabbableItems items by value.
		/// </summary>
		/// <returns>The value of the highest valued loot on the ship.</returns>
		public static List<GrabbableObject> SortByValue(List<GrabbableObject> objects, bool ascending)
		{
			if (ascending) return objects.OrderBy(obj => obj.scrapValue).ToList();
			else return objects.OrderByDescending(obj => obj.scrapValue).ToList();
		}

		/// <summary>
		/// Get a list of all scrap on the map ouside of the ship room.
		/// </summary>
		/// <returns>List of all scrap on map.</returns>
		internal static List<GrabbableObject> FindAllScrapOnMap()
		{
			List<GrabbableObject> scrapList = new List<GrabbableObject>();

			var genericObjectThatIsScrap = UnityEngine.Object.FindObjectsByType<GrabbableObject>(FindObjectsSortMode.None);
			foreach (var actualScrap in genericObjectThatIsScrap)
			{
				if (!actualScrap.isInShipRoom)
				{
					scrapList.Add(actualScrap);
					FireSale.Log($"Found scrap: {actualScrap.name}");
				}
			}
			return scrapList;
		}
	}
}