using System;

namespace Modules.Content.Kill_Zone
{
    public static class TrapEvents
    {
        public static event Action OnActivateTrap;

        public static void ExecuteEventActivateTrap() => OnActivateTrap?.Invoke();
    }
}