// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Gets a Game Object's Transform as either a Transform or individual Pos/Rot/Sca Vector3's.")]
	public class GetTransformParts : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(Transform))]
		[Tooltip("The entire transform object.")]
		public FsmObject storeTransform;

		[UIHint(UIHint.Variable)]
		[Tooltip("The position Vector3.")]
		public FsmVector3 storePosition;

		[UIHint(UIHint.Variable)]
		[Tooltip("The rotation EulerAngles.")]
		public FsmVector3 storeRotation;

		[UIHint(UIHint.Variable)]
		[Tooltip("The rotation Quaternion.")]
		public FsmQuaternion storeQuaternion;

		[UIHint(UIHint.Variable)]
		[Tooltip("The scale Vector3.")]
		public FsmVector3 storeScale;
		
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			storeTransform = null;
			storePosition = null;
			storeRotation = null;
			storeQuaternion = null;
			storeScale = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetTransform();
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoGetTransform();
		}
		
		void DoGetTransform()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				Debug.Log ("GameObject is null.");
				Finish ();
				return;
			}

			if (storeTransform != null)
			{
				storeTransform.Value = go.transform;
			}
			if (!storePosition.IsNone)
			{
				storePosition.Value = go.transform.position;	
			}
			if (!storeRotation.IsNone)
			{
				storeRotation.Value = go.transform.rotation.eulerAngles;
			}
			if (!storeQuaternion.IsNone)
			{
				storeQuaternion.Value = go.transform.rotation;
			}
			if (!storeScale.IsNone)
			{
				storeScale.Value = go.transform.localScale;
			}
		}
	}
}
