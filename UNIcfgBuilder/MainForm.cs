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
            chkOptimizeRoute = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            SuspendLayout();
            btnLoadFiles.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLoadFiles.Location = new Point(14, 14);
            btnLoadFiles.Margin = new Padding(4, 3, 4, 3);
            btnLoadFiles.Name = "btnLoadFiles";
            btnLoadFiles.Size = new Size(175, 40);
            btnLoadFiles.TabIndex = 6;
            btnLoadFiles.Text = "Load JSON Files";
            btnLoadFiles.UseVisualStyleBackColor = true;
            btnLoadFiles.Click += btnLoadFiles_Click;
            btnSort.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSort.Location = new Point(14, 127);
            btnSort.Margin = new Padding(4, 3, 4, 3);
            btnSort.Name = "btnSort";
            btnSort.Size = new Size(175, 40);
            btnSort.TabIndex = 3;
            btnSort.Text = "Sort Waypoints";
            btnSort.UseVisualStyleBackColor = true;
            btnSort.Click += btnSort_Click;
            btnExport.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExport.Location = new Point(204, 127);
            btnExport.Margin = new Padding(4, 3, 4, 3);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(175, 40);
            btnExport.TabIndex = 2;
            btnExport.Text = "Export Results";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += btnExport_Click;
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Location = new Point(14, 208);
            dgvResults.Margin = new Padding(4, 3, 4, 3);
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.Size = new Size(897, 427);
            dgvResults.TabIndex = 0;
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Blue;
            lblStatus.Location = new Point(14, 179);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(129, 15);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Ready - No files loaded";
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(14, 63);
            lblInfo.Margin = new Padding(4, 0, 4, 0);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(396, 15);
            lblInfo.TabIndex = 5;
            lblInfo.Text = "Load multiple JSON files to merge them, then sort waypoints by proximity";
            chkOptimizeRoute.AutoSize = true;
            chkOptimizeRoute.Checked = true;
            chkOptimizeRoute.CheckState = CheckState.Checked;
            chkOptimizeRoute.Location = new Point(14, 92);
            chkOptimizeRoute.Margin = new Padding(4, 3, 4, 3);
            chkOptimizeRoute.Name = "chkOptimizeRoute";
            chkOptimizeRoute.Size = new Size(260, 19);
            chkOptimizeRoute.TabIndex = 4;
            chkOptimizeRoute.Text = "Optimize route (nearest neighbor algorithm)";
            chkOptimizeRoute.UseVisualStyleBackColor = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(925, 647);
            Controls.Add(dgvResults);
            Controls.Add(lblStatus);
            Controls.Add(btnExport);
            Controls.Add(btnSort);
            Controls.Add(chkOptimizeRoute);
            Controls.Add(lblInfo);
            Controls.Add(btnLoadFiles);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            ShowInTaskbar = false;
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
        private CheckBox chkOptimizeRoute;

        private void btnLoadFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                ofd.Multiselect = true;
                ofd.Title = "Select JSON waypoint files to merge";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    allWaypoints.Clear();
                    int totalRecords = 0;
                    List<string> loadedFiles = new List<string>();

                    foreach (string fileName in ofd.FileNames)
                    {
                        try
                        {
                            string jsonContent = File.ReadAllText(fileName);
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
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error loading {Path.GetFileName(fileName)}:\n{ex.Message}", 
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (totalRecords > 0)
                    {
                        lblStatus.Text = $"Loaded {totalRecords} waypoints from {loadedFiles.Count} file(s): {string.Join(", ", loadedFiles)}";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        DisplayData(allWaypoints);
                    }
                    else
                    {
                        lblStatus.Text = "No waypoints loaded";
                        lblStatus.ForeColor = System.Drawing.Color.Red;
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

            List<Waypoint> sorted;

            if (chkOptimizeRoute.Checked)
            {
                sorted = OptimizeRoute(allWaypoints);
                lblStatus.Text = $"Sorted {sorted.Count} waypoints using nearest neighbor optimization";
            }
            else
            {
                // Simple sort by distance from first point
                Waypoint reference = allWaypoints[0];
                sorted = allWaypoints
                    .OrderBy(w => Calculate3DDistance(reference.X, reference.Y, reference.Z, w.X, w.Y, w.Z))
                    .ToList();
                lblStatus.Text = $"Sorted {sorted.Count} waypoints by distance from first point";
            }

            allWaypoints = sorted;
            DisplayData(allWaypoints);
            lblStatus.ForeColor = System.Drawing.Color.Green;

            // Calculate total route distance
            double totalDistance = 0;
            for (int i = 0; i < allWaypoints.Count - 1; i++)
            {
                totalDistance += Calculate3DDistance(
                    allWaypoints[i].X, allWaypoints[i].Y, allWaypoints[i].Z,
                    allWaypoints[i + 1].X, allWaypoints[i + 1].Y, allWaypoints[i + 1].Z);
            }
            lblStatus.Text += $" | Total route distance: {totalDistance:F2} units";
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
                            WriteIndented = true
                        };

                        string jsonOutput = JsonSerializer.Serialize(allWaypoints, options);
                        File.WriteAllText(sfd.FileName, jsonOutput);
                        
                        lblStatus.Text = $"Exported {allWaypoints.Count} waypoints to {Path.GetFileName(sfd.FileName)}";
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

        private double Calculate3DDistance(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            double dz = z2 - z1;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        private List<Waypoint> OptimizeRoute(List<Waypoint> waypoints)
        {
            if (waypoints.Count <= 2)
                return new List<Waypoint>(waypoints);

            List<Waypoint> result = new List<Waypoint>();
            List<Waypoint> remaining = new List<Waypoint>(waypoints);

            // Start with the first waypoint
            Waypoint current = remaining[0];
            result.Add(current);
            remaining.RemoveAt(0);

            // Keep finding the nearest unvisited waypoint
            while (remaining.Count > 0)
            {
                Waypoint nearest = null;
                double minDistance = double.MaxValue;
                int nearestIndex = -1;

                for (int i = 0; i < remaining.Count; i++)
                {
                    double distance = Calculate3DDistance(
                        current.X, current.Y, current.Z,
                        remaining[i].X, remaining[i].Y, remaining[i].Z);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearest = remaining[i];
                        nearestIndex = i;
                    }
                }

                if (nearest != null)
                {
                    result.Add(nearest);
                    remaining.RemoveAt(nearestIndex);
                    current = nearest;
                }
            }

            return result;
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
