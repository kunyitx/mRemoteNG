using System.Threading;
using mRemoteNG.UI;
using NUnit.Framework;

namespace mRemoteNGTests.UI.Controls;

[TestFixture]
[Apartment(ApartmentState.STA)]
public class TextBoxExtensionsTests
{
    [SetUp]
    public void Setup()
    {
        _textBoxExtensionsTestForm = new TextBoxExtensionsTestForm();
        _textBoxExtensionsTestForm.Show();
    }

    [TearDown]
    public void Teardown()
    {
        _textBoxExtensionsTestForm.Dispose();
        while (_textBoxExtensionsTestForm.Disposing)
        {
        }

        _textBoxExtensionsTestForm = null;
    }

    private TextBoxExtensionsTestForm _textBoxExtensionsTestForm;

    [Test]
    public void SetCueBannerSetsTheBannerText()
    {
        const string text = "Type Here";
        var textBox = _textBoxExtensionsTestForm.textBox1;
        Assert.That(textBox.SetCueBannerText(text), Is.True);
    }

    [Test]
    public void GetCueBannerReturnsCorrectValue()
    {
        const string text = "Type Here";
        var textBox = _textBoxExtensionsTestForm.textBox1;
        textBox.SetCueBannerText(text);
        Assert.That(textBox.GetCueBannerText(), Is.EqualTo(text));
    }
}