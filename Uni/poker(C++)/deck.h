/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: deck.h
 * Interface defining functionality for a deck
 */
#include "card.h"

#ifndef _deck_h
#define _deck_h

const int CARDS_IN_DECK = 52;
const int NUM_SUITS = 4;
const int NUM_RANKS = 13;

class Deck {
    public:
        Deck();
        ~Deck();
        Card* DealNextCard();
        void Shuffle();
        void DisplayDeck();
        
    private:
        Card** cards;
        int cardsDealt;
   
};

#endif
