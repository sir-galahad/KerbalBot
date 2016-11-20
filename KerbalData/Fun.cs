/*
 * Created by SharpDevelop.
 * User: Aaron2
 * Date: 11/20/2016
 * Time: 11:37 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
namespace KerbalData
{
	/// <summary>
	/// Description of Fun.
	/// </summary>
	public static class Fun
	{
		static string[] definitions={
			"{0} is a small springfed pond on the outskirts of Las Vega.",
			"{0} is a lesser known character in greek mythology best known for being sneezed into the sun by Zeus.",
			"One drop of {0} can poison the watershed for hundreds of years.",
			"{0} is most commonly remembered for once attempting to eat a donut the size of their head"
		};
		public static string WhatIs(string input)
		{
			Random rand=new Random();
			return String.Format(definitions[rand.Next()%definitions.Length],input);
		}
	}
}
