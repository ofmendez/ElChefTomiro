using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour
{

  [SerializeField] private Audio aud;
  [SerializeField] private Material[] grassMaterials;
  [SerializeField] private MeshRenderer backgroundMeshR;
  [SerializeField] private MeshRenderer cloudsMeshR;
  public bool isDay { get; private set; } = true;
  public float durationEnv { get; private set; } = 30.0f;
  private Color initialBgColor;

  private bool turningDay = false;
  private bool turningNight = false;
  public float turningTime = 0.0f;
  private float transitionPeriod = 4.0f;

  private void Awake()
  {
    initialBgColor = backgroundMeshR.material.color;
    isDay = true;
    SetDayVisual();
  }

  public void SetDayVisual()
  {
    foreach (Material mat in grassMaterials)
    {
      mat.color = Color.white;
    }
    cloudsMeshR.material.color = Color.white;
    backgroundMeshR.material.color = initialBgColor;
  }

  public void SetVisual(float clampValue)
  {
    foreach (Material mat in grassMaterials)
    {
      mat.color = Color.Lerp(Color.white, new Color(4 / 255f, 87 / 255f, 228 / 255f), clampValue);
    }
    cloudsMeshR.material.color = Color.Lerp(Color.white, new Color(94 / 255f, 94 / 255f, 94 / 255f), clampValue);
    backgroundMeshR.material.color = Color.Lerp(initialBgColor, new Color(36 / 255f, 61 / 255f, 71 / 255f), clampValue);
  }


  void Update()
  {
    if (turningDay)
    {
      // clamp progressive color change by SetVisual in "transitionPeriod" seconds counting by turningTime:
      SetVisual(1 - turningTime / transitionPeriod);
      turningTime += Time.deltaTime;
      if (turningTime > transitionPeriod)
      {
        isDay = true;
        turningDay = false;
        turningTime = 0.0f;
      }
    }
    if (turningNight)
    {
      // clamp progressive color change by SetVisual in "transitionPeriod" seconds counting by turningTime:
      SetVisual( turningTime / transitionPeriod);
      turningTime += Time.deltaTime;
      if (turningTime > transitionPeriod)
      {
        isDay = false;
        turningNight = false;
        turningTime = 0.0f;
      }
    }
  }

  public void Init()
  {
    aud.PlaySoundLoop( isDay? soundType.dayGameplay:soundType.nightGameplay);
    StartCoroutine(isDay?  TurnNight():TurnDay());
  }
  IEnumerator TurnNight()
  {
    yield return new WaitForSeconds(aud.Duration(soundType.dayGameplay));
    turningNight = true;
    aud.PlaySoundLoop(soundType.nightGameplay);
    StartCoroutine(TurnDay());
  }

  IEnumerator TurnDay()
  {
    yield return new WaitForSeconds(aud.Duration(soundType.nightGameplay));
    turningDay = true;
    aud.PlaySoundLoop(soundType.dayGameplay);
    StartCoroutine(TurnNight());
  }

  public void Reset()
  {
    StopAllCoroutines();
    if (turningDay)
    {
      SetVisual(0 );
      isDay = true;
    }
    if (turningNight)
    {
      SetVisual(1);
      isDay = false;
    }
    // SetDayVisual();
    // isDay = true;
    turningDay = false;
    turningNight = false;
    turningTime = 0.0f;
  }


}