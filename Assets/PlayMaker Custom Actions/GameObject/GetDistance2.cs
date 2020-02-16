// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Measures the Distance betweens 2 Game Objects or vectors and stores the result in a Float Variable. Vector3 are offseted to GameObjects if declared")]
	public class GetDistance2 : FsmStateAction
	{
		
		public FsmOwnerDefault gameObject;
		
		public FsmVector3 orVector3;
		
		public FsmGameObject target;
		
		public FsmVector3 orVector3Target;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeResult;
		public bool everyFrame;
		
		public override void Reset()
		{
			gameObject = null;
			orVector3  = null;
			target = null;
			orVector3Target = null;
			
			storeResult = null;
			everyFrame = true;
		}
		
		public override void OnEnter()
		{
			DoGetDistance();
			
			if (!everyFrame)
				Finish();
		}
		public override void OnUpdate()
		{
			DoGetDistance();
		}		
		
		void DoGetDistance()
		{
			GameObject go = gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value;
			
			if (storeResult == null)
				return;
				
			Vector3 start = Vector3.zero;
			if (go!=null)
			{
				start = go.transform.position;
			}
			start += orVector3.Value;
			
			Vector3 end = Vector3.zero;
			if (target.Value!=null)
			{
			    end = target.Value.transform.position;
			}
			end += orVector3Target.Value;
			
			storeResult.Value = Vector3.Distance(start, end);
		}

	}
}
