using UnityEngine;

public class Player : MonoBehaviour
{
  public Sprite[] sprites;
  public float strength = 5f;
  public float gravity = -9.81f;
  public float tilt = 5f;
  public FallPlayer fall;
  public Audio aud;

  private SpriteRenderer spriteRenderer;
  private Vector3 direction;
  private int spriteIndex;

  private Vector3 initialPosition;
  private Vector3 initialRotation;

  private void Awake()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    initialPosition = transform.position;
    initialRotation = transform.eulerAngles;
  }

  private void Start()
  {
    InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
  }

  public void ResetPosition()
  {
    transform.position = initialPosition;
    transform.eulerAngles = initialRotation;
    fall.enabled = false; 
    direction = Vector3.zero;
    gravity = -9.81f;
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.A) || Input.GetMouseButtonDown(0))
    {
      if (transform.position.y < 5.2f)
      {
        aud.PlaySound(soundType.salto);
        direction = Vector3.up * strength;
      }
    }

    // Apply gravity and update the position
    direction.y += gravity * Time.deltaTime;
    transform.position += direction * Time.deltaTime;

    // Tilt the bird based on the direction
    Vector3 rotation = transform.eulerAngles;
    rotation.z = direction.y * tilt;
    transform.eulerAngles = rotation;
  }

  public void Fall()
  {
    fall.enabled = true; 
  }

  private void AnimateSprite()
  {
    spriteIndex++;

    if (spriteIndex >= sprites.Length)
    {
      spriteIndex = 0;
    }

    if (spriteIndex < sprites.Length && spriteIndex >= 0)
    {
      spriteRenderer.sprite = sprites[spriteIndex];
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("Obstacle"))
    {
      GameManager.Instance.GameOver();
      gravity = gravity* 500;
    }
    else if (other.gameObject.CompareTag("Scoring"))
    {
      other.gameObject.transform.parent.GetComponent<Pipes>().Catch();
      GameManager.Instance.IncreaseScore();
    }
  }

}
