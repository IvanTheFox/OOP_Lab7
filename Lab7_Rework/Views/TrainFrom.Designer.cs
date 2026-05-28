namespace Lab7_Rework
{
    partial class TrainFrom
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            lblTitle = new Label();
            StatusLabel = new Label();
            TrainNumberInput = new TextBox();
            TrainDestinationInput = new TextBox();
            TrainDepartureInput = new TextBox();
            TrainTypeInput = new ComboBox();
            TrainSeatsInput = new NumericUpDown();
            TrainFreeSeatsInput = new NumericUpDown();
            SearchQueryInput = new TextBox();
            TrainTable = new DataGridView();
            AddTrainButton = new Button();
            DeleteTrainButton = new Button();
            SearchTrainButton = new Button();
            panel1 = new Panel();
            ModifyTrainButton = new Button();
            ((System.ComponentModel.ISupportInitialize)TrainSeatsInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrainFreeSeatsInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrainTable).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 12);
            label1.Name = "label1";
            label1.Size = new Size(55, 20);
            label1.TabIndex = 0;
            label1.Text = "Номер:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(13, 51);
            label2.Name = "label2";
            label2.Size = new Size(86, 20);
            label2.TabIndex = 6;
            label2.Text = "Назначение:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 95);
            label3.Name = "label3";
            label3.Size = new Size(137, 20);
            label3.TabIndex = 7;
            label3.Text = "Время отправления:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 136);
            label4.Name = "label4";
            label4.Size = new Size(34, 20);
            label4.TabIndex = 8;
            label4.Text = "Тип:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 178);
            label5.Name = "label5";
            label5.Size = new Size(83, 20);
            label5.TabIndex = 9;
            label5.Text = "Мест всего:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(16, 220);
            label6.Name = "label6";
            label6.Size = new Size(109, 20);
            label6.TabIndex = 10;
            label6.Text = "Мест свободно:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(432, 223);
            label7.Name = "label7";
            label7.Size = new Size(50, 20);
            label7.TabIndex = 11;
            label7.Text = "Поиск:";
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Arial Narrow", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(13, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(860, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Расписание поездов";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // StatusLabel
            // 
            StatusLabel.ForeColor = Color.DimGray;
            StatusLabel.Location = new Point(13, 648);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(860, 22);
            StatusLabel.TabIndex = 3;
            StatusLabel.Text = "Готово.";
            // 
            // TrainNumberInput
            // 
            TrainNumberInput.Location = new Point(212, 9);
            TrainNumberInput.Name = "TrainNumberInput";
            TrainNumberInput.Size = new Size(225, 26);
            TrainNumberInput.TabIndex = 5;
            // 
            // TrainDestinationInput
            // 
            TrainDestinationInput.Location = new Point(212, 48);
            TrainDestinationInput.Name = "TrainDestinationInput";
            TrainDestinationInput.Size = new Size(225, 26);
            TrainDestinationInput.TabIndex = 6;
            // 
            // TrainDepartureInput
            // 
            TrainDepartureInput.Location = new Point(212, 92);
            TrainDepartureInput.Name = "TrainDepartureInput";
            TrainDepartureInput.Size = new Size(227, 26);
            TrainDepartureInput.TabIndex = 7;
            // 
            // TrainTypeInput
            // 
            TrainTypeInput.DropDownStyle = ComboBoxStyle.DropDownList;
            TrainTypeInput.Location = new Point(212, 133);
            TrainTypeInput.Name = "TrainTypeInput";
            TrainTypeInput.Size = new Size(225, 28);
            TrainTypeInput.TabIndex = 8;
            // 
            // TrainSeatsInput
            // 
            TrainSeatsInput.Location = new Point(168, 175);
            TrainSeatsInput.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            TrainSeatsInput.Name = "TrainSeatsInput";
            TrainSeatsInput.Size = new Size(120, 26);
            TrainSeatsInput.TabIndex = 9;
            // 
            // TrainFreeSeatsInput
            // 
            TrainFreeSeatsInput.Location = new Point(168, 217);
            TrainFreeSeatsInput.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            TrainFreeSeatsInput.Name = "TrainFreeSeatsInput";
            TrainFreeSeatsInput.Size = new Size(120, 26);
            TrainFreeSeatsInput.TabIndex = 10;
            // 
            // SearchQueryInput
            // 
            SearchQueryInput.Location = new Point(511, 217);
            SearchQueryInput.Name = "SearchQueryInput";
            SearchQueryInput.Size = new Size(225, 26);
            SearchQueryInput.TabIndex = 11;
            // 
            // TrainTable
            // 
            TrainTable.AllowUserToAddRows = false;
            TrainTable.AllowUserToDeleteRows = false;
            TrainTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TrainTable.ColumnHeadersHeight = 40;
            TrainTable.Location = new Point(13, 45);
            TrainTable.MultiSelect = false;
            TrainTable.Name = "TrainTable";
            TrainTable.ReadOnly = true;
            TrainTable.RowHeadersWidth = 51;
            TrainTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TrainTable.Size = new Size(860, 250);
            TrainTable.TabIndex = 0;
            TrainTable.SelectionChanged += TrainTable_SelectionChanged;
            // 
            // AddTrainButton
            // 
            AddTrainButton.Location = new Point(524, 9);
            AddTrainButton.Name = "AddTrainButton";
            AddTrainButton.Size = new Size(130, 35);
            AddTrainButton.TabIndex = 13;
            AddTrainButton.Text = "Добавить";
            AddTrainButton.Click += AddTrainButton_Click;
            // 
            // DeleteTrainButton
            // 
            DeleteTrainButton.Location = new Point(524, 126);
            DeleteTrainButton.Name = "DeleteTrainButton";
            DeleteTrainButton.Size = new Size(171, 35);
            DeleteTrainButton.TabIndex = 15;
            DeleteTrainButton.Text = "Удалить выбранный";
            DeleteTrainButton.Click += DeleteTrainButton_Click;
            // 
            // SearchTrainButton
            // 
            SearchTrainButton.Location = new Point(750, 217);
            SearchTrainButton.Name = "SearchTrainButton";
            SearchTrainButton.Size = new Size(100, 30);
            SearchTrainButton.TabIndex = 12;
            SearchTrainButton.Text = "Найти";
            SearchTrainButton.Click += SearchTrainButton_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(ModifyTrainButton);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(TrainNumberInput);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(TrainDestinationInput);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(TrainDepartureInput);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(TrainTypeInput);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(TrainSeatsInput);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(TrainFreeSeatsInput);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(SearchQueryInput);
            panel1.Controls.Add(SearchTrainButton);
            panel1.Controls.Add(AddTrainButton);
            panel1.Controls.Add(DeleteTrainButton);
            panel1.Location = new Point(13, 308);
            panel1.Name = "panel1";
            panel1.Size = new Size(860, 259);
            panel1.TabIndex = 2;
            // 
            // ModifyTrainButton
            // 
            ModifyTrainButton.Location = new Point(524, 71);
            ModifyTrainButton.Name = "ModifyTrainButton";
            ModifyTrainButton.Size = new Size(171, 33);
            ModifyTrainButton.TabIndex = 17;
            ModifyTrainButton.Text = "Изменить выбранный";
            ModifyTrainButton.UseVisualStyleBackColor = true;
            ModifyTrainButton.Click += ModifyTrainButton_Click;
            // 
            // TrainFrom
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(901, 598);
            Controls.Add(lblTitle);
            Controls.Add(TrainTable);
            Controls.Add(panel1);
            Controls.Add(StatusLabel);
            Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4);
            Name = "TrainFrom";
            Text = "Вокзал — MVC";
            ((System.ComponentModel.ISupportInitialize)TrainSeatsInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)TrainFreeSeatsInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)TrainTable).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lblTitle;
        private Label StatusLabel;
        private TextBox TrainNumberInput;
        private TextBox TrainDestinationInput;
        private TextBox TrainDepartureInput;
        private ComboBox TrainTypeInput;
        private NumericUpDown TrainSeatsInput;
        private NumericUpDown TrainFreeSeatsInput;
        private TextBox SearchQueryInput;
        private DataGridView TrainTable;
        private Button AddTrainButton;
        private Button DeleteTrainButton;
        private Button SearchTrainButton;
        private Panel panel1;
        private Button ModifyTrainButton;
    }
}