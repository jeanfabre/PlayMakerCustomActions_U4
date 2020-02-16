// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/__Internal/FsmStateActionAdvanced.cs"
					]
}
EcoMetaEnd
---*/
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("RectTransform")]
	[Tooltip("Set the local scale of this RectTransform.")]
	public class RectTransformSetLocalScale : FsmStateActionAdvanced
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		[Tooltip("The GameObject target.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The scale. Set to none for no effect")]
		public FsmVector3 scale;
		
		[Tooltip("The x component of the scale. Set to none for no effect")]
		public FsmFloat x;
		
		[Tooltip("The y component of the scale. Set to none for no effect")]
		public FsmFloat y;

		[Tooltip("The z component of the scale. Set to none for no effect")]
		public FsmFloat z;
		
		RectTransform _rt;
		
		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			scale = new FsmVector3(){UseVariable=true};
			x = new FsmFloat(){UseVariable=true};
			y = new FsmFloat(){UseVariable=true};
			z = new FsmFloat(){UseVariable=true};
		}
		
		public override void OnEnter()
		{
			GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_rt = go.GetComponent<RectTransform>();
			}
			
			DoSetValues();
			
			if (!everyFrame)
			{
				Finish();
			}		
		}
		
		public override void OnActionUpdate()
		{
			DoSetValues();
		}
		
		void DoSetValues()
		{
			if (_rt==null)
			{
				return;
			}

			Vector3 _rot = _rt.localScale;

			if (!scale.IsNone) _rot = scale.Value;

			if (!x.IsNone) _rot.x = x.Value;
			if (!y.IsNone) _rot.y = y.Value;
			if (!z.IsNone) _rot.z = z.Value;

			_rt.localScale = _rot;

		}
	}
}