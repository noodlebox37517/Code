/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: graph.h
 * Interface defining functionality for graph class  has numvertices, graph, adjacency list, edgelist
 */
#include "vertex.h"
#include "Edge.h"
#include "float.h"
#include "disjointset.h"

#ifndef GRAPH_H
#define GRAPH_H
const double INFINITY = 99999999;

class Graph
{
	public:
//Constructor which sets the number of vertices in
//this Graph
		Graph(int);
//Destructor		
		~Graph();
/*Adds pointer to Vertex to the adjacency list
for this Graph. This function will be called by
the driver program for initialisation purposes.
The adjacency list will be used to store the edges
in the Minimum Spanning Tree for this graph as
calculated by the MinimumSpanningTree
function.		
*/		
		void AddVertex(Vertex*);
/*
Accessor returns a pointer to the Vertex with
the identifier/index in the parameter
*/		
		Vertex* GetVertex(int);
/*
Adds pointer to Edge to the edge list for this
Graph. The AddEdge function will store the
information for the edge in the underlying
adjacency matrix AND also in a private Edge list
that will be used by the MinimumSpanningTree
function.
*/		
		void AddEdge(Edge*);
	        /*  returns optimal tsp */
		double OptimalTSP();
			/*  returns approximate tsp */
		double ApproximateTSP();
	        /*  uses brute force algorithim to find distance */		
		double TSPBruteForce(int, bool*);
	        /*  usesdynamic programming algorithim to find distance */			
		double TSPDP(int, int);
			/* finds the minumspaning tree using the dijoint set */	
		void MinimumSpanningTree();
			/* Returns the approximation for the TSP tour taken  */	
		double DepthFirstSearch();
		
	private:
		int numVertices;
		Edge*** graph;
		double** wieghts;
		 vector<Vertex*> adjacencyList;
		vector<Edge*>edgeList;

};

#endif
