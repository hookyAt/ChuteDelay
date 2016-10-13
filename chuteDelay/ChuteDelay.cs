//
// <summary>
// ChuteDelay Copyright (C) 2016, Daniel Hooker (hookyAt)
// This program comes with ABSOLUTELY NO WARRANTY!
// This is free software, and you are welcome to redistribute it under certain conditions, as outlined in the full content of the GNU General Public License (GNU GPL), version 3, revision date 29 June 2007.
//
// Source Code can be found at https://github.com/hookyAt/ChuteDelay
//</summary>
using System.Threading;
using UnityEngine;

namespace chuteDelay
{
	public class ChuteDelay : ModuleParachute
	{
		[KSPField(guiName = "Delay", isPersistant = true, guiActive = true),
			UI_Toggle(disabledText = "Off", scene = UI_Scene.All, enabledText = "On", affectSymCounterparts = UI_Scene.All)]
		public bool delayActive = false;

		[KSPField(guiName = "Deploy when", isPersistant = true, guiActive = true),
			UI_Toggle(disabledText = "Unsafe", scene = UI_Scene.All, enabledText = "Safe", affectSymCounterparts = UI_Scene.All)]
		public bool deployWhenSafe = false;

		[KSPField(guiName = "Deploy Airbrakes", isPersistant = true, guiActive = true),
			UI_Toggle(disabledText = "No", scene = UI_Scene.All, enabledText = "Yes", affectSymCounterparts = UI_Scene.All)]
		public bool deployAirbrakes = false;

		[KSPField(isPersistant = true, guiActiveEditor = true, guiActive = true, guiFormat = "F1", guiUnits = "sec", guiName = "Delay Time"),
			UI_FloatRange(minValue = 0.0f, maxValue = 20f, stepIncrement = 0.1f, affectSymCounterparts = UI_Scene.All)]
		public float delayTime = 5.0f;

		public override void OnActive()
		{
			Debug.Log("[ChuteDelay] part " + name + " is OnActive");
			if (delayActive)
			{
				Debug.Log("[ChuteDelay] creating delay thread");
				var delayThread = new Thread(new ThreadStart(DelayOnActive));
				delayThread.Start();
				Debug.Log("[ChuteDelay] delay thread alive: " + delayThread.IsAlive);
			}
			else {
				Debug.Log("[ChuteDelay] no delay, call base.OnActive on base class" + name);
				base.OnActive();
			}

		}

		[KSPAction("Deploy w Delay")]
		public void ActionDeployWithDelay(KSPActionParam param)
		{
			Debug.Log("[ChuteDelay] " + param + " deploy w delay active " + name);
			Debug.Log("[ChuteDelay] creating delay thread");
			var delayThread = new Thread(new ThreadStart(DelayActionDeploy));
			delayThread.Start();
			Debug.Log("[ChuteDelay] delay thread alive: " + delayThread.IsAlive);
		}

		public void DelayOnActive()
		{
			float threadSleep = delayTime * 1000;
			Thread.Sleep((int)threadSleep);
			Debug.Log("[ChuteDelay] waited " + threadSleep + " ms, call base.OnActive on base class" + name);
			DeployAirbrakes();
			DeployWhenSafe();
			base.OnActive();
		}

		public void DelayActionDeploy()
		{
			float threadSleep = delayTime * 1000;
			Thread.Sleep((int)threadSleep);
			Debug.Log("[ChuteDelay] waited " + threadSleep + " ms, call base.Deploy on base class" + name);
			DeployAirbrakes();
			DeployWhenSafe();
			Deploy();
		}

		private void DeployWhenSafe()
		{
			if (deployWhenSafe)
			{
				while (true)
				{
					if (deploymentSafeState == deploymentSafeStates.SAFE)
					{
						Debug.Log("[ChuteDelay] safe, stop waiting " + name);
						break;
					}
					Thread.Sleep(1000);
				}
			}
		}

		private void DeployAirbrakes()
		{
			if (deployAirbrakes)
			{
				Debug.Log("[ChuteDelay] find all airbrakes on vessel " + name);
				vessel.parts.ForEach(p => p.Modules.GetModules<ModuleAeroSurface>().ForEach(a =>
				{
					a.deploy = true;
					a.part.SendEvent("deploy");
					Debug.Log("[ChuteDelay] deploy airbrakes " + name);
				}));
			}
		}
	}
}
