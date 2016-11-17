/*
 * Created by SharpDevelop.
 * User: aaron
 * Date: 09/16/2014
 * Time: 21:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
//using CSharpEval;
namespace Kerbcalc
{
	/// <summary>
	/// Description of CodeFinishedHandlerObject.
	/// </summary>
	public class CodeFinishedHandlerObject:MarshalByRefObject
	{
		Action<object,string> Handler;
		public CodeFinishedHandlerObject(Action<object,string> handler)
		{
			Handler=handler;
		}
		public virtual void OnCodeFinished(string output){
			Handler(null,output);
		}
	}
}
