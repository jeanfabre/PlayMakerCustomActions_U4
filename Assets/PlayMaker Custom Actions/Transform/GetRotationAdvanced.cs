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
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Gets the Rotation of a Game Object and stores it in a Vector3 Variable or each Axis in a Float Variable. Advanced option let you define when to execute the action, on update, fixedupdate or lateupdate")]
	public class GetRotationAdvanced : FsmStateActionAdvanced
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		public FsmQuaternion quaternion;

        [UIHint(UIHint.Variable)]
        public FsmFloat qXAngle;

        [UIHint(UIHint.Variable)]
        public FsmFloat qYAngle;

        [UIHint(UIHint.Variable)]
        public FsmFloat qZAngle;

        [UIHint(UIHint.Variable)]
		[Title("Euler Angles")]
		public FsmVector3 vector;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat xAngle;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat yAngle;
		
		[UIHint(UIHint.Variable)]
		public FsmFloat zAngle;
		
		public Space space;

		public override void Reset()
		{
			base.Reset();

			gameObject = null;
			quaternion = null;
            qXAngle = null;
            qYAngle = null;
            qZAngle = null;
            vector = null;
			xAngle = null;
			yAngle = null;
			zAngle = null;
			space = Space.World;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoGetRotation();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoGetRotation();
		}


		void DoGetRotation()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}

			if (space == Space.World)
			{
				quaternion.Value = go.transform.rotation;
                qXAngle.Value = go.transform.rotation.x;
                qYAngle.Value = go.transform.rotation.y;
                qZAngle.Value = go.transform.rotation.z;

                var rotation = go.transform.eulerAngles;
				
				vector.Value = rotation;
				xAngle.Value = rotation.x;
				yAngle.Value = rotation.y;
				zAngle.Value = rotation.z;
			}
			else
			{
				var rotation = go.transform.localEulerAngles;

				quaternion.Value = Quaternion.Euler(rotation);
                qXAngle.Value = Quaternion.Euler(rotation).x;
                qYAngle.Value = Quaternion.Euler(rotation).y;
                qZAngle.Value = Quaternion.Euler(rotation).z;

                vector.Value = rotation;
				xAngle.Value = rotation.x;
				yAngle.Value = rotation.y;
				zAngle.Value = rotation.z;
			}
		}
		
	}
}
