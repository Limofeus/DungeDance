using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Shop
{
    public class PlayerMoney : MonoBehaviour
    {
        [SerializeField] private TMP_Text _money;
        [SerializeField] private Shop _shop;

        private void OnEnable()
        {
            _shop.MoneyChanged += Render;
        }

        private void OnDisable()
        {
            _shop.MoneyChanged -= Render;
        }

        private void Render(int money)
        {
            _money.text = money.ToString();
        }
    }
}