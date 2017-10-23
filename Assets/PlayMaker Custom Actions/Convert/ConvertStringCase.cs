// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// original action: http://hutonggames.com/playmakerforum/index.php?topic=4839.0

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Converts a string to upper or lower case.")]
	public class ConvertStringCase : FsmStateAction
	{
		public enum Case
		{
			Lower,
			Upper
		}
		[RequiredField]
		public FsmString String;
		
        [Tooltip("Select upper or lower case.")]
		[ObjectType(typeof(Case))]
		public FsmEnum operation;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmString result;
		
		public bool everyFrame;

		public override void Reset()
		{
			String = null;
			result = null;
			operation = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetStringValue();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoSetStringValue();
		}
		
		void DoSetStringValue()
		{
			Case _case = (Case)operation.Value;
			switch (_case)
			{
				case Case.Lower:
					result.Value = String.Value.ToLower();
					break;

				case Case.Upper:
					result.Value = String.Value.ToUpper();;
					break;
			}

		}
		
	}
}