// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// Keywords: All Recursive Childs

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	
	#pragma warning disable 168

	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Finds GameObjects by Tag.")]
	public class GetAllRecursiveChildren : FsmStateAction
	{

		[RequiredField]
		[Tooltip("The GameObject to get recursive children from.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("if true, will list inactive gameobjects")]
		public FsmBool includeInactive;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a GameObject variable.")]
		[VariableType(VariableType.GameObject)]
		public FsmArray store;
		

		GameObject _go;
		Transform[] _result;

		public override void Reset()
		{
			gameObject = null;
			includeInactive = false;
			store = null;
		}
		
		public override void OnEnter()
		{
			Find();
			Finish();
		}
		
		void Find()
		{
		
			_go  =  Fsm.GetOwnerDefaultTarget(gameObject);

			if (_go==null)
			{
				return;
			}

			_result = _go.GetComponentsInChildren<Transform>(includeInactive.Value);

			store.RawValue = _result;
			
		}

		
	}
}