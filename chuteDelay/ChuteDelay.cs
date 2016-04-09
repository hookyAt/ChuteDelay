using System;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;

namespace chuteDelay
{
	public class ChuteDelay : ModuleParachute
	{
		[KSPField (isPersistant = true)]
		public bool delayActive = false;

		[UI_FloatRange (minValue = 0.0f, maxValue = 10f, stepIncrement = 0.1f)]
		[KSPField (isPersistant = true, guiActiveEditor = true, guiActive = true, guiFormat = "F1", guiUnits = "sec", guiName = "Delay")]
		public float delayTime = 2.0f;

		public override void OnActive ()
		{
			Debug.Log ("[ChuteDelay] Part " + this.name + " is OnActive");
			if (delayActive) {
				Debug.Log ("[ChuteDelay] Creating delay thread");
				Thread delayThread = new Thread (new ThreadStart (this.DelayActive));
				delayThread.Start ();
				Debug.Log ("[ChuteDelay] Delay thread alive: " + delayThread.IsAlive);
			} else {
				Debug.Log ("[ChuteDelay] No Delay, call OnActive on base class" + this.name);
				base.OnActive ();
			}

		}

		[KSPAction ("Deploy w Delay")]
		public void DeployWDelay (KSPActionParam param)
		{
			Debug.Log ("[ChuteDelay] Part " + this.name + " is Deploy");
			if (delayActive) {
				Debug.Log ("[ChuteDelay] Creating delay thread");
				Thread delayThread = new Thread (new ThreadStart (this.DelayDeploy));
				delayThread.Start ();
				Debug.Log ("[ChuteDelay] Delay thread alive: " + delayThread.IsAlive);
			} else {
				Debug.Log ("[ChuteDelay] No Delay, call Deploy on base class" + this.name);
				base.Deploy ();
			}
		}

		public void DelayActive ()
		{
			float threadSleep = delayTime * 1000;
			Thread.Sleep ((int)threadSleep);
			Debug.Log ("[ChuteDelay] waited " + threadSleep + " ms, call OnActive on base class" + this.name);
			base.OnActive ();
		}

		public void DelayDeploy ()
		{
			float threadSleep = delayTime * 1000;
			Thread.Sleep ((int)threadSleep);
			Debug.Log ("[ChuteDelay] waited " + threadSleep + " ms, call Deploy on base class" + this.name);
			base.Deploy ();
		}

		[KSPEvent (active = true, guiActive = true, guiActiveEditor = true, guiName = "Switch Delay On", name = "ToggleDelay")]
		public void ToggleDelay ()
		{
			delayActive = !delayActive;
			Debug.Log ("[ChuteDelay] toggle delay to: " + delayActive);
			ToggleDelayUpdateUiString ();
		}

		[KSPEvent (active = true, guiActive = false, guiActiveEditor = false, name = "ToggleDelayUpdateUiString")]
		public void ToggleDelayUpdateUiString ()
		{
			Debug.Log ("[ChuteDelay] display correct ui string");
			if (delayActive) {
				Events ["ToggleDelay"].guiName = "Switch Delay Off";
			} else {
				Events ["ToggleDelay"].guiName = "Switch Delay On";
			}
		}

		public void SyncSettingsToSymParts ()
		{
			Debug.Log ("[ChuteDelay] Syncing to symmetry parts");
			this.part.symmetryCounterparts.ForEach (
				p => {
					p.Modules.GetModules<ChuteDelay> ().ForEach (m => {
						if (!m.Equals (this)) {
							m.delayActive = this.delayActive;
							m.delayTime = this.delayTime;
						}
					});
					p.SendEvent ("ToggleDelayUpdateUiString");
				}
			);
		}

		public override void OnAwake ()
		{
			Debug.Log ("[ChuteDelay] register onPartActionUIDismiss");
			GameEvents.onPartActionUIDismiss.Add (this.onPartActionUIDismiss);
		}

		public virtual void Destroy ()
		{
			Debug.Log ("[ChuteDelay] unregister onPartActionUIDismiss");
			GameEvents.onPartActionUIDismiss.Remove (this.onPartActionUIDismiss);
		}

		protected virtual void onPartActionUIDismiss (Part data)
		{
			if (this.part != null && this.part.vessel == null && data == this.part) {
				Debug.Log ("[ChuteDelay] Called on onPartActionUIDismiss");
				SyncSettingsToSymParts ();
			}
		}
	}
}
