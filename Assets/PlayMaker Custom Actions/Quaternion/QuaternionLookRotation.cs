// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/Quaternion/Editor/QuaternionLookRotationCustomEditor.cs",
						"Assets/PlayMaker Custom Actions/Quaternion/Editor/_internal/QuaternionCustomEditorBase.cs",
						"Assets/PlayMaker Custom Actions/Quaternion/_internal/QuaternionBaseAction.cs",
					]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Quaternion")]
	[Tooltip("Creates a rotation that looks along forward with the the head upwards along upwards.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1143")]
	public class QuaternionLookRotation : QuaternionBaseAction
	{
		[RequiredField]
		[Tooltip("the rotation direction")]
		public FsmVector3 direction;
		
		[Tooltip("The up direction")]
		public FsmVector3 upVector;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the inverse of the rotation variable.")]
		public FsmQuaternion result;

		public override void Reset()
		{
			direction = null;
			upVector = new FsmVector3(){UseVariable=true};
			result = null;
			everyFrame = true;
			everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		public override void OnEnter()
		{
			DoQuatLookRotation();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatLookRotation();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatLookRotation();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatLookRotation();
			}
		}

		void DoQuatLookRotation()
		{
			if (upVector.IsNone)
			{
				result.Value = Quaternion.LookRotation(direction.Value,upVector.Value);
			}else{
				result.Value = Quaternion.LookRotation(direction.Value);
			}
		}
	}
}

