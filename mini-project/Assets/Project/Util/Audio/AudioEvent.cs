using UnityEngine;

namespace Amheklerior.Solitaire.Util {

    [CreateAssetMenu(menuName = "Util/Audio Event")]
    public class AudioEvent : ScriptableObject {

        [SerializeField] private AudioClip[] clips;
        [SerializeField] private Vector2 volumeRange;
        [SerializeField] private Vector2 pitchRange;

        public void Play(AudioSource source) {
            if (clips.Length == 0) return;

            source.clip = clips[Random.Range(0, clips.Length)];
            source.volume = Random.Range(volumeRange.x, volumeRange.y);
            source.pitch = Random.Range(pitchRange.x, pitchRange.y);
            source.Play();
        }
        
    }
}
