// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10049.0
// This action was created in 2015 by Wesley M. Brown of BadSeedGames for use with the PlayMaker system.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions 
{
	[ActionCategory("Trigonometry")]
	[Tooltip("Gets the Delta-angle between to rotations.")]
	public class GetDeltaAngle : FsmStateAction
	{
		public enum GetSignedAngleToTargetDirection {x,y,z};

		[RequiredField]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		public FsmGameObject targetObject;
		
		public GetSignedAngleToTargetDirection direction;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeAngle;
		
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			targetObject = null;
			direction = GetSignedAngleToTargetDirection.x;
			storeAngle = null;
			everyFrame = false;
		}

		public override void OnLateUpdate()
		{
			DoGetAngleToTarget(); //Perform the action.
			if (!everyFrame) //Finish the action if not every frame.
			{
				Finish();
			}
		}

		void DoGetAngleToTarget()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject); //Gets the game object to work with on the first spot.
			if (go == null) {return;} //Breaks this if there is no object detected.

			var gr = targetObject.Value; //Gets the game object to work with on the first spot.
			if (gr == null) {return;} //Breaks this if there is no object detected.

			switch(direction) //Choose which axis to work on.
			{
			case GetSignedAngleToTargetDirection.x:
				//Gets the delta angle between the two values.
				storeAngle.Value = Mathf.DeltaAngle(gr.transform.localEulerAngles.x, go.transform.localEulerAngles.x);
				break;
			case GetSignedAngleToTargetDirection.y:
				//Gets the delta angle between the two values.
				storeAngle.Value = Mathf.DeltaAngle(gr.transform.localEulerAngles.y, go.transform.localEulerAngles.y);
				break;
			case GetSignedAngleToTargetDirection.z:
				//Gets the delta angle between the two values.
				storeAngle.Value = Mathf.DeltaAngle(gr.transform.localEulerAngles.z, go.transform.localEulerAngles.z);
				break;
			}
		}
	}
}