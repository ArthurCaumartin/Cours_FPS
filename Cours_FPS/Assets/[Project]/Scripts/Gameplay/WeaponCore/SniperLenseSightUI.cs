using TMPro;
using UnityEngine;

public class SniperLenseSightUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _texteObjectAimName;
    private AimCursor _aimCursor;

    public void Init(AimCursor aimCursor)
    {
        _aimCursor = aimCursor;
    }

    public void OnNewObjectAim(GameObject objAim)
    {

    }
}