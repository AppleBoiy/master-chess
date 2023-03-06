using UnityEngine;

public class PieceSfx : MonoBehaviour
{

    public static PieceSfx Instance;
    
    [Header("Audio Source")] [SerializeField]
    private AudioSource audioSource;
    
    [Header("Sound effect")] 
    [SerializeField] private AudioClip destroyPiece;
    [SerializeField] private AudioClip promotionPiece;

    private void Awake()
    {
        Instance = this;
    }

    public void DestroyPieceSfx()
    {
        if (!audioSource) return;
        audioSource.PlayOneShot(destroyPiece);
    }

    public void PromotionPieceSfx()
    {
        if (!audioSource) return;
        audioSource.PlayOneShot(promotionPiece);
    }
}
