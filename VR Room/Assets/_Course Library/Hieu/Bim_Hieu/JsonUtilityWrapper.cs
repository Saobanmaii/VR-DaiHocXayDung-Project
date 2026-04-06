using System;
using System.Collections.Generic;
using UnityEngine;

public static class JsonUtilityWrapper
{
    [Serializable]
    private class Wrapper<T>
    {
        public List<string> keys;
        public List<T> values;
    }

    public static Dictionary<string, T> FromJson<T>(string json)
    {
        var dict = new Dictionary<string, T>();
        var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);

        for (int i = 0; i < wrapper.keys.Count; i++)
        {
            dict[wrapper.keys[i]] = wrapper.values[i];
        }

        return dict;
    }
}