/*
 * Created by SharpDevelop.
 * User: aaron
 * Date: 7/13/2010
 * Time: 10:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Runtime.Remoting;
using System.Threading;
using System.IO;
namespace Kerbcalc
{
	public interface IBleh{
		string function();
	}
	public class CSharpEvaluator : MarshalByRefObject
	{
		public event Action<object,string> CodeFinished;
		Evaluator eval=null;
		AppDomain sandbox=null;
		string extra;
		Assembly extraAssembly;
		public void TryCode(string code,string assembly)
		{
			extra=assembly;
			extraAssembly=Assembly.LoadFile(extra);
			TryCode(code);
		}
		public void TryCode(string code){
			if(eval!=null){
				Console.WriteLine("code already running");
				return;
			}
			AppDomainSetup setup=new AppDomainSetup();
			setup.ApplicationBase=AppDomain.CurrentDomain.SetupInformation.ApplicationBase+"\\.";
			PermissionSet permissions=new PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);
			permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
			Assembly thisAssembly=typeof(KerbalEval).Assembly;
			StrongName trustedAssembly=thisAssembly.Evidence.GetHostEvidence<StrongName>();
			sandbox=AppDomain.CreateDomain("Sandbox",null,setup,permissions,trustedAssembly);
			try{
				if (extra!=null)sandbox.Load(extraAssembly.FullName);
				eval=(Evaluator)sandbox.CreateInstanceFromAndUnwrap(typeof(Evaluator).Assembly.ManifestModule.FullyQualifiedName,
				                                         typeof(Evaluator).FullName);
				eval.SetCodeFinishedHandler(new CodeFinishedHandlerObject(new Action<object,string>(CodeFinishedHandler)));
				
				eval.Evaluate(code,extra);
			}catch(Exception Ex){
				Console.WriteLine(Ex.ToString());
				foreach(Assembly a in sandbox.GetAssemblies()){
					Console.WriteLine(a.FullName);
				}
				CodeFinishedHandler(null,Ex.Message);
			}
		}
		
		
		void CodeFinishedHandler(object s, string e){
			//if this method is called from the test code's thread in
			//the sandboxed domain unloading that domain will throw an exception
			//so we'll do it from a started in this domain
			new Thread(ShutDownSandbox).Start();
			CodeFinished(s,e);
		}
		
		void ShutDownSandbox(){
			eval=null;
			AppDomain.Unload(sandbox);
			if(CodeFinished!=null){	
				CodeFinished(this,"");
			}
		}
		public void StopCode(){
			CodeFinishedHandler(this,"");
		}
	}
}