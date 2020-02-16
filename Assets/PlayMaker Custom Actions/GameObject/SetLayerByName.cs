// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Sets a Game Object's Layer using a string reference for the layer.")]
	public class SetLayerByName : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		public FsmString layer;
		
		public FsmEvent layerNotFoundEvent;
			
		public override void Reset()
		{
			gameObject = null;
			layer = "Default";
			layerNotFoundEvent = null;
		}

		public override void OnEnter()
		{
			DoSetLayer();
			
			Finish();
		}

		void DoSetLayer()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null) return;
			
			try
			{
				int _layer = LayerMask.NameToLayer(layer.Value);
				if (_layer!=-1)
				{
					go.layer = _layer;	
					return;
				}
			}catch(System.Exception e)
			{
				Debug.LogWarning("<"+layer.Value+"> is not a valid layer: "+e.Message);
			}
			
			Fsm.Event(layerNotFoundEvent);
		}

	}
}
