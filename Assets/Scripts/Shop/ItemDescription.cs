using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Shop
{
    public class ItemDescription : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text[] _rarity;

        public void Init(int id)
        {
            _name.text = LocalisationSystem.GetLocalizedValue("item_name_id" + id.ToString());
            _description.text = LocalisationSystem.GetLocalizedValue("item_desc_id" + id.ToString());
        }

        public void Init(string nameKey, string descriptionKey)
        {
            _name.text = LocalisationSystem.GetLocalizedValue(nameKey);
            _description.text = LocalisationSystem.GetLocalizedValue(descriptionKey);
        }
    }

}