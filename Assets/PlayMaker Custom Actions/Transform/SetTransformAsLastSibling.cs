// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Move the transform to the end of the local transform list.")]
	public class SetTransformAsLastSibling : FsmStateAction
	{
		#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5
		[UIHint(UIHint.Description)] // Use on a string field to format the text in a large readonly info box.
		public string descriptionArea =" NOT AVAILABLE UNTIL Unity 4.6";
		#endif

		[RequiredField]
		[Tooltip("The Game Object to move as the last sibling.")]
		public FsmOwnerDefault gameObject;

		public override void Reset()
		{
			gameObject = null;
		}
		
		public override void OnEnter()
		{
			#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5
			UnityEngine.Debug.Log("SetTransformParent isn't available until Unity 4.6. Use 'Set Parent' Action instead if you must work on this version of Unity");
			#else
				var go = Fsm.GetOwnerDefaultTarget(gameObject);

				if (go != null)
				{
					go.transform.SetAsLastSibling();
				}
			#endif
			Finish();
		}
	}
}