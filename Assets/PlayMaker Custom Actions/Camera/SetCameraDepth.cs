// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Sets the depth of a Camera.")]
	public class SetCameraDepth : FsmStateAction
	{

		[RequiredField]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The depth of a Camera.")]
		public FsmInt depth;

		public bool everyFrame;
		
		private Camera _camera;
		
		public override void Reset()
		{
			gameObject = null;
			depth = null;
			
			everyFrame = false;
		}
		
		public override void OnEnter()
		{
			GameObject go = gameObject.OwnerOption == OwnerDefaultOption.UseOwner ? Owner : gameObject.GameObject.Value;
			if (go == null)
			{
				LogError("gameObject is missing!");
				return;
			}
			
			
			_camera = go.camera;
			
			if (_camera == null)
			{
				LogError("Missing Camera Component!");
				return;
			}
			
			
			DoSetCameraDepth();
			
			if (!everyFrame)
				Finish();
		}
		
		public override void OnUpdate()
		{
			DoSetCameraDepth();
		}
		
		void DoSetCameraDepth()
		{
			if (_camera != null)
			{
				_camera.depth = depth.Value;
			}
		}
	}
}
