/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: handcomparer.h
 * Interface defining functionality for a handcomparer operater
 */
#ifndef HANDCOMPARER_H
#define HANDCOMPARER_H

#include "hand.h"
#include "card.h"

class handcomparer
{
	public:
		//operater for hand comparison
	bool operator() (Hand*, Hand*);
};


#endif
