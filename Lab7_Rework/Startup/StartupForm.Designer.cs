namespace Lab7_Rework
{
    partial class StartupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            ConsoleButton = new Button();
            GuiButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 9);
            label1.Name = "label1";
            label1.Size = new Size(254, 25);
            label1.TabIndex = 0;
            label1.Text = "Выберите режим работы:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ConsoleButton
            // 
            ConsoleButton.FlatStyle = FlatStyle.Flat;
            ConsoleButton.Location = new Point(18, 56);
            ConsoleButton.Name = "ConsoleButton";
            ConsoleButton.Size = new Size(116, 36);
            ConsoleButton.TabIndex = 1;
            ConsoleButton.Text = "Консоль";
            ConsoleButton.UseVisualStyleBackColor = true;
            ConsoleButton.Click += ConsoleButton_Click;
            // 
            // GuiButton
            // 
            GuiButton.FlatStyle = FlatStyle.Flat;
            GuiButton.Location = new Point(156, 56);
            GuiButton.Name = "GuiButton";
            GuiButton.Size = new Size(116, 36);
            GuiButton.TabIndex = 2;
            GuiButton.Text = "GUI";
            GuiButton.UseVisualStyleBackColor = true;
            GuiButton.Click += this.GuiButton_Click;
            // 
            // StartupForm
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(284, 108);
            Controls.Add(GuiButton);
            Controls.Add(ConsoleButton);
            Controls.Add(label1);
            Font = new Font("Cascadia Code", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(5);
            Name = "StartupForm";
            Text = "MVC - Вокзал";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button ConsoleButton;
        private Button GuiButton;
    }
}