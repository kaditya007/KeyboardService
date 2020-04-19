using System;
using Keyboard.iOS.Service;
using Rocket.Surgery.Extensions.Testing.Fixtures;

namespace Keyboard.Tests
{
    public class KeyboardServiceFixture : ITestFixtureBuilder
    {
        public static implicit operator KeyboardService(KeyboardServiceFixture keyboardService) => keyboardService.Build();

        private KeyboardService Build() => new KeyboardService();
    }
}
