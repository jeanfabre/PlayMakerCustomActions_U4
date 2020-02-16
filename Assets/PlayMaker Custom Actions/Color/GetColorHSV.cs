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
	[Tooltip("Get the HSV values from a color.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1146")]
	public class GetColorHSV : FsmStateAction
	{
		
		[RequiredField]
		[Tooltip("The color source")]
		public FsmColor colorSource;
		
		[ActionSection("Result")]
		[UIHint(UIHint.Variable)]
		[Tooltip("The hsv values of the color source")]
		public FsmVector3 HSV;

		[UIHint(UIHint.Variable)]
		[Tooltip("The Hue value of the color source")]
		public FsmFloat hue;

		[UIHint(UIHint.Variable)]
		[Tooltip("The Saturation value of the color source")]
		public FsmFloat saturation;

		[UIHint(UIHint.Variable)]
		[Tooltip("The Value value of the color source")]
		public FsmFloat value;

		[UIHint(UIHint.Variable)]
		[Tooltip("The alpha value of the color source")]
		public FsmFloat alpha;
		
		[Tooltip("Repeat every frame")]
		public bool everyframe;
		
		public override void Reset()
		{
			colorSource = null;
			
			HSV = new FsmVector3(){UseVariable=true};
			hue = new FsmFloat(){UseVariable=true};
			saturation = new FsmFloat(){UseVariable=true};
			value = new FsmFloat(){UseVariable=true};
			alpha = new FsmFloat(){UseVariable=true};
			
			everyframe = false;
		}

		public override void OnEnter()
		{
			
			GetHSVColor();
			
			if (!everyframe)
			{
				Finish();
			}
		}
		
		public override void OnUpdate()
		{
			GetHSVColor();
		}
		
		private void GetHSVColor()
		{
			ColorHSV colorhsv = new ColorHSV(colorSource.Value);
			
			HSV.Value = new Vector3(colorhsv.h,colorhsv.s,colorhsv.v);
			
			hue.Value = colorhsv.h;
			saturation.Value = colorhsv.s;
			value.Value = colorhsv.v;
			
			alpha.Value = colorhsv.a;
		}
		
	}
}
