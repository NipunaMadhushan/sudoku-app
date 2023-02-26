# Sudoku Game
Sudoku is one of the most popular puzzle games of all time. It is an exellent brain game which is really good for children to improve the local thinking capacity.

## Rules
The game consists of 9 x 9 grid which includes 9 small 3 x 3 boxes. 
The goal is to fill the grid with numbers from 1 to 9 such that, 
- Each row should contain `all the numbers` from 1 to 9 and numbers should not be repeated in the same row.
- Each column should contain `all the numbers` from 1 to 9 and numbers should not be repeated in the same column.
- Each small 3 x 3 box should contain `all the numbers` from 1 to 9 and numbers should not be repeated in the same box.

## How to play
In the game, there four difficulty levels `EASY`, `MEDIUM`, `HARD` & `EXPERT`. User can select the difficulty level at the beginning of a new game. It is recommended to start with `EASY` level and increase the difficulty.

#### *Placing a number*
User can select a `cell (square)` by clicking on the cell. The selected cell will be filled with `light blue` colour.
Then user can select the number which he/she needs to insert on the cell by clicking on the number in the number line at the bottom. If your number is correct, number will be in `blue` colour. Otherwise number will be in `red` colour.

#### *Highlighted cells with same number*
When the user selects a cell which contains a number, then the other cells containing the same number will be highlighted with `grey` colour.

#### *Set Notes*
User can set notes on each cell if the user need to set multiple possible values at the cell. User need to click on `notes` button at the bottom to swich to note-on mode.

#### *Mistakes*
When the user put a wrong number in a cell, it will be counted as a mistake. The `maximum amount` of mistakes that a player can make is `2`. If a user makes mistakes more than 2, the game will be over.

#### *Hints*
User is given `3` hints. In each Hint, an empty cell (even a cell with a wrong number or with notes) will be filled with the actual value.
