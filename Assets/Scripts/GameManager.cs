using UnityEngine;
using UnityEngine.UI;


public enum soundType
{
  Comida,
  logo,
  muerte,
  Salto
}
[DefaultExecutionOrder(-1)]

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] private Player player;
	[SerializeField] private Spawner spawner;
	[SerializeField] private Text scoreText;
	[SerializeField] private GameObject playButton;
	[SerializeField] private GameObject gameOver;
	[SerializeField] private AudioClip[] sounds;

	public int score { get; private set; } = 0;

	private void Awake()
	{
		if (Instance != null)
		{
			DestroyImmediate(gameObject);
		}
		else
		{
			Instance = this;
		}
	}

	private void OnDestroy()
	{
		if (Instance == this)
		{
			Instance = null;
		}
	}

	private void Start()
	{
		Pause();
	}
	private void Update()
	{
		if ((Input.GetKeyDown(KeyCode.A) || Input.GetMouseButtonDown(0)) && gameOver.activeSelf)
		{
			Play();
		}
	}

	public void Pause()
	{
		Time.timeScale = 0f;
		player.enabled = false;
	}

	public void Play()
	{
		score = 0;
		scoreText.text = score.ToString();

		playButton.SetActive(false);
		gameOver.SetActive(false);

		Time.timeScale = 1f;
		player.enabled = true;

		Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);

		for (int i = 0; i < pipes.Length; i++)
		{
			Destroy(pipes[i].gameObject);
		}
	}

	public void GameOver()
	{
		playButton.SetActive(true);
		gameOver.SetActive(true);
    spawner.ResetSpeed();
    PlaySound(soundType.muerte);
		Pause();
	}

	public void IncreaseScore()
	{
		score++;
		scoreText.text = score.ToString();
    PlaySound(soundType.Comida);
	}

  public void PlaySound(soundType type)
  {
    AudioSource.PlayClipAtPoint(sounds[(int)type], Camera.main.transform.position);
  }

}
