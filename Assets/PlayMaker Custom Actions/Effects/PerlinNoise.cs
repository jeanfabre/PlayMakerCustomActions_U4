// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Effects)]
	[Tooltip("PerlinNoise action")]
	public class PerlinNoise : FsmStateAction {

		[RequiredField]
		[Tooltip("PerlinNoise animation speed")]
		public FsmFloat speed;

		[Tooltip("PerlinNoise seed ( the x component). Leave to none for random")]
		public FsmFloat seed;

		[RequiredField]
		[Tooltip("the actual PerlinNoise result ranging from 0 to 1")]
		[UIHint(UIHint.Variable)]
		public FsmFloat perlinNoise;
		
		[Tooltip("If set to false, will not animate over time")]
		public bool everyFrame;
		
		/// <summary>
		/// randomness for the perlinnoise.
		/// </summary> 
		private float _seed;
		
		/// <summary>
		/// Reset all values
		/// </summary>
		public override void Reset()
		{		
			_seed = Random.Range(0f, 65535f);
			seed = new FsmFloat (){UseVariable=true};
			speed = new FsmFloat();
			speed.Value = 1f;
			perlinNoise= null;
			everyFrame = true;	
			
		}// Reset
	
		/// <summary>
		/// Compute the perlinNoise and finish the action if only supposed to run once.
		/// <see cref="everyFrame"/>
		/// </summary>		
		public override void OnEnter()
		{
			ComputePerlinNoise();
			
			if (!everyFrame)
				Finish();
			
		}// OnEnter
	
		
		/// <summary>
		/// If the action is suppose to run every frame, we compute the noise here
		/// <see cref="everyFrame"/>
		/// </summary>
		public override void OnUpdate()
		{
			
			ComputePerlinNoise();	
			
		}// OnUpdate

		
		/// <summary>
		/// Compute and store the current perlin noise.
		/// </summary>
		private void ComputePerlinNoise(){

			if (!seed.IsNone) {
				_seed = seed.Value;
			}

			perlinNoise.Value = Mathf.PerlinNoise(_seed, speed.Value*Time.time);
			
		}// ComputePerlinNoise
	
	}// PerlinNoise
}// namespace HutongGames.PlayMaker.Actions
