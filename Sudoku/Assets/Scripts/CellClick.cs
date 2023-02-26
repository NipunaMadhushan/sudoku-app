using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellClick : MonoBehaviour
{
    public int row;
    public int column;
    
    // Start is called before the first frame update
    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(onButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onButtonClick() {
        Debug.Log("Button clicked on cell " + row.ToString() + " " + column.ToString());
        
        BoardUI.Instance.changeCurrentCell(row, column);
    } 
}
