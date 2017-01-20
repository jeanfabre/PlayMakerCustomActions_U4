// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Keywords: line renderer

using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Renderer)]
	[Tooltip("Set a particular position of a lineRenderer")]
	public class SetLineRendererPosition : FsmStateAction
	{

		[ActionSection("Setup")]
		[Tooltip("The GameObject with the LineRenderer component.")]
		[CheckForComponent(typeof(LineRenderer))]
		public FsmOwnerDefault gameObject;

		[Tooltip("The index")]
		public FsmInt index;

		[Tooltip("The Position")]
		public FsmVector3 position;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;


		LineRenderer _lr;

		public override void Reset ()
		{
			gameObject = null;

		}

		public override void OnEnter ()
		{
			SetPosition();

			if (!everyFrame)
			{
				Finish ();
			}
		}

		public override void OnUpdate()
		{
			SetPosition();
		}

		void SetPosition()
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

			_lr.SetPosition(index.Value,position.Value);

		}
	}
}
