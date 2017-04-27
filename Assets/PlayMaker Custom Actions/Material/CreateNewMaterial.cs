// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
// __ECO__ __PLAYMAKER__ __ACTION__


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
	
{
	[ActionCategory (ActionCategory.Material)]
	public class CreateNewMaterial : FsmStateAction
	{
		[Tooltip("The Name of the Shader to create the Material from")]
		public FsmString shader;

		[UIHint(UIHint.Variable)]
		[Tooltip("The newly created Material")]
		public FsmMaterial NewMaterial;

		[Tooltip("Event sent if shader reference not found")]
		public FsmEvent ShaderNotFound;

		Shader _s;

		public override void Reset()
		{
			shader = "Diffuse";

			NewMaterial = null;
			ShaderNotFound = null;
		}
		
		
		public override void OnEnter()
		{
			_s = Shader.Find(shader.Value);
			if (_s==null)
			{
				this.Fsm.Event(ShaderNotFound);
			}else{
				NewMaterial.Value = new Material(_s);
			}
			Finish ();
		}
	}
}