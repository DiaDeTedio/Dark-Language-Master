using System;
using System.Windows.Forms;
using System.Drawing;

namespace CSharpEssentials.Designer.Controls
{
	public class NProgressBar : ProgressBar
	{
		/// <summary>
		/// Occurs when finished a timer ticking.
		/// </summary>
		public static event EventHandler StaticOnTimerTick;
		public event EventHandler OnTimerTick;
		string displayText;
		public string DisplayText
		{
			get
			{
				return displayText;
			}
			set
			{
				displayText = value;
				if(this.ParentForm != null)
				{
					this.ParentForm.Text = DisplayText;
				}
			}
		}
		Timer timer = new Timer();
		public Form ParentForm
		{
			get
			{
				return FindForm();
			}
		}
		bool cof;
		public bool CloseOnFinish
		{
			get
			{
				return cof;
			}
			set
			{
				cof = value;
			}
		}

		public NProgressBar () : base()
		{
		}
		protected override void Dispose (bool disposing)
		{
			timer.Stop();
			base.Dispose (disposing);
		}
		protected override void OnCreateControl ()
		{
			timer.Tick += Tick;
			timer.Interval = 1;
			timer.Start();
			base.OnCreateControl ();
		}
		void Tick(object sender,EventArgs e)
		{
			if(StaticOnTimerTick != null)
			{
				StaticOnTimerTick.Invoke(this,new EventArgs());
			}
			if(OnTimerTick != null)
			{
				OnTimerTick.Invoke(this,new EventArgs());
			}
			if(Value >= Maximum)
			{
				if(cof && this.ParentForm != null)
				{
					this.ParentForm.Close();
				}
			}
		}
		public static NProgressBar GetProgressBar(int width = 300,int height = 50,bool closeOnFinish = false,int step = 10)
		{
			Form f = new Form();
			f.ShowInTaskbar = false;
			f.FormBorderStyle = FormBorderStyle.FixedSingle;
			f.Size = new Size(width,height);
			f.Text = "Progress...";
			NProgressBar pb = new NProgressBar();
			pb.DisplayText = "Progress...";
			pb.Step = step;
			pb.CloseOnFinish = closeOnFinish;
			pb.Parent = f;
			pb.Dock = DockStyle.Fill;
			pb.Show();
			pb.Minimum = 0;
			pb.Maximum = 100;
			f.ShowDialog(Form.ActiveForm);
			return pb;
		}
		protected override void OnPaint (PaintEventArgs e)
		{
			base.OnPaint (e);
			e.Graphics.DrawString(DisplayText,Font,Brushes.Black,e.ClipRectangle);
		}
	}
}
