namespace Practice2
{
    public partial class Form1 : Form
    {
        // 재사용 가능한 난수생성기
        private readonly Random rd = new Random();

        // 점수 저장
        private int score = 0;

        // 놓친 횟수 및 게임오버 관리
        private int missCount = 0;
        private const int MaxMisses = 20;
        private bool isGameOver = false;

        // 난이도 조절: 클릭 시 버튼 크기 비율(10% 감소)
        private const double ShrinkFactor = 0.9;
        private const int MinButtonWidth = 30;
        private const int MinButtonHeight = 20;

        // 초기 버튼 상태 저장(다시 시작 시 복원용)
        private Size initialRunButtonSize;
        private Point initialRunButtonLocation;

        public Form1()
        {
            InitializeComponent();
            runButton.Click += runButton_Click;

            // 초기 버튼 크기/위치 저장
            initialRunButtonSize = runButton.Size;
            initialRunButtonLocation = runButton.Location;

            // restart 버튼은 Designer에서 Visible=false로 초기화했지만 안전하게 숨김 처리
            restartButton.Visible = false;
        }

        private void runButton_MouseEnter(object sender, EventArgs e)
        {
            if (isGameOver)
                return; // 게임 종료 후에는 아무 동작 안함

            // 놓침 카운트 증가 및 점수 감점
            missCount++;
            score -= 10;

            // 게임오버 판정
            if (missCount >= MaxMisses)
            {
                isGameOver = true;
                DisableAllButtons();
                // Game Over 메시지 출력 (최종 점수 포함)
                MessageBox.Show(this, $"최종 점수: {score}", "Game Over");

                // 다시 시작 버튼 보이게 하고 중앙에 배치
                restartButton.Visible = true;
                restartButton.BringToFront();
                restartButton.Location = new Point(
                    Math.Max(0, (this.ClientSize.Width - restartButton.Width) / 2),
                    Math.Max(0, (this.ClientSize.Height - restartButton.Height) / 2)
                );

                UpdateTitle(); // 제목 갱신
                return;
            }

            // 1. 가용영역계산(버튼이 폼 경계 밖으로 나가지 않도록 버튼 크기만큼 제외)
            int maxX = Math.Max(0, this.ClientSize.Width - runButton.Width);
            int maxY = Math.Max(0, this.ClientSize.Height - runButton.Height);

            // 마우스의 현재 위치를 클라이언트 좌표계로 변환
            Point cursorClient = this.PointToClient(Cursor.Position);

            // 2. 랜덤좌표추출(버튼이 마우스 아래로 이동하지 않도록 시도)
            int nextX = 0, nextY = 0;
            const int maxAttempts = 20;
            int attempts = 0;
            Rectangle candidate;
            do
            {
                nextX = (maxX == 0) ? 0 : rd.Next(0, maxX + 1);
                nextY = (maxY == 0) ? 0 : rd.Next(0, maxY + 1);
                candidate = new Rectangle(nextX, nextY, runButton.Width, runButton.Height);
                attempts++;
            }
            while (candidate.Contains(cursorClient) && attempts < maxAttempts);

            // 3. 위치할당
            runButton.Location = new Point(nextX, nextY);

            // 4. 폼 제목 업데이트(점수 + 좌표 + 놓친횟수)
            UpdateTitle(nextX, nextY);
        }

        // 버튼 클릭 시 메시지 박스 표시 및 점수 추가, 버튼 크기 축소 처리
        private void runButton_Click(object sender, EventArgs e)
        {
            if (isGameOver)
                return; // 게임오버면 클릭 처리 안함

            // 점수 추가
            score += 100;

            // 버튼 크기 축소 (최소 크기 보장)
            int newWidth = Math.Max(MinButtonWidth, (int)Math.Round(runButton.Width * ShrinkFactor));
            int newHeight = Math.Max(MinButtonHeight, (int)Math.Round(runButton.Height * ShrinkFactor));
            runButton.Size = new Size(newWidth, newHeight);

            // 축소 후에도 버튼이 폼 안에 있도록 위치 보정
            int maxX = Math.Max(0, this.ClientSize.Width - runButton.Width);
            int maxY = Math.Max(0, this.ClientSize.Height - runButton.Height);
            int correctedX = Math.Min(runButton.Location.X, maxX);
            int correctedY = Math.Min(runButton.Location.Y, maxY);
            runButton.Location = new Point(Math.Max(0, correctedX), Math.Max(0, correctedY));

            // 메시지박스의 캡션에 현재 점수 표시
            MessageBox.Show(this, "잡았다~!", $"점수: {score}");

            // 제목 갱신
            UpdateTitle(runButton.Location.X, runButton.Location.Y);
        }

        // 다시 시작 버튼 클릭 핸들러
        private void restartButton_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        // 게임 상태 초기화
        private void ResetGame()
        {
            // 상태 초기화
            score = 0;
            missCount = 0;
            isGameOver = false;

            // 버튼 원상복구
            runButton.Size = initialRunButtonSize;
            runButton.Location = initialRunButtonLocation;

            // 모든 버튼 활성화(다시 시작 버튼 포함)
            EnableAllButtons();

            // 다시 시작 버튼 숨김
            restartButton.Visible = false;

            // 제목 갱신
            UpdateTitle(runButton.Location.X, runButton.Location.Y);
        }

        // 모든 버튼 비활성화 (게임오버 시 호출) — 다시 시작 버튼은 활성 상태 유지
        private void DisableAllButtons()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Button b && b != restartButton)
                {
                    b.Enabled = false;
                }
            }
        }

        // 모든 버튼 활성화 (다시 시작 시 호출)
        private void EnableAllButtons()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Button b)
                {
                    b.Enabled = true;
                }
            }
        }

        // 제목 업데이트 헬퍼(좌표는 선택적)
        private void UpdateTitle(int? x = null, int? y = null)
        {
            string baseText = $"점수: {score}   놓친횟수: {missCount}/{MaxMisses}";
            if (x.HasValue && y.HasValue)
            {
                this.Text = $"{baseText}   버튼위치: ({x.Value}, {y.Value})";
            }
            else
            {
                this.Text = baseText;
            }
        }
    }
}
