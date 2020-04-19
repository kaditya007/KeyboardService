using System;
using FluentAssertions;
using Foundation;
using Keyboard.iOS.Data;
using Keyboard.iOS.Service;
using UIKit;
using Xunit;

namespace Keyboard.Tests
{
    public sealed class KeyboardServiceTests
    {
        public class KeyboardVisibility
        {
            [Fact]
            public void Should_Be_True_If_Keyboard_Shows()
            {
                //Given
                KeyboardService sut = new KeyboardServiceFixture();
                var result = new KeyboardData();
                sut.KeyboardChanged.Subscribe(d => result = d);

                //When
                NSNotificationCenter.DefaultCenter.PostNotificationName(UIKeyboard.WillShowNotification, null);

                //Then
                result.IsVisible.Should().BeTrue();

            }

            [Fact]
            public void Should_Be_Faslse_If_Keyboard_Hides()
            {
                //Given
                KeyboardService sut = new KeyboardServiceFixture();
                var result = new KeyboardData();
                sut.KeyboardChanged.Subscribe((d => result = d));
              
                //When
                NSNotificationCenter.DefaultCenter.PostNotificationName(UIKeyboard.WillHideNotification, null);
              
                //Then
                result.IsVisible.Should().BeFalse();
            }
        }


        public class KeyboardChanged
        {
            [Fact]
            public void Should_Notify_If_Keyborad_Shows()
            {
                KeyboardService sut = new KeyboardServiceFixture();
                var isNotified = false;
                sut.KeyboardChanged.Subscribe(_ => isNotified = true);

                //When
                NSNotificationCenter.DefaultCenter.PostNotificationName(UIKeyboard.WillShowNotification, null);

                //Then
                isNotified.Should().BeTrue();
            }

            [Fact]
            public void Should_Notify_If_Keyborad_Hide()
            {
                KeyboardService sut = new KeyboardServiceFixture();
                var isNotified = false;
                sut.KeyboardChanged.Subscribe(_ => isNotified = true);

                //When
                NSNotificationCenter.DefaultCenter.PostNotificationName(UIKeyboard.WillHideNotification, null);

                //Then
                isNotified.Should().BeTrue();
            }
        }

        public class KeyboardHeight
        {
            [Fact]
            public void Should_Be_Greater_Than_Zero_If_Notified()
            {
                //Given
                KeyboardService sut = new KeyboardServiceFixture();
                var result = new KeyboardData();
                sut.KeyboardChanged.Subscribe(d => result = d);

                //When
                NSNotificationCenter.DefaultCenter.PostNotificationName(UIKeyboard.WillShowNotification, null);
               
                //Then
                result.Height.Should().BeGreaterThan(0);
            }
        }
    }
}
