// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Remove an item from an array")]
	public class arrayRemove : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		[RequiredField]
		[MatchElementType("array")]
		[Tooltip("Item to remove.")]
		public FsmVar value;


		public override void Reset ()
		{
			array = null;
			value = null;
		}

		public override void OnEnter ()
		{
			DoRemoveValue ();
			Finish ();
		}

		private void DoRemoveValue ()
		{

			value.UpdateValue ();

			List<object> _new = new List<object>();

			int i = 0;
			foreach(object _obj in array.Values)
			{
				if (!_obj.Equals(value.GetValue()))
				{
					_new.Add(_obj);
				}

				i++;
			}


			array.Values = _new.ToArray();
			array.SaveChanges();

		}

	}

}

