using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameArea : MonoBehaviour
{
    public Rect PlayArea { get; private set; }
    public GameObject Square;

    private void Awake()
    {
        PlayArea = Square.GetComponent<SpriteRenderer>().sprite.rect;
    }
}
