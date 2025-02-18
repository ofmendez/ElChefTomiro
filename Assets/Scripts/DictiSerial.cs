using System.Collections.Generic;

[System.Serializable]
public class KeyValuePair<TKey, TValue>
{
    public TKey key;
    public TValue value;

    public KeyValuePair() {} // Required for deserialization

    public KeyValuePair(TKey key, TValue value)
    {
        this.key = key;
        this.value = value;
    }
}

[System.Serializable]
public class DictionaryWrapper<TKey, TValue>
{
    public List<KeyValuePair<TKey, TValue>> items = new List<KeyValuePair<TKey, TValue>>();

    // Convert Dictionary to List<KeyValuePair>
    public void FromDictionary(Dictionary<TKey, TValue> dict)
    {
        items.Clear();
        foreach (var kvp in dict)
        {
            items.Add(new KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value));
        }
    }

    // Convert List<KeyValuePair> back to Dictionary
    public Dictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
        foreach (var item in items)
        {
            dict[item.key] = item.value;
        }
        return dict;
    }
}