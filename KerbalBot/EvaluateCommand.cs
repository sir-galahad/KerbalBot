/*
 * Created by SharpDevelop.
 * User: Aaron2
 * Date: 11/21/2016
 * Time: 9:42 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Commands;
using Kerbcalc;
using IrcFx;
namespace KerbalBot
{
	/// <summary>
	/// You know...like a command...or something.
	/// </summary>
	public class EvaluateCommand:ICommand 
	{
		KerbalEval eval;
		IrcSession Session;
		string Channel;
		int sent;
		int calls;
		public EvaluateCommand(IrcSession s,string channel)
		{
			sent=0;
			calls=0;
			Session=s;
			Channel=channel;
			//eval.SetCodeFinishedHandler((o,output)=>{Session.Msg(Channel,output);sent++;});
		}
		public string Prompt{get{return "!calc";}}
		
		public string Command(int priv,string[] args)
		{
			string code="";
			
			for(int x=1;x<args.Length;x++)
			{
				code=code+args[x];
				code=code+" ";
			}
			eval=new KerbalEval();
			eval.SetCodeFinishedHandler((o,output)=>{Session.Msg(Channel,output);sent++;});
			eval.Evaluate(code);
			calls++;
			return null;
			
		}
	}
}
