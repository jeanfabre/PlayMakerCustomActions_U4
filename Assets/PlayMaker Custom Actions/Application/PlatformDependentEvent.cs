// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Application)]
	[Tooltip("Sends Events based on platform dependent flag")]
	public class PlatformDependentEvent : FsmStateAction
	{
		public enum platformDependentFlags
		{
			UNITY_EDITOR,
			UNITY_EDITOR_WIN,
			UNITY_EDITOR_OSX,
			UNITY_STANDALONE_OSX,
			UNITY_DASHBOARD_WIDGET,
			UNITY_STANDALONE_WIN,
			UNITY_STANDALONE_LINUX,
			UNITY_STANDALONE,
			UNITY_WEBPLAYER,
			UNITY_WII,
			UNITY_IPHONE,
			UNITY_ANDROID,
			UNITY_PS3,
			UNITY_XBOX360,
			UNITY_NACL,
			UNITY_FLASH,
			UNITY_BLACKBERRY,
			UNITY_WP8,
			UNITY_METRO,
			UNITY_WINRT,
			UNITY_IOS,
			UNITY_PS4,
			UNITY_XBOXONE,
			UNITY_TIZEN,
			UNITY_WP8_1,
			UNITY_WSA,
			UNITY_WSA_8_0,
			UNITY_WSA_8_1,
			UNITY_WINRT_8_0,
			UNITY_WINRT_8_1,
			UNITY_WEBGGL
		}

		[Tooltip("The platform")]
		public platformDependentFlags platform;

		[Tooltip("The event to send for that platform")]
		public FsmEvent matchEvent;

		[Tooltip("The event to send for that platform")]
		public FsmEvent noMatchEvent;

		public override void Reset()
		{
			platform = platformDependentFlags.UNITY_WEBPLAYER;
			matchEvent =null;
			noMatchEvent = null;
		}


		bool isMatch(platformDependentFlags valueA,platformDependentFlags valueB)
		{

			string A = Enum.GetName(typeof(platformDependentFlags),valueA);
			string B = Enum.GetName(typeof(platformDependentFlags),valueB);

			UnityEngine.Debug.Log("is match?: "+A+" == "+B+" => "+String.Equals(A,B));

			return String.Equals(A,B);
		}

		public override void OnEnter()
		{

			platformDependentFlags _flag = platform;

				UnityEngine.Debug.Log("checking for "+_flag.ToString());
				FsmEvent _event = matchEvent;
				
				#if UNITY_EDITOR 
				if(_flag == platformDependentFlags.UNITY_EDITOR ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_EDITOR_WIN 
				if(_flag == platformDependentFlags.UNITY_EDITOR_WIN ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_EDITOR_OSX 
				if(_flag == platformDependentFlags.UNITY_EDITOR_OSX ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_STANDALONE_OSX 
				if(_flag == platformDependentFlags.UNITY_STANDALONE_OSX ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_DASHBOARD_WIDGET 
				if(_flag == platformDependentFlags.UNITY_DASHBOARD_WIDGET ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_STANDALONE_WIN 
				if(_flag == platformDependentFlags.UNITY_STANDALONE_WIN ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_STANDALONE_LINUX 
				if(_flag == platformDependentFlags.UNITY_STANDALONE_LINUX ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_STANDALONE 
				if(_flag == platformDependentFlags.UNITY_STANDALONE ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_WEBPLAYER
				if(_flag == platformDependentFlags.UNITY_WEBPLAYER ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_WII 
				if(_flag == platformDependentFlags.UNITY_WII ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_IPHONE || UNITY_IOS

				UnityEngine.Debug.Log("we are in UNITY_IPHONE || UNITY_IOS ");

			/*
				if (isMatch(_flag,platformDependentFlags.UNITY_IPHONE))
				{
					UnityEngine.Debug.Log("---------- WE FIRE USING STRING COMPARISION "+_event.Name);
					Fsm.Event(_event);	
					return;
				}
				
				if (isMatch(_flag,platformDependentFlags.UNITY_IOS))
				{
					UnityEngine.Debug.Log("---------- WE FIRE USING STRING COMPARISION "+_event.Name);
					Fsm.Event(_event);	
					return;
				}

*/
				if (Enum.Equals(_flag,platformDependentFlags.UNITY_IPHONE ) )
				{
					UnityEngine.Debug.Log("---------- WE FIRE "+_event.Name);
					Fsm.Event(_event);
					return;
				}
				if( Enum.Equals(_flag,platformDependentFlags.UNITY_IOS ) )
				{
					UnityEngine.Debug.Log("---------- WE FIRE "+_event.Name);
					Fsm.Event(_event);	
					return;
				}


				#endif
				
				#if UNITY_ANDROID 
				if(_flag == platformDependentFlags.UNITY_ANDROID ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_PS3 
				if(_flag == platformDependentFlags.UNITY_PS3 ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_XBOX360 
				if(_flag == platformDependentFlags.UNITY_XBOX360 ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_NACL 
				if(_flag == platformDependentFlags.UNITY_NACL ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_FLASH 
				if(_flag == platformDependentFlags.UNITY_FLASH ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_BLACKBERRY 
				if(_flag == platformDependentFlags.UNITY_BLACKBERRY ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_WP8 
				if(_flag == platformDependentFlags.UNITY_WP8 ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_METRO 
				if(_flag == platformDependentFlags.UNITY_METRO ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_WINRT 
				if(_flag == platformDependentFlags.UNITY_WINRT ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_PS4 
				if(_flag == platformDependentFlags.UNITY_PS4 ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_XBOXONE 
				if(_flag == platformDependentFlags.UNITY_XBOXONE ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_TIZEN 
				if(_flag == platformDependentFlags.UNITY_TIZEN ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_WP8_1 
				if(_flag == platformDependentFlags.UNITY_WP8_1 ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_WSA 
				if(_flag == platformDependentFlags.UNITY_WSA ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_WSA_8_0 
				if(_flag == platformDependentFlags.UNITY_WSA_8_0 ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_WSA_8_1 
				if(_flag == platformDependentFlags.UNITY_WSA_8_1 ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_WINRT_8_0 
				if(_flag == platformDependentFlags.UNITY_WINRT_8_0 ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_WINRT_8_1 
				if(_flag == platformDependentFlags.UNITY_WINRT_8_1 ) Fsm.Event(_event);	
				#endif
				
				#if UNITY_WEBGL 
				if(_flag == platformDependentFlags.UNITY_WEBGL ) Fsm.Event(_event);	
				#endif

			Fsm.Event(noMatchEvent);
			
			Finish();
		}
		
	}
}
