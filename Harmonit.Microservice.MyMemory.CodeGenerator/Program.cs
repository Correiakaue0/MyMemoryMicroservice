using Harmonit.Microservice.Base.Library.CodeGenerator;
using Newtonsoft.Json;
using System.Reflection;

public class Program
{
    private static void Main(string[] args)
    {
        var main = new List<MainCodeGenerator>
        {
            new MainCodeGenerator()
            {
                BaseName = "Translate",
                BaseNamespace = "Harmonit.Microservice.MyMemory",
                GenerateRoot = true,
                GenerateTranslations = true,
                Method = new List<CodeGeneratorMethod>
                {
                    new CodeGeneratorMethod()
                    {
                        HasOutputClass = true,
                        HttpMethod = EnumHttpMethod.HttpGet,
                        ListErrorStatusCode = new List<EnumHttpStatusCode>
                        {
                            EnumHttpStatusCode.Status400BadRequest
                        },
                        ListSuccessStatusCode = new List<EnumHttpStatusCode>
                        {
                            EnumHttpStatusCode.Status200OK
                        },
                        MethodDescription = "Tradução de palavras",
                        MethodName = "Translate",
                        RefitRoute = "get?q={text}&langpair={langpair}",
                        ReturnResponseIsList = false,
                        ListParameter = new List<CodeGeneratorClassParam>
                        {
                            new CodeGeneratorClassParam()
                            {
                                HeaderParamName = "q",
                                isHeader = true,
                                ParamType = "string",
                                ParamName = "text",
                            },
                            new CodeGeneratorClassParam()
                            {
                                HeaderParamName = "langpair",
                                isHeader = true,
                                ParamType = "string",
                                ParamName = "langpair",
                            }
                        }
                    }
                }
            }
        };

        CodeGen(main, 1);
    }

    private static void CodeGen(List<MainCodeGenerator> main, int version)
    {
        var dirPath = Assembly.GetExecutingAssembly().Location;
        var st = dirPath[..dirPath.IndexOf("\\bin\\Debug\\")];

        var jsonFolder = Path.Combine(st, "JsonFolder");

        if (!Directory.Exists(jsonFolder))
            Directory.CreateDirectory(jsonFolder);

        var json = JsonConvert.SerializeObject(main, Formatting.Indented);

        var jsonFilePath = $"{jsonFolder}\\jsonCodeGeneratorHistory_v_{version}.json";

        if (File.Exists(jsonFilePath))
            throw new Exception("Este número de versão já foi utilizado");

        using StreamWriter sw = new(jsonFilePath);
        sw.WriteLine(json);
    }
}