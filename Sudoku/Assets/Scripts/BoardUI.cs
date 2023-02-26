using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardUI : MonoBehaviour
{
    public DifficultyLevel difficultyLevel;

    public bool notesOn = false;
    private CellCoordinates currentCell;

    public bool isGamePaused = false;
    private float timer = 0;
    private int mistakes = 0;

    // panel
    public GameObject boardPanel;
    private Puzzle puzzle;

    // Text fields
    public Text timeText;
    public Text mistakesText;
    public Text difficultyLevelText;
    public Text pausePanelTimeText;
    public Text pausePanelMistakesText;
    public Text pausePanelDifficultyLevelText;
    public Text gameOverPanelTimeText;
    public Text gameOverPanelMistakesText;
    public Text gameOverPanelDifficultyLevelText;


    // Board
    public GameObject boardObject;
    private Cell[ , ] board;

    public static BoardUI Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    // Start is called before the first frame update
    void Start() {
        notesOn = false;
        timer = 0;
        isGamePaused = false;

        createBoardUI();
    }

    // Update is called once per frame
    void Update() {        
        if (!isGamePaused && boardPanel.activeSelf) {
            timer += Time.deltaTime;
            displayTime();
        }
    }

    private void displayTime() {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void updateMistakes() {
        mistakesText.text = string.Format("{0:0}/{1:0}", mistakes, 3);
    }

    private void updateDifficultyLevel() {
        difficultyLevelText.text = difficultyLevel.ToString();
    }

    private void setData() {
        timer = 0;
        mistakes = 0;

        updateDifficultyLevel();
        updateMistakes();
        displayTime();
    }

    private void createBoardUI() {
        board = new Cell[9, 9];
        for (int boxNum = 0; boxNum < 9; boxNum ++) {
            GameObject box = boardObject.transform.GetChild(boxNum).gameObject;
            for (int cellNum = 0; cellNum < 9; cellNum ++) {
                GameObject cellUI = box.transform.GetChild(cellNum).gameObject;
                int row = (boxNum / 3) * 3 + (cellNum / 3);
                int column = (boxNum % 3) * 3 + (cellNum % 3);

                Cell cell = new Cell(row, column, cellUI);
                board[row, column] = cell;
            }
        }
        Debug.Log("Board UI created");
    }

    

    private void setBoard() {
        clearAllCells();

        for (int row = 0; row < 9; row ++) {
            for (int column = 0; column < 9; column ++) {
                int number = puzzle.getCellValue(row, column);
                int actualNumber = puzzle.getActualCellValue(row, column);
                if (number != 0) {
                    setCellPermanent(number, row, column);
                } else {
                    disableMainTextOfCell(row, column);
                    disableNotesOfCell(row, column);
                }
                board[row, column].setActualValue(actualNumber);
            }
        }

        setData();

        Debug.Log("Puzzle created");
    }

    private void setCellNumber(int number, int row, int column) {
        if (notesOn) {
            board[row, column].setValueEmpty();

            disableMainTextOfCell(row, column);
            enableNotesOfCell(row, column);

            if (isNotesOfCellActive(number, row, column)) {
                removeNotesOfCell(number, row, column);
            } else {
                setNotesOfCell(number, row, column);
            }
        } else {
            if (number == board[row, column].getValue()) {
                board[row, column].setValueEmpty();

                disableNotesOfCell(row, column);
                enableMainTextOfCell(row, column);

                setMainTextOfCell(0, row, column);
            } else {
                board[row, column].setValue(number);

                disableNotesOfCell(row, column);
                enableMainTextOfCell(row, column);

                setMainTextOfCell(number, row, column);
                if (isNumberCorrect(row, column)) {
                    setCellTextColorCorrect(row, column);
                } else {
                    setCellTextColorIncorrect(row, column);
                    
                    mistakes += 1;
                    updateMistakes();
                    if (mistakes >= 3) {
                        // Enable game over panel
                        UIManager.Instance.onGameOver();
                    }
                }
            }
        }
    }

    private void setCellColorSelected(int row, int column) {
        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        Image image = button.GetComponent<Image>();
        Color color = Color.cyan;
        color.a = 0.25f;
        image.color = color;

        Debug.Log("Set color select for cell " + row.ToString() + " " + column.ToString());
    }

    private void setCellColorNormal(int row, int column) {
        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        Image image = button.GetComponent<Image>();
        Color color = Color.white;
        color.a = 0.0f;
        image.color = color;

        Debug.Log("Set color normal for cell " + row.ToString() + " " + column.ToString());
    }

    private void setCellColorHighlighted(int row, int column) {
        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        Image image = button.GetComponent<Image>();
        Color color = Color.black;
        color.a = 0.25f;
        image.color = color;

        Debug.Log("Set color highlighted for cell " + row.ToString() + " " + column.ToString());
    }

    private void setCellPermanent(int number, int row, int column) {
        board[row, column].setCellPermanent();
        board[row, column].setValue(number);

        disableNotesOfCell(row, column);
        enableMainTextOfCell(row, column);

        setMainTextOfCell(number, row, column);
        setCellTextColorPermanent(row, column);
    }

    private void setCellTextColorPermanent(int row, int column) {
        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        GameObject mainText = button.transform.GetChild(0).gameObject;
        Text text = mainText.GetComponent<Text>();
        text.color = Color.black;
    }

    private void setCellTextColorCorrect(int row, int column) {
        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        GameObject mainText = button.transform.GetChild(0).gameObject;
        Text text = mainText.GetComponent<Text>();
        text.color = Color.blue;
    }

    private void setCellTextColorIncorrect(int row, int column) {
        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        GameObject mainText = button.transform.GetChild(0).gameObject;
        Text text = mainText.GetComponent<Text>();
        text.color = Color.red;
    }

    private void setMainTextOfCell(int number, int row, int column) {
        string numberText = "";
        if (number > 0) {
            numberText = number.ToString();
        }

        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        GameObject mainText = button.transform.GetChild(0).gameObject;
        Text text = mainText.GetComponent<Text>();
        text.text = numberText;
    }

    private void enableMainTextOfCell(int row, int column) {
        disableNotesOfCell(row, column);

        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        GameObject mainText = button.transform.GetChild(0).gameObject;
        mainText.SetActive(true);
    }

    private void disableMainTextOfCell(int row, int column) {
        board[row, column].setValueEmpty();

        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        GameObject mainText = button.transform.GetChild(0).gameObject;
        Text text = mainText.GetComponent<Text>();
        text.text = "";
        mainText.SetActive(false);
    }

    private void setNotesOfCell(int number, int row, int column) {
        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        GameObject notes = button.transform.GetChild(1).gameObject;
        GameObject numberCell = notes.transform.GetChild(number-1).gameObject;
        numberCell.SetActive(true);
    }

    private bool isNotesOfCellActive(int number, int row, int column) {
        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        GameObject notes = button.transform.GetChild(1).gameObject;
        GameObject numberCell = notes.transform.GetChild(number-1).gameObject;
        return numberCell.activeSelf;
    }

    private void removeNotesOfCell(int number, int row, int column) {
        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        GameObject notes = button.transform.GetChild(1).gameObject;
        GameObject numberCell = notes.transform.GetChild(number-1).gameObject;
        numberCell.SetActive(false);
    }

    private void enableNotesOfCell(int row, int column) {
        disableMainTextOfCell(row, column);

        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        GameObject notes = button.transform.GetChild(1).gameObject;
        notes.SetActive(true);
    }

    private void disableNotesOfCell(int row, int column) {
        for (int num = 1; num < 10; num ++) {
            removeNotesOfCell(num, row, column);
        }
        GameObject cell = board[row, column].getCellUI();
        GameObject button = cell.transform.GetChild(0).gameObject;
        GameObject notes = button.transform.GetChild(1).gameObject;
        notes.SetActive(false);
    }

    private bool isNumberCorrect(int row, int column) {
        int number = board[row, column].getValue();
        int actualNumber = board[row, column].getActualValue();

        return number == actualNumber;
    }

    private void highlightSameNumberCells() {
        if (board[currentCell.getRow(), currentCell.getColumn()].isValueSet()) {
            int currentCellNumber = board[currentCell.getRow(), currentCell.getColumn()].getValue();
            for (int row = 0; row < 9; row ++) {
                for (int column = 0; column < 9; column ++) {
                    if (board[row, column].getValue() == currentCellNumber) {
                        if (row == currentCell.getRow() && column == currentCell.getColumn()) {
                            setCellColorSelected(row, column);
                        } else {
                            setCellColorHighlighted(row, column);
                        }
                    } else {
                        setCellColorNormal(row, column);
                    }
                }
            }
        } else {
            for (int row = 0; row < 9; row ++) {
                for (int column = 0; column < 9; column ++) {
                    if (row == currentCell.getRow() && column == currentCell.getColumn()) {
                        setCellColorSelected(row, column);
                    } else {
                        setCellColorNormal(row, column);
                    }
                }
            }
        }
    }

    private void clearAllCells() {
        for (int row = 0; row < 9; row ++) {
            for (int column = 0; column < 9; column ++) {
                clearCell(row, column);
            }
        }
    }

    private void clearCell(int row, int column) {
        setCellTextColorCorrect(row, column);
        setCellColorNormal(row, column);
        disableMainTextOfCell(row, column);
        disableNotesOfCell(row, column);
        board[row, column].setCellNormal();
    }

    public void createBoard() {
        puzzle = new Puzzle(difficultyLevel);
        setBoard();
    }

    public void updatePausePanel() {
        pausePanelDifficultyLevelText.text = difficultyLevel.ToString();
        pausePanelMistakesText.text = mistakesText.text;
        pausePanelTimeText.text = timeText.text;
    }

    public void updateGameOverPanel() {
        gameOverPanelDifficultyLevelText.text = difficultyLevel.ToString();
        gameOverPanelMistakesText.text = mistakesText.text;
        gameOverPanelTimeText.text = timeText.text;
    }

    public void eraseNumber() {
        if (currentCell != null) {
            int row = currentCell.getRow();
            int column = currentCell.getColumn();
            if (!board[row, column].isCellPermanent()) {
                clearCell(row, column);
            }
        }
        highlightSameNumberCells();
    }

    public void setNotesOn() {
        notesOn = !notesOn;
    }

    public void hint() {
        while (true) {
            CellCoordinates coordinates = puzzle.getHint();
            if (board[coordinates.getRow(), coordinates.getColumn()].getValue() != 
            board[coordinates.getRow(), coordinates.getColumn()].getActualValue()) {
                int actualNumber = board[coordinates.getRow(), coordinates.getColumn()].getActualValue();
                setCellPermanent(actualNumber, coordinates.getRow(), coordinates.getColumn());
                break;
            }
        }
    }

    public void setNumber(int number) {
        if (currentCell != null) {
            int row = currentCell.getRow();
            int column = currentCell.getColumn();
            if (!board[row, column].isCellPermanent()) {
                setCellNumber(number, row, column);
            }
        } 
        highlightSameNumberCells();
    }

    public void changeCurrentCell(int row, int column) {
        if (currentCell != null) {
            setCellColorNormal(currentCell.getRow(), currentCell.getColumn());
            currentCell.setRow(row);
            currentCell.setColumn(column);
        } else {
            currentCell = new CellCoordinates(row, column);
        }
        highlightSameNumberCells();
    }
}

public enum DifficultyLevel
{
    EASY,
    MEDIUM,
    HARD,
    EXPERT
}


public class Cell {
    private int row;
    private int column;
    private GameObject cellUI;
    private bool isPermanent = false;
    private int actualValue;
    private int value = 0;

    public Cell(int row, int column, GameObject cellUI) {
        this.row = row;
        this.column = column;
        this.cellUI = cellUI;
    }

    public int getRow() {
        return row;
    }

    public int getColumn() {
        return column;
    }

    public GameObject getCellUI() {
        return cellUI;
    }

    public void setCellPermanent() {
        this.isPermanent = true;
    }
    public void setCellNormal() {
        this.isPermanent = false;
    }

    public bool isCellPermanent() {
        return isPermanent;
    }

    public void setValue(int number) {
        this.value = number;
    }

    public void setValueEmpty() {
        this.value = 0;
    }

    public bool isValueSet() {
        return value > 0;
    }

    public int getValue() {
        return value;
    }

    public void setActualValue(int number) {
        this.actualValue = number;
    }

    public int getActualValue() {
        return actualValue;
    }
}
