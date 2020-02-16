// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ 
 * Keywords: index of contains
 * ---*/


using System;

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.String)]
	[Tooltip("Finds a single char string from a string variable.")]
	public class StringGetIndexOf : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The String variable to test.")]
		public FsmString stringVariable;
		
		[RequiredField]
		[Tooltip("Test if the String variable contains this string.")]
		public FsmString containsString;
		
		[Tooltip("Index at which search will start. Leave to none or 0 to start search at begining of string. Other values may " +
			"be used to help find multiple Char within a single string by updating the value each time the char is found, " +
			"and runing the action again until no more are found.")]
		public FsmInt startIndex;
		
		[Tooltip("Index at which search will start. Leave to none or 0 to start search at begining of string. Other values may " +
			"be used to help find multiple Char within a single string by updating the value each time the char is found, " +
			"and runing the action again until no more are found.")]
		public FsmInt count;
		
		[Tooltip("Comparision setting for the search.")]
		public StringComparison stringComparision;
		
		
		[ActionSection("Results")]
		
		[UIHint(UIHint.Variable)]
		[Tooltip("True if containsString was found in stringVariable")]
		public FsmBool found;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("Index at which the containsString was first found within the String. -1 means the ContainsString was not found in StringVariable.")]
		public FsmInt index;
		
		[Tooltip("Event fired if Char was found in String.")]
		public FsmEvent foundEvent;

		[Tooltip("Event fired if Char was not found in String.")]
		public FsmEvent notFoundEvent;

		[Tooltip("Event fired if startSearchAt is below 0 or greater than the length of stringVariable or if containsString is empty")]
		public FsmEvent failureEvent;
		
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			stringVariable = null;
			containsString = null;
			
			startIndex = null;
			count = null;
			
			stringComparision = StringComparison.CurrentCulture;
			
			startIndex = new FsmInt() {UseVariable=true};
			count = new FsmInt() {UseVariable=true};
			
			found = null;
			index = null;
			
			foundEvent = null;
			notFoundEvent = null;
			failureEvent = null;
			
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetIndexOf();
			
			if (!everyFrame)
				Finish();
		}

		public override void OnUpdate()
		{
			DoGetIndexOf();
		}
		
		void DoGetIndexOf()
		{
			int _startIndex = startIndex.Value; // will be 0 if th euser left the field to "none".
			
			int _searchLength = count.IsNone ? stringVariable.Value.Length - _startIndex : count.Value; // default to full search

			int _index = -1;
			
			try // catch any errors, including out of range and potential null strings, though PlayMaker should never give you a null string.
			{
				_index = stringVariable.Value.IndexOf(containsString.Value,_startIndex,_searchLength,stringComparision);
			}catch(Exception e)
			{
				found.Value = false;

				if (failureEvent == null)
				{
					UnityEngine.Debug.Log(e.Message);
					Fsm.Event(notFoundEvent);
				}else{
					Debug.Log(e.Message);
					UnityEngine.Debug.Log(e.Message);
					Fsm.Event(failureEvent);
				}
				
				return;
			}
			
			if (!index.IsNone) index.Value = _index;
			
			bool _found = _index != -1;
			
		   	if (!found.IsNone) found.Value = _found;
			
			if (_found)
			{
				Fsm.Event(foundEvent);
			}else{
				Fsm.Event(notFoundEvent);
			}
			
			
		}
		
	}
}