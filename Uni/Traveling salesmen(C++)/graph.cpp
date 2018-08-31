/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: graph.cpp
 * --------------
 * source file includes the main code for the graph matrix class
 * 
 *
 */
#include "graph.h"
#include "vertex.h"
#include "float.h"
#include "edgecomparer.h"
#include "disjointset.h"

#include <stack>
#include <vector>
#include <algorithm>
#include <iostream>
/*  constructor for graph maxtrices and adjacency lists */
Graph::Graph(int numVertices){
	vector<Vertex*> adjacencyList;
			this->numVertices = numVertices;
    graph = new Edge**[numVertices];
    for (unsigned i = 0; i < numVertices; i++) {
        graph[i] = new Edge*[numVertices];
    }			
	for(int i=0; i<numVertices; i++){
		for(int j=0; j<numVertices; j++){
			graph[i][j];
		}
		graph[i][i];
	}

	adjacencyList.resize(numVertices);
    for (unsigned i = 0; i < numVertices; i++) {
    }
	//for number of vertices(vectorofvectors)->pushback(for number of vertices vector.pushback(max double/2))
	//vector< vector<double> > name of matrix;
}
/*  destructor for graph class */
Graph::~Graph()
{
	    for (unsigned i = 0; i < numVertices; i++) {
        delete[] graph[i];
    }
    delete[] graph;
}



/*  adds vertex to the adjacency list */
void Graph::AddVertex(Vertex* vertex){
	adjacencyList.push_back(vertex);
}
/*  Accsesor for the adjacency list */
Vertex* Graph::GetVertex(int vertexNum){
	return adjacencyList[vertexNum];
}
/*  adds edgeto the edge list  and graph maxtrices*/
void Graph::AddEdge(Edge* edge){
Vertex* edgev =edge->GetSource();
	int src = edge->GetSource()->GetId();
	int dst = edge->GetDestination()->GetId();
	graph[src][dst]= edge;
	graph[dst][src]= edge;
	edgeList.push_back(edge);
	sort(edgeList.begin(), edgeList.end(),  EdgeComparer());
}




/*  uses brute force to find optimal tsp then returns found distance*/
double Graph::OptimalTSP(){
	    bool visited[numVertices];
    	for(unsigned i = 0; i < numVertices; i++){
		visited[i] = false;
		return TSPBruteForce(0,visited);
	}
}
/*  uses dfs to find approximate distance
*Note i was unsure howto fully implement the minimum
 spanning tree and  dfs together through the adjacenccies, however it seams to work*/
double Graph::ApproximateTSP(){
	double distance;
		MinimumSpanningTree();
		distance=DepthFirstSearch();
		return distance;



	
}
//------------------------------------------------------------
/*  uses brute force to find distance then returns distance*/
double Graph::TSPBruteForce(int current, bool* visited){
	bool vistedCopy[numVertices];
	int alltrue= 0;
	double distance=0;
	int currentVertex;
	double minDistance;	
	for(unsigned i = 0; i < numVertices; i++){
		if(visited[i]){
			alltrue+= 1;
			
		}
	}	
	for(unsigned i = 0; i < numVertices; i++){
		vistedCopy[i] = visited[i];
	}
	for(unsigned adjacent = 0; adjacent < numVertices; adjacent++){	
		if((current !=  adjacent) && (vistedCopy[adjacent] == false)){
				minDistance = INFINITY;	
			vistedCopy[adjacent]= true;
			distance = graph[current][adjacent]->GetWeight()+ TSPBruteForce(adjacent, vistedCopy);
		minDistance = min(minDistance, distance);
		vistedCopy[adjacent]= false;
		}	
	}
	return minDistance;
	
	
}
//----------------------------------------------------------
/*  not implemented*/
double Graph::TSPDP(int,int){
	
	
}
/*  uses disjointset to find minimum spanning tree*/
void Graph::MinimumSpanningTree(){
DisjointSet set(adjacencyList.size());
double minCost = 0;
int edgeCount =0;
int vectorCount=0;
	while((vectorCount<=edgeList.size()) && (edgeCount < (adjacencyList.size()-1))){
		if(!set.SameComponent(edgeList.at(vectorCount)->GetSource()->GetId(),(edgeList.at(vectorCount)->GetDestination()->GetId()))){
		edgeCount+= 1;
		set.Union(edgeList.at(vectorCount)->GetSource()->GetId(), edgeList.at(vectorCount)->GetDestination()->GetId());
		edgeList.at(vectorCount)->GetSource()->AddAdjacency(edgeList.at(vectorCount)->GetDestination());
		edgeList.at(vectorCount)->GetDestination()->AddAdjacency(edgeList.at(vectorCount)->GetSource());
		minCost +=edgeList.at(vectorCount)->GetWeight();
		}
		vectorCount+=1;
	}
}	
/*  uses dfs to find distance*/
double Graph::DepthFirstSearch(){
	double distance=0;
	int visited[adjacencyList.size()];
	for (unsigned i = 0; i < adjacencyList.size(); i++) {
        visited[i] = false;
    }

    stack<Vertex*> vertexStack;
    vertexStack.push(edgeList[0]->GetSource());
    visited[edgeList[0]->GetSource()->GetId()] = true;
    
	Vertex* currentVertex = 0;
	Vertex* previousVertex = 0;

    while (!vertexStack.empty()) {
        currentVertex = vertexStack.top();
        vertexStack.pop();
        if(previousVertex != 0){
        	distance = distance +graph[previousVertex->GetId()][currentVertex->GetId()]->GetWeight(); 
        }
        
        for(unsigned j = 0; j < adjacencyList.size(); j++){
        if(visited[j]== false){
		vertexStack.push(adjacencyList.at(j));
			visited[j]= true;
			}
		}
		previousVertex = currentVertex;
	}
	distance = distance +graph[edgeList[0]->GetSource()->GetId()][currentVertex->GetId()]->GetWeight();
	return distance;
    
}
	

