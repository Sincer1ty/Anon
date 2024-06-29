using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour, IJsonSaveable
{
    float speed = 2.0f;
    public List<int> list = new List<int>();
    Dictionary<int, int> dict = new Dictionary<int, int>()
    {
        {1, 1},
        {2, 2}
    };

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
        JObject state = new JObject();
        IDictionary<string, JToken> stateDict = state;

        stateDict["transform"] = transform.position.ToToken();
        stateDict["dict"] = JsonConvert.SerializeObject(dict);
        stateDict["list"] = JToken.Parse(JsonConvert.SerializeObject(list));

        return state;
    }

    public void RestoreFromJToken(JToken state)
    {        
        transform.position = state["transform"].ToVector3();
        dict = JsonConvert.DeserializeObject<Dictionary<int, int>>(state["dict"].ToString());
        list = state["list"].ToObject<List<int>>();
    }
}
