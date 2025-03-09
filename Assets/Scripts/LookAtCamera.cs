using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    
    private enum Mode
    {
        LookAt,
        LookAtInvert,
        LookFoward,
        LookFowardInvert
    }
    
    [SerializeField] private Mode mode;
 
    void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInvert:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            
            case Mode.LookFoward:
                transform.forward = Camera.main.transform.forward;
                break;
            
            case Mode.LookFowardInvert:
                transform.forward = -Camera.main.transform.forward;
                break;
            
            default :
                transform.LookAt(Camera.main.transform.position);
                break;
        }
    }
}
