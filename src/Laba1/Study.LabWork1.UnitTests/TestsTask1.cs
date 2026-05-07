using Study.LabWork1.Features;
namespace Study.LabWork1.UnitTests;

[TestFixture]
class TestTask1
{
    [Test]
    public void Addition_ShouldSumChannels()
    {
        var p1 = new RgbaPixel(1, 220, 21, 0.5f);
        var p2 = new RgbaPixel(150, 25, 12, 0.75f);

        RgbaPixel result = p1 + p2;

        Assert.Multiple(() =>
        {
            Assert.That(result.Red, Is.EqualTo(151));
            Assert.That(result.Green, Is.EqualTo(245));
            Assert.That(result.Blue, Is.EqualTo(33));
            Assert.That(result.Alpha, Is.EqualTo(1f));
        });

    }


    [Test]
    public void Addition_ShouldClampAt255()
    {
        var p1 = new RgbaPixel(150, 150, 150, 0.5f);
        var p2 = new RgbaPixel(150, 150, 150, 0.5f);

        RgbaPixel result = p1 + p2;

        Assert.Multiple(() =>
        {
            Assert.That(result.Red, Is.EqualTo(255));
            Assert.That(result.Green, Is.EqualTo(255));
            Assert.That(result.Blue, Is.EqualTo(255));
            Assert.That(result.Alpha, Is.EqualTo(1f));
        });
    }

    [Test]
    public void Subtraction_ShouldSubtractChannels()
    {
        var p1 = new RgbaPixel(30, 120, 0, 0.1f);
        var p2 = new RgbaPixel(12, 28, 0, 0.5f);

        RgbaPixel result = p1 - p2;

        Assert.Multiple(() =>
        {
            Assert.That(result.Red, Is.EqualTo(30 - 12));
            Assert.That(result.Green, Is.EqualTo(120 - 28));
            Assert.That(result.Blue, Is.EqualTo(0));
            Assert.That(result.Alpha, Is.EqualTo(0));
        });
    }

    [Test]
    public void Subtraction_ShouldClampAt0WhenUnderflow()
    {
        var p1 = new RgbaPixel(0, 12, 0, 0.1f);
        var p2 = new RgbaPixel(12, 28, 255, 0.5f);

        RgbaPixel result = p1 - p2;

        Assert.Multiple(() =>
        {
            Assert.That(result.Red, Is.EqualTo(0));
            Assert.That(result.Green, Is.EqualTo(0));
            Assert.That(result.Blue, Is.EqualTo(0));
            Assert.That(result.Alpha, Is.EqualTo(0));
        }

        );

    }

    [Test]
    public void Multiply_ShouldMultiplyChannels()
    {
        var p1 = new RgbaPixel(25, 2, 12, 0.4f);
        RgbaPixel result = 2 * p1;

        Assert.Multiple(() =>
        {
            Assert.That(result.Red, Is.EqualTo(50));
            Assert.That(result.Green, Is.EqualTo(4));
            Assert.That(result.Blue, Is.EqualTo(24));
            Assert.That(result.Alpha, Is.EqualTo(0.8f));
        });
    }

    [Test]
    public void Division_ShouldDivideChannels()
    {
        var p1 = new RgbaPixel(25, 45, 35, 0.5f);

        RgbaPixel result = p1 / 5;

        Assert.Multiple(() =>
        {
            Assert.That(result.Red, Is.EqualTo(5));
            Assert.That(result.Green, Is.EqualTo(9));
            Assert.That(result.Blue, Is.EqualTo(7));
            Assert.That(result.Alpha, Is.EqualTo(0.1f));
        });
    }

    [Test]
    public void Division_ByZero_ShouldThrowException()
    {
        var p1 = new RgbaPixel(25, 45, 35, 0.5f);

        Assert.Throws<DivideByZeroException>(() => { RgbaPixel _ = p1 / 0; });
    }

    [Test]
    public void Equality_SamePixels_ShouldBeEqual()
    {
        RgbaPixel p1 = new(100, 50, 25, 0.5f);
        RgbaPixel p2 = new(100, 50, 25, 0.5f);

        Assert.That(p1 == p2, Is.True);
    }

    [Test]
    public void Inequality_DifferentPixels_ShouldNotBeEqual()
    {
        var p1 = new RgbaPixel(30, 120, 0, 0.1f);
        var p2 = new RgbaPixel(12, 28, 0, 0.5f);

        Assert.That(p1 != p2, Is.True);
    }

    [Test]
    public void Constructor_ShouldSetChannelsCorrectly()
    {
        RgbaPixel p1 = new(100, 50, 25, 0.5f);

        Assert.Multiple(() =>
        {
            Assert.That(p1.Red, Is.EqualTo(100));
            Assert.That(p1.Green, Is.EqualTo(50));
            Assert.That(p1.Blue, Is.EqualTo(25));
            Assert.That(p1.Alpha, Is.EqualTo(0.5f));
        });
    }
}
