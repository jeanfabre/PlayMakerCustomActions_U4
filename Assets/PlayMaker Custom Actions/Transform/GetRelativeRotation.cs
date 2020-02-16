// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Gets the relative Rotation of a Game Object compared to another")]
	public class GetRelativeRotation : QuaternionBaseAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		public FsmOwnerDefault reference;

		[ActionSection("Results")]

		[UIHint(UIHint.Variable)]
		public FsmQuaternion quaternion;

		[UIHint(UIHint.Variable)]
		[Title("Euler Angles")]
		public FsmVector3 vector;

		[UIHint(UIHint.Variable)]
		public FsmFloat xAngle;
		[UIHint(UIHint.Variable)]
		public FsmFloat yAngle;
		[UIHint(UIHint.Variable)]
		public FsmFloat zAngle;


		Quaternion _result;
		GameObject _go;
		GameObject _ref;

		public override void Reset()
		{
			gameObject = null;
			reference = new FsmOwnerDefault ();
			reference.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			reference.GameObject = new FsmGameObject(){UseVariable=true};

			quaternion = null;
			vector = null;
			xAngle = null;
			yAngle = null;
			zAngle = null;
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			ExecuteAction();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnUpdate()
		{
			if (everyFrameOption == everyFrameOptions.Update )
			{
				ExecuteAction();
			}
		}
		public override void OnLateUpdate()
		{
			if (everyFrameOption == everyFrameOptions.LateUpdate )
			{
				ExecuteAction();
			}
		}
		
		public override void OnFixedUpdate()
		{
			if (everyFrameOption == everyFrameOptions.FixedUpdate )
			{
				ExecuteAction();
			}
		}
		
		void ExecuteAction()
		{
			_go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go == null)
			{
				return;
			}

			var _ref = Fsm.GetOwnerDefaultTarget(reference);
			if (_ref == null)
			{
				return;
			}

			Quaternion _result =  Quaternion.Inverse(_go.transform.rotation) * _ref.transform.rotation;

			if (!quaternion.IsNone)
				quaternion.Value = _result;

			if (!vector.IsNone)
				vector = _result.eulerAngles;

			if (!xAngle.IsNone)
				xAngle.Value = _result.eulerAngles.x;

			if (!yAngle.IsNone)
				yAngle.Value = _result.eulerAngles.y;

			if (!zAngle.IsNone)
				zAngle.Value = _result.eulerAngles.z;
		}
	}
}