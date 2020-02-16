// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Sets a Quaternion Variable or vector3 variable to a random rotation.")]
	public class RandomRotation : FsmStateAction
	{

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the random rotation")]
		public FsmQuaternion storeResult;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the random rotation as euler angles")]
		public FsmVector3 storeEulerResult;

		[Tooltip("Seems to be related to the randomization algorythm, could provide better results when checked to avoid similar rotations over time.")]
		public bool uniform;

		Quaternion _quat;

		public override void Reset()
		{

			storeResult = null;
			storeEulerResult = null;
			uniform = false;
		}
		
		public override void OnEnter()
		{

			if (uniform)
			{
				_quat = Random.rotation;
			}else{
				_quat = Random.rotationUniform;
			}

			if (!storeResult.IsNone) storeResult.Value = _quat;
			if (!storeEulerResult.IsNone) storeEulerResult.Value = _quat.eulerAngles;

			Finish();
		}
	}
}