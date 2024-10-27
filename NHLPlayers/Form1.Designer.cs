namespace NHLPlayers
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dtGV_results = new DataGridView();
            tb_filter = new TextBox();
            tb_order = new TextBox();
            lbl_filters = new Label();
            lbl_sort = new Label();
            btn_update = new Button();
            lbl_ResultCount = new Label();
            ((System.ComponentModel.ISupportInitialize)dtGV_results).BeginInit();
            SuspendLayout();
            // 
            // dtGV_results
            // 
            dtGV_results.AllowUserToAddRows = false;
            dtGV_results.AllowUserToDeleteRows = false;
            dtGV_results.AllowUserToOrderColumns = true;
            dtGV_results.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtGV_results.Dock = DockStyle.Bottom;
            dtGV_results.Location = new Point(0, 64);
            dtGV_results.Name = "dtGV_results";
            dtGV_results.RowHeadersWidth = 51;
            dtGV_results.Size = new Size(800, 386);
            dtGV_results.TabIndex = 0;
            // 
            // tb_filter
            // 
            tb_filter.Location = new Point(-1, 38);
            tb_filter.Name = "tb_filter";
            tb_filter.Size = new Size(235, 27);
            tb_filter.TabIndex = 1;
            tb_filter.KeyDown += tb_OnEnter;
            // 
            // tb_order
            // 
            tb_order.Location = new Point(249, 38);
            tb_order.Name = "tb_order";
            tb_order.Size = new Size(235, 27);
            tb_order.TabIndex = 2;
            tb_order.KeyDown += tb_OnEnter;
            // 
            // lbl_filters
            // 
            lbl_filters.AutoSize = true;
            lbl_filters.Location = new Point(-1, 15);
            lbl_filters.Name = "lbl_filters";
            lbl_filters.Size = new Size(48, 20);
            lbl_filters.TabIndex = 3;
            lbl_filters.Text = "Filters";
            // 
            // lbl_sort
            // 
            lbl_sort.AutoSize = true;
            lbl_sort.Location = new Point(249, 15);
            lbl_sort.Name = "lbl_sort";
            lbl_sort.Size = new Size(36, 20);
            lbl_sort.TabIndex = 4;
            lbl_sort.Text = "Sort";
            // 
            // btn_update
            // 
            btn_update.Location = new Point(660, 15);
            btn_update.Name = "btn_update";
            btn_update.Size = new Size(94, 29);
            btn_update.TabIndex = 5;
            btn_update.Text = "Update";
            btn_update.UseVisualStyleBackColor = true;
            btn_update.Click += btn_update_Click;
            // 
            // lbl_ResultCount
            // 
            lbl_ResultCount.AutoSize = true;
            lbl_ResultCount.Location = new Point(493, 15);
            lbl_ResultCount.Name = "lbl_ResultCount";
            lbl_ResultCount.Size = new Size(92, 20);
            lbl_ResultCount.TabIndex = 6;
            lbl_ResultCount.Text = "Result Count";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lbl_ResultCount);
            Controls.Add(btn_update);
            Controls.Add(lbl_sort);
            Controls.Add(lbl_filters);
            Controls.Add(tb_order);
            Controls.Add(tb_filter);
            Controls.Add(dtGV_results);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dtGV_results).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dtGV_results;
        private TextBox tb_filter;
        private TextBox tb_order;
        private Label lbl_filters;
        private Label lbl_sort;
        private Button btn_update;
        private Label lbl_ResultCount;
    }
}
