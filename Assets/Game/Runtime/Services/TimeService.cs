namespace Game.Runtime.Services
{
    public static class TimeService
    {
        public static float Time => UnityEngine.Time.time;
        public static float DeltaTime => UnityEngine.Time.deltaTime;
        public static float UnscaledTime => UnityEngine.Time.unscaledTime;
        public static float UnscaledDeltaTime => UnityEngine.Time.unscaledDeltaTime;
        public static float TimeSinceLevelLoad => UnityEngine.Time.timeSinceLevelLoad;
    }
}