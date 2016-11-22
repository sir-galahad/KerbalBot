/*
 * Created by SharpDevelop.
 * User: ValuedCustomer
 * Date: 3/9/2005
 * Time: 2:38 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
namespace Commands
{
	public class CommandList
		{
		Dictionary<string,ICommand>[] Commands=new Dictionary<string,ICommand>[3]{
		new Dictionary<string,ICommand>(), // ring 0	(owner)
		new Dictionary<string,ICommand>(), // ring 1	(operator)
		new Dictionary<string,ICommand>()	// ring 2	(user)
		};

		public string Parse(int priv,string input)
			{
			string[] args=System.Text.RegularExpressions.Regex.Split(input,@"\s");
			int x;
			for(x=priv;x<3;x++)
				{
				if(Commands[x].ContainsKey(args[0]))
					{
					try{
						return Commands[x][args[0]].Command(priv,args);
					}
					catch(System.Exception e)
						{
						this.RemoveCommand(0,args[0]);
						return e.GetType().Name+" occured calling remote command "+args[0]+" it has been removed";
						}
					
					}
				}
			return null;
			}
		public bool AddLocalCommand(ICommand cmd, int priv)
			{
			int x;
			for(x=0;x<3;x++)
			if(Commands[x].ContainsKey(cmd.Prompt))
				return false;
			if((priv>2)||(priv<0)){return false;}
			Commands[priv].Add(cmd.Prompt,cmd);
			return true;
			}
		public bool AddRemoteCommand(string address,int priv)
			{
			ICommand remoteCommand;
			try{
				remoteCommand=(ICommand)Activator.GetObject(typeof(RemoteCommand),address);
				remoteCommand.Prompt.ToString();
			}
			catch(Exception)
			{
				Console.WriteLine("Caught Exception");
				return false;
			}
			if(remoteCommand==null) return false;
			Console.WriteLine("adding");
			return AddLocalCommand(remoteCommand,priv);
			}

	//this will remove all commands with the Prompt name priv is provided so users can't 
	//remove other operator/owner commands
	//returns true on success
	public bool RemoveCommand(int priv,string name)
			{
			int x;
			Type cmdtype;
			bool returnvalue=false;
			for(x=priv;x<3;x++)
				{
				if(Commands[x].ContainsKey(name))
					{
					cmdtype=typeof(RemoteCommand);
					if(!cmdtype.IsInstanceOfType(Commands[x][name])){
					   return false;
					   }
					Commands[x].Remove(name);
					returnvalue=true;
				   	}
				}
			return returnvalue;
			}
	//GetCommands will return a list of commands available at privilege level priv
	public string[] GetCommands(int priv)
	{
		List<string> tmp=new List<string>();
		int x;
		for(x=priv;x<3;x++)
		{
			foreach(string commandName in Commands[x].Keys){
				tmp.Add(commandName);
			}
		}
		return tmp.ToArray();
	}
	}
}
