using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour, IJsonSaveable
{
    float speed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))//우측 이동
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))//좌측 이동
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))//위쪽 이동
            transform.Translate(0, speed * Time.deltaTime, 0);
        if (Input.GetKey(KeyCode.DownArrow))//아래 이동
            transform.Translate(0, -speed * Time.deltaTime, 0);
    }

    public JToken CaptureAsJToken()
    {

        return transform.position.ToToken();
    }

    public void RestoreFromJToken(JToken state)
    {
        transform.position = state.ToVector3();
    }
}
