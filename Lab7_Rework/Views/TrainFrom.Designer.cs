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
            lblStatus = new Label();
            tbTrainNumber = new TextBox();
            tbDestination = new TextBox();
            tbTime = new TextBox();
            cmbTrainType = new ComboBox();
            nudSeatsTotal = new NumericUpDown();
            nudSeatsAvailable = new NumericUpDown();
            tbSearch = new TextBox();
            dataGridView1 = new DataGridView();
            btnAdd = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            btnSearch = new Button();
            panel1 = new Panel();
            btnEdit = new Button();
            ((System.ComponentModel.ISupportInitialize)nudSeatsTotal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSeatsAvailable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
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
            label7.Location = new Point(439, 279);
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
            // lblStatus
            // 
            lblStatus.ForeColor = Color.DimGray;
            lblStatus.Location = new Point(13, 648);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(860, 22);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Готово.";
            // 
            // tbTrainNumber
            // 
            tbTrainNumber.Location = new Point(212, 9);
            tbTrainNumber.Name = "tbTrainNumber";
            tbTrainNumber.Size = new Size(225, 26);
            tbTrainNumber.TabIndex = 5;
            // 
            // tbDestination
            // 
            tbDestination.Location = new Point(212, 48);
            tbDestination.Name = "tbDestination";
            tbDestination.Size = new Size(225, 26);
            tbDestination.TabIndex = 6;
            // 
            // tbTime
            // 
            tbTime.Location = new Point(212, 92);
            tbTime.Name = "tbTime";
            tbTime.Size = new Size(227, 26);
            tbTime.TabIndex = 7;
            // 
            // cmbTrainType
            // 
            cmbTrainType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTrainType.Location = new Point(212, 133);
            cmbTrainType.Name = "cmbTrainType";
            cmbTrainType.Size = new Size(225, 28);
            cmbTrainType.TabIndex = 8;
            // 
            // nudSeatsTotal
            // 
            nudSeatsTotal.Location = new Point(168, 175);
            nudSeatsTotal.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            nudSeatsTotal.Name = "nudSeatsTotal";
            nudSeatsTotal.Size = new Size(120, 26);
            nudSeatsTotal.TabIndex = 9;
            // 
            // nudSeatsAvailable
            // 
            nudSeatsAvailable.Location = new Point(168, 217);
            nudSeatsAvailable.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            nudSeatsAvailable.Name = "nudSeatsAvailable";
            nudSeatsAvailable.Size = new Size(120, 26);
            nudSeatsAvailable.TabIndex = 10;
            // 
            // tbSearch
            // 
            tbSearch.Location = new Point(518, 273);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(225, 26);
            tbSearch.TabIndex = 11;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.Location = new Point(13, 45);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(860, 250);
            dataGridView1.TabIndex = 0;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(524, 9);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(130, 35);
            btnAdd.TabIndex = 13;
            btnAdd.Text = "Добавить";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(524, 126);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(171, 35);
            btnDelete.TabIndex = 15;
            btnDelete.Text = "Удалить выбранный";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(18, 273);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(270, 35);
            btnClear.TabIndex = 16;
            btnClear.Text = "Очистить ввод";
            btnClear.Click += btnClear_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(757, 273);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 30);
            btnSearch.TabIndex = 12;
            btnSearch.Text = "Найти";
            btnSearch.Click += btnSearch_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnEdit);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(tbTrainNumber);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(tbDestination);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(tbTime);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(cmbTrainType);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(nudSeatsTotal);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(nudSeatsAvailable);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(tbSearch);
            panel1.Controls.Add(btnSearch);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(btnDelete);
            panel1.Controls.Add(btnClear);
            panel1.Location = new Point(13, 308);
            panel1.Name = "panel1";
            panel1.Size = new Size(860, 330);
            panel1.TabIndex = 2;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(524, 71);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(171, 33);
            btnEdit.TabIndex = 17;
            btnEdit.Text = "Изменить выбранный";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // TrainFrom
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(931, 683);
            Controls.Add(lblTitle);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Controls.Add(lblStatus);
            Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4);
            Name = "TrainFrom";
            Text = "Вокзал — MVC";
            ((System.ComponentModel.ISupportInitialize)nudSeatsTotal).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSeatsAvailable).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
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
        private Label lblStatus;
        private TextBox tbTrainNumber;
        private TextBox tbDestination;
        private TextBox tbTime;
        private ComboBox cmbTrainType;
        private NumericUpDown nudSeatsTotal;
        private NumericUpDown nudSeatsAvailable;
        private TextBox tbSearch;
        private DataGridView dataGridView1;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnClear;
        private Button btnSearch;
        private Panel panel1;
        private Button btnEdit;
    }
}