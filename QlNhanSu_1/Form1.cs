using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace QlNhanSu_1
{
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();

            Load += new EventHandler(Form_Load);

            btnThem.Click += new EventHandler(Them);
            btnSua.Click += new EventHandler(Sua);
            btnXoa.Click += new EventHandler(Xoa);
        }

        private void Xoa(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Sua(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #region xác định tính hợp lệ của dữ liệu
        public bool isNumber(string value)
        {
            bool ktra;
            float result;
            ktra = float.TryParse(value, out result);
            return ktra;
        }
        #endregion

        private void Them(object sender, EventArgs e)
        {
            string sqlCommand_1 = string.Format("SELECT COUNT(*) FROM DSNV WHERE maNV = '{0}'", txtMaNV.Text);
            if (DataProvider.checkData(sqlCommand_1) == 0 && isNumber(txtHSL.Text))
            {
                string sqlCommand = "INSERT INTO DSNV(HoTen, NgaySinh, GioiTinh, SoDT, HeSoLuong, MaPhong, MaChucVu) " +
                "VALUES(@ht, @ns, @gt, @soDT, @hsLuong, @maPhong, @maChucVu)";

                string[] names = { "@ht", "@ns", "@gt", "@soDT", "@hsLuong", "@maPhong", "@maChucVu" };

                bool gender = true;
                if (rdNu.Checked == true)
                {
                    gender = false;
                }

                object[] values = { txtHoTen.Text, dtpNgaySinh.Value, gender, txtSoDT.Text, txtHSL.Text, cboTenPhong.SelectedValue.ToString(), cboChucVu.SelectedValue.ToString() };

                DataProvider.openConnection();
                DataProvider.updateData(sqlCommand, values, names);
                DataProvider.closeConnection();
                loadDSNV();
            }
        }

        #region
        public void loadPhongBan()
        {
            string sqlCommand = "SELECT * FROM DMPHONG";

            //Lấy dữ liệu từ Dataset đổ lên ComboBox
            cboTenPhong.DataSource = DataProvider.getTable(sqlCommand);
            cboTenPhong.DisplayMember = "TenPhong";
            cboTenPhong.ValueMember = "MaPhong";
        }

        public void loadChucVu()
        {
            string sqlCommand = "SELECT * FROM CHUCVU ";

            //Lấy dữ liệu từ Dataset đổ lên ComboBox
            cboChucVu.DataSource = DataProvider.getTable(sqlCommand);
            cboChucVu.DisplayMember = "TenChucVu";
            cboChucVu.ValueMember = "MaChucVu";
        }

        public void loadDSNV()
        {
            string sqlCommand = "SELECT * FROM DSNV";

            //Lấy dữ liệu từ Dataset đổ lên DataGridView
            dataGridView.DataSource = DataProvider.getTable(sqlCommand);
        }
        #endregion

        private void Form_Load(object sender, EventArgs e)
        {
            DataProvider.openConnection();
            loadChucVu();
            loadPhongBan();
            loadDSNV();
            DataProvider.closeConnection();
        }

    }
}
