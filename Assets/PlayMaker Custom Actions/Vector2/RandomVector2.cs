// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Vector2)]
    [Tooltip("Creates a random Vector2. The unitCircleRadius represents the magnitude of the randomized vector. Result can be set fo be on the circle perimeter or inside the circle.")]
    public class RandomVector2 : FsmStateAction
    {
		
		public enum RandomUnitCircleOption {OnCirclePerimeter,InsideCircle};
		
		[Tooltip("Seed for the random number generator. Leave to none for true randomness. Only set to a number if you want predictability and repeatability")]
		public FsmInt seed;
		
		[Tooltip("Radius of imaginary circle")]
		public FsmFloat unitCircleRadius;
		
		[Tooltip("Define where the randomize occurs")]
		public RandomUnitCircleOption option;
		
        [UIHint(UIHint.Variable)]
		[RequiredField]
		[Tooltip("The randomized vector 2")]
        public FsmVector2 storeResult;
		

		[Tooltip("Update every frame")]
		public bool everyFrame;
		
        public override void Reset()
        {
			seed = new FsmInt(){UseVariable=true};
            storeResult = null;
            unitCircleRadius = 1;
			option = RandomUnitCircleOption.OnCirclePerimeter;
			everyFrame = false;
        }

        public override void OnEnter()
        {
			if (!seed.IsNone)
			{
				Random.seed = seed.Value;
			}
			
			GetValue();
			
			if (!everyFrame)
			{
				    Finish();
			}
		}
		
		public override void OnUpdate()
		{
			GetValue();
		}
		
		void GetValue()
		{
			if (option == RandomUnitCircleOption.InsideCircle)
			{
            	storeResult.Value = Random.insideUnitCircle*unitCircleRadius.Value;
			}else{
				Vector3 _r = Random.onUnitSphere*unitCircleRadius.Value;
				
				storeResult.Value = new Vector2(_r.x,_r.y);
			}
        }
    }
}
