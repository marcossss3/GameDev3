using UnityEngine;
using System.Collections;

public class BackgroundLoop : MonoBehaviour {


	SpriteRenderer sprite;

	void Awake () {

		// Get the current sprite with an unscaled size
		sprite = GetComponent<SpriteRenderer>();
		Vector2 spriteSize = new Vector2(sprite.bounds.size.x / transform.localScale.x, sprite.bounds.size.y / transform.localScale.y);

		// Generate a child prefab of the sprite renderer
		GameObject childPrefab = new GameObject();
		SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
		childPrefab.transform.position = transform.position;
		childSprite.sprite = sprite.sprite;

		GameObject child;

		// Loop through and spit out repeated tiles
		for (int i = 1, l = (int)Mathf.Round(sprite.bounds.size.x + 1); i < l; i++) {
			child = Instantiate(childPrefab) as GameObject;
			child.transform.position = transform.position + (new Vector3(spriteSize.x - 1, 0, 0) * i);
			child.transform.parent = transform;
		}

		// Set the parent last on the prefab to prevent transform displacement
		childPrefab.transform.parent = transform;

		// Disable the currently existing sprite component since its now a repeated image
		sprite.enabled = false;

	}

}
