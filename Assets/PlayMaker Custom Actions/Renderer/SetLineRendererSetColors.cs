// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: line renderer color start end

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Renderer)]
	[Tooltip("Set the start and end colors of a lineRenderer")]
	public class SetLineRendererSetColors : FsmStateAction
	{

		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("The GameObject with the LineRenderer component.")]
		[CheckForComponent(typeof(LineRenderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("The start Color")]
		#if ! UNITY_5_5_OR_NEWER 
		[RequiredField]
		#endif
		public FsmColor startColor;

		[Tooltip("The end Color")]
		#if ! UNITY_5_5_OR_NEWER 
		[RequiredField]
		#endif
		public FsmColor endColor;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		LineRenderer _lr;

		public override void Reset ()
		{
			gameObject = null;
			startColor = null;
			endColor = null;
			everyFrame = false;
		}

		public override void OnEnter ()
		{
			SetColors();

			if (!everyFrame)
			{
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			SetColors();
		}

		void SetColors()
		{
			var go = Fsm.GetOwnerDefaultTarget (gameObject);
			if (go == null) {
				return;
			}
			
			_lr = go.GetComponent<LineRenderer>();

			if (_lr == null)
			{
				return;
			}

			#if UNITY_5_5_OR_NEWER 
				if (!startColor.IsNone)	_lr.startColor = startColor.Value;
				if (!endColor.IsNone)	_lr.endColor = endColor.Value;
			#else
				_lr.SetColors(startColor.Value,endColor.Value);
			#endif

		}
	}
}
