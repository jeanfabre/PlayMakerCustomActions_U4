// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Aligns a GameObject to another using a proxy( relative to that proxy basically)")]
	public class AlignTransformUsingProxy : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		public FsmGameObject target;

		[RequiredField]
		public FsmGameObject proxy;
		
		public override void Reset()
		{
			gameObject = null;
			target = null;
			proxy = null;
		}
		
		public override void OnEnter()
		{
			DoAlignTransformUsingProxy();

			Finish();		
		}

		void DoAlignTransformUsingProxy()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if(go == null) return;

			GameObject goTarget = target.Value;
			if (goTarget == null) return;

			GameObject goProxy = proxy.Value;
			if (goProxy == null) return;

			Transform _go = go.transform;
			Transform _target = goTarget.transform;
			Transform _proxy = goProxy.transform;


			Transform proxy_parent = _proxy.parent;
			Transform go_parent = _go.parent;

			_proxy.parent = null;
			_go.parent = _proxy;

			_proxy.position = _target.position;
			_proxy.rotation = _target.rotation;

			_go.parent = go_parent;

			_proxy.parent = proxy_parent;

			//Quaternion _go_proxy_delta_q = Quaternion.FromToRotation(_proxy.transform.forward,go.transform.forward);
			
			
			//go.transform.rotation = _go_proxy_delta_q * _target.transform.rotation;


			//Vector3 _proxy_go_delta = _target.transform.InverseTransformPoint(_proxy.transform.position);

			//go.transform.Translate(-_proxy_go_delta);





		}
	}
}

