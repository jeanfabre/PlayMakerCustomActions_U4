using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

using HutongGames.PlayMaker;
using UnityEditor;
using UnityEngine;
using System;

[CustomActionEditor(typeof(SetStringValue))]
public class SetStringValueCustomEditor : CustomActionEditor
{
	bool textEdit = false;
	Vector2 _scroll = Vector2.zero;
    public override bool OnGUI()
    {
	
		
		bool edited = false;
		
		
		SetStringValue _target = (SetStringValue)target;

		EditField("stringVariable");
		
		if (_target.stringValue!=null && !_target.stringValue.UseVariable)
		{
			
			if (textEdit)
			{
				bool ReturnKey = false;
				if (Event.current.type == EventType.KeyDown && Event.current.character == '\n')
				{
					ReturnKey = true;
				}
				
					
				_scroll = GUILayout.BeginScrollView(_scroll,"box", GUILayout.Height (200),GUILayout.ExpandWidth(false));
				GUISkin skin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector);
				skin.textField.wordWrap = true;
				_target.stringValue.Value = GUILayout.TextArea(_target.stringValue.Value,"Label");
				TextEditor editor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
				GUILayout.EndScrollView();
						
				if (ReturnKey)
				{
					if (_target.stringValue.Value.Length==editor.selectPos)
					{
						_scroll.y = Mathf.Infinity;
					}
				}
				
				
				
				if(GUILayout.Button("Close text edit mode"))
				{
					textEdit = false;
				}
			}else{
				EditField("stringValue");
				if(GUILayout.Button("Open text edit mode"))
				{
					textEdit = true;
				}
			}
			
			
			
		}else{
			EditField("stringValue");
		}
		//edited = DataMakerActionEditorUtils.EditFsmXmlSourceField(_target.Fsm,_target.xmlSource);
		
		
		EditField("everyFrame");
		
		return GUI.changed || edited;
    }

}
