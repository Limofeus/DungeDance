using UnityEngine;

namespace Shop
{
    public class HeadAnimator : MonoBehaviour
    {
        [SerializeField] private Shop _shop;
        private const string _itemBought = "ItemBought";
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _shop.ItemBought += ChangeFace;
        }

        private void OnDisable()
        {
            _shop.ItemBought -= ChangeFace;
        }

        private void ChangeFace()
        {
            _animator.SetTrigger(_itemBought);
        }
    }
}