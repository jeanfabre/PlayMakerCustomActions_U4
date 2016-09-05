// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Flips the value of a Bool Variable and save it into another bool")]
	public class GetBoolInvert : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Bool variable.")]
		public FsmBool boolVariable;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
        [Tooltip("Store the invertion of boolVariable.")]
		public FsmBool invertedBoolVariable;
		

		public override void Reset()
		{
			boolVariable = null;
			invertedBoolVariable = null;
		}

		public override void OnEnter()
		{
			invertedBoolVariable.Value = !boolVariable.Value;
			Finish();		
		}
	}
}
