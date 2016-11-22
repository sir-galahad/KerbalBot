/*standardised commands*/

namespace Commands
	{
	
	public interface ICommand //the basic blue print for all commands
		{
		string Prompt {get;} //the command name
		
		string Command(int priv, string[] args); 	//function to call command
														//the return value is the output
		}
	
	
	//A simple implementation of ICommand where output will be static
	public class BasicCommand : ICommand
		{
		string input;
		string output;
		public BasicCommand(string match, string response)
			{
			input=match;
			output=response;
			}
		
		public string Prompt
			{	
			get{return input;}
			}
		
		public string Command(int priv,string[] args)
			{
			return output;	
			}
		
		}
	
	//CommandDelegate is declared to be part of CallCommand
	public delegate string CommandDelegate(int priv,string[] args);
	
	//CallCommand is an Implementation of ICommand that allows users to create commands without
	//implementing the whole class
	public class CallCommand : ICommand
		{
		string input;
		CommandDelegate call;
		public CallCommand(string match,CommandDelegate cmd)
			{
			input=match;
			call=cmd;
			}
		public string Prompt
			{
			get{return input;}
			}
		public string Command(int priv, string[] args)
			{
			return call(priv,args);
			}	
		
		}	
	}
