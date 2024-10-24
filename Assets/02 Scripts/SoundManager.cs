using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] soundClips;  // ȿ���� �迭

    public void PlaySound(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= soundClips.Length)
        {
            Debug.Log("�������� ���� ȿ�����Դϴ�.");
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
