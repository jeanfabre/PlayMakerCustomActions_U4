// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.
// AudioClip support by LampRabbit
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Resources")]
	[Tooltip("Loads an asset stored at path in a Resources folder. The path is relative to any Resources folder inside the Assets folder of your project, extensions must be omitted.")]
	public class ResourcesLoad : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The path is relative to any Resources folder inside the Assets folder of your project, extensions must be omitted.")]
		public FsmString assetPath;
		
		[RequiredField]
		[Tooltip("The stored asset")]
		[UIHint(UIHint.Variable)]
		public FsmVar storeAsset;
		
		public FsmEvent successEvent;
		public FsmEvent failureEvent;
		
		
		public override void Reset()
		{
			assetPath = null;
			storeAsset = new FsmVar();
			storeAsset.Type = VariableType.Texture;
		}
		
		
		public override void OnEnter()
		{
			bool ok = false;
			try
			{
				ok = loadResource();
			}catch(UnityException e)
			{
				Debug.LogWarning(e.Message);
			}
			
			if (ok)
			{
				Fsm.Event(successEvent);
			}else{
				Fsm.Event(failureEvent);
			}
			
			Finish ();
		}
		
		public override string ErrorCheck ()
		{
			switch (storeAsset.Type)
				{
				case VariableType.GameObject:
					break;
				case VariableType.Texture:
					break;
				case VariableType.Material:
					break;
			case VariableType.String:
					break;
			case VariableType.Object:
					break;
				default:
					// not supported.
					return "Only GameObject, Texture, Sprites, AudioClip and Material are supported";
				}	
			
			return "";
		}
		
		public bool loadResource()
		{
			switch (storeAsset.Type)
			{
			case VariableType.GameObject:
				GameObject source = (GameObject)Resources.Load(assetPath.Value, typeof(GameObject));
				if (source==null)
				{
					return false;
				}
				GameObject _go = (GameObject)Object.Instantiate(source);
				if (_go==null)
				{
					return false;
				}else{
					FsmGameObject _target= this.Fsm.Variables.GetFsmGameObject(storeAsset.variableName);
					_target.Value = _go;
				}
				
				break;
			case VariableType.Texture:
				Texture2D _texture = (Texture2D)Resources.Load(assetPath.Value, typeof(Texture2D));
				if (_texture==null)
				{
					return false;
				}else{
					FsmTexture _target= this.Fsm.Variables.GetFsmTexture(storeAsset.variableName);
					_target.Value = _texture;
				}
				break;
			case VariableType.Material:
				Material _material = (Material)Resources.Load(assetPath.Value, typeof(Material));
				if (_material==null)
				{
					return false;
				}else{
					FsmMaterial _target= this.Fsm.Variables.GetFsmMaterial(storeAsset.variableName);
					_target.Value = _material;
				}
				break;
			case VariableType.String:
				TextAsset _asset = (TextAsset)Resources.Load(assetPath.Value, typeof(TextAsset));
				if (_asset==null)
				{
					return false;
				}else{
					FsmString _target= this.Fsm.Variables.GetFsmString(storeAsset.variableName);
					_target.Value = _asset.text;
				}
				break;
			case VariableType.Object:

				
				
				#if ! UNITY_3_5 
				System.Type _type = storeAsset.objectReference.GetType();
				if (_type == typeof(UnityEngine.Sprite))
				{
					/* Can't find a way to make a reusable code for All types...
					FsmObject _target= this.Fsm.Variables.GetFsmObject(storeAsset.variableName);
					//	_target.Value = this.LoadResourceByType<_type>(assetPath.Value);

					var mi = typeof(ResourcesLoad).GetMethod("LoadResourceByType");
					var fooRef = mi.MakeGenericMethod(_type);
					_target.Value = (Object)fooRef.Invoke(new ResourcesLoad(), new object[] { assetPath.Value });
					*/
					
					
					UnityEngine.Sprite _sprite = (UnityEngine.Sprite)Resources.Load(assetPath.Value,typeof(UnityEngine.Sprite));
					if (_sprite==null)
					{
						return false;
					}else{
						FsmObject _target= this.Fsm.Variables.GetFsmObject(storeAsset.variableName);
						_target.Value = _sprite;
						return true;
					}
				}
				if (_type == typeof(AudioClip))
					{
					AudioClip audioClip = (AudioClip)Resources.Load(assetPath.Value,typeof(AudioClip));
					if (audioClip==null)
					{
						return false;
					}else{
						FsmObject _target= this.Fsm.Variables.GetFsmObject(storeAsset.variableName);
						_target.Value = audioClip;
						return true;
					}
				}
				#else
					AudioClip audioClip = (AudioClip)Resources.Load(assetPath.Value,typeof(AudioClip));
					if (audioClip==null)
					{
						return false;
					}else{
						FsmObject _target= this.Fsm.Variables.GetFsmObject(storeAsset.variableName);
						_target.Value = audioClip;
						return true;
					}
				#endif
				
				

				break;
			default:
				// not supported.
				return false;
			}
			return true;
		}

		/*
		System.Object LoadResourceByType<T>(string assetPath)
		{
			return (T)Resources.Load(assetPath,typeof(T));
		}
		*/
	}
}

