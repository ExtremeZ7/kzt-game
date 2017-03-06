using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    [RequireComponent(typeof(AudioSource))]
    public class playAudioThenDestroySelf : MonoBehaviour
    {

        [SerializeField]
        private AudioSource audioSource;

        [Range(0f, 2f)]
        public float randomPitchRange;

        [Space(10)]
        public bool destroyOtherAudioSourcesWithSameClip;

        private float clipLength;

        void OnValidate()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            OnValidate();

            if (audioSource.clip == null)
                Object.Destroy(gameObject);

            clipLength = audioSource.clip.length;
		
            if (destroyOtherAudioSourcesWithSameClip)
            {
                AudioSource[] otherAudioSources = GetComponents<AudioSource>();
                for (int i = 0; i < otherAudioSources.Length; i++)
                {
                    if (!otherAudioSources[i].Equals(audioSource) && otherAudioSources[i].clip.Equals(audioSource.clip))
                    {
                        Object.Destroy(otherAudioSources[i]);
                    }
                }
            }

            audioSource.pitch = 1f + Random.Range(-randomPitchRange, randomPitchRange);
        }

        void Update()
        {
            if (Helper.UseAsTimer(ref clipLength) && !audioSource.isPlaying)
                Object.Destroy(gameObject);
        }
    }
}