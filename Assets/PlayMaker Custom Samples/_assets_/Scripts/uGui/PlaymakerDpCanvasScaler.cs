/*
    This is a modified version of Unity's CanvasScaler, as found here:
    https://bitbucket.org/Unity-Technologies/ui/src/fadfa14d2a5c?at=4.6
    
    I share it under the same MIT/X11 license.
    
    Modder: Tess Snider, Hidden Achievement
*/

// modified to not clash with any use outside the Ecosystem, so renamed both the namespace and script

using UnityEngine;
using UnityEngine.EventSystems;


namespace HutongGames.PlayMaker.Ecosystem.UI
{
	[RequireComponent(typeof(Canvas))]
	[ExecuteInEditMode]
	[AddComponentMenu("Layout/PlayMaker/UI/DP Canvas Scaler")]
	public class PlaymakerDpCanvasScaler : UIBehaviour
	{
		[Tooltip("If a sprite has this 'Pixels Per Unit' setting, then one pixel in the sprite will cover one unit in the UI.")]
		[SerializeField]
		protected float m_ReferencePixelsPerUnit = 100;
		public float referencePixelsPerUnit { get { return m_ReferencePixelsPerUnit; } set { m_ReferencePixelsPerUnit = value; } }
		
		// The log base doesn't have any influence on the results whatsoever, as long as the same base is used everywhere.
		private const float kLogBase = 2;
		
		[Tooltip("The DPI to assume if the screen DPI is not known.")]
		[SerializeField]
		protected float m_FallbackScreenDPI = 96;
		public float fallbackScreenDPI { get { return m_FallbackScreenDPI; } set { m_FallbackScreenDPI = value; } }
		
		[Tooltip("The pixels per inch to use for sprites that have a 'Pixels Per Unit' setting that matches the 'Reference Pixels Per Unit' setting.")]
		[SerializeField]
		protected float m_DefaultSpriteDPI = 96;
		public float defaultSpriteDPI { get { return m_DefaultSpriteDPI; } set { m_DefaultSpriteDPI = value; } }
		
		// World Canvas settings
		[Tooltip("The amount of pixels per unit to use for dynamically created bitmaps in the UI, such as Text.")]
		[SerializeField]
		protected float m_DynamicPixelsPerUnit = 1;
		public float dynamicPixelsPerUnit { get { return m_DynamicPixelsPerUnit; } set { m_DynamicPixelsPerUnit = value; } }
		
		
		// General variables
		private Canvas m_Canvas;
		[System.NonSerialized]
		private float m_PrevScaleFactor = 1;
		[System.NonSerialized]
		private float m_PrevReferencePixelsPerUnit = 100;
		
		
		protected PlaymakerDpCanvasScaler() { }
		
		protected override void OnEnable()
		{
			base.OnEnable();
			m_Canvas = GetComponent<Canvas>();
			Handle();
		}
		
		protected override void OnDisable()
		{
			SetScaleFactor(1);
			SetReferencePixelsPerUnit(100);
			base.OnDisable();
		}
		
		protected virtual void Update()
		{
			Handle();
		}
		
		protected virtual void Handle()
		{
			if (m_Canvas == null || !m_Canvas.isRootCanvas)
				return;
			
			if (m_Canvas.renderMode == RenderMode.WorldSpace)
			{
				HandleWorldCanvas();
				return;
			}
			
			HandleConstantPhysicalSize();
		}
		
		protected virtual void HandleWorldCanvas()
		{
			SetScaleFactor(m_DynamicPixelsPerUnit);
			SetReferencePixelsPerUnit(m_ReferencePixelsPerUnit);
		}
		
		protected virtual void HandleConstantPhysicalSize()
		{
			float currentDpi = Screen.dpi;
			float dpi = (currentDpi == 0 ? m_FallbackScreenDPI : currentDpi);
			float targetDPI = 160;
			
			#if (UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS))
			targetDPI = 96;
			#endif
			
			SetScaleFactor(dpi / targetDPI);
			SetReferencePixelsPerUnit(m_ReferencePixelsPerUnit * targetDPI / m_DefaultSpriteDPI);
		}
		
		protected void SetScaleFactor(float scaleFactor)
		{
			if (scaleFactor == m_PrevScaleFactor)
				return;
			
			m_Canvas.scaleFactor = scaleFactor;
			m_PrevScaleFactor = scaleFactor;
		}
		
		protected void SetReferencePixelsPerUnit(float referencePixelsPerUnit)
		{
			if (referencePixelsPerUnit == m_PrevReferencePixelsPerUnit)
				return;
			
			m_Canvas.referencePixelsPerUnit = referencePixelsPerUnit;
			m_PrevReferencePixelsPerUnit = referencePixelsPerUnit;
		}
	}
}