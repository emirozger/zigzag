
using UnityEngine;
using UnityEngine.UI;

    [RequireComponent(typeof(Text))]
    public class FPSCounter : MonoBehaviour
    {
        const float fpsMeasurePeriod = 0.5f;
        private int fpsAccumulator = 0;
        private float fpsNextPeriod = 0;
        private int currentFps;
        const string display = "{0} FPS";
        private Text text;
        
        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 999;
            fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
            text = GetComponent<Text>();
        }
        private void Update()
        {
            fpsAccumulator++;
            if (Time.realtimeSinceStartup > fpsNextPeriod)
            {
                currentFps = (int)(fpsAccumulator / fpsMeasurePeriod);
                fpsAccumulator = 0;
                fpsNextPeriod += fpsMeasurePeriod;
                text.text = string.Format(display, currentFps);
            }
        }
    }
