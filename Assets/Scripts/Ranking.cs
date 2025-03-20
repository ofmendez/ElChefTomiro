using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class Ranking : MonoBehaviour
{
  List<RankingCard> rankingCards = new List<RankingCard>();
  float timeInsideRanking = 0.0f;
  float scaleOfStop = 0.001f; //change in  Ranking GameManager
  float timeToStay = 60.0f;
    void LoadCards()
    {
      rankingCards = GetComponentsInChildren<RankingCard>( true ).ToList();
    }

    void OnEnable(){
      timeInsideRanking = 0.0f;
    }

    void Update()
    {
      timeInsideRanking += Time.deltaTime;
      if (timeInsideRanking > timeToStay*scaleOfStop)
      {
        timeInsideRanking = 0.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
    }


    public void UpdateRanking(Data data)
    {
      if (rankingCards.Count == 0)
        LoadCards();

      Dictionary<string, int> ranking = data.Ranking;
      ranking = ranking.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
      for (int i = 0; i < rankingCards.Count; i++)
      {
        if (i < ranking.Count)
        {
          System.Collections.Generic.KeyValuePair<string, int> kvp = ranking.ElementAt(i);
          rankingCards[i].UpdateCard(kvp.Key, kvp.Value);
        }
        else
        {
          rankingCards[i].UpdateCard("Player", 0);
        }
      }
    }
}
