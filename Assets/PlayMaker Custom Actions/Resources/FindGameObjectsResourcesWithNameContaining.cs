// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// original action: http://hutonggames.com/playmakerforum/index.php?topic=14546.0
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Linq;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Finds any GameObject(s) with a Name that contains a particular set of String and stores the count of them in an FSM Int.")]
	public class FindGameObjectsResourcesWithNameContaining : FsmStateAction
	{
		[Tooltip("Find any GameObject(s) with a Name containing this string and Count the number of them.")]
		public FsmString[] withNameContaining;

		[Tooltip("If this bool is set to True then the String search is case insensitive.")]
		[VariableType(VariableType.Bool)]
		public FsmBool caseInsensitive;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in an FSM Int of the Count of the found GameObject(s).")]
		public FsmInt storeCount;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a GameObject variable.")]
		[VariableType(VariableType.GameObject)]
		public FsmArray storeResults;

		public override void Reset ()
		{
			withNameContaining = new FsmString[1];
			storeCount = null;
			storeResults = null;
		}
		
		public override void OnEnter ()
		{
			Find();

			Finish();
		}

		string name;

		void Find ()
		{

			StringComparison _compare = caseInsensitive.Value?StringComparison.InvariantCultureIgnoreCase:StringComparison.InvariantCulture;
	

			List<GameObject> _list = new List<GameObject>();

			foreach(FsmString _containString in withNameContaining)
			{

				if (!string.IsNullOrEmpty(_containString.Value))
				{
					_list.AddRange(
							Resources.FindObjectsOfTypeAll(typeof(GameObject)).Cast<GameObject>().
								Where(
									g => g.name.IndexOf(_containString.Value, _compare) >= 0                                            
								).ToList()
							);
				}

			}
			storeCount.Value = _list.Count;
			storeResults.RawValue = _list.ToArray();

		}
		
		public override string ErrorCheck ()
		{

			if (withNameContaining.Length == 0)
			{
				return "Please Specify at least one String to be found within GameObject(s') Name(s).";
			}

			if (storeCount.IsNone && storeResults.IsNone)
			{
				return "Please use either storeCount or storeResults else this action is not necessary";
			}


			return string.Empty;
		}
	}
}