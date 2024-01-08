using GameNetcodeStuff;
using HarmonyLib;
using FireSale.EntityHelpers;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using static FireSale.Networking.NetworkFunctions;
using static FireSale.EntityHelpers.HangarShipHelper;
using Object = UnityEngine.Object;
using FireSale.HelperFunctions;
using FireSale.Configuration;
using TMPro;

namespace FireSale
{
	[HarmonyPatch]
	internal class FireSaleFunctions
	{
		public static Vector3 GetDepositCounterLocation()
		{
			GameObject despositCounter = GameObject.Find("/BellDinger");
			return despositCounter.gameObject.transform.position;
		}

		public static bool OnCompanyPlanet()
		{
			GameObject companyPlanet = GameObject.Find("/Environment/Map/CompanyPlanet");
			return companyPlanet != null;
		}

		/* ***    **********z - >
		 * ***    ****************
		 * ^
		 * -
		 * x
		 *
		 *
		 *
		 *
		 *
		 */

		/// <summary>
		/// Moves all loot to the ship if you are in it.
		/// Moves loot to the player if outside the ship.
		/// </summary>
		///
		internal static void RetrieveAllLoot()
		{
			if (OnCompanyPlanet())
			{
				HangarShipHelper hsh = new();
				if (Keybinds.localPlayerController.isInHangarShipRoom)
				{
					var allScrap = ScrapHelperFunctions.FindAllScrapOnMap();
					if (allScrap.Where(obj => obj.isInShipRoom == false).Any())
					{
						FireSale.Log("--------------- Move Items to Ship ----------");
						foreach (var obj in allScrap)
						{
							if (!obj.isInShipRoom)
							{
								hsh.MoveItemToShip(obj);
							}
						}
					}
					FireSale.Log("--------------- Organize Ship Loot ----------");
					LootOrganizingFunctions.OrganizeShipLoot();
				}
				else
				{
					FireSale.Log("--------------- Move Items To Player ----------");
					// TODO optional move to players direct location
					MoveObjectsToDeskArea(hsh);
				}
			}
			else
			{
				FireSale.Log("No actions allowed off company planet");
			}
		}

		private static void MoveObjectsToDeskArea(HangarShipHelper hsh)
		{
			var depositCounterLocation = GetDepositCounterLocation();
			var shipObjects = ScrapHelperFunctions.SortByValue(hsh.ObjectsInShip(), false);
			// TODO optional exclude items

			var targetPosition = new Vector3(depositCounterLocation.x + 1, depositCounterLocation.y, depositCounterLocation.z - 2);

			int previousTenPercentStep = 0;
			float xCounter = 1f;
			for (int i = 0; i < shipObjects.Count; i++)
			{
				GrabbableObject obj = shipObjects[i];
				int tenPercentStep = i / 10;

				Vector3 placementLocation = new(targetPosition.x + xCounter, targetPosition.y, targetPosition.z + tenPercentStep);
				NetworkingObjectManager.MakeObjectFallRpc(obj, placementLocation, true);
				if (tenPercentStep != previousTenPercentStep)
				{
					xCounter = 1f;
					previousTenPercentStep = tenPercentStep;
				}
				else
				{
					xCounter += 1f;
				}
				FireSale.Log($"XCounter: {xCounter} - tenPercentStep: {tenPercentStep} - i: {i}");
			}
		}
	}
}