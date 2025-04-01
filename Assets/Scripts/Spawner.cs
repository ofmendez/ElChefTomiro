using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Pipes prefab;
    public float spawnRate ;
    public float minHeight;
    public float maxHeight;
    public float verticalGap = 3f;
    private float incrementSpeed = 0.1f;

    private void OnEnable()
    {
      ResetSpeed();
      InvokeRepeating(nameof(Spawn), 0, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    public void ResetSpeed()
    {
        incrementSpeed = 0.1f;
    }

    private void Spawn()
    {
        Pipes pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
        pipes.SetBlockAndPizza(pipes.transform.position);
        pipes.AddToSpeed(incrementSpeed);
        incrementSpeed += 0.006f;
    }

}
