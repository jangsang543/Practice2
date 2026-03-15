namespace Practice2
{
    public partial class Form1 : Form
    {
        // 재사용 가능한 난수생성기
        private readonly Random rd = new Random();

        // 점수 저장
        private int score = 0;

        // 난이도 조절: 클릭 시 버튼 크기 비율(10% 감소)
        private const double ShrinkFactor = 0.9;
        private const int MinButtonWidth = 30;
        private const int MinButtonHeight = 20;

        public Form1()
        {
            InitializeComponent();
            runButton.Click += runButton_Click;
        }

        private void runButton_MouseEnter(object sender, EventArgs e)
        {
            // 도망칠 때 점수 감점
            score -= 10;

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

            // 4. 폼 제목 업데이트(점수 + 좌표)
            UpdateTitle(nextX, nextY);
        }

        // 버튼 클릭 시 메시지 박스 표시 및 점수 추가, 버튼 크기 축소 처리
        private void runButton_Click(object sender, EventArgs e)
        {
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

        // 제목 업데이트 헬퍼(좌표는 선택적)
        private void UpdateTitle(int? x = null, int? y = null)
        {
            if (x.HasValue && y.HasValue)
            {
                this.Text = $"점수: {score}   버튼위치: ({x.Value}, {y.Value})";
            }
            else
            {
                this.Text = $"점수: {score}";
            }
        }
    }
}
