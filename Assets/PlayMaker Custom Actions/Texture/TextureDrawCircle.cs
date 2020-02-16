// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
 
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Texture")]
	[Tooltip("Draw a circle on a texture.")]
	public class TextureDrawCircle : FsmStateAction
	{
		[RequiredField]
		public FsmTexture texture;
		
		[RequiredField]
		public FsmVector2 center;

		
		[RequiredField]
		public FsmInt radius;
		
		public FsmColor color;

		public bool everyFrame;

		public override void Reset()
		{
			texture = null;
			center = null;
			radius = 1;
			color = Color.red;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoDrawTextureCircle((Texture2D)texture.Value,(int)center.Value.x,(int)center.Value.y,radius.Value,color.Value);
			
			if (!everyFrame)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			DoDrawTextureCircle((Texture2D)texture.Value,(int)center.Value.x,(int)center.Value.y,radius.Value,color.Value);
		}
		
		
		public void DoDrawTextureCircle(Texture2D tex, int cx, int cy, int r, Color col)
	    {
			//Color32 _col = (Color32)col;
			
	       int x, y, px, nx, py, ny, d;
			
	      // Color32[] tempArray = tex.GetPixels32();
	 
	       for (x = 0; x <= r; x++)
	       {
	         d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
	         for (y = 0; y <= d; y++)
	         {
	          px = cx + x;
	          nx = cx - x;
	          py = cy + y;
	          ny = cy - y;
	 
			 tex.SetPixel(px, py, col);
          	tex.SetPixel(nx, py, col);
 
         	 tex.SetPixel(px, ny, col);
         	 tex.SetPixel(nx, ny, col);
					
					
	         // tempArray[py*1024 + px] = _col;
	         // tempArray[py*1024 + nx] = _col;
	         // tempArray[ny*1024 + px] = _col;
	       //   tempArray[ny*1024 + nx] = _col;
	         }
	       } 
	     //  tex.SetPixels32(tempArray);
	       tex.Apply ();
	    }

	}
}
