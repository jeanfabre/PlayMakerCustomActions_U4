// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

#pragma warning disable 0162  

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Resources")]
	[Tooltip("Asynchronously Loads an asset stored at path in a Resources folder. The path is relative to any Resources folder inside the Assets folder of your project, extensions must be omitted.")]
	public class ResourcesLoadAsynch : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The path is relative to any Resources folder inside the Assets folder of your project, extensions must be omitted.")]
		public FsmString assetPath;
		
		[RequiredField]
		[Tooltip("The stored asset")]
		[UIHint(UIHint.Variable)]
		public FsmVar storeAsset;

		[ActionSection("Result")]

		[Tooltip("true if the loading succedded or not")]
		[UIHint(UIHint.Variable)]
		public FsmBool success;

		[Tooltip("The isDone property of the asynch request")]
		[UIHint(UIHint.Variable)]
		public FsmBool isDone;

		[Tooltip("The progress of the asynch loading")]
		[UIHint(UIHint.Variable)]
		public FsmFloat progress;

		[Tooltip("Event sent when loading is done")]
		public FsmEvent doneEvent;

		[Tooltip("Event sent when loading failed")]
		public FsmEvent failureEvent;


		ResourceRequest _request;
		FsmObject _objectVar;

		public override void Reset()
		{
			assetPath = null;
			storeAsset = new FsmVar();
			storeAsset.Type = VariableType.Texture;

			success = null;
			isDone = null;
			progress = null;
			doneEvent = null;
			failureEvent = null;
		}
		
		
		public override void OnEnter()
		{
			isDone.Value  = false;

			bool ok = false;
			try
			{
				ok = loadResourceAsynch();
			}catch(UnityException e)
			{
				Debug.LogWarning(e.Message);
			}
			
			if (!ok)
			{
				success.Value = false;
				Fsm.Event(failureEvent);
				Finish();
			}
		}

		public override void OnUpdate()
		{
		
			if (_request==null)
			{
				return;
			}

			progress.Value = _request.progress;

			if(_request.isDone)
			{
				isDone.Value = _request.isDone;
				bool _result = StoreResource();

				success.Value = _result;

				if (_result)
				{
					Fsm.Event(doneEvent);
				}else{
					Fsm.Event(failureEvent);
				}
				Finish();
			}
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

		public bool loadResourceAsynch()
		{
			switch (storeAsset.Type)
			{
			case VariableType.GameObject:
				_request = Resources.LoadAsync(assetPath.Value, typeof(GameObject));

				break;
			case VariableType.Texture:
				_request = Resources.LoadAsync(assetPath.Value, typeof(Texture2D));

				break;
			case VariableType.Material:
				_request = Resources.LoadAsync(assetPath.Value, typeof(Material));

				break;
			case VariableType.String:
				_request = Resources.LoadAsync(assetPath.Value, typeof(TextAsset));

				break;
			case VariableType.Object:
				_objectVar = this.Fsm.Variables.GetFsmObject(storeAsset.variableName); 
				_request = Resources.LoadAsync(assetPath.Value,_objectVar.ObjectType);

				break;
			default:
				// not supported.
				return false;
			}

			return true;
		}

		public bool StoreResource()
		{
			if (_request == null)
			{
				return false;
			}

			if (_request.asset == null)
			{
				return false;
			}


			switch (storeAsset.Type)
			{
			case VariableType.GameObject:
				GameObject source = (GameObject)_request.asset;
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
				Texture2D _texture = (Texture2D)_request.asset;
				if (_texture==null)
				{
					return false;
				}else{
					FsmTexture _target= this.Fsm.Variables.GetFsmTexture(storeAsset.variableName);
					_target.Value = _texture;
				}
				break;
			case VariableType.Material:
				Material _material = (Material)_request.asset;
				if (_material==null)
				{
					return false;
				}else{
					FsmMaterial _target= this.Fsm.Variables.GetFsmMaterial(storeAsset.variableName);
					_target.Value = _material;
				}
				break;
			case VariableType.String:
				TextAsset _asset = (TextAsset)_request.asset;
				if (_asset==null)
				{
					return false;
				}else{
					FsmString _target= this.Fsm.Variables.GetFsmString(storeAsset.variableName);
					_target.Value = _asset.text;
				}
				break;
			case VariableType.Object:

				FsmObject _var= this.Fsm.Variables.GetFsmObject(storeAsset.variableName);
				
				_var.Value = _request.asset;
				
				if (_var.Value != null && _var.Value.GetType() == _var.ObjectType)
				{
					return true;
				}else{
					_var.Value = null;
					return false;
				}

				break;
			default:
				// not supported.
				return false;
			}
			return true;
		}

	}
}

