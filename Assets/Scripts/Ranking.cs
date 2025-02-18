using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Ranking : MonoBehaviour
{
  List<RankingCard> rankingCards = new List<RankingCard>();
    void LoadCards()
    {
      rankingCards = GetComponentsInChildren<RankingCard>( true ).ToList();
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
