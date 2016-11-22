/*
 * Created by SharpDevelop.
 * User: Aaron
 * Date: 4/9/2006
 * Time: 10:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Runtime.Remoting;

namespace Commands
{
	/// <summary>
	/// a remotable abstract class implementing ICommand
	/// </summary>

	public abstract class RemoteCommand : MarshalByRefObject, ICommand
		{
		
		public virtual string Prompt
		{
			get
			{
			return "";
			}
		}
		public virtual string Command(int priv,string[] args)
			{
			return "";
			}
		}
	
}
