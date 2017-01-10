// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// original action http://hutonggames.com/playmakerforum/index.php?topic=10007.msg47426#msg47426


using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions 
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Decode an integer (bit-flag) value into a collection of booleans")]
	public class DecodeBitFlagField : FsmStateAction
	{
		[Tooltip("Integer value to decode into booleans. Use Decimal values.")]
		public FsmInt inputInt;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Output location of the boolean variables after decoding.")]
		public FsmBool[] boolVariables;

		public override void Reset()
		{
			boolVariables = null;
			inputInt = null;
		}
		
		public override void OnEnter()
		{
			DoDecodeBitFlag(); //Call the de-conversion function.
		}
		
		void DoDecodeBitFlag() //The de-conversion function.
		{
			if (boolVariables.Length == 0) return; //Test to see if the number of booleans to check is 0.

			//Flush the variables for a clean slate
			//NOTE:
			//  Yes I know this is rough and somewhat sloppy but it's the best workaround that I know of at this point in time.
			for (int i = 0; i < boolVariables.Length; i++) boolVariables[i].Value = false;

			var binString = Convert.ToString(inputInt.Value, 2); //This will convert the decimal value into a binary string representation

			for (int i = (binString.Length - 1); i >= 0; i--) //Reverse iterative loop
			{
				var ar = binString.Length - (i+1); //Get an inverted value since the two arrays are built inverse to one another.
				boolVariables[ar].Value = binString[i] == '1' ? true : false; //Basic test for each condition.
			}
		}
	}
}