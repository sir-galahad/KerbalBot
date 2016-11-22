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
using Commands;
namespace KerbalBot
{
	/// <summary>
	/// Description of Fun.
	/// </summary>
	public static class Fun
	{
		static string[] definitions={
			"{0} is a small springfed pond on the outskirts of Las Vegas.",
			"{0} is a lesser known character in greek mythology best known for being sneezed into the sun by Zeus.",
			"One drop of {0} can poison the watershed for hundreds of years.",
			"{0} is most commonly remembered for once attempting to eat a donut the size of their head",
			"Who cares what {0} is. I've got my own problems",
			"{0} was the code name for the top secret pie eating contest that decided the out come of the cold war.",
			"{0} was the name of a short-lived BBQ viscera burger marketed by McDonald's",
			"{0} is likely to be the \"must have\" Christmas Gift of 2018.",
			"The {0} are a race of large eared exceptionally greedy aliens from Star Trek: The Next Generation.",
			"{0} was a revolutionary videogame that allowed the player to use camera technology to create a life like avatar, and then fuck themselves",
			"I haven't heard the name {0} in a long time...",
			"{0} is the name of a *special* massage parlor in Detroit, MI, unable to afford the overhead of happy endings they were the first to give their customers reasonable conclusions"
		};
		public static string WhatIs(string input)
		{
			Random rand=new Random();
			return String.Format(definitions[rand.Next()%definitions.Length],input);
		}
	}
}
