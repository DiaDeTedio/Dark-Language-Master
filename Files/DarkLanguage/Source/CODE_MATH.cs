using System;
using DarkLanguage.Classes;
using System.Collections.Generic;

namespace DarkLanguage.Source
{
	public static class CODE_MATH
	{
		public static object DoMath(string code)
		{
			char[] mathops = {'+','-','/','*'};
			object ret = null;

			List<CodeOperation> operations = new List<CodeOperation>();
			if(code[0] != '+' && code[0] != '-' && code[0] != '/' && code[0] != '*')
			{
				code = code.Insert(0,"+");
			}
			while(code.Length > 0)
			{
				CodeOperation op = new CodeOperation();
				int p = code.IndexOf('+');
				int m = code.IndexOf('-');
				int d = code.IndexOf('/');
				int mp = code.IndexOf('*');
				int less = StringUtils.GetLess(-1,p,m,d,mp);
				if(less == p)
				{
					op.Operation = 0;
				}
				if(less == m)
				{
					op.Operation = 1;
				}
				if(less == d)
				{
					op.Operation = 2;
				}
				if(less == mp)
				{
					op.Operation = 3;
				}
				if(less != int.MaxValue)
				{
					string subs = StringUtils.CustomSubstring(code,1,mathops);
					op.Code = subs;
					op.Value = StringUtils.ToPossibleValue(subs,out op.Type);
					if(string.IsNullOrEmpty(subs))
					{
						break;
					}
					code = code.Replace(subs,"");
					code = code.Remove(0,1);
				}else
				{
					break;
				}
				operations.Add(op);
			}

			var plus = StringUtils.OnlySplit(code,'+','-','/','*');
			List<object> args = new List<object>();
			Type t = null;
			foreach(var p in plus)
			{
				Type nT = null;
				var obj = StringUtils.ToPossibleValue(p,out nT);
				if(t != null && nT != null && nT != t)
				{
					t = null;
					break;
				}else
				{
					args.Add(obj);
				}
				t = nT;
			}
			if(t != null)
			{
				ret = DoMathOperation(t,0,args.ToArray());
				t = null;
			}
			args.Clear();

			return ret;
		}
		#region Math
		/// <summary>
		/// Dos the mathematical operation.
		/// </summary>
		/// <returns>The mathematical operation result.</returns>
		/// <param name="type">Type.</param>
		/// <param name="operation">Operation of this Math(0=Plus,1=Subtract,2=Divide,3=Multiply.</param>
		/// <param name="args">Arguments.</param>
		static object DoMathOperation(Type type,int operation = 0,params object[] args)
		{
			object ret = null;
			int start = 0;
			int i = 0;
			float f = 0;
			if((operation==1||operation==2||operation==3)&&type == typeof(int)){i = (int)args[0];start=1;}
			if((operation==1||operation==2||operation==3)&&type == typeof(float)){f = (float)args[0];start=1;}
			for(int index=start;index<args.Length;index++)
			{
				var arg = args[index];
				if(operation == 0)
				{
					if(type == typeof(int)){i += (int)arg;ret = i;}
					if(type == typeof(float)){f += (float)arg;ret = f;}
				}
				if(operation == 1)
				{
					if(type == typeof(int)){i -= (int)arg;ret = i;}
					if(type == typeof(float)){f -= (float)arg;ret = f;}
				}
				if(operation == 2)
				{
					if(type == typeof(int)){i /= (int)arg;ret = i;}
					if(type == typeof(float)){f /= (float)arg;ret = f;}
				}
				if(operation == 3)
				{
					if(type == typeof(int)){i *= (int)arg;ret = i;}
					if(type == typeof(float)){f *= (float)arg;ret = f;}
				}
			}
			return ret;
		}

		#endregion
		public class CodeOperation
		{
			public string Code;
			public object Value;
			public int Operation;
			public Type Type;

			public CodeOperation(string code="",object value = null,int operation = -1)
			{
				Code = code;
				Value = value;
				Operation = operation;
			}
		}
	}
}
