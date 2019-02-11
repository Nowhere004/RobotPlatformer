using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour {

    
    private Camera mainCam;
    private float shakeamount = 0;

    private void Awake()
    {
        if (mainCam==null)
        {
            mainCam = Camera.main;
        }
    }


    public void CamShake(float amt,float lenght)
    {
        shakeamount = amt;
        InvokeRepeating("DoShake", 0,0.01f);
        Invoke("StopShake", lenght);
    }

    void DoShake()
    {
        if (shakeamount>0)
        {
            Vector3 camPos = mainCam.transform.position;
            float offsetX = Random.value * shakeamount * 2 - shakeamount;
            float offsetY = Random.value * shakeamount * 2 - shakeamount;
            camPos.x+=offsetX;
            camPos.y += offsetY;
            mainCam.transform.position = camPos;
        }
    }

    void StopShake()
    {
        CancelInvoke("DoShake");
        mainCam.transform.localPosition = Vector3.zero;
    }

}
