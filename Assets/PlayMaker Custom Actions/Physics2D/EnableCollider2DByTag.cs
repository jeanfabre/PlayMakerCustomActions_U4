// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0)
// License: Attribution 4.0 International (CC BY 4.0)
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/
// Source http://hutonggames.com/playmakerforum/index.php?topic=10242
// Edited by DjayDino for 2d version

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Enables/Disables a Collider2D(or a Rigidbody) by Tag (and Layer).")]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=10242")]
    public class EnableCollider2DByTag : FsmStateAction
	{
		[ActionSection("Setup")]
		[TitleAttribute("Collider2D Type Select")]
		public Selection ColliderSelect;
		public enum Selection {None, Box, Circle, Edge, Polygon, Rigidbody };
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Set to True to enable/disable all Collider2D.")]
		[TitleAttribute("or All Collider2D")]
		public FsmBool allCollider2D;

		[ActionSection("Options")]
		[RequiredField]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Set to True to enable, False to disable.")]
		public FsmBool enable;
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Should the object Children be included?")]
		public FsmBool inclChildren;

		[ActionSection("Tag and Layer Options")]
		[Tooltip("Activate this option?")]
		[UIHint(UIHint.Tag)]
		public FsmString tag;
		[TitleAttribute("Incl Layer Filter")]
		[UIHint(UIHint.FsmBool)]
		[Tooltip("Also filter by layer?")]
		public FsmBool layerFilterOn;
		[Tooltip("Filter layer on child?")]
		public FsmBool inclLayerFilterOnChild;
		[UIHint(UIHint.Layer)]
		public int layer;



		Collider2D componentTarget;

		public override void Reset()
		{
		
			enable = true;
			allCollider2D = false;
			inclChildren = false;
			layerFilterOn = false;
			layer = 0;
			inclLayerFilterOnChild = false;
			
		}


		public override void OnEnter()
		{


			if (allCollider2D.Value == false & ColliderSelect == Selection.None)
			{
				Debug.LogWarning(" !!! Check your setup - Collider2D Type Select = None");
				return;
			}

			if (allCollider2D.Value == true & layerFilterOn.Value == false){
				ColliderSelect = Selection.None;
				DisableAllTag();
			}

			if (allCollider2D.Value == true & layerFilterOn.Value == true){
				ColliderSelect = Selection.None;
				DisableAllTagFilter();
			}


			switch (ColliderSelect)
			{
			case Selection.None:
				break;
			case Selection.Box:
				DisableBoxCollider2D();
				break;
				
			case Selection.Circle:
				DisableCircleCollider2D();
				break;
				
			case Selection.Edge:
				DisableEdgeCollider2D();
				break;
				
			case Selection.Polygon:
				DisablePolygonCollider2D();
				break;
				
			case Selection.Rigidbody:
				DisableRigidbody2D();
				break;
			
			}
			
			Finish();
		}


		void DisableAllTagFilter()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);

			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			for(int i = 0; i<objtag.Length;i++){

				if (objtag[i].layer == layer){


			Collider2D[] scriptComponents = objtag[i].gameObject.GetComponents<Collider2D>();    
			foreach(Collider2D script in scriptComponents) {
			
			script.enabled = enable.Value;
			}

			if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
			{
				Collider2D[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<Collider2D>();    
				foreach(Collider2D script in scriptChildComponents) {
					script.enabled = enable.Value;
				}
			}

			if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
			{
						  
						foreach(Collider2D script in objtag[i].gameObject.GetComponentsInChildren<Collider2D>()) {
							if (script.gameObject.layer == layer)
							script.enabled = enable.Value;
						}

			}

			}
			}
		}

		void DisableAllTag()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			for(int i = 0; i<objtag.Length;i++){
				
				Collider2D[] scriptComponents = objtag[i].gameObject.GetComponents<Collider2D>();    
				foreach(Collider2D script in scriptComponents) {
					
					script.enabled = enable.Value;
				}
				
				if (inclChildren.Value == true)
				{
					Collider2D[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<Collider2D>();    
					foreach(Collider2D script in scriptChildComponents) {
						script.enabled = enable.Value;
					}
				}
			}
		}

		void DisableBoxCollider2D()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}

			if (inclLayerFilterOnChild.Value == false)
			{

			for(int i = 0; i<objtag.Length;i++){

			Collider2D[] scriptComponents = objtag[i].gameObject.GetComponents<BoxCollider2D>();    
			foreach(BoxCollider2D temp in scriptComponents) {
				temp.enabled = enable.Value;
			}
			
			if (inclChildren.Value == true)
			{
				Collider2D[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<BoxCollider2D>();    
				foreach(BoxCollider2D temp in scriptChildComponents) {
					temp.enabled = enable.Value;
				}
			}
			}
				return;
			}

			if (inclLayerFilterOnChild.Value == true)
			{
				
				for(int i = 0; i<objtag.Length;i++){

					if (objtag[i].layer == layer){
					
					Collider2D[] scriptComponents = objtag[i].gameObject.GetComponents<BoxCollider2D>();    
					foreach(BoxCollider2D temp in scriptComponents) {
						temp.enabled = enable.Value;
					}
					
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
						{
							Collider2D[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<BoxCollider2D>();    
							foreach(BoxCollider2D script in scriptChildComponents) {
								script.enabled = enable.Value;
							}
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
						{
							
							foreach(BoxCollider2D script in objtag[i].gameObject.GetComponentsInChildren<BoxCollider2D>()) {
								if (script.gameObject.layer == layer)
									script.enabled = enable.Value;
							}
							
						}
				}
				}
				return;
			}


		}

		void DisableCircleCollider2D()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			if (inclLayerFilterOnChild.Value == false)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					Collider2D[] scriptComponents = objtag[i].gameObject.GetComponents<CircleCollider2D>();    
					foreach(CircleCollider2D temp in scriptComponents) {
						temp.enabled = enable.Value;
					}
					
					if (inclChildren.Value == true)
					{
						Collider2D[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<CircleCollider2D>();    
						foreach(CircleCollider2D temp in scriptChildComponents) {
							temp.enabled = enable.Value;
						}
					}
				}
				return;
			}
			
			if (inclLayerFilterOnChild.Value == true)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					if (objtag[i].layer == layer){
						
						Collider2D[] scriptComponents = objtag[i].gameObject.GetComponents<CircleCollider2D>();    
						foreach(CircleCollider2D temp in scriptComponents) {
							temp.enabled = enable.Value;
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
						{
							Collider2D[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<CircleCollider2D>();    
							foreach(CircleCollider2D script in scriptChildComponents) {
								script.enabled = enable.Value;
							}
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
						{
							
							foreach(CircleCollider2D script in objtag[i].gameObject.GetComponentsInChildren<CircleCollider2D>()) {
								if (script.gameObject.layer == layer)
									script.enabled = enable.Value;
							}
							
						}
					}
				}
				return;
			}
			
			
		}

		void DisableEdgeCollider2D()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			if (inclLayerFilterOnChild.Value == false)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					Collider2D[] scriptComponents = objtag[i].gameObject.GetComponents<EdgeCollider2D>();    
					foreach(EdgeCollider2D temp in scriptComponents) {
						temp.enabled = enable.Value;
					}
					
					if (inclChildren.Value == true)
					{
						Collider2D[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<EdgeCollider2D>();    
						foreach(EdgeCollider2D temp in scriptChildComponents) {
							temp.enabled = enable.Value;
						}
					}
				}
				return;
			}
			
			if (inclLayerFilterOnChild.Value == true)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					if (objtag[i].layer == layer){
						
						Collider2D[] scriptComponents = objtag[i].gameObject.GetComponents<EdgeCollider2D>();    
						foreach(EdgeCollider2D temp in scriptComponents) {
							temp.enabled = enable.Value;
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
						{
							Collider2D[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<EdgeCollider2D>();    
							foreach(EdgeCollider2D script in scriptChildComponents) {
								script.enabled = enable.Value;
							}
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
						{
							
							foreach(EdgeCollider2D script in objtag[i].gameObject.GetComponentsInChildren<EdgeCollider2D>()) {
								if (script.gameObject.layer == layer)
									script.enabled = enable.Value;
							}
							
						}
					}
				}
				return;
			}
			
			
		}

		void DisableRigidbody2D()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			if (inclLayerFilterOnChild.Value == false)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					Rigidbody2D[] scriptComponents = objtag[i].gameObject.GetComponents<Rigidbody2D>();    
					foreach(Rigidbody2D temp in scriptComponents) {
						temp.isKinematic = !enable.Value;
					//	temp.detectCollisions = enable.Value;
					}
					
					if (inclChildren.Value == true)
					{
						Rigidbody2D[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<Rigidbody2D>();    
						foreach(Rigidbody2D temp in scriptChildComponents) {
							temp.isKinematic = !enable.Value;
					//		temp.detectCollisions = enable.Value;
						}
					}
				}
				return;
			}
			
			if (inclLayerFilterOnChild.Value == true)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					if (objtag[i].layer == layer){
						
						Rigidbody2D[] scriptComponents = objtag[i].gameObject.GetComponents<Rigidbody2D>();    
						foreach(Rigidbody2D temp in scriptComponents) {
							temp.isKinematic = !enable.Value;
					//		temp.detectCollisions = enable.Value;
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
						{
							Rigidbody2D[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<Rigidbody2D>();    
							foreach(Rigidbody2D temp in scriptChildComponents) {
								temp.isKinematic = !enable.Value;
						//		temp.detectCollisions = enable.Value;
							}
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
						{
							
							foreach(Rigidbody2D temp in objtag[i].gameObject.GetComponentsInChildren<Rigidbody2D>()) {
								if (temp.gameObject.layer == layer){
									temp.isKinematic = !enable.Value;
						//			temp.detectCollisions = enable.Value;
								}
							}
							
						}
					}
				}
				return;
			}
			
			
		}
		void DisablePolygonCollider2D()
		{
			GameObject[] objtag = GameObject.FindGameObjectsWithTag(tag.Value);
			
			if (objtag.Length == 0) {
				Debug.LogWarning ("No object with tag:  "+tag.Value);
				return;
			}
			
			if (inclLayerFilterOnChild.Value == false)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					Collider2D[] scriptComponents = objtag[i].gameObject.GetComponents<PolygonCollider2D>();    
					foreach(PolygonCollider2D temp in scriptComponents) {
						temp.enabled = enable.Value;
					}
					
					if (inclChildren.Value == true)
					{
						Collider2D[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<PolygonCollider2D>();    
						foreach(PolygonCollider2D temp in scriptChildComponents) {
							temp.enabled = enable.Value;
						}
					}
				}
				return;
			}
			
			if (inclLayerFilterOnChild.Value == true)
			{
				
				for(int i = 0; i<objtag.Length;i++){
					
					if (objtag[i].layer == layer){
						
						Collider2D[] scriptComponents = objtag[i].gameObject.GetComponents<PolygonCollider2D>();    
						foreach(PolygonCollider2D temp in scriptComponents) {
							temp.enabled = enable.Value;
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == false)
						{
							Collider2D[] scriptChildComponents = objtag[i].gameObject.GetComponentsInChildren<PolygonCollider2D>();    
							foreach(PolygonCollider2D script in scriptChildComponents) {
								script.enabled = enable.Value;
							}
						}
						
						if (inclChildren.Value == true && inclLayerFilterOnChild.Value == true)
						{
							
							foreach(PolygonCollider2D script in objtag[i].gameObject.GetComponentsInChildren<PolygonCollider2D>()) {
								if (script.gameObject.layer == layer)
									script.enabled = enable.Value;
							}
							
						}
					}
				}
				return;
			}
			
			
		}
	}
}
