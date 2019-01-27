using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeEmojis : MonoBehaviour
{
    public GameObject[] emojis;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void OnEnable()
    {
        int sprite = Random.Range(0, sprites.Length);
        int emojiCount = Random.Range(0, emojis.Length);
        for (int i = 0; i < emojis.Length; i++)
        {
            emojis[i].SetActive(i <= emojiCount);
            emojis[i].GetComponent<UnityEngine.UI.Image>().sprite = sprites[sprite];
        }
    }

}
