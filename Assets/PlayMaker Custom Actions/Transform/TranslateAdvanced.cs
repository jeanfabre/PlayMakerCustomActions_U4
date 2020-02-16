// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
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
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Translates a Game Object. Use a Vector3 and/or Vector2 variable and/or XYZ components. To leave any axis unchanged, set variable to 'None'.")]
	public class TranslateAdvanced : FsmStateActionAdvanced
	{
		[RequiredField]
		[Tooltip("The game object to translate.")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		[Tooltip("A translation vector3. NOTE: You can override individual axis below.")]
		public FsmVector3 vector;

		[UIHint(UIHint.Variable)]
		[Tooltip("A translation vector2. NOTE: You can override individual axis below.")]
		public FsmVector2 vector2;

		[Tooltip("Translation along x axis.")]
		public FsmFloat x;
		
		[Tooltip("Translation along y axis.")]
		public FsmFloat y;
		
		[Tooltip("Translation along z axis.")]
		public FsmFloat z;
		
		[Tooltip("Translate in local or world space.")]
		public Space space;
		
		[Tooltip("Translate over one second")]
		public bool perSecond;

		[Tooltip("is translation per seconds, uses realtime ( unaffected by time scale)")]
		public bool realtime;

	
		Transform _transform;

		public override void Reset()
		{
			base.Reset();
			gameObject = null;
			vector = null;
			vector2 = new FsmVector2(){UseVariable=true};
			// default axis to variable dropdown with None selected.
			x = new FsmFloat { UseVariable = true };
			y = new FsmFloat { UseVariable = true };
			z = new FsmFloat { UseVariable = true };
			space = Space.Self;
			perSecond = true;

		}
		
		public override void OnEnter()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (go != null)
			{
				_transform = go.GetComponent<Transform>();
			}

			DoTranslate();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnActionUpdate()
		{
			DoTranslate();
		}


		void DoTranslate()
		{
			if (_transform==null)
			{
				return;
			}

			
			// Use vector if specified
			
			var translate = vector.IsNone ? new Vector3(x.Value, y.Value, z.Value) : vector.Value;

			if (!vector2.IsNone)
			{
				translate.x = vector2.Value.x;
				translate.y = vector2.Value.y;
			}
			// override any axis
			
			if (!x.IsNone) translate.x = x.Value;
			if (!y.IsNone) translate.y = y.Value;
			if (!z.IsNone) translate.z = z.Value;
			
			// apply
			
			if (!perSecond)
			{
				_transform.Translate(translate, space);
			}
			else
			{
				if ( realtime)
				{
					_transform.Translate(translate * Time.unscaledDeltaTime, space);
				}else{
					_transform.Translate(translate * Time.deltaTime, space);
				}

			}
		}
		
	}
}
