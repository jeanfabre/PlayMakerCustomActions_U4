// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Terrain")]
	[Tooltip("Get Terrain Splat Texture Map Name over Game Object Position.")]
	public class GetTerrainSampleHeight : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to sample height from")]
		public FsmOwnerDefault GameObject;

		[Tooltip("Set GameObject to none and use this position. Else It will be on offset from GameObject world position")]
		public FsmVector3 position;
		
		[RequiredField]
		[Tooltip("The terrain. Leave to null to pick the active one")]
		public Terrain terrain;
		
		[RequiredField]
		[Tooltip("The texture name")]
		[UIHint(UIHint.Variable)]
		public FsmFloat height;

		[Tooltip("Repeat everyframe")]
		public bool everyframe;

		GameObject _go;

		Vector3 _position;

		Terrain _terrain;

		public override void Reset ()
		{
			GameObject = null;
			position = null;
			terrain = null;
			height = null;
		}
		
		public override void OnEnter ()
		{
			
			ExecuteAction();
			
			Finish();
		}
		
		public void ExecuteAction () 
		{
			_position = Vector3.zero;

			_go = Fsm.GetOwnerDefaultTarget(GameObject); // our game object

			if (_go != null) {
				_position = _go.transform.position;
			}

			if (!position.IsNone) {
				_position += position.Value;
			}

			if (terrain == null) {
				height.Value = Terrain.activeTerrain.SampleHeight(_position);
			} else {
				height.Value = terrain.SampleHeight(_position);
			}

		}
	}
}
