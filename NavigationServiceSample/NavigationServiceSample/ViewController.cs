using Foundation;
using Keyboard.iOS.Abstraction;
using Keyboard.iOS.Data;
using Keyboard.iOS.Service;
using System;
using UIKit;

namespace NavigationServiceSample
{
    public partial class ViewController : UIViewController
    {
        private readonly IKeyboardService _keyboardService;

        public ViewController(IntPtr handle) : base(handle)
        {
            _keyboardService = new KeyboardService();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            _keyboardService
                .KeyboardChanged
                .Subscribe( x => HandleKeyboardChanged(x) );
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void HandleKeyboardChanged(KeyboardData data)
        {
            //System.Diagnostics.Debug.WriteLine("IsVisible = " + data.IsVisible + " Height = " + data.Height);
            DisplayLabel.Text = "Visible = " + data.IsVisible + " Height = " + data.Height;
            //Do the cool stuff here
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            View.EndEditing(true);
           // base.TouchesBegan(touches, evt);
        }
    }
}