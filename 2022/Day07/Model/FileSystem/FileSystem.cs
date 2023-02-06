using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Day07.Model.Commands;

namespace Day07.Model.FileSystem;

internal class FileSystem
{
	readonly Directory root = new("/");
	Stack<Directory> path;

	public FileSystem()
	{
		CdRoot();
	}

	public void RecreateCommandResults(IEnumerable<ICommand> commands)
	{
		foreach (var command in commands)
		{
			RecreateCommandResult(command);
		}
	}

	private void RecreateCommandResult(ICommand command)
	{
		if(command is CD cd)
			EnterDir(cd);
		else if(command is LS ls)
			RecreateStructure(ls);
		else
			throw new Exception("What is this !#¤!#¤%");
	}

	private void EnterDir(CD cd)
	{
		if(cd.Target == "..")
			path.Pop();
		else if(cd.Target == "/")
			CdRoot();
		else
			EnterSubdir(cd);
	}

	private void EnterSubdir(CD cd)
	{
		var child = Current.GetChild(cd.Target);
		if(child != null && child is Directory childDir)
			path.Push(childDir);
	}

	[MemberNotNull(nameof(path))]
	private void CdRoot()
	{
		path = new();
		path.Push(root);
	}

	private void RecreateStructure(LS ls)
	{
		foreach(var item in ls.Content)
		{
			// Right now just adding parsed item instead of making new structure ¯\_(ツ)_/¯
			Current.AddContent(item);
		}
	}

	private Directory Current => path.Peek();
	public Directory GetRoot() => root;
}
