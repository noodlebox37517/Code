/*
 * File:  TSP.cpp
 *
 * Driver program for Travelling Salesman Problem.
 *
 * The Travelling Salesamen Problem works on graphs which adhere to the triangle inequality (the sum of the
 * lengths of any two sides must be greater than or equal to the length of the remaining side).  For this reason, this
 * program uses points on the Cartesian plane as the basis for its vertices and the Euclidean (straight line)
 * distance between them as the edge weight.
 *
 * This program can use as input:
 * 1.  Randomly generated points from the Cartesian plane
 * 2.  Points from the Cartesian plane read from a file (file name given as a command line argument)
 *
 * The points are only required to compute the edge weights between the vertices to satisfy the triangle inequality.
 *
 * 1.  Generate or read N points
 * 2.  Create an instance of a Graph.
 * 3.  Add a vertex for each point to the graph (store only the id).
 *      - the vertex will be added to an adjacency list for the graph
 *      - this adjacency list will store the edges in the Minimum Spanning Tree of the graph
 *      - each vertex will need to store the vertices it is adjacent to in the Minimum Spanning Tree for use in
 *        the Depth First Search which details the visit order of cities in the TSP tour.
 * 4.  Create an undirected edge from each point to every other point using the Euclidean distance between the two
 *     vertices as the edge weight.
 *      - This edge will be added to the adjacency matrix in the graph.
 *      - The edge weight from a vertex to itself will be 0.
 * 5.  Compute the distance of the optimal TSP tour.
 *      - This is only feasible using the Brute Force approach if NUM_CITIES < 11 +/- 1.
 *      - This is only feasible using the Dynamic Programming approach if NUM_CITIES < 21 +/- 2.
 *      - These upper bounds for NUM_CITIES depend on the memory and speed of the computer being used.
 * 6.  Compute the distance of the approximate TSP tour
 *      - Calculate the Minimum Spanning Tree for the input graph.
 *      - Store the edges in the graph's adjacency list.
 *      - Perform a Depth First Traversal of the sub-graph defined by the Minimum Spanning Tree
 *      - Calculate the distance of the tour given by the visit order of the Depth First Search.
 *      - Remember to include the distance from the final vertex back to the starting vertex in the tour.
 *
 *
 *  NOTES: The given code uses pointers to objects in most places.
 *         The Point class and Edge class defined in this solution use C++ Templates.
 *         *** Type parameters may be removed from this code without affecting the mark for the assignment. ***
 *
 */

#include <iostream>
#include <fstream>
#include <iomanip>
#include <vector>

#include "random.h"
#include "point.h"
#include "graph.h"

using namespace std;

const int MINIMUM_COORDINATE = 0;
const int MAXIMUM_COORDINATE = 100;
const int NUM_CITIES = 15;
const int MAXIMUM_OPTIMAL_CITIES = 23;

int main(int argc, char *argv[]) {

    int numCities = NUM_CITIES;
    Random* random = new Random();
    vector< Point<int>* > cities = vector< Point<int>* >();

    // allow for testing from file
    if (argc == 2) {

        // open the file and check it exists
        ifstream infile;
        infile.open(argv[1]);
        if (infile.fail()) {
            cerr <<  "Error: Could not find file" << endl;
            return 1;
        }

        // read the number of cities and their co-ordinates
        int xCoordinate, yCoordinate;
        infile >> numCities;
        for (int city = 0; city < numCities; city++) {
            infile >> xCoordinate >> yCoordinate;
            cities.push_back(new Point<int>(xCoordinate, yCoordinate));
            cout << "City " << setw(2) << city << " co-ordinates : " << cities[city]->ToString() << endl;
        }

        // close the file
        infile.close();
    } else {

        // randomly create and store numCities points
        for (int city = 0; city < numCities; city++) {
            Point<int>* point = new Point<int>(random->GetRandomInteger(MINIMUM_COORDINATE, MAXIMUM_COORDINATE),
                                               random->GetRandomInteger(MINIMUM_COORDINATE, MAXIMUM_COORDINATE));
            cities.push_back(point);
            cout << "City " << setw(2) << city << " co-ordinates : " << cities[city]->ToString() << endl;
        }
    }
    cout << endl;

    // create the graph and add edges for all cities
    Graph* graph = new Graph(numCities);
    for (int i = 0; i < numCities; i++) {
        Vertex* v = new Vertex(i);
        graph->AddVertex(v);
    }

    for (int i = 0; i < numCities; i++) {
        for (int j = 0; j < numCities; j++) {
            Edge<double>* edge = new Edge<double>(graph->GetVertex(i), graph->GetVertex(j), cities[i]->DistanceTo(cities[j]));
            graph->AddEdge(edge);
        }
    }

    //graph->Display();

    // calculate the optimal and/or approximate distance of the tour for this graph
    double optimalTour = 0.0;
    double approximateTour = 0.0;
    if (numCities <= MAXIMUM_OPTIMAL_CITIES) {
        optimalTour = graph->OptimalTSP();
        cout << "Total Distance of the Optimal Tour is " << optimalTour << endl;
    }

    approximateTour = graph->ApproximateTSP();
    cout << "Total Distance of the Approximate Tour is " << approximateTour << endl;

    return 0;
}
