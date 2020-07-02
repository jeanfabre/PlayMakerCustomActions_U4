// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//
// Keywords: All Recursive Childs

using UnityEngine;
using System.Linq;

namespace HutongGames.PlayMaker.Actions
{

	
	#pragma warning disable 168

	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("gets all children recursivly")]
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
			_result = _result.Where((source, index) => index != 0).ToArray();

			store.Resize(_result.Length);
			int i = 0;
			foreach (Transform transform in _result)
			{
				store.Set(i,transform.gameObject);
				i++;
			}

		}

		
	}
}