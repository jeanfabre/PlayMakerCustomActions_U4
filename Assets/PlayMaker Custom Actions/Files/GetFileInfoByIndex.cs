// (c) Copyright HutongGames, LLC 2010-2020. All rights reserved.  
// License: Attribution 4.0 International(CC BY 4.0) 
// __ECO__ __PLAYMAKER__ __ACTION__

using UnityEngine;
using System.Collections;
using System.IO;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Files")]
	[Tooltip("Get infos from a file by index within a directory.")]
	public class GetFileInfoByIndex : FsmStateAction
	{

		[Tooltip("The path of the folder.")]
		[RequiredField]
		public FsmString folderPath;
		
		[Tooltip("The search pattern. Use * for all")]
		[RequiredField]
		public FsmString searchPattern;
		
		[Tooltip("The search options. let you search only in the folder or in all sub folders too")]
		public SearchOption searchOption;
		
		[Tooltip("The number of files in the folder")]
		[RequiredField]
		public FsmInt fileIndex;
		
		[Tooltip("The datetime format to get datetime properties of the file in the folder")]
		public FsmString dateTimeFormat;
		
		[ActionSection("Result")]
		
		[Tooltip("The name of the file")]
		[UIHint(UIHint.Variable)]
		public FsmString fileName;
		
		[Tooltip("The extension  of the file")]
		[UIHint(UIHint.Variable)]
		public FsmString extension;
		
		[Tooltip("The full name of the file")]
		[UIHint(UIHint.Variable)]
		public FsmString fullName;
		
		[Tooltip("The directory name of the file")]
		[UIHint(UIHint.Variable)]
		public FsmString directoryName;
		
		[Tooltip("The readonly state of the file")]
		[UIHint(UIHint.Variable)]
		public FsmBool isReadOnly;
		
		[Tooltip("The creation time of the file")]
		[UIHint(UIHint.Variable)]
		public FsmString creationTime;
		
		[Tooltip("The last access time of the file")]
		[UIHint(UIHint.Variable)]
		public FsmString lastAccessTime;
		
		[Tooltip("The last write time of the file")]
		[UIHint(UIHint.Variable)]
		public FsmString lastWriteTime;
		
		[Tooltip("The file size in bytes")]
		[UIHint(UIHint.Variable)]
		public FsmInt fileSize;
		
		[Tooltip("Event sent if no file found")]
		public FsmEvent IndexOutOfRangeEvent;
		
		public override void Reset()
		{
			folderPath = null;
			searchPattern ="*";
			searchOption = SearchOption.TopDirectoryOnly;
			fileIndex = 0;
			
			dateTimeFormat = "dddd, MMMM dd, yyyy h:mm:ss tt";
			IndexOutOfRangeEvent = null;
			directoryName = null;
			extension = null;
			fileName = null;
			fullName = null;
			isReadOnly = null;
			creationTime = null;
			lastAccessTime = null;
			lastWriteTime = null;
			fileSize = null;
		}

		public override void OnEnter()
		{
			DoGetFileInfoByIndex();
	
			Finish();
		}

		
		void DoGetFileInfoByIndex()
		{
			 string[] _files = Directory.GetFiles(folderPath.Value, searchPattern.Value, searchOption);
			
			
			if (_files.Length==0)
			{
				Fsm.Event(IndexOutOfRangeEvent);
			}
			
			if (fileIndex.Value<0 || fileIndex.Value>=_files.Length)
			{
				Fsm.Event(IndexOutOfRangeEvent);
			}
			
			FileInfo _info = new FileInfo(_files[fileIndex.Value]);
			
			if (!fileName.IsNone) fileName.Value = _info.Name;
			
			if (!extension.IsNone) extension.Value = Path.GetExtension(_info.FullName);
			
			if (!directoryName.IsNone) directoryName.Value = _info.DirectoryName;
			
			if (!fullName.IsNone) fullName.Value = _info.FullName;
			
			if (!isReadOnly.IsNone) isReadOnly.Value = _info.IsReadOnly;
			
			if (!creationTime.IsNone) creationTime.Value = _info.CreationTime.ToString(dateTimeFormat.Value);
			
			if (!lastWriteTime.IsNone) lastWriteTime.Value = _info.LastWriteTime.ToString(dateTimeFormat.Value);
			
			if (!lastAccessTime.IsNone) lastAccessTime.Value = _info.LastAccessTime.ToString(lastAccessTime.Value);
			
			if (!fileSize.IsNone) fileSize.Value = (int)_info.Length;
		}
		
	}
}