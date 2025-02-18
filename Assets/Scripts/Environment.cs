using UnityEngine;


public class Environment : MonoBehaviour
{

  [SerializeField] private Audio aud;
  [SerializeField] private Material[] grassMaterials;
  [SerializeField] private MeshRenderer backgroundMeshR;
  [SerializeField] private MeshRenderer cloudsMeshR;
  public bool isDay { get; private set; } = true;
  public float durationEnv { get; private set; } = 30.0f;
  private Color initialBgColor;

  private void Awake()
  {
    initialBgColor = backgroundMeshR.material.color;
  }


  public void ToggleDayNight()
  {
    if (isDay)
      SetNight();
    else
      SetDay();
    isDay = !isDay;
  }

  public void SetDayVisual()
  {
    foreach (Material mat in grassMaterials)
    {
      mat.color = Color.white;
    }
    backgroundMeshR.material.color = initialBgColor;
    cloudsMeshR.material.color = Color.white;
  }
  private void SetNightVisual()
  {
    foreach (Material mat in grassMaterials)
    {
      mat.color = new Color(4 / 255f, 87 / 255f, 228 / 255f);
    }
    //rgb(36, 61, 71)
    backgroundMeshR.material.color = new Color(36 / 255f, 61 / 255f, 71 / 255f);
    //rgb(94, 94, 94)
    cloudsMeshR.material.color = new Color(94 / 255f, 94 / 255f, 94 / 255f);

  }
  private void SetDay()
  {
    SetDayVisual();
    aud.PlaySoundLoop(soundType.dayGameplay);
  }

  private void SetNight()
  {
    SetNightVisual();
    aud.PlaySoundLoop(soundType.nightGameplay);
  }

  public void Reset()
  {
    SetDayVisual();
    isDay = true;
  }


}