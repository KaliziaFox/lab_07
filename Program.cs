using library07;
using System.Reflection;
using System.Xml.Linq;
public class Program
{
    static void Main(string[] args)
    {
        // working with attribute
        Lion Simba = new Lion("lion", "Africa", "Simba", "king");
        Pig Fnuf = new Pig("pig", "Russia", "Fnuf", "cool");
        TellMeSmth(Simba);
        TellMeSmth(Fnuf);

        // making xml-file
        var mylib = Assembly.LoadFrom("C:\\Users\\user\\Documents\\GitHub\\AlgorithmicLanguages\\3term\\library07\\obj\\Debug\\net6.0\\library07.dll");
        Type[] types = mylib.GetExportedTypes();
        foreach (Type type in types) { Console.WriteLine(type); }
        XDocument xdoc = new XDocument();
        uint num;
        XElement Classes = new XElement("classes");
        xdoc.Add(Classes);
        foreach (Type type in types)
        {
            XElement el = new XElement("class");
            XAttribute el_attr = new XAttribute("name", type.Name);
            el.Add(el_attr);
            XElement Properties = new XElement("properties");
            el.Add(Properties);
            XElement Methods = new XElement("methods");
            el.Add(Methods);
            num = 1;
            foreach (var method in type.GetMethods())
            {
                XElement mymethod = new XElement($"method{num}", method);
                Methods.Add(mymethod);
                ++num;
            }
            num = 1;
            foreach (var prop in type.GetProperties())
            {
                XElement myprop = new XElement($"property{num}", prop);
                Properties.Add(myprop);
                ++num;
            }
            Classes.Add(el);
        }

        xdoc.Save("classes.xml");
    }
    public static void TellMeSmth(Animal animal)
    {
        Type type = animal.GetType();
        object[] array = type.GetCustomAttributes(false);
        foreach (object o in array)
        {
            if (o is MyAtrribute atrribute) Console.WriteLine(atrribute.Comment);
        }
    }
}