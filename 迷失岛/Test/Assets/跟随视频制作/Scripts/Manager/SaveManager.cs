using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MySingleton<SaveManager>
{
    public void Save(Object data, string key)
    {
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }

    public void Load(Object data, string key)
    {
        if (PlayerPrefs.HasKey(key))
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), data);
    }

    public void Save<T>(List<T> data, string key)
    {
        //Debug.LogWarning("save list");
        string jsonData = JsonUtility.ToJson(new Serialization<T>(data));
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }

    public void Load<T>(out List<T> data, string key)
    {
        //Debug.LogWarning("load list");
        if (PlayerPrefs.HasKey(key))
            data = JsonUtility.FromJson<Serialization<T>>(PlayerPrefs.GetString(key)).ToList();
        else
            data = new List<T>();
    }

    public void Save<TKey, TValue>(Dictionary<TKey, TValue> data, string key)
    {
        string jsonData = JsonUtility.ToJson(new Serialization<TKey,TValue>(data));
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
        Debug.LogWarning("prop save" + key + " " + data.Count);
    }

    public void Load<TKey, TValue>(out Dictionary<TKey, TValue> data, string key)
    {
        if (PlayerPrefs.HasKey(key))
            data = JsonUtility.FromJson<Serialization<TKey, TValue>>(PlayerPrefs.GetString(key)).ToDictionary();
        else
            data = new Dictionary<TKey, TValue>();
        Debug.LogWarning(PlayerPrefs.HasKey(key) + "prop load" + key + " " + data.Count);
    }
}

// List<T>
[System.Serializable]
public class Serialization<T>
{
    [SerializeField]
    private List<T> target;

    public List<T> ToList()
    { return target; }

    public Serialization(List<T> target)
    {
        this.target = target;
    }
}

// Dictionary<TKey, TValue>
[System.Serializable]
public class Serialization<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys;

    [SerializeField]
    private List<TValue> values;

    private Dictionary<TKey, TValue> target;

    public Dictionary<TKey, TValue> ToDictionary()
    { return target; }

    public Serialization(Dictionary<TKey, TValue> target)
    {
        this.target = target;
    }

    public void OnBeforeSerialize()
    {
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }

    public void OnAfterDeserialize()
    {
        var count = Mathf.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, TValue>(count);
        for (var i = 0; i < count; ++i)
        {
            target.Add(keys[i], values[i]);
        }
    }
}