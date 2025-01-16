using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TouchController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch toch = Input.GetTouch(0);

            if (toch.phase == TouchPhase.Began)
            {
                // TODo background moves with touch
            }
        }
    }

    void HandleToch(Vector2 tochPos)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(tochPos);

    }
}
