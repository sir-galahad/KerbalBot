/*
 * Created by SharpDevelop.
 * User: Aaron2
 * Date: 11/15/2016
 * Time: 10:33 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace KerbalData
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	
	public static class Universe
	{
		public static readonly double G=6.674*Math.Pow(10,-11);
		//public static readonly double G= 9.8*Math.Pow(Kerbin.Radius,2)/Kerbin.Mass;
	}
	public static class Kerbin
	{
		public static readonly double Mass=5.292*Math.Pow(10,22);
		public static readonly double Radius=600000;
		public static readonly double g=Mass*Universe.G/Math.Pow(Radius,2);
		
	}
	public static class Mun
	{
		public static readonly double Mass=9.76*Math.Pow(10,20);
		public static readonly double Radius=200000;
		public static readonly double g=Mass*Universe.G/Math.Pow(Radius,2);
	}
	public static class Minmus
	{
		public static readonly double Mass=2.646*Math.Pow(10,19);
		public static readonly double Radius=60000;
		public static readonly double g=Mass*Universe.G/Math.Pow(Radius,2);
	}
}