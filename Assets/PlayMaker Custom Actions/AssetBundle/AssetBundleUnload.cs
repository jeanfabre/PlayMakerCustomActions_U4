// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("AssetBundle")]
	[Tooltip("Unload AssetBundle. Optionaly unload all Loaded objects")]
	public class AssetBundleUnload : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The Asset Bundle")]
		public FsmObject assetBundle;
		
		[Tooltip("When unloadAllLoadedObjects is false, compressed file data for assets inside the bundle will be unloaded, but any actual objects already loaded from this bundle will be kept intact. Of course you won't be able to load any more objects from this bundle. When unloadAllLoadedObjects is true, all objects that were loaded from this bundle will be destroyed as well. If there are game objects in your scene referencing those assets, the references to them will become missing.")]
		public FsmBool unloadAllLoadedObjects;
		
		public override void Reset()
		{
			assetBundle = null;
			unloadAllLoadedObjects = false;
		}

		public override void OnEnter()
		{
			AssetBundle _bundle = (AssetBundle)assetBundle.Value ;
			
			if (_bundle != null)
			{
				_bundle.Unload(unloadAllLoadedObjects.Value);
			}
			Finish();
		}
		

	}
}
