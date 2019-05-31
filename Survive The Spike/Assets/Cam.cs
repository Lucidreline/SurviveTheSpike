using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 zeroVector3 = Vector3.zero;
    [SerializeField] float cameraSmoothing = .2f;

    [SerializeField] float clampWidth = 1;
    [SerializeField] float clampHeight = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosClamp(clampWidth, clampHeight);
        followTarget(target, cameraSmoothing);
    }

    void cameraPosClamp(float _clampWidth, float _clampHeight) {
        float xClamped = Mathf.Clamp(transform.position.x, -_clampWidth, _clampWidth);
        float yClamped = Mathf.Clamp(transform.position.y, -_clampHeight, _clampHeight);

        transform.position = new Vector3(xClamped, yClamped, transform.position.z);
    }

    void followTarget(Transform _target, float _smoothing) {

        Vector3 targetPosY = new Vector3(_target.position.x, transform.position.y, transform.position.z);
        Vector3 targetPosX = new Vector3(transform.position.x, _target.position.y, transform.position.z);

        //to remove the shake when target it outside the clamp
        if(Mathf.Abs( targetPosY.y) <= clampHeight) {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosX, ref zeroVector3, _smoothing);
        }
        if(Mathf.Abs(targetPosX.x) <= clampWidth) {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosY, ref zeroVector3, _smoothing);
        }
        
        
    }
}
