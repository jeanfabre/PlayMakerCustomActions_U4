// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// This Action requires ArrayMaker: https://hutonggames.fogbugz.com/default.asp?W715
// original action http://hutonggames.com/playmakerforum/index.php?topic=10256.msg48427#msg48427


using UnityEngine;
using HutongGames.PlayMaker;

namespace HutongGames.PlayMaker.Actions
{
[ActionCategory(ActionCategory.Renderer)]
	[Tooltip("Set any renderer Sorting Layer (Sprite, Particle, Mesh, etc).")]
	public class SetSortingRendererLayer : FsmStateAction
{

		[ActionSection("Setup")]
		[Tooltip("The GameObject that owns the Renderer.")]
		public FsmOwnerDefault gameObject;
		[Tooltip("If more than one renderer on gameobject then select Renderer Object.")]
		[TitleAttribute("or select Renderer Object")]
		public Renderer gameObjectRenderer;

		[ActionSection("Sorting Layer Setup")]
		[Tooltip("Select Sorting layer name.")]
		[TitleAttribute("Renderer Layer Name")]
		[UIHint(UIHint.FsmString)]
		public FsmString layerString;
		[Tooltip("Edit Sorting Order in Layer")]
		[TitleAttribute("Edit Sorting Order")]
		[UIHint(UIHint.FsmInt)]
		public FsmInt layerOrder;


	public override void Reset()
		{
			gameObject = null;

			layerOrder = 0;
			
		}

	public override void OnEnter()
	{
		
			var gos = Fsm.GetOwnerDefaultTarget(gameObject);
			if (gos == null)
			{
				Debug.LogWarning("missing gameObject: "+ gos.name);
				return;
			}


			if (gameObjectRenderer != null)
			RenderSetupObj();

			if (gameObjectRenderer == null)
			RenderSetup();
		
			Finish();

	}

		void RenderSetup(){

			Renderer _target = Fsm.GetOwnerDefaultTarget(gameObject).gameObject.GetComponent<Renderer>();

			if (_target == null)
			{
				Debug.LogWarning("missing renderer: "+ Fsm.GetOwnerDefaultTarget(gameObject).name);
				Finish();
				return;
			}

			
			if (layerString.IsNone || string.IsNullOrEmpty (layerString.Value) || layerString.Value == null){

				_target.sortingOrder = layerOrder.Value;
				
			}

		else 

			_target.sortingLayerName = layerString.Value;
			_target.sortingOrder = layerOrder.Value;
				

			Finish();

		}

		void RenderSetupObj(){

			if (gameObjectRenderer == null)
			{
				Debug.LogWarning("missing renderer: "+ gameObjectRenderer.name);
				Finish();
				return;
			}

			if (layerString.IsNone || string.IsNullOrEmpty (layerString.Value) || layerString.Value == null){
	
				gameObjectRenderer.sortingOrder = layerOrder.Value;
			}

			else 

				gameObjectRenderer.sortingLayerName = layerString.Value;
				gameObjectRenderer.sortingOrder = layerOrder.Value;


			Finish();
			
		}


}
}
