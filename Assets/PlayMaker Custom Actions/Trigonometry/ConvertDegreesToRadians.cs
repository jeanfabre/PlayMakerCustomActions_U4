// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// Original action by Red, and Wesley M. Brown of BadSeedGames
// http://hutonggames.com/playmakerforum/index.php?topic=10470.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Trigonometry")]
	[Tooltip("Convert Degrees to Radians. This is using a constant: (PI * 2) / 360.")]
	public class ConvertDegreesToRadians : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The angle in degrees")]
		public FsmFloat angleInDegrees;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The angle expressed in radians")]
		public FsmFloat angleInRadians;
				
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
			angleInRadians.Value = angleInDegrees.Value*Mathf.Deg2Rad;
		}
	}
}
