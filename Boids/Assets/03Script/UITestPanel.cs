using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using TMPro;
using UnityEngine;

public class UITestPanel : MonoBehaviour
{
    public GamePadController gamePadController;
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update() {
        text.text = gamePadController.GetDebugStatusText();
    }
}
