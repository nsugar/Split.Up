﻿using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Tooltip	= HutongGames.PlayMaker.TooltipAttribute;

namespace VoxelBusters.NativePlugins.PlayMaker
{
	[ActionCategory("VB - Cross Platform Native Plugins")]
	[Tooltip("Registers to the event and notifies you when specified web view is first displayed.")]
	public class WebViewDidShowEvent : FsmStateAction 
	{
		#region Fields
		
		[ActionSection("Setup")]

		[RequiredField]
#if USES_WEBVIEW
		[CheckForComponent(typeof(WebView))]
#endif
		[Tooltip("The gameObject with the web view component.")]
		public	FsmOwnerDefault	gameObject;

		[ActionSection("Events")]

		[Tooltip("Event to send when web view is first displayed.")]
		public	FsmEvent		showedEvent;

#if USES_WEBVIEW
		private	WebView			m_targetWebView;
#endif
		
		#endregion
		
		#region FSM Methods
		
		public override void Reset ()
		{
			// Setup properties
			gameObject		= null;
			
			// Events properties
			showedEvent		= null;
			
			// Misc. properties
#if USES_WEBVIEW
			m_targetWebView	= null;
#endif
		}

		public override void OnEnter () 
		{
#if USES_WEBVIEW
			GameObject	_webViewGO	= Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (_webViewGO == null)
			{
				LogWarning(string.Format("[WebView] Game object is null."));
				return;
			}

			WebView		_webView	= _webViewGO.GetComponent<WebView>();
			
			if (_webView == null)
			{
				LogWarning(string.Format("[WebView] WebView component not found in game object: {0}.", _webViewGO.name));
				return;
			}
			
			// Cache reference
			m_targetWebView			= _webView;
			
			// Register for event
			WebView.DidShowEvent	+= OnDidShow;
#endif
		}
		
		#endregion

		#region Callback Methods

#if USES_WEBVIEW
		private void OnDidShow (WebView _webview)
		{
			if (_webview == m_targetWebView)
			{
				// Send event
				Fsm.Event(showedEvent);
			}
		}
#endif

		#endregion
	}
}