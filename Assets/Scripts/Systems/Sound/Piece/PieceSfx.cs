using System;
using UnityEngine;

public class PieceSfx : MonoBehaviour
{

    public static PieceSfx Instance;
    
    [Header("Audio Source")] [SerializeField]
    private AudioSource audioSource;
    
    [Header("Sound effect")] 
    [SerializeField] private AudioClip destroyPiece;

    private void Awake()
    {
        Instance = this;
    }

    public void DestroyPieceSfx()
    {
        if (!audioSource) return;
        audioSource.PlayOneShot(destroyPiece);
    }
}
