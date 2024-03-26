namespace Practice
{
    public class Tests
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Console.WriteLine("This is test OneTimeSetup");
            
        }

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("This is test Setup");
            int a = 5;
            Console.WriteLine(a);
        }

        [Test]
        public void Test1()
        {
            Console.WriteLine("This is test Test1");
            string ab = "Test";
            Console.WriteLine(ab);
        }

        [Test]
        public void Test2()
        {
            Console.WriteLine("This is test Test2");
            var ab = 3;
            Console.WriteLine(ab);
        }

        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("This is test TearDown");
            dynamic ab = 3;
            ab = "testt";
            Console.WriteLine(ab);
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            Console.WriteLine("This is test OneTimeTearDown");
        }


    }
}