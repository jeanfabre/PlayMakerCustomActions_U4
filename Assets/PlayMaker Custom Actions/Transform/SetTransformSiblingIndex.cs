// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// @KEYWORDS: child

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Move the transform to a given index in the local transform list.")]
	public class SetTransformSiblingIndex : FsmStateAction
	{
		#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5
		[UIHint(UIHint.Description)] // Use on a string field to format the text in a large readonly info box.
		public string descriptionArea =" NOT AVAILABLE UNTIL Unity 4.6";
		#endif

		[RequiredField]
		[Tooltip("The Game Object to set its sibling index")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The index in the local parent transform list to move the transform to. Value will be clamped between 0 and the last child")]
		public FsmInt index;

		public override void Reset()
		{
			gameObject = null;
			index = null;
		}
		
		public override void OnEnter()
		{
			#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5
			UnityEngine.Debug.Log("SetTransformParent isn't available until Unity 4.6. Use 'Set Parent' Action instead if you must work on this version of Unity");
			#else
				var go = Fsm.GetOwnerDefaultTarget(gameObject);

				if (go != null)
				{
					go.transform.SetSiblingIndex(Mathf.Clamp(index.Value,0,go.transform.parent.childCount-1));
				}
			#endif
			Finish();
		}
	}
}
