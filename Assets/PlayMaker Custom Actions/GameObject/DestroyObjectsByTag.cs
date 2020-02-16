// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Destroys all Game Object with a given tag.")]
	public class DestroyObjectsByTag : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Tag)]
		public FsmString tag;
		
		[HasFloatSlider(0, 5)]
		[Tooltip("Optional delay before destroying the Game Object.")]
		public FsmFloat delay;
		
		[Tooltip("if delay not 0, accumulate the dealy for object to be destroy, destroy them one after the other basically")]
		public FsmBool AccumulateDelay;
			
		[Tooltip("Detach children before destroying the Game Object.")]
		public FsmBool detachChildren;

		public override void Reset()
		{
			tag = null;
			delay = 0;
			AccumulateDelay = false;
			detachChildren = false;
	
		}
		
		private float _delay;

		public override void OnEnter()
		{
			
			GameObject[] gos = GameObject.FindGameObjectsWithTag(tag.Value);
			
	        foreach (GameObject go in gos) {
				if (detachChildren.Value)
					go.transform.DetachChildren();
				
				if (delay.Value <= 0 )
				{
					Object.Destroy(go);
				
				}else{
					if (AccumulateDelay.Value)
					{
						_delay += delay.Value;
					}else{
						_delay = delay.Value;
					}
					Object.Destroy(go, _delay);
				}
				
	        }

			Finish();
			
		}

	}
}
