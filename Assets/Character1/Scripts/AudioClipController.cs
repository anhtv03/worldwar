using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioClipController : MonoBehaviour
{
    [Header("Slides")]
    [SerializeField] AudioClip slidingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f;
    [Header("Kick")]
    [SerializeField] AudioClip kickingClip;
    [SerializeField][Range(0f, 1f)] float kickingVolume = 1f;
    [Header("Teleport")]
    [SerializeField] AudioClip teleportClip;
    [SerializeField][Range(0f, 1f)] float teleportVolume = 1f;
    [Header("Flash")]
    [SerializeField] AudioClip flashClip;
    [SerializeField][Range(0f, 1f)] float flashVolume = 1f;
    [Header("Take damage")]
    [SerializeField] AudioClip takeDamageClip;
    [SerializeField][Range(0f, 1f)] float takeDamageVolume = 1f;
    [Header("Combo")]
    [SerializeField] AudioClip comboClip;
    [SerializeField][Range(0f, 1f)] float comboVolume = 1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamageClip()
    {
        if (takeDamageClip != null)
        {
            AudioSource.PlayClipAtPoint(takeDamageClip, Camera.main.transform.position, takeDamageVolume);
        }
    }
    public void ComboClip()
    {
        if (comboClip != null)
        {
            AudioSource.PlayClipAtPoint(comboClip, Camera.main.transform.position, comboVolume);
        }
    }
    public void PlaySlideClip()
    {
        if (slidingClip != null)
        {
            AudioSource.PlayClipAtPoint(slidingClip, Camera.main.transform.position, shootingVolume);
        }
    }
    public void PlayKickClip()
    {
        if (kickingClip != null)
        {
            AudioSource.PlayClipAtPoint(kickingClip, Camera.main.transform.position, kickingVolume);
        }
    }
    public void TeleportClip()
    {
        if (teleportClip != null)
        {
            AudioSource.PlayClipAtPoint(teleportClip, Camera.main.transform.position, teleportVolume);
        }
    }
    public void FlashClip()
    {
        if (flashClip != null)
        {
            AudioSource.PlayClipAtPoint(flashClip, Camera.main.transform.position, flashVolume);
        }
    }
}
