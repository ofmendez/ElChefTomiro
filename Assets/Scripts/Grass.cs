using UnityEngine;
using System;

public class Grass : MonoBehaviour
{
  public float speed = 5f;
  private float leftEdge;


  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 4f;
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
}
