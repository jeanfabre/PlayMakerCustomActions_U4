// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Shuriken")]
	[Tooltip("Stops a particleSystem")]
	public class ParticleSystemStop : FsmStateAction
	{
		[RequiredField]
		[Tooltip("the GameObject with the particleSystem to stop")]
		[CheckForComponent(typeof(ParticleSystem))]
		public FsmOwnerDefault gameObject;
		
		[Tooltip("start playing again when state exits")]
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
			
			_ps.Stop();
			
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