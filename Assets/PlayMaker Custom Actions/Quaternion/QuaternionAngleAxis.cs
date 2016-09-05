// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/Quaternion/Editor/QuaternionAngleAxisCustomEditor.cs",
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
	[Tooltip("Creates a rotation which rotates angle degrees around axis.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1095")]
	public class QuaternionAngleAxis : QuaternionBaseAction
	{
		[RequiredField]
		[Tooltip("The angle.")]
		public FsmFloat angle;
		
		[RequiredField]
		[Tooltip("The rotation axis.")]
		public FsmVector3 axis;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the rotation of this quaternion variable.")]
		public FsmQuaternion result;

		public override void Reset()
		{
			angle = null;
			axis = null;
			result = null;
			everyFrame = true;
			everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		public override void OnEnter()
		{
			DoQuatAngleAxis();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatAngleAxis();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatAngleAxis();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatAngleAxis();
			}
		}

		void DoQuatAngleAxis()
		{
			result.Value = Quaternion.AngleAxis(angle.Value,axis.Value);
		}
	}
}

