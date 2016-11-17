/*
 * Created by SharpDevelop.
 * User: aaron
 * Date: 9/3/2014
 * Time: 7:50 PM
 * 
 * 
 */
using System;
using System.Linq;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Threading;
//using System.Xml;

namespace Kerbcalc
{
	/// <summary>
	/// Description of Evaluator.
	/// </summary>
	
	public class Evaluator:MarshalByRefObject
	{
		public string codePrefix{get; set;}
		public string codePostfix{get;set;}
		public string extra;
		string output;
		CodeFinishedHandlerObject CodeFinished;
		Thread testThread;
		public Evaluator()
		{
			codePrefix="using System;"+
				"using System.Collections.Generic;"+
				"using System.Text;"+
				"using System.Xml.Linq;"+
				"using System.Linq;"+
				"using System.Threading;"+
				"using KerbalData;"+
				"public class Bleh:System.MarshalByRefObject{public string function(){try{return( ";
			
			codePostfix=").ToString();}catch(ThreadAbortException ex){Console.WriteLine(\"Aborted\"); return \"aborted\";}" +
				"catch(Exception ex){Console.WriteLine(ex.Message);return \"An Exception occured\";}}}";
		}
		public Evaluator(string pre,string post){
			codePrefix=pre;
			codePostfix=post;
		}
		public void Evaluate(string code,string extra)
		{
			this.extra=extra;
			Evaluate(code);
		}
		public void Evaluate(string code){
			
			
			CSharpCodeProvider compiler=new CSharpCodeProvider();
			
			
			int[] bleh={0,1,2,3};
			
			StringBuilder sb=new StringBuilder();
			sb.Append(codePrefix);
			sb.Append(code);
			sb.Append(codePostfix);
			PermissionSet perm=new PermissionSet(PermissionState.None);
			perm.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			perm.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			perm.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			
			perm.Assert();
			//Assembly.LoadFrom(typeof(Evaluator).Assembly.ManifestModule.FullyQualifiedName);
			//AppDomain.CurrentDomain.Load(
				
			//CodeFinished=new CodeFinishedHandlerObject(delegate(object o,EventArgs ev){Console.WriteLine("hihihi");});
			CompilerParameters options=new CompilerParameters();
			options.GenerateExecutable=false;
			options.GenerateInMemory=true;
			options.ReferencedAssemblies.Add("System.Xml.Linq.dll");
			options.ReferencedAssemblies.Add("System.Xml.dll");
			options.ReferencedAssemblies.Add("System.Core.dll");
			if(extra!=null)options.ReferencedAssemblies.Add(extra);
			//options.OutputAssembly=".\\bleh.dll";		
			CompilerResults result=compiler.CompileAssemblyFromSource(options,sb.ToString());
			if(result.Errors.Count>0){
				Console.WriteLine(result.Errors[0].ToString());
				//CodeFinished.OnCodeFinished();
				throw new Exception("compile failed");
			}
			CodeAccessPermission.RevertAssert();
			//Assembly source=Assembly.Load("bleh");
			Assembly source;
			source=result.CompiledAssembly;
			Object target = source.CreateInstance("Bleh");
			dynamic d=target;
			testThread=new Thread(new ThreadStart(delegate(){output=d.function();OnCodeFinished();}));
			testThread.Start();
			
		}
		public bool StillRunning{get{return testThread.IsAlive;}}
		
		public void SetCodeFinishedHandler(CodeFinishedHandlerObject handler){
			PermissionSet perm=new PermissionSet(PermissionState.None);
			perm.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			perm.AddPermission(new EnvironmentPermission(PermissionState.Unrestricted));
			perm.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			
			perm.Assert();
			CodeFinished=(CodeFinishedHandlerObject)handler;
		
			CodeAccessPermission.RevertAssert();
		}
		
	
		void OnCodeFinished(){
			if(CodeFinished!=null) CodeFinished.OnCodeFinished(output);			
		}
		
	}
}
