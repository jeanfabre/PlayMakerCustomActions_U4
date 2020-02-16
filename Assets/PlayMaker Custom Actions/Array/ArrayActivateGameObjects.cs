// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Active/Deactivate all GameObjects within an array.")]
	public class ArrayActivateGameObjects : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;
		
		[RequiredField]
		[Tooltip("Check to activate, uncheck to deactivate Game Objects.")]
		public FsmBool activate;
		
		[Tooltip("Resets the affected game objects when exiting this state to their original activate state. Useful if you want an object to be controlled only while this state is active.")]
		public FsmBool resetOnExit;


		bool[] _activeStates;

		
		public override void Reset()
		{
			array = null;
			activate = null;
			resetOnExit = false;
		}
		
		
		public override void OnEnter()
		{
			DoAction();

			Finish();

		}

		
		void DoAction()
		{
			_activeStates = new bool[array.Length];

			int i= 0;
			foreach(GameObject _go in array.objectReferences)
			{	
				if (_go==null)
				{
					continue;
				}

				#if UNITY_3_5 || UNITY_3_4
				_activeStates[i] = _go.active;
				_go.active = activate.Value;
				#else			
				_activeStates[i] = _go.activeSelf;
				_go.SetActive(activate.Value);
				#endif
				
				i++;
			}
		}

		public override void OnExit()
		{
			if( resetOnExit.Value && _activeStates!=null)
			{
				int i= 0;
				foreach(GameObject _go in array.objectReferences)
				{	
					if (_go==null)
					{
						continue;
					}
					
					#if UNITY_3_5 || UNITY_3_4
					_go.active = _activeStates[i];
					#else			
					_go.SetActive(_activeStates[i]);
					#endif
					
					i++;
				}
			}
		}
		
	}
}