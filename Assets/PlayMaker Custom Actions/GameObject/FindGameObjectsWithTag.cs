// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	
	#pragma warning disable 168

	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Finds GameObjects by Tag.")]
	public class FindGameObjectsWithTag : FsmStateAction
	{

		[UIHint(UIHint.Tag)]
		[Tooltip("Find a GameObject with this tag. ")]
		public FsmString withTag;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a GameObject variable.")]
		[VariableType(VariableType.GameObject)]
		public FsmArray store;
		
		[Tooltip("Event fired if tag is not found, typically possible if you are using a FsmString to define the tag")]
		public FsmEvent TagDoesntExistEvent;
		

		GameObject[] _result;

		public override void Reset()
		{

			withTag = "Untagged";
			store = null;
			TagDoesntExistEvent = null;
		}
		
		public override void OnEnter()
		{
			Find();
			Finish();
		}
		
		void Find()
		{
		
			try{
				_result = GameObject.FindGameObjectsWithTag(withTag.Value);
			
				store.RawValue = _result;

			}catch(UnityException e)
			{
				Fsm.Event(TagDoesntExistEvent);
			}
	
			
		}

		
	}
}