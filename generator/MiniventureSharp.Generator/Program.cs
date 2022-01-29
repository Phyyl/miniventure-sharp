using MiniventureSharp.Generator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

Generator.Clean();
var files = Generator.CopySource();
Generator.Process(files);
Generator.CopyAssets();

class Generator
{
    const string generatedDirectory = @"..\..\..\..\MiniventureSharp\Generated\";
    const string resourcesDirectory = @"..\..\..\..\MiniventureSharp\Resources\";
    const string sourceDirectory = @"..\..\..\..\miniventure\src\com\mojang\ld22\";
    const string assetsDirectory = @"..\..\..\..\miniventure\src\";

    public static void Clean()
    {
        try
        {
            Directory.Delete(generatedDirectory, true);
            Directory.Delete(resourcesDirectory, true);
        }
        catch { }

        Directory.CreateDirectory(generatedDirectory);
    }

    public static IEnumerable<FileInfo> CopySource()
    {
        foreach (var file in IOHelper.GetFilesRecursive(sourceDirectory, "*.java"))
        {
            string relativePath = Path.GetRelativePath(sourceDirectory, file.FullName);
            string newPath = Path.Combine(generatedDirectory, relativePath).Replace(".java", ".cs");
            string newDir = Path.GetDirectoryName(newPath) ?? generatedDirectory;

            Directory.CreateDirectory(newDir);
            File.Copy(file.FullName, newPath);

            yield return new FileInfo(newPath);
        }
    }

    public static void Process(IEnumerable<FileInfo> files)
    {
        HashSet<string> imports = new();

        foreach (var file in files)
        {
            string input = File.ReadAllText(file.FullName);

            imports.AddRange(input.RegexFind(@"import (.*);").Select(i => i[0..i.LastIndexOf(".")]));

            string className = input.RegexFindFirst(@"class (\w+)", 1) ?? "ERROR COULD NOT FIND CLASS";
            string output = input
                .RegexReplace(@"package (.*);", "namespace $1;")
                .RegexRemove(@"import .*;")
                .RegexReplace(@"((?:public|private) \w+ \w+)(\(.*<)\? (?::|extends) (\w+)(>.*\))", "$1<T>$2T$4 where T : $3")
                .RegexReplace(@"extends (.*) implements (.*)", ": $1, $2")
                .RegexReplace(@"extends (.*)", ": $1")
                .RegexReplace(@"implements (.*)", ": $1")
                .RegexReplace(@"static final", "static readonly")
                .RegexReplace(@"static {", $"static {className}() {{")
                .RegexReplace(@"boolean", "bool")
                .RegexReplace(@"String", "string")
                .RegexReplace(@"instanceof", "is")
                .RegexReplace(@"final", "readonly")
                .RegexReplace(@"ArrayList", "List")
                .RegexReplace(@"Throwable", "Exception")
                .RegexReplace(@"\.length\(\)", ".Length")
                .RegexReplace(@"\.length", ".Length")
                .RegexReplace(@"InterruptedException", "ThreadInterruptedException")
                .RegexReplace(@"System\.out\.println", "Console.WriteLine")
                .RegexReplace(@"System\.", "SystemInfo.")
                .RegexReplace(@"(\w+)\.class", "typeof($1)")
                .RegexReplace(@"@Override.*\s*(public|private) (\w+ \w+\()", "$1 override $2")
                .RegexReplace(@"(new Comparator<\w+>\(\)\s*{\s*(?:\/\/.*)?\s*)public override int compare\((.*)\)", "$1compare = ($2) =>")
                .RegexReplace(@"(new Thread\(\)\s*{\s*(?:\/\/.*)?\s*)public override void run\((.*)\)", "$1run = ($2) =>")
                .RegexReplace(@"Class<\? : (\w+)>", "Type<$1>")
                .RegexReplace(@"{\s*super\((.*)\);", ": base($1) {")
                .RegexReplace(@"{\s*this\((.*)\);", ": this($1) {")
                .RegexReplace(@"super", "base")
                .RegexRemove(@"throws (?:\w+,? ?)+")
                .RegexReplace(@"IllegalArgumentException", "ArgumentException")
                .RegexReplace(@"RuntimeException", "Exception")
                .RegexReplace(@"throw new Exception\(ex?\)", "throw")
                .RegexReplace(@"protected", "public")
                .RegexReplace(@"(public) (\w+ \w+\()", "$1 virtual $2")
                .RegexReplace(@"readonly (\w+ \w+\()", "$1")
                .RegexReplace(@"(\S*)(@.*)", "$1 //$2")
                .RegexReplace(@"\? : (\w+)", "$1")
                .RegexReplace(@"continue (\w+)", "goto $1")
                .RegexReplace(@"(\w+:)\s+(for[^{}]*{[^{}]*
(
((?'Open'{)[^{}]*)+
((?'Close-Open'})[^{}]*)+
)*
(?(Open)(?!))[^{}]
)}", "$2$1 while (false) ; }", true, true)
                .RegexReplace(@"(\w+)\.\.\.", "params $1[]")
                .RegexReplace(@"int\[\] pixels", "Buffer pixels")
                .RegexReplace(@"int\[\] oPixels", "Buffer oPixels")
                .RegexReplace(@"pixels = new int\[w \* h\]", "pixels = new Buffer(w, h)")
                .RegexReplace(@"0x\w{8}", "unchecked((int)$0)")
                .Replace(@"new Game()", "new GameOverrides()")
                ;

            File.WriteAllText(file.FullName, output);
        }

        List<string> globalUsings = new()
        {
            "global using System;",
            "global using System.Collections.Generic;",
            "global using System.Linq;",
            "global using System.Threading;",
            "global using System.IO;"
        };

        globalUsings.AddRange(imports.Where(i => !i.StartsWith("java")).Select(i => $"global using {i};"));

        File.WriteAllText(Path.Combine(generatedDirectory, "Usings.cs"), string.Join(Environment.NewLine, globalUsings));
    }

    public static void CopyAssets()
    {
        foreach (var file in IOHelper.GetFilesRecursive(assetsDirectory, "*.wav", "*.png"))
        {
            string relativePath = Path.GetRelativePath(assetsDirectory, file.FullName);
            string newPath = Path.Combine(resourcesDirectory, relativePath);
            string newDir = Path.GetDirectoryName(newPath) ?? resourcesDirectory;

            Directory.CreateDirectory(newDir);
            File.Copy(file.FullName, newPath);

            //yield return new FileInfo(newPath);
        }
    }
}
