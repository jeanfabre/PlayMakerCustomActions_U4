// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Shuriken")]
	[Tooltip("Pauses a particleSystem")]
	public class ParticleSystemPause : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject with the particleSystem to pause")]
		[CheckForComponent(typeof(ParticleSystem))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("Plays again when state exits")]
		public FsmBool playOnExit;

		ParticleSystem _ps;

		public override void Reset()
		{
			gameObject = null;
			playOnExit = null;
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
			
			_ps.Pause();
			
			Finish();
		}
		
		public override void OnExit()
		{
			if (_ps!=null && playOnExit.Value)
			{
				_ps.Play();
			}
		}
	}
}