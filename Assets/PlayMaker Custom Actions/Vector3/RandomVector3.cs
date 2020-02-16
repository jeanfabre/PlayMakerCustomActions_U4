// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Vector3)]
    [Tooltip("Creates a random Vector3. The unitSphereRadius represents the magnitude of the randomized vector. Result can be set fo be on the Sphere Surface or inside the sphere.")]
    public class RandomVector3 : FsmStateAction
    {
		
		public enum RandomUnitSphereOption {OnSphereSurface,InsideSphere};
		
		[Tooltip("Seed for the random number generator. Leave to none for true randomness. Only set to a number if you want predictability and repeatability")]
		public FsmInt seed;
		
		[Tooltip("Radius of imaginary sphere")]
		public FsmFloat unitSphereRadius;
		
		[Tooltip("Define where the randomize occurs")]
		public RandomUnitSphereOption option;
		
        [UIHint(UIHint.Variable)]
		[RequiredField]
		[Tooltip("The randomized vector 3")]
        public FsmVector3 storeResult;
		
		[Tooltip("Update every frame")]
		public bool everyFrame;
		
        public override void Reset()
        {
			seed = new FsmInt(){UseVariable=true};
            storeResult = null;
            unitSphereRadius = 1;
			option = RandomUnitSphereOption.OnSphereSurface;
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
			if (option == RandomUnitSphereOption.InsideSphere)
			{
            	storeResult.Value = Random.insideUnitSphere*unitSphereRadius.Value;
			}else{
				storeResult.Value = Random.onUnitSphere*unitSphereRadius.Value;
			}
        }
    }
}
