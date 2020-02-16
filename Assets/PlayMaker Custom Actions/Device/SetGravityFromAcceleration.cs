// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Adjust the gravity direction based on the device acceleration. Typically to control rolling balls")]
	public class SetGravityfromAcceleration : FsmStateAction
	{
		

		public FsmFloat multiplier;
	
		
		public override void Reset()
		{
			multiplier = 10f;
		}
		
		public override void OnPreprocess()
        {
            Fsm.HandleFixedUpdate = true;
        }
		

		public override void OnFixedUpdate()
		{
			SetGravity();
		}
		
		
		void SetGravity()
		{
			float _mult = multiplier.Value;
			
			if (Screen.orientation == ScreenOrientation.LandscapeLeft) {;

                     Physics.gravity = new Vector3 (-Input.acceleration.y * _mult , Input.acceleration.x * _mult , -Input.acceleration.z * _mult);

              }  else {

                     Physics.gravity = new Vector3 (Input.acceleration.y * _mult , -Input.acceleration.x * _mult , -Input.acceleration.z * _mult);

              }
			
		}
		
	}
}
