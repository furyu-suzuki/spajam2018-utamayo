using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonHelper {

    [System.Serializable]
    class Wrapper<T> {
        public List<T> list;
    }

    public static List<T> ListFromJson<T>(string json) {
        var newJson = "{ \"list\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.list;
    }
}
