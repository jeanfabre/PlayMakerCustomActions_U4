// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	 [ActionCategory(ActionCategory.GUIElement)]
	 [Tooltip("Sets the Pixel Inset Soze of the GUITexture attached to a Game Object. Useful for sizing GUI elements around.")]
	 public class SetGUITexturePixelInsetSize : FsmStateAction
	 {
		 [RequiredField]
		 [CheckForComponent(typeof(GUITexture))]
		 public FsmOwnerDefault gameObject;
		 [RequiredField]
		 public FsmFloat width;
		 public FsmFloat height;
		 public bool everyFrame;
		
		 public override void Reset()
		 {
			 gameObject = null;
			 width = null;
			 height = null;
			 everyFrame = false;
		 }

		 public override void OnEnter()
		 {
			 DoGUITexturePixelInsetSize();
			
			 if(!everyFrame)
				 Finish();
		 }
		
		 public override void OnUpdate()
		 {
			 DoGUITexturePixelInsetSize();
		 }
		
		
		 void DoGUITexturePixelInsetSize()
		 {
			 GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			 if (go != null && go.guiTexture != null)
			 {
				 Rect pixelInset = go.guiTexture.pixelInset;
				
				if (!width.IsNone)
				{
					 pixelInset.width = width.Value;
				}
				if (!height.IsNone)
				{
					 pixelInset.height = height.Value;	
				}
				 go.guiTexture.pixelInset = pixelInset;
			 }			
		 }
	 }
}
