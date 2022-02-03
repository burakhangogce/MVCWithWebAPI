using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebAPIWithWindowsForm
{
    public partial class Form1 : Form
    {
        private string url = "http://localhost:31527/api/";
        private int selectedID = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            await DataGridViewFill();
            CmbGenderFill();
        }

        private async Task DataGridViewFill()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var users = await httpClient.GetFromJsonAsync<List<UserDetailDto>>(new Uri(url + "Users/GetList"));
                dataGridView1.DataSource = users;
            }
        }

        void ClearForm()
        {
            txtAdress.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtMail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtUserName.Text = string.Empty;
            cmbGender.SelectedValue = 0;
            dtpDateOfBirth.Value = DateTime.Now;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void CmbGenderFill()
        {
            List<Gender> genders = new List<Gender>();
            genders.Add(new Gender() { Id = 1, GenderName = "Erkek" });
            genders.Add(new Gender() { Id = 2, GenderName = "Kadın" });
            cmbGender.DataSource = genders;
            cmbGender.DisplayMember = "GenderName";
            cmbGender.ValueMember = "Id";
        }

        private class Gender
        {
            public int Id { get; set; }
            public string GenderName { get; set; }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using(HttpClient httpClient = new HttpClient())
            {
                UserAddDto userAddDto = new UserAddDto()
                {
                    FirstName = txtFirstName.Text,
                    Adress = txtAdress.Text,
                    DateOfBirth = Convert.ToDateTime(dtpDateOfBirth.Text),
                    Email = txtMail.Text,
                    Gender = cmbGender.Text == "Erkek" ? true : false,
                    LastName = txtLastName.Text,
                    Password = txtPassword.Text,
                    UserName = txtUserName.Text
                };
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(url + "Users/Add", userAddDto);
                if (response.IsSuccessStatusCode)
                {
                    await DataGridViewFill();
                    MessageBox.Show("Ekleme işlemi başarılı!");
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Ekleme işlemi başarısız!");
                }
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                UserUpdateDto userUpdateAddDto = new UserUpdateDto()
                {
                    Id = selectedID,
                    FirstName = txtFirstName.Text,
                    Adress = txtAdress.Text,
                    DateOfBirth = Convert.ToDateTime(dtpDateOfBirth.Text),
                    Email = txtMail.Text,
                    Gender = cmbGender.Text == "Erkek" ? true : false,
                    LastName = txtLastName.Text,
                    Password = txtPassword.Text,
                    UserName = txtUserName.Text
                };
                HttpResponseMessage response = await httpClient.PutAsJsonAsync(url + "Users/Update", userUpdateAddDto);
                if (response.IsSuccessStatusCode)
                {
                    await DataGridViewFill();
                    MessageBox.Show("Düzenleme işlemi başarılı!");
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Düzenleme işlemi başarısız!");
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                
                HttpResponseMessage response = await httpClient.DeleteAsync(url + "Users/Delete/" + selectedID);
                if (response.IsSuccessStatusCode)
                {
                    await DataGridViewFill();
                    MessageBox.Show("Silme işlemi başarılı!");
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Silme işlemi başarısız!");
                }
            }
        }

        private async void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            selectedID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            using (HttpClient httpClient = new HttpClient())
            {
                var user = await httpClient.GetFromJsonAsync<UserDto>(new Uri(url + "Users/GetById/" + selectedID));

                txtAdress.Text = user.Adress;
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                txtUserName.Text = user.UserName;
                txtMail.Text = user.Email;
                txtUserName.Text = user.Adress;
                cmbGender.SelectedValue = user.Gender == true ? 1 : 2;
                dtpDateOfBirth.Value = user.DateOfBirth;
            }
            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        
    }
}
