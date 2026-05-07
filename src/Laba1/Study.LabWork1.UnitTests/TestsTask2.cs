using Study.LabWork1.Features;
namespace Study.LabWork1.UnitTests;

//MemRepo_SaveOrder_ShouldAddToList
// MemRepo_GetOrderById_ShouldReturnCorrectOrder
// MemRepo_GetOrderById_NotFound_ShouldReturnNull
// MemRepo_GetOrders_ShouldReturnAllOrders
// CsvRepo_SaveOrder_ShouldWriteToFile
// CsvRepo_GetOrderById_ShouldReturnCorrectOrder
// CsvRepo_GetOrders_ShouldReturnAllOrders
// Client_Request_ShouldSaveOrderToRepo
[TestFixture]
class TestTask2
{
    [Test]
    public void MemoRepo_SaveOrder_ShouldAddToList()
    {
        MemOrderSaver memRepo = new();
        CSVOrderRepo csvRepo = new("orders_tests.csv");
        Client client_test = new("Олег Оскорбин", memRepo, csvRepo);

        client_test.Request("ковбойские сапоги");
        client_test.Request("ковбойская шляпа");

        Assert.That(memRepo.GetOrders().Count, Is.EqualTo(2));
    }

    [Test]
    public void MemRepo_GetOrderById_ShouldReturnCorrectOrder()
    {
        MemOrderSaver memRepo = new();
        CSVOrderRepo csvRepo = new("orders_tests.csv");
        Client client_test = new("Мужик", memRepo, csvRepo);

        client_test.Request("Шляпа");
        List<Order> orders = memRepo.GetOrders();
        Order manreq = orders[0];

        Order request = memRepo.GetOrderById(manreq.Id);
        Assert.Multiple(() =>
        {
            Assert.That(request.Id, Is.EqualTo(manreq.Id));
            Assert.That(request.ClientName, Is.EqualTo(manreq.ClientName));
            Assert.That(request.Garment, Is.EqualTo(manreq.Garment));
        });
    }

    [Test]
    public void MemRepo_GetOrderById_NotFound_ShouldReturnNull()
    {
        MemOrderSaver memRepo = new();
        Order result = memRepo.GetOrderById("несуществующий_id");
        Assert.That(result, Is.Null);
    }

    [Test]
    public void MemRepo_GetOrders_ShouldReturnAllOrders()
    {
        MemOrderSaver memRepo = new();
        CSVOrderRepo csvRepo = new("orders_tests.csv");
        Client client = new("Тест", memRepo, csvRepo);
        client.Request("Шляпа");
        client.Request("Куртка");
        client.Request("Сапоги");
        Assert.That(memRepo.GetOrders().Count, Is.EqualTo(3));
    }

    [Test]
    public void CsvRepo_SaveOrder_ShouldWriteToFile()
    {
        string testFile = "test_save.csv";
        File.WriteAllText(testFile, "");
        MemOrderSaver memRepo = new();
        CSVOrderRepo csvRepo = new(testFile);
        Client client = new("Тест", memRepo, csvRepo);
        client.Request("Пальто");
        Assert.That(File.Exists(testFile), Is.True);
        Assert.That(File.ReadAllText(testFile), Is.Not.Empty);
    }

    [Test]
    public void CsvRepo_GetOrderById_ShouldReturnCorrectOrder()
    {
        string testFile = "test_getbyid.csv";
        File.WriteAllText(testFile, "");
        MemOrderSaver memRepo = new();
        CSVOrderRepo csvRepo = new(testFile);
        Client client = new("Тест", memRepo, csvRepo);
        client.Request("Пальто");
        List<Order> orders = memRepo.GetOrders();
        Order saved = orders[0];
        Order result = csvRepo.GetOrderById(saved.Id);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(saved.Id));
            Assert.That(result.ClientName, Is.EqualTo(saved.ClientName));
            Assert.That(result.Garment, Is.EqualTo(saved.Garment));
        });
    }

    [Test]
    public void CsvRepo_GetOrders_ShouldReturnAllOrders()
    {
        string testFile = "test_getall.csv";
        File.WriteAllText(testFile, "");
        MemOrderSaver memRepo = new();
        CSVOrderRepo csvRepo = new(testFile);
        Client client = new("Тест", memRepo, csvRepo);
        client.Request("Шляпа");
        client.Request("Куртка");
        Assert.That(csvRepo.GetOrders().Count, Is.EqualTo(2));
    }

    [Test]
    public void Client_Request_ShouldSaveOrderToRepo()
    {
        string testFile = "test_client.csv";
        File.WriteAllText(testFile, "");
        MemOrderSaver memRepo = new();
        CSVOrderRepo csvRepo = new(testFile);
        Client client = new("Тест", memRepo, csvRepo);
        client.Request("Рубашка");
        Assert.Multiple(() =>
        {
            Assert.That(memRepo.GetOrders().Count, Is.EqualTo(1));
            Assert.That(csvRepo.GetOrders().Count, Is.EqualTo(1));
        });
    }
}
