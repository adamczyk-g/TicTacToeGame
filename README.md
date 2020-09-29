# TDD example with Tic Tac Toe game
A simple tic tac toe app, creating with tdd aproach. Domain work with console gui.

## Assumptions
- the game is played on a board with nine fields
- each field of the board is empty until the player places his symbol on it
- two symbols are allowed: a circle and a cross
- the game may end with a win by one player or a draw
- the players make moves alternately, the player playing with a cross starts

## Unit Tests
- before the first move, the game returns "in progress" state
- after the first move, the field where the move was made, contains a cross
- after the second move, the field where the move was made, contains a circle
- performing an even move places a cross in the field, performing an odd move places a circle in the field
- moving to a field with a number below zero raises an exception
- moving to a field with a number greater than eight raises an exception
- moving to a non-empty field raises an exception
- an attempt to read the state of a field with a number below zero raises an exception
- an attempt to read the state of a field with a number above eight raises an exception
- making a move when the game is over raises an exception
