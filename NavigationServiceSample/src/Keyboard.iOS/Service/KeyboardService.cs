using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Foundation;
using Keyboard.iOS.Abstraction;
using Keyboard.iOS.Data;
using Keyboard.iOS.Extensions.Notification;
using UIKit;

namespace Keyboard.iOS.Service
{
    public class KeyboardService : UIViewController, IKeyboardService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Keyboard.iOS.Service.KeyboardService"/> class.
        /// </summary>
        public KeyboardService()
        {
            SetupKeyboardSubscription();
        }

        /// <summary>
        /// The keyboard changed.
        /// </summary>
        private readonly Subject<KeyboardData> _keyboardChanged = new Subject<KeyboardData>();

        /// <summary>
        /// Gets the keyboard changed.
        /// </summary>
        /// <value>The keyboard changed.</value>
        public IObservable<KeyboardData> KeyboardChanged => _keyboardChanged;

        /// <summary>
        /// Setups the keyboard subscription.
        /// </summary>
        private void SetupKeyboardSubscription()
        {
            NSNotificationCenter
                .DefaultCenter
                .Notifications(UIKeyboard.WillShowNotification)
                .Merge(NSNotificationCenter
                        .DefaultCenter
                        .Notifications(UIKeyboard.WillHideNotification))
                .Subscribe(HandlesKeyboardNotifications);
        }

        /// <summary>
        /// Handleses the keyboard notifications.
        /// </summary>
        /// <param name="notification">Notification.</param>
        private void HandlesKeyboardNotifications(NSNotification notification)
        {
            // Check if the keyboard is becoming visible
            bool keyboardVisibile = notification.Name == UIKeyboard.WillShowNotification;

            // Start an animation, using values from the keyboard
            UIView.BeginAnimations("AnimateForKeyboard");
            UIView.SetAnimationBeginsFromCurrentState(true);
            UIView.SetAnimationDuration(UIKeyboard.AnimationDurationFromNotification(notification) <= 0.0f ? 0.3f : UIKeyboard.AnimationDurationFromNotification(notification));
            UIView.SetAnimationCurve((UIViewAnimationCurve)UIKeyboard.AnimationCurveFromNotification(notification));

            // Pass the notification, calculating keyboard height, etc.
            bool landscape = InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || InterfaceOrientation == UIInterfaceOrientation.LandscapeRight;
            if (keyboardVisibile)
            {
                var keyboardFrame = UIKeyboard.FrameEndFromNotification(notification);
                _keyboardChanged.OnNext(new KeyboardData { IsVisible = keyboardVisibile, Height = landscape ? keyboardFrame.Width : keyboardFrame.Height });
            }
            else
            {
                var keyboardFrame = UIKeyboard.FrameBeginFromNotification(notification);
                _keyboardChanged.OnNext(new KeyboardData { IsVisible = keyboardVisibile, Height = landscape ? keyboardFrame.Width : keyboardFrame.Height });
            }

            // Commit the animation
            UIView.CommitAnimations();
        }
    }
}
