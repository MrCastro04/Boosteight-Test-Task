using System;

namespace Modules.Content.Player
{
    public static class PlayerEvents
    {
        public static event Action OnLose;
        public static event Action OnJump;

        public static void ExecuteEventPlayerLose()
        {
            OnLose?.Invoke();
        }
        public static void ExecuteEventPlayerJump()
        {
            OnJump?.Invoke();
        }
    }
}