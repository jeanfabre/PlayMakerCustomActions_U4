// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
//--- __ECO__ __PLAYMAKER__ __ACTION__ ---//

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Shuriken")]
	[Tooltip("Plays All particles from a GameObject. Can include All children as well")]
	public class ParticleSystemPlayAll : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject with particles")]
		public FsmOwnerDefault gameObject;
		

		[Tooltip("Play All particles even on children")]
		public FsmBool includeChilden;

		
		public override void Reset()
		{
			gameObject = null;
			includeChilden = true;
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				foreach(ParticleSystem p in go.GetComponents<ParticleSystem>())
				{
					p.Play();
				}

				if (includeChilden.Value)
				{
					foreach(ParticleSystem p in go.GetComponentsInChildren<ParticleSystem>())
					{
						p.Play();
					}
				}
			}

			Finish();
		}

	}
}