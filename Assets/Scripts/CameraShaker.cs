using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private Vector3 cameraInitPos;
    public float shakeMagnitude = 0.05f;

    public static CameraShaker singleton;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
    }

    public void Shake(float shakeTime)
    {
        cameraInitPos = Camera.main.transform.position;
        InvokeRepeating("StartShake", 0f, 0.005f);
        Invoke("StopShake", shakeTime);
    }

    private void StartShake()
    {
        float cameraShakeOffsetX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float cameraShakeOffsetY = Random.value * shakeMagnitude * 2 - shakeMagnitude;

        Vector3 cameraIntermediatePosition = Camera.main.transform.position;
        cameraIntermediatePosition.x += cameraShakeOffsetX;
        cameraIntermediatePosition.y += cameraShakeOffsetY;

        Camera.main.transform.position = cameraIntermediatePosition;
    }
    private void StopShake()
    {
        CancelInvoke("StartShake");
        Camera.main.transform.position = cameraInitPos;
    }
}
