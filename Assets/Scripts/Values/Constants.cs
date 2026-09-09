public static class Constants
{
    public static class Player
    {
        public const float MoveSpeed = 8.0f;
        public const float RunSpeed = 11.0f;
        public const float GroundAcceleration = 30.0f;
        public const float GroundDeceleration = 36.0f;
        public const float AirAcceleration = 18.0f;
        public const float JumpForce = 8.0f;

        public const float MaxStamina = 3.0f;
        public const float RunStaminaCostPerSecond = 1.8f;
        public const float StaminaRecoveryPerSecond = 4.5f;

        public const float DodgeSpeed = 14.0f;
        public const float DodgeDuration = 0.18f;
        public const float DodgeRecoveryDuration = 0.25f;

        public const float HardLandingVelocity = 12.0f;
        public const float KnockdownLandingVelocity = 18.0f;
    }

    public static class Input
    {
        public const string PlayerActionMap = "Player";
        public const string MoveAction = "Move";
        public const string JumpAction = "Jump";
    }
}
