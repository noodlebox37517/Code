/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: card.cpp
 * --------------
 * source file includes the main code for the Card object
 * 
 *
 */
#include <iostream>
#include "card.h"

/* No arg constructor */
Card::Card() {
}

/* Constructor to create a new Card from a given rank and suit */
Card::Card(Rank rank, Suit suit) {
	this->rank = rank;
	this->suit = suit;
}

/* Destructor - does nothing */
Card::~Card() {
    // Does nothing
}

/* Returns the rank of a Card */
Rank Card::GetRank() {
	return rank;
}

/* Returns the suit of a card */
Suit Card::GetSuit() {
	return suit;
}

/* Converts a Card to a string representation - 2C, 3C, ... TS, JS, QS, KS, AS */
string Card::GetCardName() {
	string cardName = "";
	switch (rank) {
		case TWO   : cardName += "2"; break;
		case THREE : cardName += "3"; break;
		case FOUR  : cardName += "4"; break;
		case FIVE  : cardName += "5"; break;
		case SIX   : cardName += "6"; break;
		case SEVEN : cardName += "7"; break;
		case EIGHT : cardName += "8"; break;
		case NINE  : cardName += "9"; break;
		case TEN   : cardName += "T"; break;
		case JACK  : cardName += "J"; break;
		case QUEEN : cardName += "Q"; break;
		case KING  : cardName += "K"; break;
		case ACE   : cardName += "A"; break;
	}
	switch (suit) {
		case CLUBS    : cardName += "C"; break;
		case DIAMONDS : cardName += "D"; break;
		case HEARTS   : cardName += "H"; break;
		case SPADES   : cardName += "S"; break;
	}
	return cardName;
}

