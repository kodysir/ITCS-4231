using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FacePlayerCam : MonoBehaviour
{
    private Transform pCam;
    // Start is called before the first frame update
    void Start()
    {
        GameObject eyes = GameObject.FindGameObjectWithTag("PlayerView");
        pCam = eyes.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (pCam != null)
        {
            transform.rotation = pCam.rotation * Quaternion.Euler(0, 180, 0);
        }
    }
}
