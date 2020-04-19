using System;
using System.Reactive;
using Foundation;
using Keyboard.iOS.Data;

namespace Keyboard.iOS.Abstraction
{
    public interface IKeyboardService
    {
        /// <summary>
        /// Gets the keyboard changed.
        /// </summary>
        /// <value>The keyboard changed.</value>
        IObservable<KeyboardData> KeyboardChanged { get; }
    }
}
