using UnityEngine;

public class  Pipes : MonoBehaviour
{
  public Transform transformBlock1;
  public Transform  transformBlock2;
  public float speed = 5f;
	private float leftEdge;
	public GameObject pizza;


  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
		leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
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
