# 🇺🇸 Waypoint Sorter - Made by kepacode

A C# WinForms application that merges multiple JSON files containing 3D waypoints and sorts them by proximity using a simple nearest-neighbor approach.

## Features

- **Merge Multiple Files**: Load and combine waypoints from multiple JSON files
- **Simple Nearest Neighbor Sorting**: Always travels to the closest unvisited waypoint
- **No Complex Formulas**: Uses simple coordinate difference comparison
- **Visual Data Grid**: View all waypoints with their coordinates and properties
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
   - **IMPORTANT**: Hold CTRL while clicking to select multiple files
   - All waypoints from selected files will be merged into one list
   - A message box will confirm how many files and waypoints were loaded

2. **Sort Waypoints**: Click "Sort Waypoints"
   - Uses nearest neighbor algorithm: always goes to the closest waypoint
   - Simple distance calculation: sum of X, Y, Z differences (no formulas)
   - Status bar shows completion message

3. **Export**: Click "Export Results" to save the merged and sorted waypoints
   - Saves as a JSON file in the same format as input
   - Can be re-imported into your application

## How Sorting Works

The sorting algorithm is very simple:

1. **Start** at the first waypoint
2. **Compare** distances to all remaining waypoints
   - Distance = |X₂-X₁| + |Y₂-Y₁| + |Z₂-Z₁|
   - Just adding up the differences, no squares or roots
3. **Move** to the waypoint with the smallest distance
4. **Repeat** until all waypoints are visited

This creates an efficient route without using complex mathematical formulas.

## Sample Data

Two sample waypoint files are included:
- `sample_waypoints1.json` - 9 waypoints
- `sample_waypoints2.json` - 102 waypoints

Load both to test the merging functionality!

## Tips

- Always use CTRL+Click to select multiple files in the file dialog
- The order in the data grid shows the sequence after sorting
- You can reload and re-sort anytime
- Check TROUBLESHOOTING.md if you have issues loading multiple files

# 🇯🇵 ウェイポイントソーター-kepacodeによって作られました

3Dウェイポイントを含む複数のJSONファイルをマージし、単純な最近傍アプローチを使用して近接によってソートするC#WinFormsアプリケーション。

#＃特長

-**複数のファイルをマージ**：複数のJSONファイルからウェイポイントをロードして結合
-**単純な最近傍ソート**：常に最も近い未訪問のウェイポイントに移動します
-**複雑な式はありません**：単純な座標差比較を使用しています
-**ビジュアルデータグリッド**：その座標とプロパティを持つすべてのウェイポイントを表示
-**エクスポート結果**：新しいJSONファイルにマージされ、ソートされたウェイポイントを保存

##要件

-.NET6.0以降
-Windows OS(WinFormsの要件)

##ビルの適用

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
   -**重要**：複数のファイルを選択するには、クリックしながらCTRLキーを押したまま
   -選択したファイルからのすべてのウェイポイントは、一つのリストにマージされます
   -メッセージボックスは、ロードされたファイルとウェイポイントの数を確認します

2. **ソートウェイポイント**：「ソートウェイポイント」をクリックします。
   -最近傍アルゴリズムを使用しています：常に最も近いウェイポイントに移動します
   -単純な距離計算：X、Y、Zの差の合計（式なし）
   -ステータスバーに完了メッセージが表示されます

3. **エクスポート**：マージされ、ソートされたウェイポイントを保存するには、"結果のエクスポート"をク
   -入力と同じ形式でJSONファイルとして保存します
   -あなたの適用に再輸入することができます

##ソートの仕組み

ソートアルゴリズムは非常に簡単です:

1. **スタート**最初のウェイポイントで
2. **比較**残りのすべてのウェイポイントへの距離
    距離=|X₂-X₁|+|Y₂-Y₁|+|Z₂-Z₁|
   -違いを合計するだけで、正方形や根はありません
3. **移動**最小の距離でウェイポイントに
4. **すべてのウェイポイントが訪問されるまで**を繰り返します

これにより、複雑な数式を使用せずに効率的なルートが作成されます。

##サンプルデータ

2つのサンプルウェイポイントファイルが含まれています:
-'sample_waypoints1.json'-9ウェイポイント
-'sample_waypoints2.json'-102ウェイポイント

両方をロードしてマージ機能をテストしてください!

#＃ヒント

-ファイルダイアログで複数のファイルを選択するには、常にCTRL+クリックを使用します
-データグリッド内の順序は、ソート後のシーケンスを示しています
-いつでもリロードして再ソートすることができます
-チェックTROUBLESHOOTING.md 複数のファイルの読み込みに問題がある場合
