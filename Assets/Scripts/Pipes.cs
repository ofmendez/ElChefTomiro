using UnityEngine;
using System;

public class Pipes : MonoBehaviour
{
  public Transform[] transformBlocks;
  public Sprite[] sprites;
  public float speed = 5f;
  private float leftEdge;
  public GameObject pizza;

  private float center = 0.5f;


  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
  }

  public void SetBlockAndPizza(Vector3 position)
  {
    int randomIndex = UnityEngine.Random.Range(0, transformBlocks.Length);
    int index = 0;
    foreach (var block in transformBlocks)
    {
      block.gameObject.SetActive(index == randomIndex);
      index++;
    }
    float randomY = UnityEngine.Random.Range(1.7f, 3.7f);
    float offset = randomY * (position.y > center ? -1 : 1);
    pizza.transform.localPosition = new Vector3(0, offset, 0);
    randomIndex = UnityEngine.Random.Range(0, sprites.Length);
    pizza.GetComponent<SpriteRenderer>().sprite = sprites[randomIndex];
  }

  public void AddToSpeed(float incrementSpeed)
  {
    speed += incrementSpeed;
  }

  // Update is called once per frame
  void Update()
  {
    transform.position += speed * Time.deltaTime * Vector3.left;
    if (transform.position.x < leftEdge)
    {
      Destroy(gameObject);
    }
  }
  public void Catch()
  {
    pizza.SetActive(false);
  }
}
