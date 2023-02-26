using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberLineUI : MonoBehaviour
{
    public GameObject notesButton;
    public Sprite notesOffSprite;
    public Sprite notesOnSprite;

    public GameObject hintButton;
    public Sprite hint0Sprite;
    public Sprite hint1Sprite;
    public Sprite hint2Sprite;
    public Sprite hint3Sprite;

    private int hintCount = 3;

    private void setNotesButtonUI() {
        Image image = notesButton.GetComponent<Image>();
        if (BoardUI.Instance.notesOn) {
            image.sprite = notesOnSprite;
        } else {
            image.sprite = notesOffSprite;
        }
    }

    private void setHintButtonUI() {
        Image image = hintButton.GetComponent<Image>();
        if (hintCount == 0) {
            image.sprite = hint0Sprite;
        } else if (hintCount == 1) {
            image.sprite = hint1Sprite;
        } else if (hintCount == 2) {
            image.sprite = hint2Sprite;
        } else {
            image.sprite = hint3Sprite;
        }
    }

    public void onEraseButtonClicked() {
        Debug.Log("Erase button selected");
        BoardUI.Instance.eraseNumber();
    }

    public void onNotesButtonClicked() {
        Debug.Log("Notes-on button selected");
        BoardUI.Instance.setNotesOn();
        
        setNotesButtonUI();
    }

    public void onHintButtonClicked() {
        Debug.Log("Hint button selected");
        if (hintCount > 0) {
            BoardUI.Instance.hint();
            hintCount -= 1;
            setHintButtonUI();
        }
    }

    public void onNumber1ButtonClicked() {
        Debug.Log("Number 1 selected");
        BoardUI.Instance.setNumber(1);
    }

    public void onNumber2ButtonClicked() {
        Debug.Log("Number 2 selected");
        BoardUI.Instance.setNumber(2);
    }

    public void onNumber3ButtonClicked() {
        Debug.Log("Number 3 selected");
        BoardUI.Instance.setNumber(3);
    }

    public void onNumber4ButtonClicked() {
        Debug.Log("Number 4 selected");
        BoardUI.Instance.setNumber(4);
    }

    public void onNumber5ButtonClicked() {
        Debug.Log("Number 5 selected");
        BoardUI.Instance.setNumber(5);
    }

    public void onNumber6ButtonClicked() {
        Debug.Log("Number 6 selected");
        BoardUI.Instance.setNumber(6);
    }

    public void onNumber7ButtonClicked() {
        Debug.Log("Number 7 selected");
        BoardUI.Instance.setNumber(7);
    }

    public void onNumber8ButtonClicked() {
        Debug.Log("Number 8 selected");
        BoardUI.Instance.setNumber(8);
    }

    public void onNumber9ButtonClicked() {
        Debug.Log("Number 9 selected");
        BoardUI.Instance.setNumber(9);
    }
}
