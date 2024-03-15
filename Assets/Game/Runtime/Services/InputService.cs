using UnityEngine;

namespace Game.Runtime.Services
{
    public static class InputService
    {
        public static float HorizontalInput => Input.GetAxis("Horizontal");
        public static float VerticalInput => Input.GetAxis("Vertical");
    }
}