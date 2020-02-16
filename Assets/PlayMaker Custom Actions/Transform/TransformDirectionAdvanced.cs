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


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Transforms a Direction from a Game Object's local space to world space. This operation is not affected by scale or position of the transform. The returned vector has the same length as direction.")]
	public class TransformDirectionAdvanced : FsmStateActionAdvanced
	{
		[RequiredField]
		[Tooltip("The GameObject")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The local direction")]
		public FsmVector3 localDirection;


		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The local direction from gameObject expressed in world space")]
		public FsmVector3 storeResult;

		
		public override void Reset()
		{
			base.Reset ();
			gameObject = null;
			localDirection = null;
			storeResult = null;

		}

		public override void OnEnter()
		{
			OnActionUpdate ();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

	
		public override void OnActionUpdate()
		{
			DoTransformDirection();
		}
		
		void DoTransformDirection()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if(go == null) return;
			
			storeResult.Value = go.transform.TransformDirection(localDirection.Value);
		}
	}
}

