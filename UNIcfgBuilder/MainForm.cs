using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Text.Json;

namespace CoordinateSorter
{
    public partial class MainForm : Form
    {
        private List<Waypoint> allWaypoints = new List<Waypoint>();
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            btnLoadFiles = new Button();
            btnSort = new Button();
            btnExport = new Button();
            dgvResults = new DataGridView();
            lblStatus = new Label();
            lblInfo = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            SuspendLayout();
            // 
            // btnLoadFiles
            // 
            btnLoadFiles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnLoadFiles.Location = new System.Drawing.Point(14, 14);
            btnLoadFiles.Margin = new Padding(4, 3, 4, 3);
            btnLoadFiles.Name = "btnLoadFiles";
            btnLoadFiles.Size = new System.Drawing.Size(175, 40);
            btnLoadFiles.TabIndex = 5;
            btnLoadFiles.Text = "Load JSON Files";
            btnLoadFiles.UseVisualStyleBackColor = true;
            btnLoadFiles.Click += btnLoadFiles_Click;
            // 
            // btnSort
            // 
            btnSort.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnSort.Location = new System.Drawing.Point(14, 92);
            btnSort.Margin = new Padding(4, 3, 4, 3);
            btnSort.Name = "btnSort";
            btnSort.Size = new System.Drawing.Size(175, 40);
            btnSort.TabIndex = 3;
            btnSort.Text = "Sort Waypoints";
            btnSort.UseVisualStyleBackColor = true;
            btnSort.Click += btnSort_Click;
            // 
            // btnExport
            // 
            btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            btnExport.Location = new System.Drawing.Point(204, 92);
            btnExport.Margin = new Padding(4, 3, 4, 3);
            btnExport.Name = "btnExport";
            btnExport.Size = new System.Drawing.Size(175, 40);
            btnExport.TabIndex = 2;
            btnExport.Text = "Export Results";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            // 
            // dgvResults
            // 
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Location = new System.Drawing.Point(14, 173);
            dgvResults.Margin = new Padding(4, 3, 4, 3);
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.Size = new System.Drawing.Size(1003, 462);
            dgvResults.TabIndex = 0;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = System.Drawing.Color.Blue;
            lblStatus.Location = new System.Drawing.Point(14, 144);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(129, 15);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Ready - No files loaded";
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new System.Drawing.Point(14, 63);
            lblInfo.Margin = new Padding(4, 0, 4, 0);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new System.Drawing.Size(435, 15);
            lblInfo.TabIndex = 4;
            lblInfo.Text = "Load multiple JSON files to merge them, then sort waypoints by nearest neighbor";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new System.Drawing.Size(1031, 647);
            Controls.Add(dgvResults);
            Controls.Add(lblStatus);
            Controls.Add(btnExport);
            Controls.Add(btnSort);
            Controls.Add(lblInfo);
            Controls.Add(btnLoadFiles);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Waypoint Sorter - https://github.com/kepacode";
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnLoadFiles;
        private Button btnSort;
        private Button btnExport;
        private DataGridView dgvResults;
        private Label lblStatus;
        private Label lblInfo;

        private void btnLoadFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                ofd.Multiselect = true;
                ofd.Title = "Select JSON waypoint files to merge";
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    allWaypoints.Clear();
                    int totalRecords = 0;
                    List<string> loadedFiles = new List<string>();
                    List<string> failedFiles = new List<string>();

                    // Show how many files were selected
                    string debugInfo = $"Selected {ofd.FileNames.Length} file(s)\n\n";

                    foreach (string fileName in ofd.FileNames)
                    {
                        try
                        {
                            debugInfo += $"Processing: {Path.GetFileName(fileName)}\n";
                            string jsonContent = File.ReadAllText(fileName);
                            debugInfo += $"  File size: {jsonContent.Length} characters\n";
                            
                            var options = new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            };

                            List<Waypoint> data = JsonSerializer.Deserialize<List<Waypoint>>(jsonContent, options);

                            if (data != null && data.Count > 0)
                            {
                                allWaypoints.AddRange(data);
                                totalRecords += data.Count;
                                loadedFiles.Add(Path.GetFileName(fileName));
                                debugInfo += $"  Loaded: {data.Count} waypoints\n";
                            }
                            else
                            {
                                debugInfo += $"  Warning: File contained no waypoints\n";
                                failedFiles.Add(Path.GetFileName(fileName));
                            }
                        }
                        catch (Exception ex)
                        {
                            failedFiles.Add(Path.GetFileName(fileName));
                            debugInfo += $"  ERROR: {ex.Message}\n";
                            MessageBox.Show($"Error loading {Path.GetFileName(fileName)}:\n\n{ex.Message}\n\nStack trace:\n{ex.StackTrace}", 
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        debugInfo += "\n";
                    }

                    // Show debug info
                    if (System.Diagnostics.Debugger.IsAttached || ofd.FileNames.Length > 1)
                    {
                        System.Diagnostics.Debug.WriteLine(debugInfo);
                    }

                    if (totalRecords > 0)
                    {
                        string statusMessage = $"✓ Loaded {totalRecords} waypoints from {loadedFiles.Count} file(s)";
                        if (failedFiles.Count > 0)
                        {
                            statusMessage += $" ({failedFiles.Count} failed: {string.Join(", ", failedFiles)})";
                        }
                        lblStatus.Text = statusMessage;
                        lblStatus.ForeColor = failedFiles.Count > 0 ? System.Drawing.Color.DarkOrange : System.Drawing.Color.Green;
                        DisplayData(allWaypoints);
                        
                        // Show summary message box
                        string summary = $"Successfully merged {loadedFiles.Count} file(s):\n\n";
                        foreach (var file in loadedFiles)
                        {
                            summary += $"✓ {file}\n";
                        }
                        summary += $"\nTotal waypoints: {totalRecords}";
                        MessageBox.Show(summary, "Files Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        lblStatus.Text = "⚠ No waypoints loaded - check file format";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                        MessageBox.Show(debugInfo, "Loading Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (allWaypoints.Count == 0)
            {
                MessageBox.Show("Please load JSON files first.", "No Data", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (allWaypoints.Count == 1)
            {
                MessageBox.Show("Only one waypoint loaded. Nothing to sort.", "Info", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<Waypoint> sorted = SortByNearestNeighbor(allWaypoints);
            
            allWaypoints = sorted;
            DisplayData(allWaypoints);
            lblStatus.Text = $"✓ Sorted {sorted.Count} waypoints by nearest neighbor";
            lblStatus.ForeColor = System.Drawing.Color.Green;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (allWaypoints.Count == 0)
            {
                MessageBox.Show("No data to export.", "No Data", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JSON files (*.json)|*.json";
                sfd.Title = "Export merged and sorted waypoints";
                sfd.FileName = "merged_waypoints.json";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var options = new JsonSerializerOptions
                        {
                            WriteIndented = true,
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase  // Makes properties lowercase
                        };

                        string jsonOutput = JsonSerializer.Serialize(allWaypoints, options);
                        File.WriteAllText(sfd.FileName, jsonOutput);
                        
                        lblStatus.Text = $"✓ Exported {allWaypoints.Count} waypoints to {Path.GetFileName(sfd.FileName)}";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        MessageBox.Show($"Successfully exported {allWaypoints.Count} waypoints!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting data: {ex.Message}", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void DisplayData(List<Waypoint> data)
        {
            var displayData = data.Select((w, index) => new
            {
                Order = index + 1,
                Name = w.Name,
                X = w.X,
                Y = w.Y,
                Z = w.Z,
                Color = w.Color
            }).ToList();

            dgvResults.DataSource = null;
            dgvResults.DataSource = displayData;
            
            if (dgvResults.Columns.Count > 0)
            {
                dgvResults.Columns["Order"].Width = 50;
                dgvResults.Columns["Name"].Width = 150;
                dgvResults.Columns["X"].Width = 120;
                dgvResults.Columns["Y"].Width = 120;
                dgvResults.Columns["Z"].Width = 120;
                dgvResults.Columns["Color"].Width = 100;
            }
        }

        // Simple comparison: which waypoint is closer to current?
        // Just compares the direct differences without any formulas
        private double GetSimpleDistance(Waypoint from, Waypoint to)
        {
            // Calculate absolute differences
            double xDiff = Math.Abs(to.X - from.X);
            double yDiff = Math.Abs(to.Y - from.Y);
            double zDiff = Math.Abs(to.Z - from.Z);
            
            // Simple sum of differences (no squares, no roots)
            return xDiff + yDiff + zDiff;
        }

        // Sort waypoints by always going to the nearest unvisited one
        private List<Waypoint> SortByNearestNeighbor(List<Waypoint> waypoints)
        {
            if (waypoints.Count <= 1)
                return new List<Waypoint>(waypoints);

            List<Waypoint> sorted = new List<Waypoint>();
            List<Waypoint> remaining = new List<Waypoint>(waypoints);

            // Start with the first waypoint
            Waypoint current = remaining[0];
            sorted.Add(current);
            remaining.Remove(current);

            // Keep finding the nearest waypoint
            while (remaining.Count > 0)
            {
                Waypoint nearest = remaining[0];
                double nearestDistance = GetSimpleDistance(current, nearest);

                // Check all remaining waypoints to find the closest one
                for (int i = 1; i < remaining.Count; i++)
                {
                    double distance = GetSimpleDistance(current, remaining[i]);
                    
                    if (distance < nearestDistance)
                    {
                        nearest = remaining[i];
                        nearestDistance = distance;
                    }
                }

                // Move to the nearest waypoint
                sorted.Add(nearest);
                remaining.Remove(nearest);
                current = nearest;
            }

            return sorted;
        }
    }

    public class Waypoint
    {
        public long Color { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}
