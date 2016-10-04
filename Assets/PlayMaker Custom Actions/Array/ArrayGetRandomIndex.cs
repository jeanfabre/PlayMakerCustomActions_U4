// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved. 
// __ECO__ __PLAYMAKER__ __ACTION__ 


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Array)]
	[Tooltip("Get a Random item from an Array.")]
	public class ArrayGetRandomIndex : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Array to use.")]
		public FsmArray array;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("Store the value in a variable.")]
        [MatchElementType("array")]
        public FsmVar storeValue;
		
		[Tooltip("The index of the value in the array.")]
		[UIHint(UIHint.Variable)]
		public FsmInt index;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			array = null;
			storeValue =null;
			index = null;
            everyFrame = false;
		}
		
		// Code that runs on entering the state.
		public override void OnEnter()
		{
			DoGetRandomValue();
			
			if (!everyFrame)
			{
				Finish();
			}
			
		}
		
		public override void OnUpdate()
		{
			DoGetRandomValue();
			
		}
		
		private void DoGetRandomValue()
		{
			if (storeValue.IsNone)
			{
				return;
			}
			index.Value = Random.Range(0,array.Length);
			storeValue.SetValue(array.Get(index.Value));	
		}

		
	}
}

