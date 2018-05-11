using System;
using System.Windows.Forms;

namespace CSharpEssentials.Designer
{
	public static class ControlUtils
	{
		public static Control MouseControl
		{
			get
			{
				return GetMouseControl();
			}
		}
		static Control GetMouseControl()
		{
			Control ret = null;
			foreach(Form form in Application.OpenForms)
			{
				var controls = form.GetAllControls();
				foreach(var control in controls)
				{
					if(control.IsMouseOn()){ret = control;break;}
				}
			}
			return ret;
		}
		public static Control GetMouseControl(params Type[] exceptions)
		{
			Control ret = null;
			foreach(Form form in Application.OpenForms)
			{
				var controls = form.GetAllControls();
				foreach(var control in controls)
				{
					if(control.IsMouseOn() && !exceptions.Contains(control.GetType()))
					{
						ret = control;break;
					}
				}
			}
			return ret;
		}
		public static Control GetMouseControl(params Control[] exceptions)
		{
			Control ret = null;
			foreach(Form form in Application.OpenForms)
			{
				var controls = form.GetAllControls();
				foreach(var control in controls)
				{
					if(control.IsMouseOn() && !exceptions.Contains(control)){ret = control;break;}
				}
			}
			return ret;
		}
	}
}
