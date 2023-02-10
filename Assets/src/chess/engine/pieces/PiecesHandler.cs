using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.Mathematics.quaternion;

public class PiecesHandler : MonoBehaviour
{

    #region Pieces

    [Header("Pieces")] 
    
    [SerializeField] private Pieces pawn1;

    #endregion

    #region params
    
    [SerializeField] private GridManager gridManager;

    private Pieces[] _piecesArray;

    private Dictionary<string, Vector2> _whiteInitPos;
    private Dictionary<string, Vector2> _blackInitPos;

    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitPieces()
    {
        foreach (var piece in _piecesArray)
        {
            Debug.Log(piece.name);
            piece.transform.position = _whiteInitPos["1A"];
        }

    }
    public Pieces[] GetPieces()
    {
        return new []{pawn1};
    }
}
