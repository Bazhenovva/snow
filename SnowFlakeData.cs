namespace snow
{
    /// <summary>
    /// инфа о снежинке: константы позиций и стркутура снежинки
    /// </summary>
    public static class SnowFlakeData
    {
        /// <summary>
        /// верхняя граница появления снежинки
        /// </summary>
        public const int LessPositionY = -100;

        /// <summary>
        /// нижняя граница начальной позиции
        /// </summary>
        public const int MorePositionY = 0;
    }

    /// <summary>
    /// представляет одну снежинку в анимации
    /// </summary>
    public struct SnowFlake
    {
        /// <summary>
        /// горизонтальная координата
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// вертикальная координата
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// скорость падения
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// масштаб относительно исходного изображения
        /// </summary>
        public float Scale { get; set; }
    }
}
