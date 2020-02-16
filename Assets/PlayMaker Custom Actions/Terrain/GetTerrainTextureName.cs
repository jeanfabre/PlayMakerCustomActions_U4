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
	public class GetTerrainTextureName : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to look under")]
		public FsmOwnerDefault texturePosition;
		
		[RequiredField]
		[Tooltip("The terrain")]
		public Terrain terrain;
		
		[RequiredField]
		[Tooltip("The texture name")]
		[UIHint(UIHint.Variable)]
		public FsmString mainTexture;
		
		public override void Reset ()
		{
			texturePosition = null;
			terrain = null;
			mainTexture = null;
		}
		
		public override void OnEnter ()
		{
			
			ExecuteAction();
			
			Finish();
		}
		
		public void ExecuteAction () 
		{
			var go = Fsm.GetOwnerDefaultTarget(texturePosition); // our game object
			
			TerrainData terrainData = terrain.terrainData; //terrain data
			var terrainPos = terrain.transform.position; //terrain position
			
			float x_floated = ((go.transform.position.x - terrainPos.x) / terrainData.size.x) * terrainData.alphamapWidth; // get terrain x
			float z_floated = ((go.transform.position.z - terrainPos.z) / terrainData.size.z) * terrainData.alphamapHeight; //get terrain y
			
			if (x_floated<0.0f || z_floated<0.0f)
			{
				mainTexture.Value = null;
				Finish();
			}
			
			else
			{
			int x = Mathf.RoundToInt(x_floated);
			int z = Mathf.RoundToInt(z_floated);
			
			float [,,] splaptexture = terrainData.GetAlphamaps(x,z,1,1);
			
			for ( var i = 0 ; i < terrainData.splatPrototypes.Length ; i ++ ) //for each splat map in terrain get his intensity value
			{
				if(splaptexture [0,0,i] > 0.5f) // if splat map intensity is more than 0.5f we got our the main splat map
				{
					mainTexture.Value = terrainData.splatPrototypes[i].texture.name;
				}
			}
			}
		}
	}
}
