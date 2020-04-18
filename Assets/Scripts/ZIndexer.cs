using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIndexer : MonoBehaviour
{
    public SpriteRenderer characterSprite;

    // Start is called before the first frame update
    void Start()
    {
        characterSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        UpdateSpriteSort();
        
    }

    void Update()
    {
        UpdateSpriteSort();
    }

    void UpdateSpriteSort()
    {
        characterSprite.sortingOrder = (int)((gameObject.transform.position.y - characterSprite.bounds.extents.y) * -100);
    }
}
