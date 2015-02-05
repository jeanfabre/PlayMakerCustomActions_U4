// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Shuriken")]
	[Tooltip("Plays a particleSystem")]
	public class ParticleSystemPlay : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject with the particleSystem to play")]
		[CheckForComponent(typeof(ParticleSystem))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Stop playing again when state exits")]
		public FsmBool stopOnExit;

		ParticleSystem _ps;

		public override void Reset()
		{
			gameObject = null;
			stopOnExit = null;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go == null)
			{
				return;
			}
			_ps = go.GetComponent<ParticleSystem>();
			if (_ps == null) 
			{
				return;
			}
			
			_ps.Play();
			
			Finish();
		}
		
		public override void OnExit()
		{
			if (_ps!=null && stopOnExit.Value)
			{
				_ps.Stop();
			}
		}
	}
}