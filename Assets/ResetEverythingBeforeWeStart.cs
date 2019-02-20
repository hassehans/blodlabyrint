using UnityEngine;

public class ResetEverythingBeforeWeStart : MonoBehaviour {
    MainMenu mm;
	void Start () {
        mm = new MainMenu();
        mm.ResetAllPlayerPrefs();
	}
}