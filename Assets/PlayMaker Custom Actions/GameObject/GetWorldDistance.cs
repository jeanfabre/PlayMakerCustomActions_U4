// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Measures the Distance betweens 2 Game Objects and stores the result in a Float Variable. \nOptionaly ignore axis to obtain planar distance axis distance. \nExample: ignore the y axis only to obtain the distance on the YZ plane. \nExample: ignore Y and Z to obtain the distance on the x axis.")]
	public class GetWorldDistance : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Game Object to work with")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The target Game Object to work with")]
		public FsmGameObject target;
		
		[RequiredField]
		[Tooltip("Store the distance between the gameObject and the target")]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeResult;
		
		[Tooltip("The world x axis is ignored")]
		public bool ignoreX;
		
		[Tooltip("The world y axis is ignored")]
		public bool ignoreY;
		
		[Tooltip("The world z axis is ignored")]
		public bool ignoreZ;
		
		
		public bool everyFrame;
		
		
		public override void Reset()
		{
			gameObject = null;
			target = null;
			storeResult = null;
			
			ignoreX = false;
			ignoreY = false;
			ignoreZ = false;
			
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
			
			if (go == null || target.Value == null || storeResult == null)
				return;
			
			
			
			if (!ignoreX && !ignoreY && ! ignoreZ)
			{
				storeResult.Value = Vector3.Distance(go.transform.position, target.Value.transform.position);
			}else{
				
				Vector3 vector = target.Value.transform.position - go.transform.position;
				if (ignoreX)
				{
					vector.x = 0;
				}
				if (ignoreY)
				{
					vector.y = 0;
				}
				if(ignoreZ)
				{
					vector.z = 0;
				}
				
				storeResult.Value = vector.magnitude;
			}
			
		}

	}
}