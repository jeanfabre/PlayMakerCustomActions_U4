// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: line renderer width start end

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Renderer)]
	[Tooltip("Set the start and end widths of a lineRenderer")]
	public class SetLineRendererSetWidths : FsmStateAction
	{

		[ActionSection("Setup")]
		[RequiredField]
		[Tooltip("The GameObject with the LineRenderer component.")]
		[CheckForComponent(typeof(LineRenderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("The start Width")]
		#if ! UNITY_5_5_OR_NEWER 
		[RequiredField]
		#endif
		public FsmFloat startWidth;

		[Tooltip("The end Width")]
		#if ! UNITY_5_5_OR_NEWER 
		[RequiredField]
		#endif
		public FsmFloat endWidth;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		LineRenderer _lr;

		public override void Reset ()
		{
			gameObject = null;
			startWidth = null;
			endWidth = null;
			everyFrame = false;
		}

		public override void OnEnter ()
		{
			SetWidths();

			if (!everyFrame)
			{
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			SetWidths();
		}

		void SetWidths()
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
			if (!startWidth.IsNone)	_lr.startWidth = startWidth.Value;
			if (!endWidth.IsNone)	_lr.endWidth = endWidth.Value;
			#else
				_lr.SetWidth(startWidth.Value,endWidth.Value);
			#endif

		}
	}
}
