using System;
using MonoTouch.UIKit;

namespace LoadingConstraintDemo
{
	public class MainViewController : UITabBarController
	{

		public override void LoadView ()
		{
			base.LoadView ();

			// Add the Stacked and Side by Side loading examples.
			ViewControllers = new UIViewController[]{ 
				new UIViewController(){
					Title = "Stacked",
					View = new LoadingOverlayStacked ()
				},
				new UIViewController() {
					Title = "Side by Side",
						View = new LoadingOverlaySideBySide ()
				}
			};

		}
	}
}

