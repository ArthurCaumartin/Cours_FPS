using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [SerializeField] private TextMeshProUGUI _textInteraction;




    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void ShowInteractionText(Interactible interactible)
    {
        if (interactible == null)
        {
            _textInteraction.text = "";
            return;
        }

        if (!_textInteraction) return;
        if (interactible is Interactible_Weapon wp)
            _textInteraction.text = "Grab " + wp.gameObject.name;
    }
}
