# 🇺🇸 Waypoint Sorter - Made by kepacode

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

----------------------------------------

# 🇯🇵 ウェイポイントソーター-kepacodeによって作られました

3Dウェイポイントを含む複数のJSONファイルをマージし、近接でソートして最適化されたルートを作成するC#WinFormsアプリケーション。

#＃特長

-**複数のファイルをマージ**：複数のJSONファイルからウェイポイントをロードして結合
-**スマートソートアルゴリズム**:
  -**最近傍最適化**(デフォルト):常に最も近い未訪問のウェイポイントに移動することにより、効率的なルートを作成します
  -**単純な距離ソート**：最初の点からの距離ですべてのウェイポイントをソートします
-**ビジュアルデータグリッド**：その座標とプロパティを持つすべてのウェイポイントを表示
-**ルート距離計算**：最適化されたルートの総距離を示しています
-**エクスポート結果**：新しいJSONファイルにマージされ、ソートされたウェイポイントを保存

##要件

-.NET6.0以降
-Windows OS(WinFormsの要件)

##アプリケーションの構築

1. プロジェクトディレクトリでターミナルを開く
2. 次のコマンドを実行します:
   ```
   ドットネットビルド
   ```

3. アプリケーションを実行するには:
   ```
   ドットネットラン
   ```

または、Visual Studioでソリューションを開き、そこからビルド/実行します。

##JSON形式

の応用が期待JSONファイル経由のデータをこのフォーマット:

```json
[
  {
    "色":4294967295,
    "名前":"一般的なポイント0",
    "x":-1396.287109375,
    "y":199.22874450683594,
    "z":7748.14111328125
  },
  {
    "色":4294967295,
    "名前":"一般的なポイント1",
    "x":-1932.005615234375,
    "y":95.70904541015625,
    "z":8597.9267578125
  }
]
```

###必須項目:
-`x`(double):X座標
-`y`(double):Y座標
-`z'(double):Z座標
-`name'(文字列):ウェイポイント名
-'color'(long):色の値(オプション、0にすることができます)

##使用法

1. **Load Files**:"Load JSON Files"をクリックし、一つ以上のwaypoint JSONファイルを選択します
   -選択したファイルからのすべてのウェイポイントは、一つのリストにマージされます
   -ステータスバーは、どのように多くのファイルからロードされたどのように多くのウェイポイントを示しています

2. **ソートウェイポイント**：「ソートウェイポイント」をクリックします。
   -**ルートの最適化（チェック）**：効率的なパスを見つけるために最近傍アルゴリズムを使用しています
   -**ルートを最適化する（チェックされていない）**：最初のウェイポイントからの距離による単純なソート
   -ソート後の総ルート距離を示しています

3. **エクスポート**：マージされ、ソートされたウェイポイントを保存するには、"結果のエクスポート"をク
   -入力と同じ形式でJSONファイルとして保存します
   -あなたの適用に再輸入することができます

#＃ソートアルゴリズム

###最近傍最適化(推奨)
このアルゴリズムは、次の方法で効率的なルートを作成します:
1. 最初のウェイポイントから始まる
2. 最も近い未訪問のウェイポイントを見つける
3. そのウェイポイントに移動する
4. すべてのウェイポイントが訪問されるまで繰り返す

これにより、各ステップが最も近い利用可能なポイントに移動するルートが生成され、バックトラッキングが最小限に抑えられます。

##＃単純な距離ソート
うすべての頂点により直線距離からのwaypoint. より単純ですが、最も効率的なルートを生成しない可能性があります。

##距離を計算

を利用しての**3Dユークリッド距離式**:

```
距離=√((x₂-x₁)2+(y₂-y₁)2+(z₂-z₁)2)
```

この計算は、直線距離点を3D空間です。

##サンプルデータ

二つのサンプル点のファイルを含むに基づくアップロードされたファイル:
-'sample_waypoints1.json`-9頂点
-`sample_waypoints2.json`-頂点を追加

荷重の両方を試す場合、ソート機能!

##ヒント

-最適なルートの最適化のために、（デフォルトで有効になっている）"ルートの最適化"チェックボックスを使用します
-データグリッド内の順序は、最適化されたルート内のウェイポイントのシーケンスを示しています
-総ルート距離は、ソート後のステータスバーに表示されます
-あなたは、異なる設定でいつでもリロードし、再ソートすることができます
