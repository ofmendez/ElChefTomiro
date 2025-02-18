using UnityEngine;

public enum soundType
{
  comida,
  logo,
  muerte,
  salto,
  header,
  dayGameplay,
  nightGameplay,
  go,
  ranking
}
public class Audio : MonoBehaviour
{
  [SerializeField] private AudioClip[] sounds;
  [SerializeField] private AudioSource audioSourceOneShot;
  [SerializeField] private AudioSource audioSourceLoop;
  public bool isInSoundLoop { get; private set; } = false;

  public void PlaySound(soundType type)
  {
    audioSourceOneShot.PlayOneShot(sounds[(int)type]);
  }
  public void PlaySoundLoop(soundType type)
  {
    isInSoundLoop = true;
    audioSourceLoop.clip = sounds[(int)type];
    audioSourceLoop.Play();
  }
  public void StopSoundLoop()
  {
    isInSoundLoop = false;
    audioSourceLoop.Stop();
  }
  public float Duration(soundType type)
  {
    return sounds[(int)type].length;
  }

}
