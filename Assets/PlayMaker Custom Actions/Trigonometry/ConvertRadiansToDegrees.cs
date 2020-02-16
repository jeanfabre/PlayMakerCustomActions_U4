// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Original action by Red, and Wesley M. Brown of BadSeedGames
// http://hutonggames.com/playmakerforum/index.php?topic=10470.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Trigonometry")]
	[Tooltip("Convert Radians to Degrees. This is using a constant: 360 / (PI * 2).")]
	public class ConvertRadiansToDegrees : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The angle expressed in radians")]
		public FsmFloat angleInRadians;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The angle in degrees")]
		public FsmFloat angleInDegrees;
				
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			angleInDegrees = null;
			everyFrame = false;
			angleInRadians = null;
		}

		public override void OnEnter()
		{
			DoConvertion();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoConvertion();
		}
		
		void DoConvertion()
		{
		 	angleInDegrees.Value = angleInRadians.Value*Mathf.Rad2Deg;
		}
	}
}
