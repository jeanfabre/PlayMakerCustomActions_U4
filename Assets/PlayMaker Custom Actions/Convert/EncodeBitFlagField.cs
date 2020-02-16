// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// original action http://hutonggames.com/playmakerforum/index.php?topic=10007.msg47426#msg47426

using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions 
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Encode a collection of boolean values into a bit-flag field which is represented as an integer value in decimal format.")]
	public class EncodeBitFlagField : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Number of Booleans to encode.")]
		public FsmBool[] boolVariables;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Variable output as a decimal value.")]
		public FsmInt intOutput;

		public override void Reset()
		{
			boolVariables = null;
			intOutput = null;
		}

		public override void OnEnter()
		{
			DoEncodeBitFlag(); //Call the conversion function.
		}

		void DoEncodeBitFlag() //The function.
		{
			if (boolVariables.Length == 0) return; //Test to see if the number of booleans to check is 0.
			string bitFlagString = ""; //Declare a cleared string variable that will be used in this function to build the bit-flag array.

			for (var i = 0; i < boolVariables.Length; i++) //Iterate through the array.
			{
				string bitVar; //Declare an empty string variable for the purposes of building the string.
				bitVar = boolVariables[i].Value ? "1" : "0"; //Conditional as to what to assign this value. 1=true, 0=false.
				bitFlagString = bitVar + bitFlagString; //Build the string.
			}

			//NOTE: I will need to learn how to grok the rest of this.
			var dec = 0; //Create an empty decimal value to work with.
			for (int i = 0 ; i < bitFlagString.Length; i++ ) //For loop that works backwards. 
			{
				if (bitFlagString[bitFlagString.Length-i-1] == '0' ) continue; //Work from the right to the left.
				dec += (int)Math.Pow( 2, i ); //Conversion.
			}
			intOutput.Value = dec; //Set the output variable's value to the converted dec value.
		}
	}
}