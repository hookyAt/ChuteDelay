//
// <summary>
// ChuteDelay Copyright (C) 2016, Daniel Hooker (hookyAt)
// This program comes with ABSOLUTELY NO WARRANTY!
// This is free software, and you are welcome to redistribute it under certain conditions, as outlined in the full content of the GNU General Public License (GNU GPL), version 3, revision date 29 June 2007.
//
// Source Code can be found at https://github.com/hookyAt/ChuteDelay
//</summary>
using System;
using System.Threading;
using UnityEngine;

namespace chuteDelay
{
	public class ChuteDelay : ModuleParachute
	{
		[KSPField (isPersistant = true)]
		public bool delayActive = false;

		[UI_FloatRange (minValue = 0.0f, maxValue = 20f, stepIncrement = 0.1f)]
		[KSPField (isPersistant = true, guiActiveEditor = true, guiActive = true, guiFormat = "F1", guiUnits = "sec", guiName = "Delay")]
		public float delayTime = 5.0f;

		public override void OnActive ()
		{
			Debug.Log ("[ChuteDelay] Part " + this.name + " is OnActive");

			if (delayActive) {
				Debug.Log ("[ChuteDelay] Creating delay thread");
				Thread delayThread = new Thread (new ThreadStart (this.DelayDeploy));
				delayThread.Start ();
				Debug.Log ("[ChuteDelay] Delay thread alive: " + delayThread.IsAlive);
			} else {
				Debug.Log ("[ChuteDelay] No Delay, call OnActive on base class" + this.name);
				base.OnActive ();
			}
		}

		public void DelayDeploy ()
		{
			float threadSleep = delayTime * 1000;
			Thread.Sleep ((int)threadSleep);
			Debug.Log ("[ChuteDelay] waited " + threadSleep + " ms, call OnActive on base class" + this.name);
			base.OnActive ();
		}

		[KSPEvent (guiActive = true, guiActiveEditor = true, guiName = "Switch Delay On")]
		public void ToggleDelay ()
		{
			delayActive = !delayActive;
			if (delayActive) {
				Events ["ToggleDelay"].guiName = "Switch Delay Off";
			} else {
				Events ["ToggleDelay"].guiName = "Switch Delay On";
			}
		}
	}
}
