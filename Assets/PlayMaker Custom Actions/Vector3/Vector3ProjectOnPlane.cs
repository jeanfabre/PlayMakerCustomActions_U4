// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// --- __ECO__ __PLAYMAKER__ __ACTION__ 
// https://hutonggames.com/playmakerforum/index.php?topic=19299.msg84027


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Vector3)]
	[Tooltip("Performs projection of a vector on a plane using its normal.")]
	public class Vector3ProjectOnPlane : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Vector to project")]
		public FsmVector3 vector;

		[RequiredField]
		[Tooltip("the Plane normal")]
		public FsmVector3 planeNormal;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("the resulting projection")]
		public FsmVector3 storeVector3Result;

		[Tooltip("Runs every frame")]
		public bool everyFrame;

		public override void Reset()
		{
			vector = null;
			planeNormal = null;
			storeVector3Result = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoProjectOnPlane();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoProjectOnPlane();
		}

		void DoProjectOnPlane()
		{
            storeVector3Result.Value = Vector3.ProjectOnPlane(vector.Value, planeNormal.Value);
		}
	}
}