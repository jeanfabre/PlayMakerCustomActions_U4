// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/Quaternion/Editor/GetQuaternionEulerAnglesCustomEditor.cs",
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
	[Tooltip("Gets a quaternion as euler angles.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1132")]
	public class GetQuaternionEulerAngles : QuaternionBaseAction
	{
		[RequiredField]
		[Tooltip("The rotation")]
		public FsmQuaternion quaternion;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The euler angles of the quaternion.")]
		public FsmVector3 eulerAngles;

		public override void Reset()
		{
			quaternion = null;
			eulerAngles = null;
			everyFrame = true;
			everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		public override void OnEnter()
		{
			GetQuatEuler();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				GetQuatEuler();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				GetQuatEuler();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				GetQuatEuler();
			}
		}

		void GetQuatEuler()
		{
			eulerAngles.Value = quaternion.Value.eulerAngles;
		}
	}
}

