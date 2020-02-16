// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/FsmStateActionAdvanced.cs"
					]
}
EcoMetaEnd
---*/
// original action:  http://hutonggames.com/playmakerforum/index.php?topic=925.msg41337#msg41337


using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Get Random Vector3 inside a sphere")]
	public class RandomVector3InsideSphere : FsmStateActionAdvanced
	{

		[Tooltip("Leave to none for position as 0,0,0.")]
		public FsmVector3 sphereCenter;

		[Tooltip("Leave to none for a radius of 1")]
		public FsmFloat sphereRadius;
		
		[UIHint(UIHint.Variable)]
		[RequiredField]
		public FsmVector3 storeResult;


		Vector3 position;

		public override void Reset()
		{
			base.Reset();
			sphereCenter = new FsmVector3 { UseVariable = true };
			sphereRadius = new FsmFloat { UseVariable = true };
			storeResult = null;

		}
		
		public override void OnEnter()
		{
			DoGetRandomPointInsideSphere();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnActionUpdate()
		{
			DoGetRandomPointInsideSphere();
		}
		
		void DoGetRandomPointInsideSphere()
		{
			position = Random.insideUnitSphere;

			if (!sphereRadius.IsNone)
			{
				position *= sphereRadius.Value;
			}

			if (!sphereCenter.IsNone)
			{
				position += sphereCenter.Value;
			}

			storeResult.Value = position;
		}
		
		
	}
}