using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static bool isShaking;

    // Start is called before the first frame update
    void Start()
    {
        isShaking = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            StartCoroutine(ScreenShake(0.15f, 0.3f));
        }
    }



    public IEnumerator ScreenShake(float time, float mag)
    {
        Vector3 origPosition = transform.position;
        float elapsedTime = 0.0f; 

        while (elapsedTime < time)
        {
            float x = Random.Range(-0.05f, 0.05f) * mag;
            float y = Random.Range(2.900f, 3.100f) * mag;

            transform.position = new Vector3(x, y, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return 0;
        }
        transform.position = origPosition;
    }
}
