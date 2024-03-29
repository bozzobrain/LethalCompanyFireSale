# LethalCompanyFireSale
This is very similar functionality to ShipMaid-[Github](https://github.com/bozzobrain/LethalCompanyShipMaid/releases)/[ThunderStore](https://thunderstore.io/c/lethal-company/p/bozzobrain/ShipMaid/) and InstantLoot-[Github](https://github.com/bozzobrain/LethalCompanyInstantLoot/releases)/[ThunderStore](https://thunderstore.io/c/lethal-company/p/bozzobrain/InstantLoot/). It is virtually a copy of InstantLoot, except it doesnt allow you to steal all of the loot from any map, but rather is limited to only permit you to teleport loot at the company.

This mod is much more directly intended to be a Quality of Life mod, not a cheat.

# Features
- You can teleport loot around with ease while on the company planet.
	- If you are outside of the ship, you can teleport the loot to the counter for easy collection.
	- If you are on the ship, any loot is teleported into the ship and automatically collected. 
- Activate the teleporting with the 'k'
	- Keybinding can be modified in the cfg file
- Returning the loot to the ship is built around the ship organizing functionaly of ShipMaid
	- ShipMaid is currently NOT a dependency but may be in the future as they share much of the codebase and config
	- If I get feedback about the annoyance of managing the settings, I will invest more time into fixing it :)

## Config File Parameters
- FireSaleKey
	- Keypress used to activate the mod

- ItemGrouping
	- Whether to group the items in tight clusters or to spread them out by value
	- Options
		- [Value]
			- Spread items up the ship by the value
		- [Stack]
			- Keep items stacked on top of one another to reduce clutter

- TwoHandedItemLocation
	- Where to place the two handed items, and inherrently where to place the single handed objects.
	- Options
		- [Front]
			- Two handed items to the front of the ship 
			- One handed items to the back of the ship
		- [Back]
			- Two handed items to the back of the ship 
			- One handed items to the front of the ship

- OrganizationTechniques
	- Options
		- [Loose]
			- Spread items accross the ship from left to right
		- [Tight]
			- Pack the items to the side of the ship with the suit rack.

- ClosetLocationOverride
	- A List of objects to force into the closet on ship cleanup
		- Enter a list of item names in comma separated form to force these items to be placed in the closet instead of on the floor.

- SortingDisabledList
	- Items on this list will be ignored during the sorting process
		- Enter a list of item names in comma separated form to ignore these items during organization.

# Installation
1. Install BepInEx
2. Run game once with BepInEx installed to generate folders/files
3. Drop the DLL inside of the BepInEx/plugins folder
4. No further steps needed

# Feedback
- Feel free to leave feedback or requests at [my github](https://github.com/bozzobrain/LethalCompanyFireSale).

# Buy me a coffee
[Buy me a beer](https://www.buymeacoffee.com/bozzobrain)
