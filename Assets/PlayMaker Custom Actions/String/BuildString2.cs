// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Builds a String from other Strings. Separator is optional and can be inserted before, after or in between parts.")]
	public class BuildString2 : FsmStateAction
	{
		public enum separatorInsertion {InBetween,After,Before};

		[RequiredField]
        [Tooltip("Array of Strings to combine.")]
		public FsmString[] stringParts;

        [Tooltip("Separator. E.g. space character.")]
        public FsmString separator;

		[Tooltip("Separator behavior.")]
		public separatorInsertion separatorInsert;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the final String in a variable.")]
        public FsmString storeResult;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;
	    
        private string result;

		public override void Reset()
		{
			stringParts = new FsmString[3];
			separator = null;
			storeResult = null;

			separatorInsert = separatorInsertion.After;

			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoBuildString();
			
			if (!everyFrame)
			{
			    Finish();
			}
		}

		public override void OnUpdate()
		{
			DoBuildString();
		}
		
		void DoBuildString()
		{
			if (storeResult == null) return;
			
			result = "";

		    for (var i = 0; i < stringParts.Length; i++)
		    {
				if (separatorInsert == separatorInsertion.Before)
				{
					result += separator.Value;
				}
		        result += stringParts[i];
		       
				if (separatorInsert == separatorInsertion.After)
				{
					result += separator.Value;
				}

				if (separatorInsert == separatorInsertion.InBetween && i<(stringParts.Length-1))
				{
					result += separator.Value;
				}

		    }
		    
		    storeResult.Value = result;
		}
		
	}
}
