namespace Miniventure.Levels.Generation;

public class LevelNoise
{
    private readonly int width;
    private readonly int height;
    private readonly double[,] values;

    public double this[int x, int y] => values[x & width - 1, y & height - 1];

    public LevelNoise(int width, int height, int featureSize)
    {
        this.width = width;
        this.height = height;

        values = Generate(width, height, featureSize);
    }

    private double[,] Generate(int width, int height, int featureSize)
    {
        Random random = new();
        double[,] result = new double[width, height];

        void SetSample(int x, int y, double value)
        {
            result[x & width - 1, y & height - 1] = value;
        }

        double Sample(int x, int y) => result[x & width - 1, y & height - 1];

        for (int y = 0; y < height; y += featureSize)
        {
            for (int x = 0; x < width; x += featureSize)
            {
                SetSample(x, y, random.NextFloat() * 2 - 1);
            }
        }

        int stepSize = featureSize;
        double scale = 1.0 / width;
        double scaleMod = 1;

        do
        {
            int halfStep = stepSize / 2;

            for (int y = 0; y < height; y += stepSize)
            {
                for (int x = 0; x < width; x += stepSize)
                {
                    double a = Sample(x, y);
                    double b = Sample(x + stepSize, y);
                    double c = Sample(x, y + stepSize);
                    double d = Sample(x + stepSize, y + stepSize);
                    double e = (a + b + c + d) / 4.0 + (random.NextFloat() * 2 - 1) * stepSize * scale;

                    SetSample(x + halfStep, y + halfStep, e);
                }
            }

            for (int y = 0; y < height; y += stepSize)
            {
                for (int x = 0; x < width; x += stepSize)
                {
                    double a = Sample(x, y);
                    double b = Sample(x + stepSize, y);
                    double c = Sample(x, y + stepSize);
                    double d = Sample(x + halfStep, y + halfStep);
                    double e = Sample(x + halfStep, y - halfStep);
                    double f = Sample(x - halfStep, y + halfStep);
                    double H = (a + b + d + e) / 4.0 + (random.NextFloat() * 2 - 1) * stepSize * scale * 0.5;
                    double g = (a + c + d + f) / 4.0 + (random.NextFloat() * 2 - 1) * stepSize * scale * 0.5;

                    SetSample(x + halfStep, y, H);
                    SetSample(x, y + halfStep, g);
                }
            }

            stepSize /= 2;
            scale *= scaleMod + 0.8;
            scaleMod *= 0.3;
        } while (stepSize > 1);

        return result;
    }
}
