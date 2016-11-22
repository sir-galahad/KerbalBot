/*
 * Created by SharpDevelop.
 * User: aaron
 * Date: 8/21/2014
 * Time: 5:38 PM
 * 
 * 
 */
using System;
using IrcFx;
using Commands;
using Kerbcalc;
using System.Text;
namespace KerbalBot
{
	/// <summary>
	/// Description of SampleHandler.
	/// </summary>
	public class MessageHandler:IrcMessageHandlerAdapter
	{
		CommandList CList=null;
		string Channel;
		public MessageHandler(string channel)
		{
			Channel=channel;
		}
		public override void OnChatMessage(IrcSession s,IrcUser sender, string target, string text){
			char[] trimOut=new Char[1];
			string cmd=null;
			string key;
			trimOut[0]='\x0001';
			
			if(CList==null){
				CList=new CommandList();
				CList.AddLocalCommand(new EvaluateCommand(s,Channel),2);
				CList.AddLocalCommand(new CallCommand("!whatis",new CommandDelegate((o,a)=>Fun.WhatIs(a[1]))),2);
				CList.AddLocalCommand(new BasicCommand("!ping","PONG!"),2);
				CList.AddLocalCommand(new CallCommand("!help",new CommandDelegate((o,a)=>{
				                                                                  	StringBuilder sb=new StringBuilder("Available commands: ");
				                                                                  	foreach(string st in CList.GetCommands(2)){
				                                                                  		sb.Append(st);
				                                                                  		sb.Append(' ');
				                                                                  	}
				                                                                  	return sb.ToString();
				                                                                  })),2);
			}
			if(text[0]==1 && text[text.Length-1]==1){
				text=text.Trim(trimOut);
				cmd=text.Split(' ')[0];
			
				text=text.Remove(0,cmd.Length);
				text=text.Trim();
			}
			key=text.Split(' ')[0];
			if(cmd!=null && cmd=="ACTION"){
				Console.WriteLine("*{0} {1}*",sender.CurrentNick,text);
			}
			else{
				Console.WriteLine("<{0}> {1}",sender.CurrentNick,text);
			}
			if(target.ToLower()==Channel.ToLower()){
				string outp=CList.Parse(2,text);
				if(outp!=null){
					s.Msg(Channel, CList.Parse(2,text));
				}
			}
		}
		public override void OnNotice(IrcSession s,IrcUser User, string Target, string Text){
			Console.WriteLine("-*{0}*- {1}",User.CurrentNick,Text);
		}
		public override void OnServerReply(IrcSession s,short code,string data){
			Console.WriteLine("{1}Server: {0}",data,code);
		}
		public override void OnDisconnect(IrcSession s){
			Environment.Exit(0);
		}
		public override void OnUserJoinedChannel(IrcSession s,string channel,IrcUser user){
			Console.WriteLine("{0} joined {1} ({2})",user.CurrentNick,channel,s.GetChannelUsers(channel).Count);
		}	                                                                                   
		public override void OnUserPartedChannel(IrcSession s, string channel,IrcUser user,string message){
			Console.WriteLine("{0} parted {1} [{2}]({3})",user.CurrentNick,channel,message,s.GetChannelUsers(channel).Count);
		}
		public override void OnUserQuit(IrcSession s,string[] affectedChannels,IrcUser user,string message){
			Console.WriteLine("{0} quit [{1}]",user.CurrentNick,message);
		}
		public override void OnUserNickChanged(IrcSession s,string[] affectdChannels,string oldNick, string newNick){
			Console.WriteLine("{0} has changed their nick to {1}",oldNick,newNick);
		}
		public override void OnUserKicked(IrcSession s,string channel,string kicker,string kickee,string message){
			Console.WriteLine("{0} kicked by {1} [{2}]",kickee,kicker,message);
		}
		public override void OnChannelModeChanged(IrcSession s,string channel,IrcUser user,string message){
			Console.WriteLine("{0} set mode to {1}",user,message);
		}
		public override void OnUserModeChanged(IrcSession s,IrcUser user, string change){
			Console.WriteLine("User mode set to: [{0}]",change);
		}
	}
}
