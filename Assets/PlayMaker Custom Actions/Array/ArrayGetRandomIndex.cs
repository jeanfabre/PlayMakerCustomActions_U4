// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// __ECO__ __PLAYMAKER__ __ACTION__ 
// Made By : DjayDino


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
		
		[Tooltip("Can hit the same number twice in a row")]
		public FsmBool Repeat;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;
		
		private int randomIndex;
		private int lastIndex = -1;

		public override void Reset()
		{
			array = null;
			storeValue =null;
			index = null;
            everyFrame = false;
			Repeat = true;
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
			
			if (Repeat.Value)
			{
				randomIndex = Random.Range(0,array.Length);
				index.Value = randomIndex;
				storeValue.SetValue(array.Get(index.Value));
			}
			else
			{
				do
				{
					randomIndex = Random.Range(0,array.Length);
				} while ( randomIndex == lastIndex);
				
				lastIndex = randomIndex;
			}
			if (randomIndex != -1)
			{
				index.Value = randomIndex;
				storeValue.SetValue(array.Get(index.Value));
			}

		}

		
	}
}

