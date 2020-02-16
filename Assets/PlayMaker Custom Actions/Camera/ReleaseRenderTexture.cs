// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Release a RenderTexture")]
	public class ReleaseRenderTexture : FsmStateAction
	{

		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault camera;

		[UIHint(UIHint.Variable)]
		[Tooltip("The renderTexture to release")]
		public FsmTexture OrRenderTexture;

		public override void Reset()
		{
			camera = null;
			OrRenderTexture = null;
		}
		
		public override void OnEnter()
		{

			GameObject go = camera.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : camera.GameObject.Value;
			if (go == null) {
				LogError ("camera is missing!");
				return;
			} else {
				Camera _c = go.GetComponent<Camera>() ;
				if (_c!=null && _c.targetTexture !=null)
				{
					_c.targetTexture.Release();
				}
			}

			if (OrRenderTexture.Value != null) {
				RenderTexture _t = 	OrRenderTexture.Value as RenderTexture;
				if (_t!=null)
				{
					_t.Release();
				}
			}
			Finish();
		}


	}
}
