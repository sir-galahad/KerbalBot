/*
 * Created by SharpDevelop.
 * User: Aaron2
 * Date: 11/15/2016
 * Time: 8:35 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using System.Reflection;
using System.IO;

namespace Kerbcalc
{
	/// <summary>
	/// Description of KerbalEval.
	/// </summary>
	public class KerbalEval
	{
	
		string AssemblyName= Path.GetFullPath(".//KerbalData.dll");
		Action<object,string> handler;
		CSharpEvaluator eval;
		public KerbalEval()
		{
			eval=new CSharpEvaluator();
			
		}
		
		public void Evaluate(string code){
			code=code.Replace(';',' ');
			eval.CodeFinished+=handler;
			eval.TryCode(code,AssemblyName);
			System.Threading.Thread.Sleep(1000);
		}
		public void SetCodeFinishedHandler(Action<object,string> handler)
		{
			this.handler=handler;
		}
	}
}
