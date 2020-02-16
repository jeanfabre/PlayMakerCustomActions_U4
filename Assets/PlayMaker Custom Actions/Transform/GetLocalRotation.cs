// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Gets the local Rotation of a Game Object and stores it in a Vector3 Variable or each Axis in a Float Variable")]
	public class GetLocalRotation : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		[UIHint(UIHint.Variable)]
		public FsmQuaternion quaternion;
		[UIHint(UIHint.Variable)]
		[Title("Euler Angles")]
		public FsmVector3 vector;
		[UIHint(UIHint.Variable)]
		public FsmFloat xAngle;
		[UIHint(UIHint.Variable)]
		public FsmFloat yAngle;
		[UIHint(UIHint.Variable)]
		public FsmFloat zAngle;

		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			quaternion = null;
			vector = null;
			xAngle = null;
			yAngle = null;
			zAngle = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoGetLocalRotation();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnUpdate()
		{
			DoGetLocalRotation();
		}
		
		void DoGetLocalRotation()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			quaternion.Value = go.transform.localRotation;
			
			var rotation = go.transform.eulerAngles;
			
			vector.Value = rotation;
			xAngle.Value = rotation.x;
			yAngle.Value = rotation.y;
			zAngle.Value = rotation.z;
		}
		
		
	}
}