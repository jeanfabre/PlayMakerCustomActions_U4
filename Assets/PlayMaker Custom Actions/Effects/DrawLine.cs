// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

// DrawLine.cs version 1.1
// http://hutonggames.com/playmakerforum/index.php?topic=3943.0
// custom action by andrew r lukasik gmail com
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;

namespace HutongGames.PlayMaker.Actions {
	[ActionCategory( ActionCategory.Effects )]
	[Tooltip( "Draws line between two objects or points using LineRenderer." )]
	public class DrawLine : FsmStateAction {
		
		[Tooltip( "Line start." )]
		public FsmGameObject startObject;
		[Tooltip( "Line end." )]
		public FsmGameObject endObject;
		[Tooltip( "Start offset or world position." )]
		public FsmVector3 startOffset;
		[Tooltip( "End offset or world position." )]
		public FsmVector3 endOffset;
		[Tooltip( "Space." )]
		public Space spaceMode;
		[Tooltip( "When Space Mode Self is selected Start Object and End Object will execute LookAt each other to align their Z axes" )]
		public FsmBool alignInSpaceModeSelf;

		[Tooltip( "Material" )]
		public FsmMaterial material;
		[Tooltip( "Shadow Casting Mode" )]
		public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
		[Tooltip( "Receive Shadows" )]
		public FsmBool receiveShadows;
		
		[Tooltip( "start width" )]
		public FsmFloat startWidth;
		[Tooltip( "end width" )]
		public FsmFloat endWidth;
		
		[Tooltip( "Start Color" )]
		public FsmColor startColor;
		[Tooltip( "End Color" )]
		public FsmColor endColor;
		
		[UIHint( UIHint.Variable )]
		[Tooltip( "Store GameObject with LineRenderer." )]
		public FsmGameObject storeGameobject;
		
		[Tooltip( "Updates positions every frame" )]
		public FsmBool everyFrame;
		[Tooltip( "Destroy drawn line on exit" )]
		public FsmBool destroyOnExit;

		//
		private FsmGameObject goLineRenderer;
		private LineRenderer _lineRenderer;
		private Transform _lineRendererTransform;
		//
		private FsmGameObject localStart;
		private Transform _localStartTransform;
		//
		private FsmGameObject localEnd;
		private Transform _localEndTransform;
		//
		private FsmGameObject startObjectPosition;
		private Transform _startObjectPositionTransform;
		//
		private FsmGameObject endObjectPosition;
		private Transform _endObjectPositionTransform;
		//
		private Transform _startObjectTransform;
		private Transform _endObjectTransform;
		//

		// RESET //
		public override void Reset () {
			//
			startObject = null;
			_startObjectTransform = null;
			//
			endObject = null;
			_endObjectTransform = null;
			//
			alignInSpaceModeSelf = false;
			startOffset = new FsmVector3 { UseVariable = true };
			endOffset = new FsmVector3 { UseVariable = true };
			spaceMode = Space.World;
			material = null;
			shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			receiveShadows = false;
			startWidth = 1.0F;
			endWidth = 1.0F;
			startColor = Color.white;
			endColor = Color.white;
			everyFrame = true;
			destroyOnExit = false;
			//
			goLineRenderer = null;
			storeGameobject = null;
			_lineRenderer = null;
			_lineRendererTransform = null;
			//
			_localStartTransform = null;
			_localEndTransform = null;
			_startObjectPositionTransform = null;
			_endObjectPositionTransform = null;

		}

		// ON ENTER //
		public override void OnEnter () {	
			if( goLineRenderer!=null ) {
				Object.Destroy( goLineRenderer.Value );
			}
			//
			goLineRenderer = new GameObject( "FSM draw line ("+this.Name+")" );
			storeGameobject.Value = goLineRenderer.Value;
			_lineRendererTransform = goLineRenderer.Value.transform;
			//
			_lineRenderer = goLineRenderer.Value.AddComponent<LineRenderer>();
			_lineRenderer.material = material.Value;
			//
			_lineRenderer.shadowCastingMode = shadowCastingMode;
			_lineRenderer.receiveShadows = receiveShadows.Value;
			_lineRenderer.SetVertexCount( 2 );
			_lineRenderer.SetWidth( startWidth.Value , endWidth.Value );
			DoUpdatePositions();
			if( !everyFrame.Value ) {
				Finish();
			}
		}

		// ON UPDATE //
		public override void OnUpdate () {
			if( everyFrame.Value ) {
				DoUpdatePositions();
				_lineRenderer.SetWidth( startWidth.Value , endWidth.Value );
				_lineRenderer.SetColors( startColor.Value , endColor.Value );
				_lineRenderer.shadowCastingMode = shadowCastingMode;
				_lineRenderer.receiveShadows = receiveShadows.Value;
				_lineRenderer.material = material.Value;
			}
		}

		// ON EXIT //
		public override void OnExit () {
			if( destroyOnExit.Value && goLineRenderer!=null ) {
				Object.Destroy( goLineRenderer.Value );
			}
		}

		// DO UPDATE POSITIONS //
		void DoUpdatePositions () {
			//
			if( spaceMode==Space.Self ) MakeSureSpaceSelfHelpersAreOk();
			//calculate Start pos:
			if( startObject.Value!=null ) {
				_startObjectTransform = startObject.Value.transform;
				if( spaceMode==Space.World ) {					
					_lineRenderer.SetPosition( 0 , (_startObjectTransform.position+startOffset.Value) );
				}
				else {
					if( startObject.Value!=null ) {
						_startObjectPositionTransform.position = _startObjectTransform.position;
						if( alignInSpaceModeSelf.Value ) {							
							_startObjectPositionTransform.LookAt( _endObjectPositionTransform );
							_localStartTransform.localPosition = startOffset.Value;
						}
						else {
							_startObjectPositionTransform.rotation = _startObjectTransform.rotation;
							_localStartTransform.localPosition = startOffset.Value;
						}
						_lineRenderer.SetPosition( 0 , _localStartTransform.position );
					}
					else {
						_startObjectPositionTransform.position = startOffset.Value;
						if( alignInSpaceModeSelf.Value ) {
							_startObjectPositionTransform.LookAt( _endObjectPositionTransform );
							_localStartTransform.localPosition = startOffset.Value;
						}
						else {
							_localStartTransform.localPosition = startOffset.Value;
						}
						_lineRenderer.SetPosition( 0 , _localStartTransform.position );
					}
				}
			}
			else {
				_lineRenderer.SetPosition( 0 , startOffset.Value );
			}
			
			//calculate End pos:
			if( endObject.Value!=null ) {
				_endObjectTransform = endObject.Value.transform;
				if( spaceMode==Space.World ) {
					_lineRenderer.SetPosition( 1 , (_endObjectTransform.position+endOffset.Value) );
				}
				else {					
					if( endObject.Value!=null ) {
						_endObjectPositionTransform.position = _endObjectTransform.position;
						if( alignInSpaceModeSelf.Value ) {
							_endObjectPositionTransform.LookAt( _startObjectPositionTransform );
							_localEndTransform.localPosition = endOffset.Value;
						}
						else {
							_endObjectPositionTransform.rotation = _endObjectTransform.rotation;
							_localEndTransform.localPosition = endOffset.Value;
						}
						_lineRenderer.SetPosition( 1 , _localEndTransform.position );
					}
					else {
						_endObjectPositionTransform.position = endOffset.Value;
						if( alignInSpaceModeSelf.Value ) {
							_endObjectPositionTransform.LookAt( _startObjectPositionTransform );
							_localEndTransform.localPosition = endOffset.Value;
						}
						else {
							_localEndTransform.localPosition = endOffset.Value;
						}
						_lineRenderer.SetPosition( 1 , _localEndTransform.position );
					}
				}
			}
			else {
				_lineRenderer.SetPosition( 1 , endOffset.Value );
			}
		}

		//
		private void MakeSureSpaceSelfHelpersAreOk () {
			if( startObjectPosition==null ) {
				startObjectPosition = new GameObject( "tracking startObject" );
				_startObjectPositionTransform = startObjectPosition.Value.transform;
				_startObjectPositionTransform.parent = _lineRendererTransform;
			}
			else {
				_startObjectPositionTransform.parent = _lineRendererTransform;
			}
			//
			if( localStart==null ) {
				localStart = new GameObject( "local start" );
				_localStartTransform = localStart.Value.transform;
				_localStartTransform.parent = _startObjectPositionTransform;
				_localStartTransform.localEulerAngles = new Vector3( 0 , 0 , 0 );
			}
			else {
				_localStartTransform.parent = _startObjectPositionTransform;
			}
			//
			if( endObjectPosition==null ) {
				endObjectPosition = new GameObject( "tracking endObject" );
				_endObjectPositionTransform = endObjectPosition.Value.transform;
				_endObjectPositionTransform.parent = _lineRendererTransform;
			}
			else {
				_endObjectPositionTransform.parent = _lineRendererTransform;
			}
			//
			if( localEnd==null ) {
				localEnd = new GameObject( "local end" );
				_localEndTransform = localEnd.Value.transform;
				_localEndTransform.parent = _endObjectPositionTransform;
				_localEndTransform.localEulerAngles = new Vector3( 0 , 0 , 0 );
			}
			else {
				_localEndTransform.parent = _endObjectPositionTransform;
			}
			//
		}

	}
}
