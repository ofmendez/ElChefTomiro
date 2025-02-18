using UnityEngine;

public class RankingCard : MonoBehaviour
{
  public TMPro.TextMeshProUGUI nameTxt;
  public TMPro.TextMeshProUGUI pointsTxt;

    public void UpdateCard(string name, int points)
    {
      nameTxt.text = name;
      pointsTxt.text = points.ToString();
    }
}
