/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: deckh
 * Interface defining functionality for a hand
 */
#include "card.h"

#ifndef _hand_h
#define _hand_h

const int CARDS_IN_HAND = 5;
const int CAPACITY = 5;


enum HandType{
HIGHCARD, ONEPAIR, TWOPAIR, THREEOFKIND, STRAIGHT, FLUSH, FULLHOUSE, FOUROFKIND, STRAIGHTFLUSH
};

class Hand
{
	public:
		/*constructor with player identifier arg */ 
		Hand(int);
		/*Adds a Card* to the Hand vector*/ 
		void AddCard(Card*);
		/*Destructor */ 
		~Hand();
		/*evaulauates handtype */ 
		void Evaluate();
		/*Accsesor for the value of  hand */
		int GetValue();
		/*Accsesor for string of Player Number */
		string GetNumber();
		/*Accsesor for handType */
		HandType GetHandType();
		/*Accsesor for string of handType */
		string GetHandTypeString();
		/* craetes a string representation of hand including handtype and a summary of its card objects*/
		string ToString();
	private:	
	vector<Card*>cards;
		int playerNum;
		HandType handType;
		int handValue;
		string playerNumString;
};

#endif
