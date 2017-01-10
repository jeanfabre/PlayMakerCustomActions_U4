// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// This Action requires ArrayMaker: https://hutonggames.fogbugz.com/default.asp?W715
// original action http://hutonggames.com/playmakerforum/index.php?topic=10258.msg48436#msg48436


using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Sets the value of a Color Variable by Hex -  You can use 6 hex = RGB or hex + apha = RGBA")]
	public class SetHexColorValue : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmColor colorVariable;
		[UIHint(UIHint.FsmString)]
		[Tooltip("6 Hex without alpha or 8 Hex with alpha")]
		public FsmString HexInput;


		public override void Reset()
		{
			colorVariable = null;
			HexInput = "";

		}

		public override void OnEnter()
		{

			if (colorVariable == null)
			{
				Debug.LogError("!!! Missing color variable");
				Finish ();
			}

			DoSetColorValue();

		}

		void DoSetColorValue()
		{
			if (!string.IsNullOrEmpty(HexInput.Value) && HexInput.Value.Length == 6)
			{
				string hex = HexInput.Value;

				hex = hex.Replace ("0x", "");
				hex = hex.Replace ("#", "");

				byte a = 255;
				byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
				byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
				byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);

				if(hex.Length == 8)
				{

				a = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
				
				}

				colorVariable.Value = new Color32 (r,g,b,a);


			}

			Finish ();
		}
	}
}