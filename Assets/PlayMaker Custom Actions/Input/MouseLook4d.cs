// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Made by djay dino

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

[ActionCategory(ActionCategory.Input)]
[Tooltip("rotate on the z axis looking to the mouse position")]
public class MouseLook4d : FsmStateAction
{
		[RequiredField]
		[Tooltip("The GameObject to rotate.")]
		public FsmOwnerDefault gameObject;


		[HasFloatSlider(-180,180)]
		public FsmFloat minimumZ;


		[HasFloatSlider(-180, 180)]
		public FsmFloat maximumZ;
		
		[Tooltip("Set the GameObject starting offset. In degrees. 0 if your object is facing right, 180 if facing left etc...")]
		public FsmFloat rotationOffset;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("the Z rotation")]
		public FsmFloat zRotation;
		
		public bool onlyGetValue;
		
		public override void Reset()
		{
		minimumZ = null;
		maximumZ = null;
		onlyGetValue = false;
		rotationOffset = 0;
		}
		
	// Code that runs on entering the state.
	public override void OnEnter()
	{			
		var go = Fsm.GetOwnerDefaultTarget(gameObject);
		if (go == null)
		{
			return;
		}
		// Make the rigid body not change rotation
		// TODO: Original Unity script had this. Expose as option?
	    var rigidbody2d = go.GetComponent<Rigidbody2D>();
           if (rigidbody2d != null)
		{
			rigidbody2d.freezeRotation = true;
		}		
	}

	// Code that runs every frame.
	public override void OnUpdate()
	{
	DoMouseLook2d();
	}
	
	void DoMouseLook2d()
	{
		var go = Fsm.GetOwnerDefaultTarget(gameObject);
		if (go == null)
		{
			return;
		}
		var transform = go.transform;
		
		var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

				// Clamp function that respects IsNone
			if (!minimumZ.IsNone && angle < minimumZ.Value)
			{
				angle = minimumZ.Value;
			}

			if (!maximumZ.IsNone && angle > maximumZ.Value)
			{
				angle = maximumZ.Value;
			}
			zRotation.Value = angle - rotationOffset.Value;
			
			if(!onlyGetValue)
			{
				transform.rotation = Quaternion.Euler(0, 0, angle - rotationOffset.Value);
			}
			
			
	}
}
}
