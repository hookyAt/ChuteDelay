ChuteDelay
==========

ChuteDelay v1.1

## SUMMARY

* ChuteDelay allows you to add a configurable delay to any attached parachute. After staging the parachute will delay its deployment until the configured time is up.
	* e.g. until the burn time of the Sepratron I is over (about 5s)
* ChuteDelay is more a karma plug-in since the “On-Rails” physics still apply's
	* You can get more information on this wiki page (http://wiki.kerbalspaceprogram.com/wiki/Atmosphere#On-rails_physics).

## REQUIREMENTS

* Kerbal Space Program version 1.0.5
* ModuleManager => 2.6.3
	* You can get more information on this forum thread (http://forum.kerbalspaceprogram.com/index.php?/topic/50533-105-module-manager-2618-january-17th-with-even-more-sha-and-less-bug/)

## INSTALLATION

* Install as you normally install mods for Kerbal Space Program - by extracting the folder in this archive into the GameData folder in Kerbal Space Program's directory.

### Folder Structure

* GameData
	* ModuleManager.2.6.3.dll
	* hooky
		* ChuteDelay
			* chuteDelay.cfg
			* chuteDelay.dll

## INSTRUCTIONS

* You can activate and assign delays for parachutes.
	* right click on the parachute and choose a delay between 0 - 20 sec
	* click on "Switch Delay On"
	* in flight decouple your stage
	* parachute gets deployed after your delay
* There is no support for symmetry parts, all parachutes have to be edited by hand.
* There is no support for action groups. If activated with an action group the parachutes will deploy without delay.


## TROUBLESHOOTING

* If you notice anything behaving strangely, please check the debug log by hitting Alt-F12 and reporting any messages that might look applicable to the problem you are having on GitHub (https://github.com/hookyAt/ChuteDelay/issues).
	* Lines from the Plugin start with [ChuteDelay]

## LICENSE

ChuteDelay Copyright (C) 2016, Daniel Hooker (hookyAt)

	This program comes with ABSOLUTELY NO WARRANTY!
	This is free software, and you are welcome to redistribute it under certain conditions, as outlined in the full content of the GNU General Public License (GNU GPL), version 3, revision date 29 June 2007.
    See attached LICENSE.md

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. 
