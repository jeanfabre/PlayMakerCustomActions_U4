// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/Quaternion/Editor/QuaternionEulerCustomEditor.cs",
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
	[Tooltip("Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order).")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1094")]
	public class QuaternionEuler : QuaternionBaseAction
	{
		[RequiredField]
		[Tooltip("The Euler angles.")]
		public FsmVector3 eulerAngles;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the euler angles of this quaternion variable.")]
		public FsmQuaternion result;


		public override void Reset()
		{
			eulerAngles = null;
			result = null;
			everyFrame = true;
			everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		public override void OnEnter()
		{
			DoQuatEuler();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatEuler();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatEuler();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatEuler();
			}
		}

		void DoQuatEuler()
		{
			result.Value = Quaternion.Euler(eulerAngles.Value);
		}
	}
}

