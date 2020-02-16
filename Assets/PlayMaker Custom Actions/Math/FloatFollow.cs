// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Follow a float value and lerp if outside the defined margin. Typically use for camera movement to follow player when getting close to the screen sides")]
	public class FloatFollow : FsmStateAction
	{
		public enum FrameUpdateSelector {OnUpdate,OnLateUpdate,OnFixedUpdate}

		[RequiredField]
		[Tooltip("The value")]
		public FsmFloat value;

		[RequiredField]
		[Tooltip("The target to follow")]
		public FsmFloat target;

		[RequiredField]
		[Tooltip("The margin to trigger lerping if distance between the target and value if greater than margin")]
		public FsmFloat margin;

		[RequiredField]
		[Tooltip("The margin to trigger lerping if distance between the target and value if greater than margin")]
		public FsmFloat lerpSmoothing;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The result")]
		public FsmFloat result;

		[ActionSection("Update type")]
		public FrameUpdateSelector updateType;

		private float startTime;
		private float currentTime;
		private float endTime;
		private bool looping;


		public override void Reset()
		{
			value = null;
			target = null;
			margin =1;
			lerpSmoothing = 1;
		}


		public override void OnPreprocess()
		{
			if (updateType == FrameUpdateSelector.OnFixedUpdate)
			{
				Fsm.HandleFixedUpdate = true;
			}
			
			#if PLAYMAKER_1_8_5_OR_NEWER
			if (updateType == FrameUpdateSelector.OnLateUpdate)
			{
				Fsm.HandleLateUpdate = true;
			}
			#endif
		}

		void OnActionUpdate()
		{
			float _result = value.Value;

			if (Mathf.Abs(value.Value - target.Value) > margin.Value)
			{
				_result = Mathf.Lerp(value.Value, target.Value, lerpSmoothing.Value * Time.deltaTime);
			}

			result.Value = _result;
		}



		public override void OnUpdate()
		{
			if (updateType == FrameUpdateSelector.OnUpdate)
			{
				OnActionUpdate();
			}
		}
		
		public override void OnLateUpdate()
		{
			if (updateType == FrameUpdateSelector.OnLateUpdate)
			{
				OnActionUpdate();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (updateType == FrameUpdateSelector.OnFixedUpdate)
			{
				OnActionUpdate();
			}
		}
		

	}
}