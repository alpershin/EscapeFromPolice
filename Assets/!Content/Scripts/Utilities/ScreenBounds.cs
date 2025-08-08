namespace Game.Core
{
    using UnityEngine;

    public class ScreenBounds
    {
        public float ScreenWidth => CalculateWidth();
        public float ScreenHeight => CalculateHeight();

        private Camera _camera => Camera.main;

        private float CalculateWidth()
        {
            float aspect = (float)Screen.width / Screen.height;
            float worldWidth = CalculateHeight() * aspect;
            
            return worldWidth;
        }

        private float CalculateHeight()
        {
            return _camera.orthographicSize * 2;;
        }
    }
}