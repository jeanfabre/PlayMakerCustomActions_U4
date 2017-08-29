// (c) Copyright HutongGames, LLC 2010-2017. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using System.Linq;

#pragma warning disable 0162  

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Resources")]
	[Tooltip("Loads all assets of a given type stored at path in a Resources folder. The path is relative to any Resources folder inside the Assets folder of your project, extensions must be omitted.")]
	public class ResourcesLoadAll : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The path is relative to any Resources folder inside the Assets folder of your project, extensions must be omitted.")]
		public FsmString assetPath;
		
		[RequiredField]
		[Tooltip("The stored assets")]
		[UIHint(UIHint.Variable)]
		public FsmArray storeAssets;
		
		public FsmEvent successEvent;
		public FsmEvent failureEvent;


		public override void Reset()
		{
			assetPath = null;
			storeAssets = null;
		}
		
		
		public override void OnEnter()
		{
			bool ok = false;
			try
			{
				ok = loadAllResource();
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
			if (storeAssets.IsNone)
			{
				return "";
			}

			switch (storeAssets.ElementType)
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
					return "Only GameObject, Texture, Sprites, Material and Unity Objects are supported";
				}	
			
			return "";
		}
		
		public bool loadAllResource()
		{

			switch (storeAssets.ElementType)
			{
			case VariableType.GameObject:

				storeAssets.objectReferences = Resources.LoadAll<GameObject>(assetPath.Value);
				
				break;
			case VariableType.Texture:
				storeAssets.objectReferences = Resources.LoadAll<Texture>(assetPath.Value);

				break;
			case VariableType.Material:
				storeAssets.objectReferences = Resources.LoadAll<Material>(assetPath.Value);
				break;
			case VariableType.String:
			
				storeAssets.stringValues = Resources.LoadAll<TextAsset>(assetPath.Value).Select(_asset => _asset.text).ToArray();

				break;
			case VariableType.Object:

				storeAssets.objectReferences = Resources.LoadAll(assetPath.Value,storeAssets.ObjectType);

				break;
			default:
				// not supported.
				return false;
			}
			return true;

		}

	}
}

