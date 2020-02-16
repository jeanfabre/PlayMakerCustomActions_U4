// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Sprite")]
	[Tooltip("Set the sorting layer name of a SpriteRendered. Optionally set all SpriteRendered found on the gameobject Target.")]
	public class SetSpriteSortingLayerName : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		public FsmString sortingLayerName;
		
		
		public bool setAllSpritesInChildren;
		
		public override void Reset()
		{
			gameObject = null;
			sortingLayerName = null;
			setAllSpritesInChildren = false;
		}

		public override string ErrorCheck ()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);

			if (go!=null && ! setAllSpritesInChildren && go.GetComponent<SpriteRenderer>() == null ){
				return "Missing SpriteRenderer on GameObject";
			}

			return "";
		}
		public override void OnEnter()
		{
			DoSetLayerName();
			Finish();
		}
		
		void DoSetLayerName()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			
			if (setAllSpritesInChildren)
			{
				SpriteRenderer[] sprites = go.GetComponentsInChildren<SpriteRenderer> ();
				foreach (SpriteRenderer _sprite in sprites) {
					_sprite.sortingLayerName = sortingLayerName.Value;
				}
			}else{
				if (go.GetComponent<SpriteRenderer>() != null)go.GetComponent<SpriteRenderer>().sortingLayerName  = sortingLayerName.Value;
			}
		}
	}
}

