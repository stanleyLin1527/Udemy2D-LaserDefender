using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeMagnitude;
    Vector3 initPos;

    void Start()
    {
        initPos = transform.position;
    }

    public void Play() { StartCoroutine(Shake());}

    IEnumerator Shake() {
        float elapseTime = 0f;
        while(elapseTime < shakeDuration) {
            transform.position = initPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapseTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initPos;
    }
}
