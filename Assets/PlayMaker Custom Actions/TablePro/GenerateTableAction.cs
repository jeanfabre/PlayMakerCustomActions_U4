using System.Diagnostics;
using System.Xml;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using SLS.Widgets.Table;

namespace Game.Scripts.CustomPlayMakerActions.TablePro
{
	[ActionCategory("TablePro")]
	[Tooltip("Generates a table based on header columns and rows provided from xml xpath")]
	public class GenerateTableAction : DataMakerXmlActions
	{
		[ActionSection("XML Source")]
		public FsmXmlSource xmlSource;

		private XmlNodeList _headerNodelist;
		private XmlNodeList _rowNodeList;
		
		[ActionSection("Table")]
		
		[RequiredField]
		[Tooltip("The gameObject with the Table component")]
		[CheckForComponent(typeof(Table))]
		public FsmOwnerDefault gameObject;

		[ActionSection("Headers")]
		[RequiredField] 
		[Tooltip("XPath for headers. For example table/tr/th")] 
		public FsmXpathQuery XPathHeaders;

		[ActionSection("Rows")]
		[RequiredField]
		[Tooltip("XPath for rows. For example table/tr[position()>1]")]
		public FsmXpathQuery XPathRows;
		
		private Table _table;

		public override void Reset()
		{
			XPathHeaders = null;
			XPathRows = null;
			xmlSource = null;
			_headerNodelist = null;
			_rowNodeList = null;
			gameObject = null;
			_table = null;
		}

		public override void OnEnter()
		{
			GenerateTable();
			Finish();
		}

		private void GenerateTable()
		{
			if (xmlSource.Value == null)
			{
				LogWarning("Xml Source is empty, or likely invalid");
			}

			var targetObject = Fsm.GetOwnerDefaultTarget(gameObject);
			
			if (targetObject == null)
			{
				return;
			}

			_table = targetObject.GetComponent<Table>();
			
			if (_table != null)
			{
				_table.ResetTable();

				// Add Column Headers

				_headerNodelist = xmlSource.Value.SelectNodes(XPathHeaders.ParseXpathQuery(this.Fsm));
				if (_headerNodelist == null) { LogWarning("Headers xpath is empty, or likely invalid"); return; }
				
				for (int i = 0; i < _headerNodelist.Count; i++)
				{
					var header = _headerNodelist[i];
					_table.AddTextColumn(header.InnerText);
				}
				
				// Initialize the table
				_table.Initialize();
				
				_table.data.Clear();
				
				_rowNodeList = xmlSource.Value.SelectNodes(XPathRows.ParseXpathQuery(this.Fsm));
				if (_rowNodeList == null) { LogWarning("Rows xpath is empty, or likely invalid"); return; }

				for (int i = 0; i < _rowNodeList.Count; i++)
				{
					var rowData = _rowNodeList[i].ChildNodes;
					var d = Datum.Body(i.ToString());
					for (int j = 0; j < rowData.Count; j++)
					{
						d.elements.Add(rowData[j].InnerText);
					}
			
					_table.data.Add(d);
				}
		
				_table.StartRenderEngine();
				
			}
			else
			{
				LogError("There is no table component on this gameobject.");
			}
		}
	}
}
