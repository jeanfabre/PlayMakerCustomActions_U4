// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Effects)]
	[Tooltip("Build a noisy texture")]
	public class BuildNoiseTexture : FsmStateAction {

		[RequiredField]
		[Tooltip("The GamObject used for spacial position of the noise")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The Resolution")]
		//[HasFloatSlider(2, 512)]
		public FsmInt resolution;

		[Tooltip("The Frequency")]
		public FsmFloat frequency;
		
		[Tooltip("The Octave")]
		[HasFloatSlider(1f, 8f)]
		public FsmInt octaves;
		
		[Tooltip("The lacunarity")]
		[HasFloatSlider(1f, 4f)]
		public FsmFloat lacunarity;

		[Tooltip("The persistence")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat persistence = 0.5f;
		
		[Tooltip("The dimensions")]
		[HasFloatSlider(1f, 3f)]
		public FsmInt dimensions = 3;
		
		public NoiseMethodType type;
		
		public Gradient coloring;

		[RequiredField]
		[Tooltip("the resulting noise texture")]
		[UIHint(UIHint.Variable)]
		public FsmTexture result;

		[Tooltip("If true, can be animated over time")]
		public bool everyFrame;


		Texture2D _texture;
		GameObject _go;
		Transform _t;

		int _resolution;
		float _frequency;
		int _octaves;
		float _lacunarity;
		float _persistence;
		int _dimensions;
		NoiseMethodType _type;
		Gradient _coloring;


		Vector3 point00;
		Vector3 point10;
		Vector3 point01;
		Vector3 point11;

		/// <summary>
		/// Reset all values
		/// </summary>
		public override void Reset()
		{		
			resolution = 256;
			frequency = 1f;
			octaves = 1;
			lacunarity = 2f;
			persistence = 0.5f;
			dimensions = 3;
			type = NoiseMethodType.Perlin;

			GradientColorKey[] gck = new GradientColorKey[2];
			gck[0].color = Color.red;
			gck[0].time = 0.0F;
			gck[1].color = Color.blue;
			gck[1].time = 1.0F;
			GradientAlphaKey[] gak = new GradientAlphaKey[2];
			gak[0].alpha = 1.0F;
			gak[0].time = 0.0F;
			gak[1].alpha = 0.0F;
			gak[1].time = 1.0F;

			coloring = new Gradient ();

			coloring.SetKeys (gck, gak);
		
			result = null;

			everyFrame = true;	
			
		}// Reset
		
		/// <summary>
		/// Compute the perlinNoise and finish the action if only supposed to run once.
		/// <see cref="everyFrame"/>
		/// </summary>		
		public override void OnEnter()
		{
			if (_texture == null) {
				_texture = new Texture2D (resolution.Value, resolution.Value, TextureFormat.RGB24, true);
				_texture.name = "Procedural Texture";
				_texture.wrapMode = TextureWrapMode.Clamp;
				_texture.filterMode = FilterMode.Trilinear;
				_texture.anisoLevel = 9;
				result.Value = _texture as Texture;
			}

			ComputeNoise();
			
			if (!everyFrame)
				Finish();
			
		}// OnEnter
		
		
		/// <summary>
		/// If the action is suppose to run every frame, we compute the noise here
		/// <see cref="everyFrame"/>
		/// </summary>
		public override void OnUpdate()
		{

			if (_t != null && _t.hasChanged) {
				_t.hasChanged = false;
				ComputeNoise();	
				return;
			}

			if (_resolution != resolution.Value ||
			    _frequency != frequency.Value ||
			    _octaves != octaves.Value ||
			    _lacunarity != lacunarity.Value ||
			    _persistence != persistence.Value ||
			    _dimensions != dimensions.Value ||
			    _type != type ||
			    _coloring != coloring)
			{
				ComputeNoise();	
			}


		}// OnUpdate
		
		
		/// <summary>
		/// Compute and store the current perlin noise.
		/// </summary>
		private void ComputeNoise(){

			if (_texture.width != resolution.Value) {
				_texture.Resize(resolution.Value, resolution.Value);
			}

			_go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go != null) {
				_t = _go.transform;
			}


			if (_t != null) {
				point00 = _t.TransformPoint(new Vector3(-0.5f,-0.5f));
				point10 = _t.TransformPoint(new Vector3( 0.5f,-0.5f));
				point01 = _t.TransformPoint(new Vector3(-0.5f, 0.5f));
				point11 = _t.TransformPoint(new Vector3( 0.5f, 0.5f));
			} else {
				point00 = new Vector3(-0.5f,-0.5f);
				point10 = new Vector3( 0.5f,-0.5f);
				point01 = new Vector3(-0.5f, 0.5f);
				point11 = new Vector3( 0.5f, 0.5f);
			}

			
			NoiseMethod method = Noise.methods[(int)type][dimensions.Value - 1];
			float stepSize = 1f / resolution.Value;
			for (int y = 0; y < resolution.Value; y++) {
				Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
				Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
				for (int x = 0; x < resolution.Value; x++) {
					Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
					float sample = Noise.Sum(method, point, frequency.Value, octaves.Value, lacunarity.Value, persistence.Value);
					if (type != NoiseMethodType.Value) {
						sample = sample * 0.5f + 0.5f;
					}
					_texture.SetPixel(x, y, coloring.Evaluate(sample));
				}
			}
			_texture.Apply();


			_resolution = resolution.Value;
			_frequency = frequency.Value;
			_octaves = octaves.Value;
			_lacunarity = lacunarity.Value;
			_persistence = persistence.Value;
			_dimensions = dimensions.Value;
			_type = type;
			_coloring = coloring;
		}
		
	}
}
