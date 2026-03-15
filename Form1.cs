namespace Practice2
{
    public partial class Form1 : Form
    {
        // 재사용 가능한 난수생성기를 필드로 이동 (매번 생성하면 시드가 같아 같은 위치가 나올 수 있음)
        private readonly Random rd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void runButton_MouseEnter(object sender, EventArgs e)
        {
            // 1. 가용영역계산(버튼이 폼 경계 밖으로 나가지 않도록 버튼 크기만큼 제외)
            // ClientSize는 타이틀바와 테두리를 제외한 실제 도화지 영역임
            int maxX = Math.Max(0, this.ClientSize.Width - runButton.Width);
            int maxY = Math.Max(0, this.ClientSize.Height - runButton.Height);

            // 2. 랜덤좌표추출(0 ~ 최대가용치 사이). Next의 상한은 exclusive 이므로 +1 사용
            int nextX = (maxX == 0) ? 0 : rd.Next(0, maxX + 1);
            int nextY = (maxY == 0) ? 0 : rd.Next(0, maxY + 1);

            // 3. 위치할당
            runButton.Location = new Point(nextX, nextY);

            // 4. 시각적 피드백(폼 제목 표시줄에 좌표 출력)
            this.Text = $"버튼위치: ({nextX}, {nextY})";
        }
    }
}
