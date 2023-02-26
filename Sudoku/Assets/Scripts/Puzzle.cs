using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Puzzle : MonoBehaviour
{
    private DifficultyLevel difficultyLevel;
    private int GRID_SIZE = 9;
    private int[ , ] puzzle;
    private int[ , ] solvedPuzzle;

    private System.Random rand = new System.Random();

    public Puzzle(DifficultyLevel difficultyLevel) {
        this.difficultyLevel = difficultyLevel;

        this.puzzle = new int[GRID_SIZE, GRID_SIZE];
        fillCells(puzzle);
        this.solvedPuzzle = this.puzzle.Clone() as int[ , ];
        removeNumbers(difficultyLevel);
        
        // For testing
        printPuzzle();
    }

    private void removeNumbers(DifficultyLevel difficultyLevel) {
        int maxRemoveCount;
        if (difficultyLevel == DifficultyLevel.EASY) {
            maxRemoveCount = (int) MaxRemoveCount.EASY;
        }
        else if (difficultyLevel == DifficultyLevel.MEDIUM) {
            maxRemoveCount = (int) MaxRemoveCount.MEDIUM;
        }
        else if (difficultyLevel == DifficultyLevel.HARD) {
            maxRemoveCount = (int) MaxRemoveCount.HARD;
        }
        else {
            maxRemoveCount = (int) MaxRemoveCount.EXPERT;
        }

        int count = 0;
        while (count < maxRemoveCount) {
            int row = rand.Next(9);
            int column = rand.Next(9);

            if (isCellFilled(row, column)) {
                puzzle[row, column] = 0;
                count += 1;
            }
        }
    }

    private bool fillCells(int[ , ] puzzle) {

        for (int row = 0; row < GRID_SIZE; row++) {
            for (int column = 0; column < GRID_SIZE; column++) {
                if (!isCellFilled(row, column)) {
                    int count = 0;
                    List<int> usedNumbers = new List<int>();
                    while (count < 9) {
                        int num = rand.Next(9) + 1;
                        if (!usedNumbers.Contains(num)) {
                            usedNumbers.Add(num);

                            if (isValidNumber(num, row, column)) {
                                puzzle[row, column] = num;

                                if (fillCells(puzzle)) {
                                    return true;
                                }
                                else {
                                    puzzle[row, column] = 0;
                                }
                            }
                            count += 1;
                        }
                    }

                    return false;
                }
            }
        }
        return true;
    }
    
    private bool isCellFilled(int row, int column) {
        return puzzle[row, column] != 0;
    }

    private bool isValidNumber(int number, int row, int column) {
        return !isNumberInRow(number, row) && !isNumberInColumn(number, column) && !isNumberInBox(number, row, column);
    }

    private bool isNumberInRow(int number, int row) {
        for (int i = 0; i < 9; i ++) {
            if (puzzle[row, i] == number) {
                return true;
            }
        }
        return false;
    }

    private bool isNumberInColumn(int number, int column) {
        for (int i = 0; i < 9; i ++) {
            if (puzzle[i, column] == number) {
                return true;
            }
        }
        return false;
    }

    private bool isNumberInBox(int number, int row, int column) {
        int boxStartRow = row - row % 3;
        int boxStartColumn = column - column % 3;

        for (int i = boxStartRow; i < boxStartRow+3; i ++) {
            for (int j = boxStartColumn; j < boxStartColumn+3; j ++) {
                if (puzzle[i, j] == number) {
                    return true;
                }
            }
        }
        return false;
    }

    private void printPuzzle() {
        for (int row = 0; row < 9; row ++) {
            string rowString = "";
            for (int column = 0; column < 9; column ++) {
                rowString += puzzle[row, column].ToString() + " ";
            }
            rowString.Trim();
            Debug.Log(rowString);
        }
    }

    public int[ , ] getPuzzle() {
        return puzzle;
    }

    public int[ , ] getSolvedPuzzle() {
        return solvedPuzzle;
    }

    public DifficultyLevel getDifficultyLevel() {
        return difficultyLevel;
    }

    public void fillCell(int row, int column, int value) {
        puzzle[row, column] = value;
    }

    public CellCoordinates getHint() {
        while (true) {
            int row = rand.Next(9);
            int column = rand.Next(9);

            if (!isCellFilled(row, column)) {
                puzzle[row, column] = solvedPuzzle[row, column];
                CellCoordinates cellCoordinates = new CellCoordinates(row, column);
                return cellCoordinates;
            }
        }
    }

    public int getCellValue(int row, int column) {
        return puzzle[row, column];
    }

    public int getActualCellValue(int row, int column) {
        return solvedPuzzle[row, column];
    }
}

enum MaxRemoveCount {
    EASY = 30,
    MEDIUM = 39,
    HARD = 48,
    EXPERT = 54
}

public class CellCoordinates {
    int row;
    int column;

    public CellCoordinates(int row, int column) {
        this.row = row;
        this.column = column;
    }

    public int getRow() {
        return row;
    }

    public int getColumn() {
        return column;
    }

    public void setRow(int row) {
        this.row = row;
    }

    public void setColumn(int column) {
        this.column = column;
    }
}
