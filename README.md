# Waypoint Sorter - Made by kepacode

A C# WinForms application that merges multiple JSON files containing 3D waypoints and sorts them by proximity to create an optimized route.

## Features

- **Merge Multiple Files**: Load and combine waypoints from multiple JSON files
- **Smart Sorting Algorithms**:
  - **Nearest Neighbor Optimization** (default): Creates an efficient route by always traveling to the closest unvisited waypoint
  - **Simple Distance Sort**: Sorts all waypoints by distance from the first point
- **Visual Data Grid**: View all waypoints with their coordinates and properties
- **Route Distance Calculation**: Shows total distance of the optimized route
- **Export Results**: Save merged and sorted waypoints to a new JSON file

## Requirements

- .NET 6.0 or later
- Windows OS (WinForms requirement)

## Building the Application

1. Open a terminal in the project directory
2. Run the following command:
   ```
   dotnet build
   ```

3. To run the application:
   ```
   dotnet run
   ```

Alternatively, open the solution in Visual Studio and build/run from there.

## JSON Format

The application expects JSON files with waypoint data in this format:

```json
[
  {
    "color": 4294967295,
    "name": "Generic Point 0",
    "x": -1396.287109375,
    "y": 199.22874450683594,
    "z": 7748.14111328125
  },
  {
    "color": 4294967295,
    "name": "Generic Point 1",
    "x": -1932.005615234375,
    "y": 95.70904541015625,
    "z": 8597.9267578125
  }
]
```

### Required Fields:
- `x` (double): X coordinate
- `y` (double): Y coordinate
- `z` (double): Z coordinate
- `name` (string): Waypoint name
- `color` (long): Color value (optional, can be 0)

## Usage

1. **Load Files**: Click "Load JSON Files" and select one or more waypoint JSON files
   - All waypoints from selected files will be merged into one list
   - Status bar shows how many waypoints were loaded from how many files

2. **Sort Waypoints**: Click "Sort Waypoints"
   - **Optimize route (checked)**: Uses nearest neighbor algorithm to find an efficient path
   - **Optimize route (unchecked)**: Simple sort by distance from first waypoint
   - Shows total route distance after sorting

3. **Export**: Click "Export Results" to save the merged and sorted waypoints
   - Saves as a JSON file in the same format as input
   - Can be re-imported into your application

## Sorting Algorithms

### Nearest Neighbor Optimization (Recommended)
This algorithm creates an efficient route by:
1. Starting at the first waypoint
2. Finding the closest unvisited waypoint
3. Moving to that waypoint
4. Repeating until all waypoints are visited

This produces a route where each step travels to the nearest available point, minimizing backtracking.

### Simple Distance Sort
Sorts all waypoints by their straight-line distance from the first waypoint. Simpler but may not produce the most efficient route.

## Distance Calculation

The application uses the **3D Euclidean distance formula**:

```
distance = √((x₂-x₁)² + (y₂-y₁)² + (z₂-z₁)²)
```

This calculates the straight-line distance between two points in 3D space.

## Sample Data

Two sample waypoint files are included based on your uploaded files:
- `sample_waypoints1.json` - 9 waypoints
- `sample_waypoints2.json` - Additional waypoints

Load both to test the merging and sorting functionality!

## Tips

- For best route optimization, use the "Optimize route" checkbox (enabled by default)
- The order in the data grid shows the sequence of waypoints in the optimized route
- Total route distance is shown in the status bar after sorting
- You can reload and re-sort anytime with different settings
