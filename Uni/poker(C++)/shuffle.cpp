/*
 * File: shuffle.cpp
 * Creates a deck of cards, shuffles them and displays them.
 */

#include <iostream>
#include <iomanip>

#include "deck.h"

using namespace std;

int main() {
	
	Deck deck;

	deck.DisplayDeck();
	cout << endl;

	deck.Shuffle();
	deck.DisplayDeck();
	
	return 0;
}

