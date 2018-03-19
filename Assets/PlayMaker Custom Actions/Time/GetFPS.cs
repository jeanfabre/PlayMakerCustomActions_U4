// (c) Copyright HutongGames, LLC 2010-2018. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("It calculates frames/second over each updateInterval.It is also fairly accurate at very low FPS counts (<10)." +
	 	"We do this not by simply counting frames per interval, but" +
	 	"by accumulating FPS for each frame. This way we end up with" +
	 	 "correct overall FPS even if the interval renders something like 5.5 frames. " +
	 	 "credits: http://unifycommunity.com/wiki/index.php?title=FramesPerSecond")]
	public class GetFPS : FsmStateAction
	{
	
		[Tooltip("Interval sampling")]
		public FsmFloat updateInterval = 0.5f;

 		float accum = 0f; // FPS accumulated over the interval
 		int frames = 0; // Frames drawn over the interval
 		float timeleft ; // Left time for current interval


		[Tooltip("The current Frame per second")]
		[UIHint(UIHint.Variable)]
		public FsmFloat FPS;
		
		[Tooltip("The current Frame per second formated as string")]
		[UIHint(UIHint.Variable)]
		public FsmString FPS_asString;
		
		public override void Reset()
		{
			updateInterval = 0.5f;
			FPS = null;
			FPS_asString = null;
		}


		public override void OnUpdate()
		{
			   timeleft -= Time.deltaTime;
			    accum += Time.timeScale/Time.deltaTime;
			    ++frames;
			    
			    // Interval ended - update values and start new interval
			    if( timeleft <= 0.0 )
			    {
					FPS.Value = accum/frames;
					FPS_asString.Value = "" + (accum/frames).ToString("f2"); // display two fractional digits (f2 format)
			       
			        timeleft = updateInterval.Value;
			        accum = 0f;
			        frames = 0;
			    }
		}
	}
}