using MySql.Data.MySqlClient;

namespace LoginForm
{
    public partial class Form1 : Form
    {
        string connectionString ="Server=localhost;" +"Port=3306;" +"Database=quanlydangnhap;" +"Uid=root;" +"Pwd=123456789;" +"CharSet=utf8mb4;";
        public Form1()
        {
            InitializeComponent();
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider.SetError(
                    txtUsername, "Vui lòng nhập tên đăng nhập");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider.SetError(
                    txtPassword, "Vui lòng nhập mật khẩu");
                isValid = false;
            }

            if (!isValid)
                return;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            try
            {
                using (MySqlConnection conn =
                       new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                SELECT COUNT(*)
                FROM taikhoan
                WHERE tendangnhap = @username
                  AND matkhau = @password";

                    using (MySqlCommand cmd =
                           new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@username", username);

                        cmd.Parameters.AddWithValue(
                            "@password", password);

                        int result = Convert.ToInt32(
                            cmd.ExecuteScalar());

                        if (result > 0)
                        {
                            MessageBox.Show(
                                "Đăng nhập thành công!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(
                                "Tên đăng nhập hoặc mật khẩu không đúng!",
                                "Đăng nhập thất bại",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể kết nối hoặc truy vấn MySQL!\n"
                    + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
