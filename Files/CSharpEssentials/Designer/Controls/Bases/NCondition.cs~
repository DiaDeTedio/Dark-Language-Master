using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CSharpEssentials.Designer.Controls.Bases
{
	public class ConditionBase
	{
		public object ConditionObj;
		public object ActionObj;
	}

	public class NCondition : ConditionBase
	{
		public Delegate Condition;
		public Delegate Action;

		public NCondition()
		{
			
		}

		NCondition(Delegate condition,Delegate action){Condition = condition;Action = action;}
		public void SetCondition<T>(Predicate<T> condition)
		{
			Condition = condition;
			ConditionObj = condition.Target;
		}
		public void SetAction<T>(Action<T> action)
		{
			Action = action;
			ActionObj = action.Target;
		}
		public void Invoke()
		{
			if((bool)Condition.DynamicInvoke(ConditionObj))
			{
				Action.DynamicInvoke(ActionObj);
			}
		}
		public NCondition Get<TCondition,TAction>(Predicate<TCondition> condition,Action<TAction> action)
		{
			var cond = new NCondition(condition,action);
			cond.ConditionObj = condition.Target;
			cond.ActionObj = action.Target;
			return cond;
		}
	}
	public class NCondition<TCondition,TAction> : ConditionBase
	{
		public Predicate<TCondition> Condition;
		public Action<TAction> Action;

		public NCondition()
		{

		}

		NCondition(Predicate<TCondition> condition,Action<TAction> action){Condition = condition;Action = action;}
		public void SetCondition(Predicate<TCondition> condition)
		{
			Condition = condition;
			ConditionObj = condition.Target;
		}
		public void SetAction(Action<TAction> action)
		{
			Action = action;
			ActionObj = action.Target;
		}
		public void Invoke()
		{
			if(Condition.Invoke((TCondition)ConditionObj))
			{
				Action.Invoke((TAction)ActionObj);
			}
		}
		public static NCondition<TCondition,TAction> Get(Predicate<TCondition> condition,Action<TAction> action)
		{
			var cond = new NCondition<TCondition,TAction>(condition,action);
			cond.ConditionObj = condition.Target;
			cond.ActionObj = action.Target;
			return cond;
		}
	}
	public class NCondition<TAction> : ConditionBase
	{
		public Delegate Condition;
		public Action<TAction> Action;

		public NCondition()
		{

		}

		NCondition(Delegate condition,Action<TAction> action){Condition = condition;Action = action;}
		public void SetCondition<TCondition>(Predicate<TCondition> condition)
		{
			Condition = condition;
			ConditionObj = condition.Target;
		}
		public void SetAction(Action<TAction> action)
		{
			Action = action;
			ActionObj = action.Target;
		}
		public void Invoke()
		{
			if((bool)Condition.DynamicInvoke(ConditionObj))
			{
				Action.Invoke((TAction)ActionObj);
			}
		}
		public static NCondition<TAction> Get<TCondition>(Predicate<TCondition> condition,Action<TAction> action)
		{
			var cond = new NCondition<TAction>(condition,action);
			cond.ConditionObj = condition.Target;
			cond.ActionObj = action.Target;
			return cond;
		}
	}
}
