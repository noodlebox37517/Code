/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: edgecomparer.h
 * Interface defining functionality for edgecomparer 
 */
#include "Edge.h"

#ifndef EDGECOMPARER_H
#define EDGECOMPARER_H



class EdgeComparer
{
	public:
/*
*This function returns true if and only if the
*weight of the first Edge is less than the weight
*of the second Edge
*/
		bool operator() (Edge*, Edge*);
	protected:
};

#endif
