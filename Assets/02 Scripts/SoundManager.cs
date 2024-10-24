using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] soundClips;  // 효과음 배열

    public void PlaySound(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= soundClips.Length)
        {
            Debug.Log("존재하지 않은 효과음입니다.");
            return;
        }

        audioSource.PlayOneShot(soundClips[clipIndex]);
    }

    // 0. Coin Get
    // 1. Rotation
    // 2. Animal Change
    // 3. Game Over
    // 4. Draw
    // 5. Setting Change
}
