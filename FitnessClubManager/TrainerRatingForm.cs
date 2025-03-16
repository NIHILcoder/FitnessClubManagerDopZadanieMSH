using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FitnessClubManager
{
    public partial class TrainerRatingForm : Form
    {
        private int trainerId;
        private string trainerName;
        private int currentRating = 0;
        private PictureBox[] stars;

        public TrainerRatingForm(int trainerId, string trainerName)
        {
            InitializeComponent();
            this.trainerId = trainerId;
            this.trainerName = trainerName;
        }

        private void TrainerRatingForm_Load(object sender, EventArgs e)
        {
            // Настраиваем форму
            this.Text = $"Оценка тренера: {trainerName}";
            lblTrainerName.Text = trainerName;

            // Инициализируем звезды рейтинга
            stars = new PictureBox[5] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5 };

            // Добавляем обработчики событий для каждой звезды
            for (int i = 0; i < stars.Length; i++)
            {
                int starIndex = i;
                stars[i].Click += (s, ev) => SetRating(starIndex + 1);
                stars[i].MouseEnter += (s, ev) => HighlightStars(starIndex + 1);
                stars[i].MouseLeave += (s, ev) => HighlightStars(currentRating);
            }

            // Загружаем текущий рейтинг, если есть
            try
            {
                double rating = DatabaseManager.GetTrainerRating(trainerId);
                lblCurrentRating.Text = $"Текущий рейтинг: {rating:F1} из 5.0";
            }
            catch (Exception ex)
            {
                lblCurrentRating.Text = "Текущий рейтинг: нет оценок";
                Console.WriteLine($"Ошибка при загрузке рейтинга: {ex.Message}");
            }

            // Инициализируем выпадающий список категорий
            comboRatingCategory.Items.Add("Общая оценка");
            comboRatingCategory.Items.Add("Профессионализм");
            comboRatingCategory.Items.Add("Пунктуальность");
            comboRatingCategory.Items.Add("Индивидуальный подход");
            comboRatingCategory.Items.Add("Результативность");
            comboRatingCategory.SelectedIndex = 0;

            // Устанавливаем начальный рейтинг
            SetRating(0);
        }

        // Метод для установки выбранного рейтинга
        private void SetRating(int rating)
        {
            currentRating = rating;
            HighlightStars(rating);

            // Обновляем текст с выбранным рейтингом
            if (rating > 0)
            {
                lblSelectedRating.Text = $"Выбранная оценка: {rating} из 5";
                btnSave.Enabled = true;
            }
            else
            {
                lblSelectedRating.Text = "Выберите оценку";
                btnSave.Enabled = false;
            }
        }

        // Метод для подсветки звезд при наведении
        private void HighlightStars(int count)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                if (i < count)
                {
                    // Закрашенная звезда
                    stars[i].Image = global::FitnessClubManager.Properties.Resources.star_filled;
                }
                else
                {
                    // Пустая звезда
                    stars[i].Image = global::FitnessClubManager.Properties.Resources.Star_empty;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentRating <= 0)
            {
                MessageBox.Show("Выберите оценку!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                string category = comboRatingCategory.SelectedItem.ToString();
                string comment = txtComment.Text;

                // Сохраняем рейтинг в базу данных
                bool result = DatabaseManager.AddTrainerRating(
                    trainerId,
                    currentRating,
                    category,
                    comment
                );

                if (result)
                {
                    MessageBox.Show("Оценка успешно сохранена!", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить оценку!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении оценки: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}