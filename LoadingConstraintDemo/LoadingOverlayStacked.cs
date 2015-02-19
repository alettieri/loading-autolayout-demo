using System;
using MonoTouch.UIKit;
using System.Drawing;
using System.Collections.Generic;

namespace LoadingConstraintDemo {

	public class LoadingOverlayStacked : UIView {
		// control declarations

		UIActivityIndicatorView ActivitySpinner;

		UILabel LoadingLabel;


		int SpinnerWidth = 20;
		int SpinnerHeight = 20;

		// Will control the margin between the label and spinner
		int margin = 5;

		public LoadingOverlayStacked () : base (UIScreen.MainScreen.Bounds)
		{
			// Set the background transparency
			BackgroundColor = UIColor.FromRGBA (0f, 0f, 0f, 0.65f);
			AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

			// create the activity spinner
			ActivitySpinner = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.White) {

				// Important when using AutoLayout
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			// create and configure the "Loading Data" label
			LoadingLabel = new UILabel (){ 

				BackgroundColor = UIColor.Clear,
				TextColor = UIColor.White,
				Text = "Loading Data...",
				TextAlignment = UITextAlignment.Center,

				// Important when using AutoLayout
				TranslatesAutoresizingMaskIntoConstraints = false
			};

			ActivitySpinner.StartAnimating ();

			AddSubviews (new UIView[]{ ActivitySpinner, LoadingLabel });

			// Important part of setting up constraints. 
			// Will call UpdateConstraints on the base implementation.
			//
			SetNeedsUpdateConstraints ();
		}

		public override void UpdateConstraints ()
		{
			// Check to see if the View needs to update it's constraints
			if(NeedsUpdateConstraints()) {
				setupConstraintStacked (); // Uncomment to view Stacked
			}
			base.UpdateConstraints (); // Important, always call this last
		}


		/// <summary>
		/// Fades out the control and then removes it from the super view
		/// </summary>
		public void Hide ()
		{
			UIView.Animate (
				0.5, // duration
				() => { Alpha = 0; },
				() => { RemoveFromSuperview(); }
			);
		}

		#region PrivateMethods

		void setupConstraints() {

			List<NSLayoutConstraint> constraints = new List<NSLayoutConstraint> ();

			// Views and Metrics (tokens) we'll be passing to the Visual Format
			var viewMetrics = new Object[] { 
				"label", LoadingLabel,
				"spinner", ActivitySpinner,
				"spinnerHeight", SpinnerHeight,
				"spinnerWidth", SpinnerWidth,
				"margin", margin
			};


			// First specify the Horizontal Rule 
			// [Label] -- margin -- // [Spinner]
			//
			constraints.AddRange(
				NSLayoutConstraint.FromVisualFormat(
					"[label]-margin-[spinner]", 
					NSLayoutFormatOptions.AlignAllCenterY, 
					viewMetrics
				)
			);

			// Align the LoadingLabel with the View's CenterX position. 
			// Because of the constraint rule above, the spinner will be centered as well.
			//
			constraints.Add (
				NSLayoutConstraint.Create (
					LoadingLabel, // View we want to constraint
					NSLayoutAttribute.CenterX, // Vertically Center
					NSLayoutRelation.Equal, // Relationship
					this, // View we want to constrain to
					NSLayoutAttribute.CenterX, // Attribute to constrain to
					1, // Multiplier

					// pull the label UP by the spinners height
					-SpinnerHeight 
				)
			);

			// Align the LoadingLabel with the View's CenterY position.
			// We continue to maintain a constraint on the spinner, so it will be centered as well!
			//
			constraints.Add (
				NSLayoutConstraint.Create(
					LoadingLabel, // View we want to constrain
					NSLayoutAttribute.CenterY, // Horizontally center
					NSLayoutRelation.Equal, // Relationship
					this, // View we want to constrain to
					NSLayoutAttribute.CenterY, // Horizontally center
					1, // Multiplier
					0 // constant
				)
			);

			// Add the constraints to the view
			AddConstraints (constraints.ToArray ());

		}


		void setupConstraintStacked() {

			List<NSLayoutConstraint> constraints = new List<NSLayoutConstraint> ();

			// Views and Metrics (tokens) we'll be passing to the Visual Format
			var viewMetrics = new Object[] { 
				"label", LoadingLabel,
				"spinner", ActivitySpinner,
				"spinnerHeight", SpinnerHeight,
				"spinnerWidth", SpinnerWidth,
				"margin", margin
			};


			// First specify the Vertical Rule 
			// [Label]
			// -- margin --
			// [Spinner]
			//
			constraints.AddRange(
				NSLayoutConstraint.FromVisualFormat(
					"V:[label]-margin-[spinner]", 
					NSLayoutFormatOptions.AlignAllCenterX, 
					viewMetrics
				)
			);

			// Align the LoadingLabel with the View's CenterX position. 
			// Because of the constraint rule above, the spinner will be centered as well.
			//
			constraints.Add (
				NSLayoutConstraint.Create (
					LoadingLabel, // View we want to constraint
					NSLayoutAttribute.CenterX, // Vertically Center
					NSLayoutRelation.Equal, // Relationship
					this, // View we want to constrain to
					NSLayoutAttribute.CenterX, // Attribute to constrain to
					1, // Multiplier
					0 // constant
				)
			);

			// Align the LoadingLabel with the View's CenterY position.
			// We continue to maintain a constraint on the spinner, so it will be centered as well!
			//
			constraints.Add (
				NSLayoutConstraint.Create(
					LoadingLabel, // View we want to constrain
					NSLayoutAttribute.CenterY, // Horizontally center
					NSLayoutRelation.Equal, // Relationship
					this, // View we want to constrain to
					NSLayoutAttribute.CenterY, // Horizontally center
					1, // Multiplier

					// constant subtract the spinner height from the layout equation.
					// Basically, pull the LoadingLabel and spinner up 20 pixels
					-SpinnerHeight 
				)
			);

			// Add the constraints to the view
			AddConstraints (constraints.ToArray ());

		}


		#endregion
	}
}