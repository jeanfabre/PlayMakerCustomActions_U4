// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ 
EcoMetaStart
{
"version":"1.0.0"
}
EcoMetaEnd
 
EcoChangeLogStart
### 1.0.0
**Note**  
Initial creation. Thanks to Magnus @Tumblehead for the suggestion  
[Source](http://allenchou.net/2015/04/game-math-precise-control-over-numeric-springing/)
EcoChangeLogEnd
*/


using UnityEngine;
using System;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ProceduralAnimation")]
	[Tooltip("Numerical spring behavior. You can apply this technique to all sorts of numeric properties. Do not require physics, totally math driven. Runs every frame by default")]
	public class NumericSpring : FsmStateAction
	{
		[ActionSection("Target")]
		[RequiredField]
		[Tooltip("The desired target position to reach")]
		public FsmFloat targetPosition;
		
		[ActionSection("Spring Setup")]
		[RequiredField]
		[Tooltip("The damping ratio. A damping ratio of zero means there is no damping at all, and the oscillation just continues indefinitely.\n" +
		 	"A damping ratio between zero and one means the spring system is underdamped; oscillation happens, and the magnitude of oscillation decreases exponentially over time.\n" +
		 	"A damping ratio of 1 signifies a critically damped system, where this is the point the system stops showing oscillation, but only converging to the target value exponentially.\n" +
		 	"Any damping ratio above 1 means the system is overdamped, and the effect of springing becomes more draggy as the damping ratio increases.")]
		public FsmFloat dampingRatio;
		
		[RequiredField]
		[Tooltip("The angular frequency of the oscillation ( will be multiplied by PI). An angular frequency of 2pi (radians per second) means the oscillation completes one full period over one second, i.e. 1Hz.")]
		public FsmFloat angularFrequency;
		
		[ActionSection("Result")]
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Position of the spring")]
		public FsmFloat position;
		
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Velocity of the spring")]
		public FsmFloat velocity;
		
		float x;
		float v;
		
		public override void Reset()
		{
			targetPosition = null;
			
			dampingRatio = 0.1f;
			angularFrequency = 8;
			
			position = null;
			velocity = null;
		}


		public override void OnUpdate()
		{	
			x = position.Value;
			v = velocity.Value;
			
			ComputeSpring
				(
				targetPosition.Value,
				angularFrequency.Value * Mathf.PI,
				dampingRatio.Value,
				Time.deltaTime,
				ref x,
				ref v
				);
			
			position.Value = x;
			velocity.Value = v;
		}
		
		/// <summary>
		/// Sourec: http://allenchou.net/2015/04/game-math-precise-control-over-numeric-springing/
		/// Computes numerical spring.
		/// You specify the initial value (x), initial velocity (v), target value (xt), and some spring-related parameters; the result is a smooth springing effect. 
		/// You can apply this technique to all sorts of numeric properties, some common ones being position, rotation, and scale of an object.
		/// </summary>
		/// <param name='xt'>
		/// The desired Target Position (Use x as your result)
		/// </param>
		/// <param name='omega'>
		/// The angular frequency of the oscillation. An angular frequency of 2\pi (radians per second) means the oscillation completes one full period over one second, i.e. 1Hz.
		/// </param>
		/// <param name='zeta'>
		/// The damping ratio. A damping ratio of zero means there is no damping at all, and the oscillation just continues indefinitely. A damping ratio between zero and one means the spring system is underdamped; oscillation happens, and the magnitude of oscillation decreases exponentially over time. A damping ratio of 1 signifies a critically damped system, where this is the point the system stops showing oscillation, but only converging to the target value exponentially. Any damping ratio above 1 means the system is overdamped, and the effect of springing becomes more draggy as the damping ratio increases.
		/// </param>
		/// <param name='h'>
		/// the time step (use Time.Deltatime for framerate indepedant behavior)
		/// </param>
		/// <param name='x'>
		/// The Spring Position
		/// </param>
		/// <param name='v'>
		/// The Spring Velocity
		/// </param>
		public static void ComputeSpring(float xt,float omega,float zeta,float h,ref float x, ref float v )
		{
	
			float f = 1.0f + 2.0f * h * zeta * omega;
			float oo = omega * omega;
			float hoo = h * oo;
  			float hhoo = h * hoo;
			float detInv = 1.0f / (f + hhoo);
			float detX = f * x + h * v + hhoo * xt;
			float detV = v + hoo * (xt - x);
			
			 x = detX * detInv;
			 v = detV * detInv;
		}
	
	}
}
