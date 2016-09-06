// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/Quaternion/Editor/GetQuaternionFromRotationCustomEditor.cs",
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
	[Tooltip("Creates a rotation which rotates from fromDirection to toDirection. Usually you use this to rotate a transform so that one of its axes eg. the y-axis - follows a target direction toDirection in world space.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W968")]
	public class GetQuaternionFromRotation : QuaternionBaseAction
	{

		[RequiredField]
		[Tooltip("the 'from' direction")]
		public FsmVector3 fromDirection;
		
		[RequiredField]
		[Tooltip("the 'to' direction")]
		public FsmVector3 toDirection;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("the resulting quaternion")]
		public FsmQuaternion result;

		public override void Reset()
		{
			fromDirection = null;
			toDirection = null;
	
			result = null;
			everyFrame = false;
			everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		
		}

		public override void OnEnter()
		{
			DoQuatFromRotation();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatFromRotation();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatFromRotation();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatFromRotation();
			}
		}
		
		void DoQuatFromRotation()
		{
			result.Value = Quaternion.FromToRotation(fromDirection.Value,toDirection.Value);		
		}
	}
}
