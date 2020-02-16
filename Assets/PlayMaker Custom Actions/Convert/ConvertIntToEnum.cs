// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Convert an int value to an Enum Variable")]
	public class ConvertIntToEnum: FsmStateAction
	{
		[Tooltip("The int value to convert to Enum")]
		public FsmInt value;

		[UIHint(UIHint.Variable)]
		[Tooltip("The Enum Variable to set.")]
		public FsmEnum enumVariable;

		[Tooltip("Event Fired if conversion failed, that is the int is not found in the enum variable")]
		public FsmEvent errorEvent;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		int _valueCache;

		public override void Reset()
		{
			enumVariable = null;
			value = null;
			errorEvent = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			DoSetEnumValue();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoSetEnumValue();
		}
		
		void DoSetEnumValue()
		{

			if (Enum.IsDefined (enumVariable.Value.GetType (), value.Value)) {
				enumVariable.Value = (Enum)Enum.Parse (enumVariable.Value.GetType (), value.Value.ToString ());
			} else {

				if (_valueCache!= value.Value)
				{
					_valueCache = value.Value;
					Fsm.Event(errorEvent);
				}
			}
		}
		
	}
}