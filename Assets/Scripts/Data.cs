using UnityEngine;
using System.Collections.Generic;

public class Data
{
  private Dictionary<string, int> RankingDict;
  private const string scores = "Scores";
  private const string ranking = "Ranking";

  public string Scores
  {
    get { return GetString(scores); }
    set { SetString(scores, value); }
  }
  public Dictionary<string, int> Ranking
  {
    get { return GetDictionary(ranking); }
    set { SetDictionary(ranking, value); }
  }


  public void InitRanking()
  {
    if (PlayerPrefs.HasKey(ranking))
      RankingDict = GetDictionary(ranking);
    else
    {
      Dictionary<string, int> rd = new Dictionary<string, int>();
      for (int i = 0; i < 11; i++)
        rd.Add($"--"+(new string(' ', i)), 0);
      RankingDict = rd;
      SetDictionary(ranking, RankingDict);
    }
  }

  private void SetDictionary(string key, Dictionary<string, int> value)
  {
    DictionaryWrapper<string, int> wrapper = new DictionaryWrapper<string, int>();
    wrapper.FromDictionary(value);
    string json = JsonUtility.ToJson(wrapper);
    PlayerPrefs.SetString(key, json);
  }

  private Dictionary<string, int> GetDictionary(string key)
  {
    string json = PlayerPrefs.GetString(key);
    DictionaryWrapper<string, int> wrapper = JsonUtility.FromJson<DictionaryWrapper<string, int>>(json);
    return wrapper.ToDictionary();
  }


  private void SetString(string key, string value)
  {
    PlayerPrefs.SetString(key, value);
  }

  private string GetString(string key)
  {
    return PlayerPrefs.GetString(key);
  }

  public void AddScore(int score)
  {
    string PlayerName = $"Player {Random.Range(1, 99)}";
    while (RankingDict.ContainsKey(PlayerName))
      PlayerName = $"Player {Random.Range(1, 99)}";
    RankingDict.Add(PlayerName, score);
    if (RankingDict.Count > 12)
    {
      string lowestPlayer = "";
      int lowestScore = int.MaxValue;
      foreach (var kvp in RankingDict)
      {
        if (kvp.Value < lowestScore)
        {
          lowestScore = kvp.Value;
          lowestPlayer = kvp.Key;
        }
      }
      RankingDict.Remove(lowestPlayer);
    }
    SetDictionary(ranking, RankingDict);

  }

}