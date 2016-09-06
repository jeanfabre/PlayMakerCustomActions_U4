// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Align an object to always face the camera. You optionnally decide to keep a constant screensize.")]
	public class BillBoard : FsmStateAction
	{
		[CheckForComponent(typeof(Camera))]
		[Tooltip("Leave to none to use the main camera")]
		public FsmGameObject cameraTarget;
		
		[Tooltip("check this if you want constant screensize")]
		public bool ConstantScreenSize;
		
		[Tooltip("if 0, will use the current distance to keep constant screensize to be scaled to 1 at that distance")]
		public FsmFloat distanceBase;
		

		public override void Reset()
		{				
			
			cameraTarget = null;
			ConstantScreenSize = true;
			distanceBase = null;
		}
		
		private GameObject _go;
		
		private Camera _cam;
		
		private Vector3 _originalScale;
		private float _originalDistance;
	
		
		public override void OnUpdate()
		{
			GameObject go = cameraTarget.Value;
			if (_cam == null || _go != go)
			{
				_go = go;
				if (go == null) 
				{
					_cam = Camera.main;
				}else{
				
					Camera camera = go.camera;
					if (camera == null)
					{
						_cam = Camera.main;
						LogError("Missing Camera Component!");
						return;
					}else{
						_cam = camera;
					}
					
				}
				
				
				_originalDistance = (_cam.transform.position - Owner.transform.position).magnitude;
				_originalScale = Owner.transform.localScale;
			}
			
			Owner.transform.LookAt(Owner.transform.position + _cam.transform.rotation * Vector3.back,
            _cam.transform.rotation * Vector3.up);
			
			if (ConstantScreenSize )
			{
				float dist = 0f;
				if (distanceBase.Value>0)
				{
					dist = (_cam.transform.position - Owner.transform.position).magnitude;
					Owner.transform.localScale = Vector3.one*(dist/distanceBase.Value);	
				}else{
					dist = (_cam.transform.position - Owner.transform.position).magnitude;
					Owner.transform.localScale = (dist/_originalDistance)*_originalScale;
				}
				
				
			}
		}

	}
}
