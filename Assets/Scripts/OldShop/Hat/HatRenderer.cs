using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shop
{
    public class HatRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _hatSprite;
        [SerializeField] private List<Sprite> _hats;

        private void Start()
        {
            _hatSprite.sprite = _hats[Random.Range(0, _hats.Count)];
        }
    }

}