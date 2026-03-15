namespace Practice2
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
            runButton = new Button();
            restartButton = new Button();
            SuspendLayout();
            // 
            // runButton
            // 
            runButton.BackColor = SystemColors.ActiveCaption;
            runButton.Font = new Font("휴먼매직체", 26F, FontStyle.Bold, GraphicsUnit.Point, 129);
            runButton.Location = new Point(404, 281);
            runButton.Name = "runButton";
            runButton.Size = new Size(363, 125);
            runButton.TabIndex = 0;
            runButton.Text = "나를 잡아라";
            runButton.UseVisualStyleBackColor = false;
            runButton.Click += runButton_Click;
            runButton.MouseEnter += runButton_MouseEnter;
            // 
            // restartButton
            // 
            restartButton.BackColor = SystemColors.MenuHighlight;
            restartButton.Font = new Font("휴먼모음T", 26F, FontStyle.Bold, GraphicsUnit.Point, 129);
            restartButton.Location = new Point(12, 12);
            restartButton.Name = "restartButton";
            restartButton.Size = new Size(508, 159);
            restartButton.TabIndex = 1;
            restartButton.Text = "다시 시작";
            restartButton.UseVisualStyleBackColor = false;
            restartButton.Click += restartButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1181, 712);
            Controls.Add(restartButton);
            Controls.Add(runButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button runButton;
        private Button restartButton;
    }
}
