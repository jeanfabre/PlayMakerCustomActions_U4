// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// forum thread: http://hutonggames.com/playmakerforum/index.php?topic=8137.msg39239#msg39239
// code source: http://nielson.io/2014/03/making-a-target-tracking-orthographic-camera-in-unity/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Camera)]
	[Tooltip("Sets the ortho size and position of the Camera to keep all targets in view.")]
	public class CameraOrthoTrackTargets : FsmStateAction
	{
		[Tooltip("The Camera. Leave to none to use the MainCamera")]
		[CheckForComponent(typeof(Camera))]
		public FsmOwnerDefault gameObject;
		
		[RequiredField]
		[Tooltip("The targets.")]
		public FsmGameObject[] targets;
		
		[Tooltip("The speed of adjustments")]
		public FsmFloat zoomSpeed;
		
		[Tooltip("The padding on each side of the camera view.")]
		public FsmFloat boundingBoxPadding;
		
		[Tooltip("The minimum orthos size if all targets gets closer to one another.")]
		public FsmFloat minimumOrthographicSize;
		
		Camera _camera;
		
		public override void Reset()
		{
			gameObject = new FsmOwnerDefault();
			gameObject.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
			gameObject.GameObject = new FsmGameObject() {UseVariable=true};
			
			zoomSpeed = 5f;
			boundingBoxPadding = 2f;
			minimumOrthographicSize = 8f;
		}
		
		public override void OnEnter()
		{
			GetCamera();
			
			DoAction();
		}
		
		public override void OnLateUpdate()
		{
			DoAction();
		}
		
		void GetCamera()
		{
			_camera = Camera.main;
				
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;

			if (go.camera != null)
			{
				_camera = go.camera;
			}
		}
		
		void DoAction()
		{
			Rect boundingBox = CalculateTargetsBoundingBox();
        	_camera.transform.position = CalculateCameraPosition(boundingBox);
       	 	_camera.orthographicSize = CalculateOrthographicSize(boundingBox);
		}
		
		/// <summary>
	    /// Calculates a bounding box that contains all the targets.
	    /// </summary>
	    /// <returns>A Rect containing all the targets.</returns>
	    Rect CalculateTargetsBoundingBox()
	    {
			float padding = boundingBoxPadding.Value;
			
	        float minX = Mathf.Infinity;
	        float maxX = Mathf.NegativeInfinity;
	        float minY = Mathf.Infinity;
	        float maxY = Mathf.NegativeInfinity;
	
	        foreach (FsmGameObject target in targets) {
				GameObject _go = target.Value;
				if (_go==null)
				{
					continue;
				}
				
	            Vector3 position = _go.transform.position;
	
	            minX = Mathf.Min(minX, position.x);
	            minY = Mathf.Min(minY, position.y);
	            maxX = Mathf.Max(maxX, position.x);
	            maxY = Mathf.Max(maxY, position.y);
	        }

			return Rect.MinMaxRect(minX - padding, maxY + padding, maxX + padding, minY - padding);
		}

    /// <summary>
    /// Calculates a camera position given the a bounding box containing all the targets.
    /// </summary>
    /// <param name="boundingBox">A Rect bounding box containg all targets.</param>
    /// <returns>A Vector3 in the center of the bounding box.</returns>
    Vector3 CalculateCameraPosition(Rect boundingBox)
    {
        Vector2 boundingBoxCenter = boundingBox.center;

        return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, _camera.transform.position.z);
    }

    /// <summary>
    /// Calculates a new orthographic size for the camera based on the target bounding box.
    /// </summary>
    /// <param name="boundingBox">A Rect bounding box containg all targets.</param>
    /// <returns>A float for the orthographic size.</returns>
    float CalculateOrthographicSize(Rect boundingBox)
    {
        float orthographicSize = _camera.orthographicSize;
        Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
        Vector3 topRightAsViewport = _camera.WorldToViewportPoint(topRight);

        if (topRightAsViewport.x >= topRightAsViewport.y)
            orthographicSize = Mathf.Abs(boundingBox.width) / _camera.aspect / 2f;
        else
            orthographicSize = Mathf.Abs(boundingBox.height) / 2f;

        return Mathf.Clamp(Mathf.Lerp(_camera.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed.Value), minimumOrthographicSize.Value, Mathf.Infinity);
    }
		
	}
}
