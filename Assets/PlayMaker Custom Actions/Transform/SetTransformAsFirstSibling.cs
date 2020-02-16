// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// @KEYWORDS: child

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Move the transform to the start of the local transform list.")]
	public class SetTransformAsFirstSibling : FsmStateAction
	{
		#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5
		[UIHint(UIHint.Description)] // Use on a string field to format the text in a large readonly info box.
		public string descriptionArea =" NOT AVAILABLE UNTIL Unity 4.6";
		#endif

		[RequiredField]
		[Tooltip("The Game Object to move as first sibling.")]
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
					go.transform.SetAsFirstSibling();
				}
			#endif
			Finish();
		}
	}
}
