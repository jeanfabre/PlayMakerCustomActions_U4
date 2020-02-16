// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Destroys a Game Object's transfom root.")]
	public class DestroyRoot : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to destroy its root.")]
		public FsmOwnerDefault gameObject;
		
		[HasFloatSlider(0, 5)]
		[Tooltip("Optional delay before destroying the Game Object.")]
		public FsmFloat delay;

		
		public override void Reset()
		{
			gameObject = null;
			delay = 0;
		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);

			if (go != null)
			{
				if (delay.Value <= 0)
				{
					GameObject.Destroy(go.transform.root.gameObject);
				}
				else
				{
					GameObject.Destroy(go.transform.root.gameObject, delay.Value);
				}
				
			
			}
			
			Finish();
		
		}

		
	}
}