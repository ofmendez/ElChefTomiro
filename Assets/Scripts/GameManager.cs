using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }

  [SerializeField] private Player player;
  [SerializeField] private Spawner spawner;
  [SerializeField] private Text scoreText;
  [SerializeField] private GameObject playButton;
  [SerializeField] private GameObject gameOver;
  [SerializeField] private GameObject header;
  [SerializeField] private GameObject ranking;
  [SerializeField] private Environment env;
  [SerializeField] private Audio aud;
  private Data data = new Data();

  public bool isPlaying { get; private set; } = false;
  public bool isInterlude { get; private set; } = false;
  public float timePlaying { get; private set; } = 0.0f;

  private float scaleOfStop = 0.001f; //change in  Ranking too

  public int score { get; private set; } = 0;

  private void Awake()
  {
    if (Instance != null)
      DestroyImmediate(gameObject);
    else
      Instance = this;
  }

  private void OnDestroy()
  {
    if (Instance == this)
      Instance = null;
  }

  private void Start()
  {
    aud.PlaySoundLoop(soundType.header);
    Pause(0.0f);
    data.InitRanking();
  }
  private void Update()
  {
    if ((Input.GetKeyDown(KeyCode.A) || Input.GetMouseButtonDown(0)) && !isPlaying)
      StartCoroutine(isInterlude ? RankingSoundThenEnable() : InitSoundThenPlay());
    if (isPlaying)
    {
      timePlaying += Time.deltaTime;
    }
  }

  IEnumerator InitSoundThenPlay()
  {
    Ready();
    yield return new WaitForSeconds(0.6f * scaleOfStop);
    RemoveScreens();
    yield return new WaitForSeconds(0.6f * scaleOfStop);
    Play();
  }

  IEnumerator RankingSoundThenEnable()
  {
    if (!aud.isInSoundLoop)
    {
      aud.PlaySoundLoop(soundType.ranking);
      ranking.SetActive(true);
      yield return new WaitForSeconds(aud.Duration(soundType.ranking) * scaleOfStop * 9.0f);
      aud.StopSoundLoop();
      isInterlude = false;
    }
  }

  public void Pause(float scale)
  {
    Time.timeScale = scale;
    player.enabled = false;
    spawner.enabled = false;
  }

  public void Ready()
  {
    aud.PlaySound(soundType.salto);
    aud.PlaySound(soundType.go);
    // env.SetDayVisual();
    env.Reset();
    Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);
    for (int i = 0; i < pipes.Length; i++)
      Destroy(pipes[i].gameObject);
    spawner.enabled = true;
    Time.timeScale = scaleOfStop;
    player.ResetPosition();
    score = 0;
    scoreText.text = score.ToString();
    timePlaying = 0.0f;
    isPlaying = true;
  }

  private void RemoveScreens()
  {
    playButton.SetActive(false);
    gameOver.SetActive(false);
    ranking.SetActive(false);
    header.SetActive(false);
  }

  public void Play()
  {
    env.Init();
    Time.timeScale = 1f;
    player.enabled = true;
  }

  public void GameOver()
  {
    if (player.enabled)
      StartCoroutine(ShowGameOver());
  }

  IEnumerator ShowGameOver()
  {
    aud.PlaySound(soundType.muerte);
    aud.StopSoundLoop();
    Pause(scaleOfStop);
    data.AddScore(score);
    yield return new WaitForSeconds(0.1f * scaleOfStop);
    player.Fall();
    yield return new WaitForSeconds(1.4f * scaleOfStop);
    player.SetFallSprite();
    gameOver.SetActive(true);
    //env.SetDayVisual();
    isPlaying = false;
    isInterlude = true;
    ranking.GetComponent<Ranking>().UpdateRanking(data);
  }

  public void IncreaseScore(bool isLogo)
  {
    score++;
    scoreText.text = score.ToString();
    aud.PlaySound(isLogo ? soundType.go : soundType.comida);
  }

}
