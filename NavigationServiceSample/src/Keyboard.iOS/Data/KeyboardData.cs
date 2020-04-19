using System;
namespace Keyboard.iOS.Data
{
    /// <summary>
    /// Data about where the keyboard appears on the screen.
    /// </summary>
    public class KeyboardData
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        public bool IsVisible { set; get; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public nfloat Height { set; get; }
    }
}
