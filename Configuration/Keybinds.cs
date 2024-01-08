using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace FireSale.Configuration
{
	[HarmonyPatch]
	internal static class Keybinds
	{
		public static PlayerControllerB localPlayerController;

		private static InputAction FireSaleGrabLoot;

		[HarmonyPatch(typeof(PlayerControllerB), "OnDisable")]
		[HarmonyPostfix]
		public static void OnDisable(PlayerControllerB __instance)
		{
			if (FireSaleGrabLoot != null && !((Object)(object)__instance != (Object)(object)localPlayerController))
			{
				FireSaleGrabLoot.performed -= OnFireSaleCalled;
				FireSaleGrabLoot.Disable();
			}
		}

		[HarmonyPatch(typeof(PlayerControllerB), "OnEnable")]
		[HarmonyPostfix]
		public static void OnEnable(PlayerControllerB __instance)
		{
			if ((Object)(object)__instance == (Object)(object)localPlayerController)
			{
				SubscribeToEvents();
			}
		}

		[HarmonyPatch(typeof(PlayerControllerB), "ConnectClientToPlayerObject")]
		[HarmonyPostfix]
		public static void OnLocalPlayerConnect(PlayerControllerB __instance)
		{
			localPlayerController = __instance;
			FireSaleGrabLoot = new InputAction(null, 0, ConfigSettings.FireSaleInputAction.Key.Value, "Press", null, null);
			if (localPlayerController.gameObject.activeSelf)
			{
				SubscribeToEvents();
			}
		}

		private static void OnFireSaleCalled(CallbackContext context)
		{
			if ((Object)(object)localPlayerController == null || !localPlayerController.isPlayerControlled || localPlayerController.inTerminalMenu || localPlayerController.IsServer && !localPlayerController.isHostPlayerObject)
			{
				return;
			}
			FireSale.Log("Get all loot");
			FireSaleFunctions.RetrieveAllLoot();
		}

		private static void SubscribeToEvents()
		{
			if (FireSaleGrabLoot != null)
			{
				FireSaleGrabLoot.Enable();

				FireSaleGrabLoot.performed += OnFireSaleCalled;
			}
		}
	}
}