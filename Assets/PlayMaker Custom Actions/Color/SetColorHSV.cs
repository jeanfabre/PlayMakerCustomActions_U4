// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":[
						"Assets/PlayMaker Custom Actions/Color/ColorHSV.cs"
					]
}
EcoMetaEnd
---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Set a color based on HSV values")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1147")]
	public class SetColorHSV : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The hsv color source")]
		public FsmVector3 HSV;
		
		[Tooltip("The Hue value source, overrides HSV Hue if set")]
		public FsmFloat hue;
		
		[Tooltip("The Saturation value source, overrides HSV Saturation if set")]
		public FsmFloat saturation;
		
		[Tooltip("The value value source, overrides HSV Value if set")]
		public FsmFloat value;
		
		[Tooltip("The alpha value source")]
		public FsmFloat alpha;

		[ActionSection("Result")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting color")]
		public FsmColor colorResult;
		
		[Tooltip("Repeat every frame")]
		public bool everyframe;
		
		public override void Reset()
		{
			
			
			HSV = new FsmVector3(){UseVariable=true};
			hue = new FsmFloat(){UseVariable=true};
			saturation = new FsmFloat(){UseVariable=true};
			value = new FsmFloat(){UseVariable=true};
			alpha = new FsmFloat(){UseVariable=true};
			
			colorResult = null;
			
			
			everyframe = false;
		}

		public override void OnEnter()
		{
			
			SetHSVColor();
			
			if (!everyframe)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			SetHSVColor();
		}
		
		private void SetHSVColor()
		{
		
			ColorHSV colorhsv = new ColorHSV(HSV.Value.x,HSV.Value.y,HSV.Value.z,alpha.Value);
			
			if (!hue.IsNone)
			{
				colorhsv.h = hue.Value;
			}
			if (!saturation.IsNone)
			{
				colorhsv.s = saturation.Value;
			}
			if (!value.IsNone)
			{
				colorhsv.v = value.Value;
			}
			
			colorResult.Value = colorhsv.ToColor();
			
		}
		
	}
}
