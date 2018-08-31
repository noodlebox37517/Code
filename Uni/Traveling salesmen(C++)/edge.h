/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: edge.h
 * Interface defining functionality for edge objects  has source , destination and wieght.
 */
#ifndef EDGE_H
#define EDGE_H
#include "vertex.h"

class Edge
{
	public:
/*
*Constructor which sets the source vertex, the
*destination vertex and the weight for this
*Edge
*/
		Edge(Vertex*, Vertex*, double);
/*  Destructor */		
		~Edge();
/*
*Returns a pointer to the source vertex
*/		
	Vertex* GetSource();
/*
*Returns a pointer to the destination vertex
*/	
Vertex* GetDestination();
/*
*Returns the weight of this Edge
*/	
double GetWeight();
/*
*Returns a string representation of this Edge.
*For testing purposes.
*/	
string ToString();	
	private:
		Vertex* source;
		Vertex* destination;
		double edgeWeight;
};

#endif
