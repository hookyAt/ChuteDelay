ChuteDelay
==========

ChuteDelay v1.2

## SUMMARY

ChuteDelay allows you to add a configurable delay to any attached parachute. After staging the parachute will delay its deployment until the configured time is up e.g. until the burn time of the Sepratron I is over (about 5s).

So now you when drop your burnt out stages with a parachute it will deploy at a safe distance. Or you can time the burn time of your  sepratron engines. So that the exhaust stream does not destroy the chute.

ChuteDelay is more a **karma** mod, because you know the stage was dropped reusable. The "On-Rails" physics still apply's. Meaning that all Vessels when they no longer are the primary focus of the simulation, will be unloaded after a certain distance (about 23km) when the atmospheric pressure is greater than 0.01 atm.

You can get more information on this [wiki page] (http://wiki.kerbalspaceprogram.com/wiki/Atmosphere#On-rails_physics).

## REQUIREMENTS

* Kerbal Space Program version 1.1.0
* ModuleManager => 2.6.22
	* You can get more information on this [forum thread] (http://forum.kerbalspaceprogram.com/index.php?/topic/50533-105-module-manager-2618-january-17th-with-even-more-sha-and-less-bug/)

## INSTALLATION

* Install as you normally install mods for Kerbal Space Program - by extracting the folder in this archive into the GameData folder in Kerbal Space Program's directory.

### Folder Structure

* GameData
	* ModuleManager.2.6.22.dll
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
* Symmetry parts now synchronize their changes in the VAB/SPH
	* In flight the changes are not synchronized
* There is an action group "Deploy w Delay" it respects the delay of each parachute, ignores the activated attribute

## CHANGES
Since Version 1.1
* made ready for KSP 1.1.0
Since Version 1.0
* Settings are synchronize to their symmetry parts
	* only apply's when in the VAB/SPH
* Parachutes have a new action group "Deploy w Delay".
	* if "deploy w delay" is used, there is no checking it the delay is activated. It will use the last configured delay saved in the persistence file.

## TROUBLESHOOTING

* If you notice anything behaving strangely, please check the debug log by hitting Alt-F12 and reporting any messages that might look applicable to the problem you are having on GitHub (https://github.com/hookyAt/ChuteDelay/issues).
	* Lines from the Plugin start with [ChuteDelay]

## LICENSE

ChuteDelay Copyright (C) 2016, Daniel Hooker (hookyAt)

	This program comes with ABSOLUTELY NO WARRANTY!
	This is free software, and you are welcome to redistribute it under certain conditions, as outlined in the full content of the GNU General Public License (GNU GPL), version 3, revision date 29 June 2007.
    See attached LICENSE.md

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software. 
