// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
#if UNITY_3_5 || UNITY_3_4
	[Tooltip("Check if a gameObject is active.")]
#else				
	[Tooltip("Check if a gameObject is active.This lets you know if a gameObject is active in the game. That is the case if its GameObject.activeSelf property is enabled, as well as that of all it's parents.")]

#endif
	public class IsActive : FsmStateAction
	{
		[RequiredField]
        [Tooltip("The GameObject to check activate state.")]
        public FsmGameObject gameObject;
		
		[UIHint(UIHint.Variable)]
#if UNITY_3_5 || UNITY_3_4
	[Tooltip("The active state of this gameObject.")]
#else				
	[Tooltip("The active state of this gameObject. It uses activeInHierarchy, not activeSelf. So it will return true if this gameobject is active in the game.")]
#endif
        public FsmBool isActive;
		
		public FsmEvent isActiveEvent;
		
		public FsmEvent isNotActiveEvent;
		
        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
		public bool everyFrame;
		
		


		public override void Reset()
		{
			gameObject = null;
			isActive = false;
			isActiveEvent = null;
			isNotActiveEvent = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoIsActiveGameObject();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoIsActiveGameObject();
		}

		void DoIsActiveGameObject()
		{
			var go = gameObject.Value;
			
			if (go == null)
			{
				return;
			}
			
			bool _active = false;
			
#if UNITY_3_5 || UNITY_3_4
			_active = go.active;
             
#else				
               _active = go.activeInHierarchy;
#endif
			
			 isActive.Value = _active;
			
			if (_active)
			{
				Fsm.Event(isActiveEvent);
			}else{
				Fsm.Event(isNotActiveEvent);
			}
        }


    }
}
