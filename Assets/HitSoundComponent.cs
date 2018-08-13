using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSoundComponent : MonoBehaviour {
    
    [SerializeField]
    AudioClip[] HitSounds;

    [SerializeField]
    float MaxHitMaginitudeIncreasingSound = 100f;

    [SerializeField]
    AudioClip TakeSound;

    AudioSource Audio;

    void Awake () {

        Audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource OtherSounds = collision.otherCollider.GetComponent<AudioSource>();
        if (!Audio.isPlaying && OtherSounds != null && !OtherSounds.isPlaying)
        {
            Audio.clip = HitSounds[(int)(Random.value * HitSounds.Length)];
            Audio.volume = Mathf.InverseLerp(0, MaxHitMaginitudeIncreasingSound, collision.relativeVelocity.magnitude);
            Audio.Play();
        }
    }
}
