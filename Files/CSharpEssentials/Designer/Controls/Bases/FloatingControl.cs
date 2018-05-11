using System;
using CSharpEssentials.Designer.Forms.Bases;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace CSharpEssentials.Designer.Controls.Bases
{
	public class FloatingControl : EmptyBorderlessForm
	{
		Timer timer = new Timer();
		Control parent;
		public Control ControlParent
		{
			get
			{
				return parent;
			}
			set
			{
				parent = value;
			}
		}
		public FloatingControl()
		{
			timer.Tick += timer_Tick;
			timer.Interval = 1;
			timer.Start();
		}
		List<Tuple<Condition,Action>> Executors = new List<Tuple<Condition, Action>>();
		public void AddExecutor(Condition condition,Action action)
		{
			Executors.Add(new Tuple<Condition, Action>(condition,action));
		}
		public delegate bool Condition();
		public delegate void Action();
		public static T DisplayFloatingControl<T>(IWin32Window owner = null,int width = 100,int height = 100,bool hasDialog = false,Point? pos = null,Keys? closeKey = null) where T : FloatingControl
		{
			var form = DisplayNewForm<T>(owner,width,height,hasDialog,pos,closeKey);
			return form;
		}
		bool keepMP;
		Point? MOffset;
		public void KeepInMousePos(bool keep = false,Point? offset = null,Size? size = null)
		{
			keepMP = keep;
			MOffset = offset;
			if(size != null)
			{
				Size = size.Value;
			}
		}
		void timer_Tick(object sender,EventArgs e)
		{
			if(keepMP)
			{
				var loc = MousePosition;
				if(MOffset != null)
				{
					loc.X += MOffset.Value.X;
					loc.Y += MOffset.Value.Y;
				}
				if(Form.ActiveForm != null)
				{
					this.Location = loc;
				}
			}
			foreach(var executor in Executors)
			{
				if(executor.Item1())
				{
					executor.Item2();
				}
			}
			OnTimerTick();
		}
		protected virtual void OnTimerTick()
		{

		}
		protected override void OnClosed (EventArgs e)
		{
			timer.Stop();
			base.OnClosed (e);
		}
	}
}
