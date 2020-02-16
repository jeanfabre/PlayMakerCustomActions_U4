// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__
EcoMetaStart
{
"script dependancies":["Assets/PlayMaker Custom Actions/Color/Editor/GetCSS3ColorEditor.cs"]
}
EcoMetaEnd
---*/


using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Pick CSS3 color, optionnaly set the alpha as well: http://www.w3.org/TR/css3-color/#svg-color ")]
	public class GetCSS3Color : FsmStateAction
	{
		public int colorindex = 0;

		[Tooltip("The CSS3 Color Name")]
		public FsmString colorName;

		[Tooltip("Set the alpha, leave to none for no effect")]
		[HasFloatSlider(0, 1)]
		public FsmFloat alpha;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The random Color")]
		public FsmColor storeResult;

		[Tooltip("repeat every frame")]
		public bool everyframe;

		Color _color;

		public override void Reset()
		{
			colorName = "AliceBlue";
			alpha = new FsmFloat(){UseVariable=true};

			storeResult = null;
		}
		
		public override void OnEnter()
		{
			SetColor();

			if(!everyframe)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			SetColor();
		}

		void SetColor()
		{

			if (CSS3Colors.ContainsKey(colorName.Value))
			{
				_color = CSS3Colors[colorName.Value];
				if (!alpha.IsNone)
				{
					_color.a = alpha.Value;
				}


				storeResult.Value = _color;
			}
		}


		
		// https://gist.github.com/LotteMakesStuff/f7ce43f11e545a151b95b5e87f76304c
		static public Dictionary<string,Color>  CSS3Colors = new Dictionary<string, Color>()
		{
		{"AliceBlue",new Color32(240,248,255,255)},
		{"AntiqueWhite",new Color32(250,235,215,255)},
		{"Aqua", new Color32(0,255,255,255)},
		{"Aquamarine",new Color32(127,255,212,255)},
		{"Azure",new Color32(240,255,255,255)},
		{"Beige",new Color32(245,245,220,255)},
		{"Bisque",new Color32(255,228,196,255)},
		{"Black",new Color32(0,0,0,255)},
		{"BlanchedAlmond",new Color32(255,235,205,255)},
		{"Blue",new Color32(0,0,255,255)},
		{"BlueViolet",new Color32(138,43,226,255)},
		{"Brown",new Color32(165,42,42,255)},
		{"Burlywood",new Color32(222,184,135,255)},
		{"CadetBlue",new Color32(95,158,160,255)},
		{"Chartreuse",new Color32(127,255,0,255)},
		{"Chocolate",new Color32(210,105,30,255)},
		{"Coral",new Color32(255,127,80,255)},
		{"CornflowerBlue",new Color32(100,149,237,255)},
		{"Cornsilk",new Color32(255,248,220,255)},
		{"Crimson",new Color32(220,20,60,255)},
		{"Cyan",new Color32(0,255,255,255)},
		{"DarkBlue",new Color32(0,0,139,255)},
		{"DarkCyan",new Color32(0,139,139,255)},
		{"DarkGoldenrod",new Color32(184,134,11,255)},
		{"DarkGray",new Color32(169,169,169,255)},
		{"DarkGreen",new Color32(0,100,0,255)},
		{"DarkKhaki",new Color32(189,183,107,255)},
		{"DarkMagenta",new Color32(139,0,139,255)},
		{"DarkOliveGreen",new Color32(85,107,47,255)},
		{"DarkOrange",new Color32(255,140,0,255)},
		{"DarkOrchid",new Color32(153,50,204,255)},
		{"DarkRed",new Color32(139,0,0,255)},
		{"DarkSalmon",new Color32(233,150,122,255)},
		{"DarkSeaGreen",new Color32(143,188,143,255)},
		{"DarkSlateBlue",new Color32(72,61,139,255)},
		{"DarkSlateGray",new Color32(47,79,79,255)},
		{"DarkTurquoise",new Color32(0,206,209,255)},
		{"DarkViolet",new Color32(148,0,211,255)},
		{"DeepPink",new Color32(255,20,147,255)},
		{"DeepSkyBlue",new Color32(0,191,255,255)},
		{"DimGray",new Color32(105,105,105,255)},
		{"DodgerBlue",new Color32(30,144,255,255)},
		{"FireBrick",new Color32(178,34,34,255)},
		{"FloralWhite",new Color32(255,250,240,255)},
		{"ForestGreen",new Color32(34,139,34,255)},
		{"Fuchsia",new Color32(255,0,255,255)},
		{"Gainsboro",new Color32(220,220,220,255)},
		{"GhostWhite",new Color32(248,248,255,255)},
		{"Gold",new Color32(255,215,0,255)},
		{"Goldenrod",new Color32(218,165,32,255)},
		{"Gray",new Color32(128,128,128,255)},
		{"Green",new Color32(0,128,0,255)},
		{"GreenYellow",new Color32(173,255,47,255)},
		{"Honeydew",new Color32(240,255,240,255)},
		{"HotPink",new Color32(255,105,180,255)},
		{"IndianRed",new Color32(205,92,92,255)},
		{"Indigo",new Color32(75,0,130,255)},
		{"Ivory",new Color32(255,255,240,255)},
		{"Khaki",new Color32(240,230,140,255)},
		{"Lavender",new Color32(230,230,250,255)},
		{"Lavenderblush",new Color32(255,240,245,255)},
		{"LawnGreen",new Color32(124,252,0,255)},
		{"LemonChiffon",new Color32(255,250,205,255)},
		{"LightBlue",new Color32(173,216,230,255)},
		{"LightCoral",new Color32(240,128,128,255)},
		{"LightCyan",new Color32(224,255,255,255)},
		{"LightGoldenodYellow",new Color32(250,250,210,255)},
		{"LightGray",new Color32(211,211,211,255)},
		{"LightGreen",new Color32(144,238,144,255)},
		{"LightPink",new Color32(255,182,193,255)},
		{"LightSalmon",new Color32(255,160,122,255)},
		{"LightSeaGreen",new Color32(32,178,170,255)},
		{"LightSkyBlue",new Color32(135,206,250,255)},
		{"LightSlateGray",new Color32(119,136,153,255)},
		{"LightSteelBlue",new Color32(176,196,222,255)},
		{"LightYellow",new Color32(255,255,224,255)},
		{"Lime",new Color32(0,255,0,255)},
		{"LimeGreen",new Color32(50,205,50,255)},
		{"Linen",new Color32(250,240,230,255)},
		{"Magenta",new Color32(255,0,255,255)},
		{"Maroon",new Color32(128,0,0,255)},
		{"MediumAquamarine",new Color32(102,205,170,255)},
		{"MediumBlue",new Color32(0,0,205,255)},
		{"MediumOrchid",new Color32(186,85,211,255)},
		{"MediumPurple",new Color32(147,112,219,255)},
		{"MediumSeaGreen",new Color32(60,179,113,255)},
		{"MediumSlateBlue",new Color32(123,104,238,255)},
		{"MediumSpringGreen",new Color32(0,250,154,255)},
		{"MediumTurquoise",new Color32(72,209,204,255)},
		{"MediumVioletRed",new Color32(199,21,133,255)},
		{"MidnightBlue",new Color32(25,25,112,255)},
		{"Mintcream",new Color32(245,255,250,255)},
		{"MistyRose",new Color32(255,228,225,255)},
		{"Moccasin",new Color32(255,228,181,255)},
		{"NavajoWhite",new Color32(255,222,173,255)},
		{"Navy",new Color32(0,0,128,255)},
		{"OldLace",new Color32(253,245,230,255)},
		{"Olive",new Color32(128,128,0,255)},
		{"Olivedrab",new Color32(107,142,35,255)},
		{"Orange",new Color32(255,165,0,255)},
		{"Orangered",new Color32(255,69,0,255)},
		{"Orchid",new Color32(218,112,214,255)},
		{"PaleGoldenrod",new Color32(238,232,170,255)},
		{"PaleGreen",new Color32(152,251,152,255)},
		{"PaleTurquoise",new Color32(175,238,238,255)},
		{"PaleVioletred",new Color32(219,112,147,255)},
		{"PapayaWhip",new Color32(255,239,213,255)},
		{"PeachPuff",new Color32(255,218,185,255)},
		{"Peru",new Color32(205,133,63,255)},
		{"Pink",new Color32(255,192,203,255)},
		{"Plum",new Color32(221,160,221,255)},
		{"PowderBlue ",new Color32(176,224,230,255)},
		{"Purple",new Color32(128,0,128,255)},
		{"Red",new Color32(255,0,0,255)},
		{"RosyBrown",new Color32(188,143,143,255)},
		{"RoyalBlue",new Color32(65,105,225,255)},
		{"SaddleBrown",new Color32(139,69,19,255)},
		{"Salmon",new Color32(250,128,114,255)},
		{"SandyBrown",new Color32(244,164,96,255)},
		{"SeaGreen",new Color32(46,139,87,255)},
		{"Seashell",new Color32(255,245,238,255)},
		{"Sienna",new Color32(160,82,45,255)},
		{"Silver",new Color32(192,192,192,255)},
		{"SkyBlue",new Color32(135,206,235,255)},
		{"SlateBlue",new Color32(106,90,205,255)},
		{"SlateGray",new Color32(112,128,144,255)},
		{"Snow",new Color32(255,250,250,255)},
		{"SpringGreen",new Color32(0,255,127,255)},
		{"SteelBlue",new Color32(70,130,180,255)},
		{"Tan",new Color32(210,180,140,255)},
		{"Teal",new Color32(0,128,128,255)},
		{"Thistle",new Color32(216,191,216,255)},
		{"Tomato",new Color32(255,99,71,255)},
		{"Turquoise",new Color32(64,224,208,255)},
		{"Violet",new Color32(238,130,238,255)},
		{"Wheat",new Color32(245,222,179,255)},
		{"White",new Color32(255,255,255,255)},
		{"WhiteSmoke",new Color32(245,245,245,255)},
		{"Yellow",new Color32(255,255,0,255)},
		{"YellowGreen",new Color32(154,205,50,255)}
		};	
	}
}