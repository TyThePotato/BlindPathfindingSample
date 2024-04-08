using System;
using System.Collections;
using System.Collections.Generic;
using PathfindingGame.Game;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static UIManager _instance;

    public string[] itemNames;
    
    // References //
    public Text itemText;
    public Text objectiveText;

    public GameObject levelEndPanel;
    public Text levelEndPanelHeader;
    
    private void Awake() {
        _instance = this;
    }
    
    // UI Functions //

    public static void SetItemPickupText(int id) {
        var name = _instance.itemNames[id]; // TODO: invalid index check
        _instance.itemText.text = "Press SPACE to pick up " + name;
    }
    
    public static void SetCurrentItemText(int id) {
        var name = _instance.itemNames[id]; // TODO: invalid index check
        _instance.itemText.text = "Press SPACE to throw " + name;
    }
    
    public static void SetNoCurrentItem() {
        _instance.itemText.text = "";
    }
    
    public static void SetObjective(string text) {
        _instance.objectiveText.text = text;
    }

    public static void ShowWinScreen() {
        _instance.levelEndPanelHeader.text = "Successfully Escaped!";
        _instance.levelEndPanel.SetActive(true);
    }
    
    public static void ShowFailScreen() {
        _instance.levelEndPanelHeader.text = "You got caught!";
        _instance.levelEndPanel.SetActive(true);
    }
    
    // Button Events //

    public void OnRestartButtonClick() {
        GameManager.Restart();
    }

}
