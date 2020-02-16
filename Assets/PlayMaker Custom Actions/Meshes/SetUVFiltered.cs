// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System;
using HutongGames.PlayMaker;

using Tooltip = HutongGames.PlayMaker.TooltipAttribute;

[ActionCategory("Meshes")]
[Tooltip("Set UV values, only for uv's matching the base value. If all Base properties are set to none, all UV's are set.")]
public class SetUVFiltered : FsmStateAction
{
	[RequiredField]
	[CheckForComponent(typeof(MeshFilter))]
	public FsmOwnerDefault gameObject;
	
	public FsmBool affectSharedMesh;
	
	public FsmVector2 baseUV;
	public FsmFloat baseU;
	public FsmFloat baseV;
	
	public FsmVector2 targetUV;
	public FsmFloat targetU;
	public FsmFloat targetV;
	
	
	
	public bool everyFrame;
	
	Mesh _mesh;
	Vector2[] _uvs;
	
	

	public override void Reset()
	{
		gameObject = null;
		
		affectSharedMesh = false;
		
		baseUV = new FsmVector2(){UseVariable=true};
		baseU = new FsmFloat(){UseVariable=true};
		baseV = new FsmFloat(){UseVariable=true};
		
		targetUV = null;
		targetU = new FsmFloat(){UseVariable=true};
		targetV = new FsmFloat(){UseVariable=true};
			
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
		
		if (affectSharedMesh.Value)
		{
			_mesh = go.GetComponent<MeshFilter>().sharedMesh;
		}else{
			_mesh = go.GetComponent<MeshFilter>().mesh;
		}
			
		if (_mesh == null)
		{
			LogError("Missing Mesh!");
			Finish();
			return;
		}
		
		DoSetUV();
		
		if (!everyFrame)
		{
			Finish();
		}
	}
	
	public override void OnUpdate ()
	{
		DoSetUV();
	}
	
	void DoSetUV()
	{
		if (_mesh == null)
		{
			return;
		}
	
		Vector2 _baseUV = Vector2.zero;
		
		bool ignoreBase = baseUV.IsNone && baseU.IsNone && baseV.IsNone;
		
		_uvs = _mesh.uv;
		
		if (!baseUV.IsNone)
		{
			_baseUV = baseUV.Value;
		}
		if (!baseU.IsNone)
		{
			_baseUV.x = baseU.Value;
		}
		if (!baseV.IsNone)
		{
			_baseUV.y = baseV.Value;
		}
		
		
		Vector2 _targetUV = Vector2.zero;
		
		if (!targetUV.IsNone)
		{
			_targetUV = targetUV.Value;
		}
		if (!targetU.IsNone)
		{
			_targetUV.x = targetU.Value;
		}
		if (!targetV.IsNone)
		{
			_targetUV.y = targetV.Value;
		}
		

	    for(int i = 0; i < _uvs.Length; i++)
		{	
			if( ignoreBase || _uvs[i].Equals(_baseUV) )
			{
				_uvs[i] = _targetUV;
			}
		}
	
		
	   // Apply the modded UV's 
	   _mesh.uv = _uvs; 
	}
}
