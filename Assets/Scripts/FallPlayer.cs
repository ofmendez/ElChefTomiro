using UnityEngine;

public class FallPlayer : MonoBehaviour
{
  private float gravity = -90.0f;
  private Vector3 direction;


  private void Awake()
  {
    direction = Vector3.zero;
  }

  private void OnEnable()
  {
    direction = Vector3.zero;
  }


  private void Update()
  {
    if (transform.position.y > -3.2f)
    {
      direction.y += gravity;
      transform.position += direction * Time.deltaTime;
    }else{
      this.enabled = false;
    }
  }

}
