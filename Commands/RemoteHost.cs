/*
 * Created by SharpDevelop.
 * User: Aaron
 * Date: 4/10/2006
 * Time: 8:15 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http; 
namespace Commands
{
	/// <summary>
	/// Description of RemoteHost.
	/// </summary>
	public static class RemoteHost
	{
		public static void StartServing(Type RemoteCommandType,string Name,short Port)
		{
			ChannelServices.RegisterChannel(new HttpChannel(Port),false);
			WellKnownServiceTypeEntry servicetype=new WellKnownServiceTypeEntry
										(RemoteCommandType,Name,
				 						WellKnownObjectMode.SingleCall);
			RemotingConfiguration.ApplicationName=Name ;
			RemotingConfiguration.RegisterWellKnownServiceType(servicetype);
		}
	}
}
