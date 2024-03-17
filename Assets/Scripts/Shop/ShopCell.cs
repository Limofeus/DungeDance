using System;
using TMPro;
using UnityEngine;

namespace Shop
{
    public class ShopCell : MonoBehaviour
    {
        [SerializeField] private int _cellID;
        [SerializeField] private SpriteRenderer _mainSprite;
        [SerializeField] private SpriteRenderer _raritySprite;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Sprite _soldIcon;
        [SerializeField] private Sprite _closedIcon;
        [SerializeField] private Color[] _rarityColor;
        [SerializeField] private SpriteRenderer _discountSprite;
        private Animator _animator;
        private const string _isSelect = "IsSelect";
        private const string _isBuy = "IsBuy";
        private Collider2D _collider;
        private int _itemID;
        private bool _mouseOnButton;
        public event Action<int> OnSelect;
        public event Action<int> OnClick;

        private Sprite[] _itemSprites => ItemSpriteDictionary.itemSprites;
        private int[] _itemRarity => ItemSpriteDictionary.itemRarity;
        public int Price { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
        }

        public void Init(int id, bool isDiscount = false)
        {
            _itemID = id;
            _mainSprite.sprite = _itemSprites[id];
            _raritySprite.sprite = _itemSprites[id];
            _raritySprite.color = _rarityColor[_itemRarity[id]];
            Price = 200 * _itemRarity[id] + 300;
            if (isDiscount)
                Price /= 2;
            _discountSprite.gameObject.SetActive(isDiscount);
            _priceText.text = Price.ToString();
            _collider.enabled = true;
        }

        public void IsClosed()
        {
            _mainSprite.sprite = _closedIcon;
            _raritySprite.sprite = default;
            _priceText.text = "";
            _collider.enabled = false;
            _discountSprite.enabled = false;
        }
        public void IsSold()
        {
            _animator.SetTrigger(_isBuy);
            _itemID = default;
            _raritySprite.sprite = default;
            _priceText.text = "SOLD";
            _collider.enabled = false;
            _discountSprite.enabled = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _mouseOnButton)
                OnClick?.Invoke(_cellID);
        }

        private void OnMouseEnter()
        {
            _animator.SetBool(_isSelect, true);
            _mouseOnButton = true;
            OnSelect?.Invoke(_itemID);
        }

        private void OnMouseExit()
        {
            _animator.SetBool(_isSelect, false);
            _mouseOnButton = false;
        }
    }
}