// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	 [ActionCategory(ActionCategory.GUIElement)]
	 [Tooltip("Sets the Pixel Inset Position of the GUITexture attached to a Game Object. Useful for moving GUI elements around.")]
	 public class SetGUITexturePixelInsetPosition : FsmStateAction
	 {
		 [RequiredField]
		 [CheckForComponent(typeof(GUITexture))]
		 public FsmOwnerDefault gameObject;
		 [RequiredField]
		 public FsmFloat PixelInsetX;
		 public FsmFloat PixelInsetY;
		
		 public FsmBool AsIncrement;
		
		 public bool everyFrame;
		
		 public override void Reset()
		 {
			 gameObject = null;
			 PixelInsetX = null;
			 PixelInsetY = null;
			 AsIncrement = null;
			 everyFrame = false;
		 }

		 public override void OnEnter()
		 {
			 DoGUITexturePixelInsetPosition();
			
			 if(!everyFrame)
				 Finish();
		 }
		
		 public override void OnUpdate()
		 {
			 DoGUITexturePixelInsetPosition();
		 }
		
		
		 void DoGUITexturePixelInsetPosition()
		 {
			 GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			 if (go != null && go.guiTexture != null)
			 {
				 Rect pixelInset = go.guiTexture.pixelInset;
				
				 if (AsIncrement.Value == true){
					 pixelInset.x += PixelInsetX.Value;
					 pixelInset.y += PixelInsetY.Value;
				 }else{
					 pixelInset.x = PixelInsetX.Value;
					 pixelInset.y = PixelInsetY.Value;	
				 }
				 go.guiTexture.pixelInset = pixelInset;
			 }			
		 }
	 }
}
