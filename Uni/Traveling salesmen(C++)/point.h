/*
 * Student number: N8589119
 * Student Name: Thomas Fleming
 *
 * File: point.h
 * Interface defining functionality for point object.
 * has a x and y coordinates
 */
#include <string>

using namespace std;

#ifndef POINT_H
#define POINT_H

class Point {
public:
/*  constructor with point identifier arg */
	Point(int,int);
/*  Destructor */		
	~Point();
/*  Accsesor for distance between point and given point */
	double DistanceTo(Point*);
/*  Accsesor for string representation of points */	
	string ToString();

	private:
		int xCoords;
		int yCoords;
		
};

#endif
