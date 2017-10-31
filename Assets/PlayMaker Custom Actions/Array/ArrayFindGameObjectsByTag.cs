// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Find all active GameObjects with a specific tag and store them in an array. Tags must be declared in the tag manager before using them")]
	public class ArrayFindGameObjectsByTag : FsmStateAction
	{
	
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array Variable to use.")]
		public FsmArray array;

		[Tooltip("the tag")]
		[UIHint(UIHint.Tag)]
		public FsmString tag;

		
		
		public override void Reset()
		{
			array = null;
			tag = new FsmString(){UseVariable=true};
			
		}

		
		public override void OnEnter()
		{

			FindGOByTag();
			
			Finish();
		}

		
		public void FindGOByTag()
		{
			array.Values = GameObject.FindGameObjectsWithTag (tag.Value);
		}
	}
}