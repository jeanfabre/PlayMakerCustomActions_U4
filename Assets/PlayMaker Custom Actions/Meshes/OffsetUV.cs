// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;
using HutongGames.PlayMaker;

using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Meshes")]
[Tooltip("Offset Mesh UV")]
public class OffsetUV : FsmStateAction
{
	[RequiredField]
	[CheckForComponent(typeof(MeshFilter))]
	public FsmOwnerDefault gameObject;
	
	public FsmBool affectSharedMesh;
	
	public FsmVector2 offset;
	
	public FsmFloat offsetX;
	public FsmFloat offsetY;
	
	
	public bool everyFrame;
	
	Mesh _mesh;
	Vector2[] _uvs;
	
	

	public override void Reset()
	{
		gameObject = null;
		offset = null;
		offsetX = null;
		offsetY = null;
		everyFrame = false;
	}

	public override void OnEnter ()
	{
		var go = Fsm.GetOwnerDefaultTarget(gameObject);
		if (go == null)
		{
			Finish();
			return;
		}
		
		_mesh = (go.GetComponent<MeshFilter>() as MeshFilter).mesh;
		
			
		if (_mesh == null)
		{
			LogError("Missing Mesh!");
			Finish();
			return;
		}

		  // Build the uvs 
	   	_uvs = new Vector2[_mesh.vertices.Length]; 
	    _uvs = _mesh.uv; // Take the existing UVs 
	
		
		
		DoOffsetUV();
		
		if (!everyFrame)
		{
			Finish();
		}
	}
	
	public override void OnUpdate ()
	{
		DoOffsetUV();
	}
	
	private float _xOffset = 0f;
	private float _yOffset = 0f;
	
	void DoOffsetUV()
	{
		if (_mesh == null)
		{
			return;
		}
	
		Vector2[] _new_uvs = new Vector2[_uvs.Length]; 
	
		float xOffset = offsetX.Value;
		float yOffset = offsetY.Value;
		if (! offset.IsNone)
		{
			xOffset += offset.Value.x;
			yOffset += offset.Value.y;
		}
		
		if (xOffset.Equals(_xOffset) && yOffset.Equals(_yOffset))
		{
			// no change.
			return;
		}
		_xOffset = xOffset;
		_yOffset = yOffset;

	    for(int i = 0; i < _uvs.Length; i++)
		{
			_new_uvs[i] = new Vector2 (_uvs[i].x + _xOffset, _uvs[i].y + _yOffset); // Offset all the UV's 
		}
	
	   // Apply the modded UV's 
	   _mesh.uv = _new_uvs; 
	}
}
