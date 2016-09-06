// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/Quaternion/Editor/QuaternionInverseCustomEditor.cs",
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
	[Tooltip("Inverse a quaternion")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1130")]
	public class QuaternionInverse : QuaternionBaseAction
	{
		[RequiredField]
		[Tooltip("the rotation")]
		public FsmQuaternion rotation;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the inverse of the rotation variable.")]
		public FsmQuaternion result;

		public override void Reset()
		{
			rotation = null;
			result = null;
			everyFrame = true;
			everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		public override void OnEnter()
		{
			DoQuatInverse();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatInverse();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatInverse();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatInverse();
			}
		}

		void DoQuatInverse()
		{
			result.Value = Quaternion.Inverse(rotation.Value);
		}
	}
}

