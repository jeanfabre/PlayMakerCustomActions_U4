// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Store all (all levels) childs of GameObject (active and/or inactive) from a parent.")]
	public class ArrayGetAllChildOfGameObject : FsmStateAction
	{
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		[Tooltip("The parent gameObject")]
		[RequiredField]
		public FsmOwnerDefault parent;
		
		[ActionSection("Option")]
		public FsmBool includeInactive;

		public FsmBool includeParent;
		
		private GameObject go;
		private Transform[] childs;

		List<GameObject> _list;

		public override void Reset()
		{
			array = null;
			parent = null;
			includeInactive = true;
			includeParent = false;
		}
		
		
		public override void OnEnter()
		{
			getAllChilds(Fsm.GetOwnerDefaultTarget(parent));
			
			Finish();
		}
		
		
		public void getAllChilds(GameObject parent)
		{

			childs = parent.GetComponentsInChildren<Transform>(includeInactive.Value);

			_list = new List<GameObject> ();

			foreach(Transform trans in childs) {
				if ( !includeParent.Value && trans.gameObject == parent)
				{
					continue;
				}
				_list.Add(trans.gameObject);
			}

			array.Values = _list.ToArray ();

		}
	}
}
