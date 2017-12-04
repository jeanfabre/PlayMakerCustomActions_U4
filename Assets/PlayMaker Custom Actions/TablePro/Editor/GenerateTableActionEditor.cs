using Game.Scripts.CustomPlayMakerActions.TablePro;
using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEngine;

[CustomActionEditor(typeof(GenerateTableAction))]
public class GenerateTableActionEditor : XmlCustomActionEditor
{

    public override bool OnGUI()
    {
        bool edited = false;
        GenerateTableAction _target = (GenerateTableAction)target;
	
        if (_target.xmlSource==null)
        {
            _target.xmlSource = new FsmXmlSource();
        }
	
        if (_target.XPathHeaders==null)
        {
            _target.XPathHeaders = new FsmXpathQuery();
        }

	    if (_target.XPathRows == null)
	    {
		    _target.XPathRows = new FsmXpathQuery();
	    }
	
        edited = edited || DataMakerActionEditorUtils.EditFsmXmlSourceField(_target.Fsm,_target.xmlSource);
				
        edited = edited || DataMakerActionEditorUtils.EditFsmXpathQueryField(_target.Fsm,_target.XPathHeaders);
	    edited = edited || DataMakerActionEditorUtils.EditFsmXpathQueryField(_target.Fsm,_target.XPathRows);
		
        EditField("xmlResult");
		
        return GUI.changed || edited;
    }
	
}
