// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/Quaternion/Editor/GetQuaternionMultipliedByVectorCustomEditor.cs",
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
	[Tooltip("Get the vector3 from a quaternion multiplied by a vector.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W970")]
	public class GetQuaternionMultipliedByVector : QuaternionBaseAction
	{

		[RequiredField]
		[Tooltip("The quaternion to multiply")]
		public FsmQuaternion quaternion;
		
		[RequiredField]
		[Tooltip("The vector3 to multiply")]
		public FsmVector3 vector3;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting vector3")]
		public FsmVector3 result;

		public override void Reset()
		{
			quaternion = null;
			vector3 = null;
	
			result = null;
			everyFrame = false;
			everyFrameOption = QuaternionBaseAction.everyFrameOptions.Update;
		}

		public override void OnEnter()
		{
			DoQuatMult();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				DoQuatMult();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				DoQuatMult();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				DoQuatMult();
			}
		}
		
		void DoQuatMult()
		{
			result.Value = quaternion.Value * vector3.Value;		
		}
	}
}
