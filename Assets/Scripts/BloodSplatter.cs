using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour {

    public List<Sprite> bloodSprites;

    int spriteIndex;

    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        spriteIndex = Random.Range(0, bloodSprites.Count -1);
        spriteRenderer.sprite = bloodSprites[spriteIndex];
	}
}
