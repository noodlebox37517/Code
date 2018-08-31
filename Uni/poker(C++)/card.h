/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: card.h
 * Interface defining functionality for a playing card type.  A playing card
 * has a suit and a rank.
 */

#include <string>

using namespace std;

#ifndef _card_h
#define _card_h

/* Enumeration type for the four suits of standard playing cards */
enum Suit {
	CLUBS, DIAMONDS, HEARTS, SPADES
};

/* Enumeration type for the thirteen ranks of standard playing cards */
enum Rank {
	TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE
};

/* Card type definition consisting of a rank and a suit */
class Card {
    
    public:
        /* No arg constructor */
        Card();

        /* Creates a new Card from a given rank and suit */
        Card(Rank, Suit);
        
        /* Destructor */
        ~Card();
        
        /* Returns the rank of a Card */
        Rank GetRank();
        
        /* Returns the suit of a card */
        Suit GetSuit();
        
        /* Converts a Card to a string representation - 2C, 3C, ... TS, JS, QS, KS, AS */
        string GetCardName();

    private:
	    Rank rank;
	    Suit suit;
};

#endif
