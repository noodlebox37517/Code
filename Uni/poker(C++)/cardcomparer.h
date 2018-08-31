/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: card.h
 * Interface defining functionality for a cardcomparer operater
 */
#ifndef CARDCOMPARER_H
#define CARDCOMPARER_H

#include "card.h"

class CardComparer
{
	public:
		//operater for cardcomparison
		bool operator() (Card*, Card*);
};

#endif
