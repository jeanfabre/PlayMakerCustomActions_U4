// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Get all children of a gameobject and save them in an Array.")]
	public class GetGameObjectChildrenInArray : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The gameObject Variable to get its children from.")]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		[UIHint(UIHint.Layer)]
		[Tooltip("Only consider objects from these layers.")]
		public FsmInt[] layerMask;
		
		[Tooltip("Invert the mask, so you pick from all layers except those defined above.")]
		public FsmBool invertMask;

		[UIHint(UIHint.Tag)]
		[Tooltip("Filter by Tag")]
		public FsmString withTag;

		public override void Reset ()
		{
			gameObject = null;
			array = null;
			withTag = "Untagged";
			layerMask = null;
			invertMask = null;

		}
		
		// Code that runs on entering the state.
		public override void OnEnter ()
		{
			DoGetChilds ();
			Finish ();
		}
		
		private void DoGetChilds ()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			int _layerMask = ActionHelpers.LayerArrayToLayerMask(layerMask, invertMask.Value);


			List<GameObject> _list = new List<GameObject>();

			foreach (Transform child in go.transform)
			{
				bool _valid = true;
				// tag filtering
				if (! string.IsNullOrEmpty(withTag.Value) || withTag.Value != "UnTagged")
				{
					_valid = child.tag == withTag.Value;
					
				}

				// layer filtering
				if (child.gameObject.layer != _layerMask)
				{
				//	continue;
				}

				if (_valid) _list.Add(child.gameObject);
			}

			array.Values = _list.ToArray();
		}
		
	}
}
