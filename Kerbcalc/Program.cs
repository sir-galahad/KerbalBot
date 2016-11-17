/*
 * Created by SharpDevelop.
 * User: Aaron2
 * Date: 11/10/2016
 * Time: 10:26 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Kerbcalc
{
	class Program
	{
		public static void Main(string[] args)
		{
			// TODO: Implement Functionality Here
			KerbalEval ke=new KerbalEval();
			while(true){
				string code=Console.ReadLine();
				ke.Evaluate(code);
			}
		
		}
	}
}