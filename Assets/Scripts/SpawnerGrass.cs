using UnityEngine;

public class SpawnerGrass : MonoBehaviour
{
    public GameObject[] prefabs;
    public float spawnRate ;

    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
      GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
      Vector3 position = new Vector3(transform.position.x, prefab.transform.position.y, transform.position.z);
      GameObject go = Instantiate(prefab, position, Quaternion.identity);
    }

}
