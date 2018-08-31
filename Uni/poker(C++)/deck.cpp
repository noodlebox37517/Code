/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: deck.cpp
 * --------------
 * source file includes the main code for the Deck class
 * 
 *
 */
#include <iostream>
#include <iomanip>

#include "deck.h"
#include "card.h"
#include "random.h"

using namespace std;

const int NUM_SHUFFLES = 1000;


Deck::Deck() {
    cards = new Card*[CARDS_IN_DECK];
	for (int suit = 0; suit < NUM_SUITS; suit++) {
		for (int rank = 0; rank < NUM_RANKS; rank++) {
			cards[suit * NUM_RANKS + rank] = new Card((Rank)rank, (Suit)suit);
		}
	}
    cardsDealt = 0;
}

Deck::~Deck() {
    for (int i = 0; i < CARDS_IN_DECK; i++) {
        delete cards[i];
    }
    delete[] cards;
}

Card* Deck::DealNextCard() {
    return cards[cardsDealt++];
}

void Swap(Card* cards[], int idx1, int idx2) {
  	Card* temp = cards[idx1];
	cards[idx1] = cards[idx2];
	cards[idx2] = temp;
}


void Deck::Shuffle() {
	int idx1, idx2;
    Random rand;
    
	// shuffle deck NUM_SHUFFLES times
	for (int i = 0; i < NUM_SHUFFLES; i++) {
		idx1 = rand.RandomInteger(0, CARDS_IN_DECK - 1);
		idx2 = rand.RandomInteger(0, CARDS_IN_DECK - 1);
		Swap(cards, idx1, idx2);
	}
}

void Deck::DisplayDeck() {
    for (int i = 0; i < NUM_SUITS; i++) {
        for (int j = 0; j < NUM_RANKS; j++) {
            cout << setw(3) << cards[i * NUM_RANKS + j]->GetCardName();
        }
        cout << endl;
    }
}


