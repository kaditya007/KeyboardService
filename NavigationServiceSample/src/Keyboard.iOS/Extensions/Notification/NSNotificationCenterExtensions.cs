using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Foundation;

namespace Keyboard.iOS.Extensions.Notification
{
    public static class NSNotificationCenterExtensions
    {
        /// <summary>
        /// Notificationses the specified notification key.
        /// </summary>
        /// <param name="notificationCenter">The notification center.</param>
        /// <param name="notificationKey">The notification key.</param>
        /// <returns>An observable sequence of NSNotifications.</returns>
        public static IObservable<NSNotification> Notifications(this NSNotificationCenter notificationCenter, NSString notificationKey) =>
          Observable.Create<NSNotification>(obs =>
          {
              var nsObserver = notificationCenter.AddObserver(notificationKey, obs.OnNext);
              return Disposable.Create(() => notificationCenter.RemoveObserver(nsObserver));
          });
    }
}
