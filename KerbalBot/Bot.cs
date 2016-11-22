/*
 * Created by SharpDevelop.
 * User: Aaron2
 * Date: 11/16/2016
 * Time: 9:50 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using IrcFx;
using System.Collections.Generic;
using System.IO;

using Kerbcalc;
namespace KerbalBot
{
	
	class Bot
	{
		IrcSession mySession;
		
		string channel="#kerbalbotprogram";
		
		
		public static void Main(string[] args){
			Bot client=new Bot();
		
			client.Run();
			
		} 
		
		public void Run(){	
			//I had been using hardcoded user information in this file but since it's about to go up
			//on github i've hastily taken that out and put in this ugly Console based transaction
			
			string userName="Kerbcalc";
			string nickNames="kerbcalc kerbcalc1 kerbcalc2";
			string password="nicetry.";
			if(!File.Exists("password")){
				FileStream fs=File.OpenWrite("password");
				StreamWriter sw=new StreamWriter(fs);
				Console.WriteLine("enter password");
				sw.WriteLine(Console.ReadLine());
				sw.Close();
				sw.Dispose();
			 }
			StreamReader sr=new StreamReader(File.OpenRead("password"));
			password=sr.ReadLine().Trim();
			sr.Close();
			sr.Dispose();
			
			IrcNetworkInfo mynet=new IrcNetworkInfo("bleh");
			mynet.AddServer("irc.freenode.net",6667,password);
			IrcUser me=new IrcUser("A. Realname",userName,nickNames.Split(" ".ToCharArray()));
			mySession=new IrcSession(me,mynet,new MessageHandler(channel));
			mySession.Connect();
			if(mySession.Connected==true)
				Console.WriteLine("Connected");
			else{
				Console.WriteLine("Connection Failed!!");
				return;
			}
			mySession.JoinChannel(channel,null);
			
			
			
			while(true){
				string bleh=Console.ReadLine();
				HandleLocalInput(bleh);
			}
		}
		
		public void HandleLocalInput(string input){
			char[] seperator=new Char[1];
			seperator[0]=' ';
			input=input.Trim();
			string[] args=input.Split(seperator);
			switch(args[0]){
				case "":
					break;
				case "/quit":
					mySession.Quit("gala out!");
					break;
				case "/msg":
					if(args.Length<3){break;}
					String text=args[2];
					if(args.Length>3){
						for(int x=3;x<args.Length;x++){
							text=String.Concat(text," ");
							text=String.Concat(text,args[x]);
						}
					}
					mySession.Msg(args[1],text);
					break;
				case "/list":
					List<IrcNick> users=mySession.GetChannelUsers(channel);
					if(users==null)break;
					foreach(IrcNick nick in users){
						Console.Write("{0} ",nick.RawNick);
					}
					Console.WriteLine("");
					break;
				case "/me":
					if(args.Length<2){break;}
					text=args[1];
					if(args.Length>1){
						for(int x=2;x<args.Length;x++){
							text=String.Concat(text," ");
							text=String.Concat(text,args[x]);
						}
					}
					mySession.Action(channel,text);
					break;
				case "/mode":
					if(args.Length==1){
						mySession.SetUserMode(null,false);
						break;
					}
					if(args[1][0]!='+'&&args[1][0]!='-') break;
					mySession.SetUserMode(args[1].Substring(1),args[1][0]=='-');
					break;
				case "/cmode":
					if(args.Length<1) break;
					if(args.Length==1){
						mySession.SetChannelMode(channel,null,null,false);
						break;
					}
					if(args[1][0]!='+'&&args[1][0]!='-') break;
					string ModeOption=null;
					if(args.Length>=3)
						ModeOption=args[2];
					Console.WriteLine(ModeOption);
					mySession.SetChannelMode(channel,args[1].Substring(1),ModeOption,args[1][0]=='-');
					break;
				default:
					if(input[0]=='/'){break;}
					mySession.Msg(channel,input);
					break;
			} 
		}
	}
}