using UnityEngine;
using System;

public class GestureVoiceCommands : MonoBehaviour
{
	// Called by GazeManager or SpeechManager 
	  //when the user performs a Select gesture or states a voice command

	void OnSelect()
	{
		CallOnParent(
			r =>
			{
				if (r.isStopped)
				{
					r.speed = -r.speed;
				}
				r.isStopped = !r.isStopped;
			}
		);
	}

	void OnStart()
	{
		CallOnParent(r => r.isStopped = false);
	}

	void OnStop()
	{
		CallOnParent(r => r.isStopped = true);
	}

	void OnQuick()
	{
		CallOnParent(r => r.isFast = true);
	}

	void OnSlow()
	{
		CallOnParent(r => r.isFast = false);
	}

	void CallOnParent(Action<Rotate> f)
	{
		var rot = this.gameObject.GetComponentInParent<Rotate>();
		if (rot)
		{
			f(rot);
		}
	}
}